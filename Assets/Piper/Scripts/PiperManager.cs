using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unity.Sentis;
using UnityEngine;

namespace Piper
{
    public class PiperManager : MonoBehaviour
    {
        public BackendType backend = BackendType.GPUCompute;
        public ModelAsset model;

        public string voice = "en-us";
        public int sampleRate = 22050;

        private Model _runtimeModel;
        private Worker _worker;

        private void Awake()
        {
            var espeakPath = Path.Combine(Application.streamingAssetsPath, "espeak-ng-data");
            PiperWrapper.InitPiper(espeakPath);

            _runtimeModel = ModelLoader.Load(model);
            _worker = new Worker(_runtimeModel, backend);
        }

        public async Task<AudioClip> TextToSpeech(string text)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            Debug.Log("Piper Phonemize processing text...");
            var phonemes = PiperWrapper.ProcessText(text, voice);
            Debug.Log($"Piper Phonemize processed text: {sw.ElapsedMilliseconds} ms");

            Debug.Log("Starting Piper inference...");
            sw.Restart();

            var inputLengthsShape = new TensorShape(1);
            var scalesShape = new TensorShape(3);
            using var scalesTensor = new Tensor<float>(scalesShape, new float[] { 0.667f, 1f, 0.8f });

            var audioBuffer = new List<float>();
            for (int i = 0; i < phonemes.Sentences.Length; i++) 
            {
                var sentence = phonemes.Sentences[i];

                var inputPhonemes = sentence.PhonemesIds;
                var inputShape = new TensorShape(1, inputPhonemes.Length);
                using var inputTensor = new Tensor<int>(inputShape, inputPhonemes);
                using var inputLengthsTensor = new Tensor<int>(inputLengthsShape, new int[] { inputPhonemes.Length });

                
                _worker.SetInput("input", inputTensor);
                _worker.SetInput("input_lengths", inputLengthsTensor);
                _worker.SetInput("scales", scalesTensor);

                _worker.Schedule();

                var outputTensor = _worker.PeekOutput() as Tensor<float>;
                using var output = await outputTensor.ReadbackAndCloneAsync();

                audioBuffer.AddRange(output.DownloadToArray());
            }

            Debug.Log($"Finished piper inference: {sw.ElapsedMilliseconds} ms");
            Debug.Log("Saving to audio clip...");
            sw.Restart();

            var audioClip = AudioClip.Create("piper_tts", audioBuffer.Count, 1, sampleRate, false);
            audioClip.SetData(audioBuffer.ToArray(), 0);

            Debug.Log($"Audio clip saved: {sw.ElapsedMilliseconds} ms");

            return audioClip;
        }

        private void OnDestroy()
        {
            PiperWrapper.FreePiper();
            _worker.Dispose();
        }
    }
}
