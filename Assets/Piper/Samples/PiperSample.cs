using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Piper.Samples
{
    public class PiperSample : MonoBehaviour
    {
        public PiperManager piper;
        public TMP_Text input;
        public Button submitButton;

        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            submitButton.onClick.AddListener(OnButtonPressed);
        }

        private void OnButtonPressed()
        {
            var text = input.text;
            OnInputSubmit(text);
        }

        private async void OnInputSubmit(string text)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var audio = piper.TextToSpeech(text);

            _source.Stop();
            if (_source && _source.clip)
                Destroy(_source.clip);

            _source.clip = await audio;
            _source.Play();
        }

        private void OnDestroy()
        {
            if (_source && _source.clip)
                Destroy(_source.clip);
        }
    }

}

