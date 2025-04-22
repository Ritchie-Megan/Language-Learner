using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unit_4
{
    public class VerbQuizManager : MonoBehaviour
    {
        public Text promptText;
        public InputField inputField;
        public Text feedbackText;
        public Button submitButton;

        public MiniGameManager mgr;

        private List<string> subjects = new List<string> { "yo", "tú", "él", "ella", "nosotros", "ellos" };
        private List<(string verb, Dictionary<string, string> conjugations)> verbs;
        private int correctCount = 0;
        private int requiredCorrect = 5;

        private string currentSubject;
        private string currentVerb;
        private string correctAnswer;

        void Start()
        {
            SetupVerbs();
            ShowNextQuestion();
            submitButton.onClick.AddListener(CheckAnswer);
        }

        void SetupVerbs()
        {
            verbs = new List<(string, Dictionary<string, string>)>();

            void AddVerb(string verb, string yo, string tu, string el, string ella, string nosotros, string ellos)
            {
                verbs.Add((verb, new Dictionary<string, string> {
                    { "yo", yo }, { "tú", tu }, { "él", el }, { "ella", ella }, { "nosotros", nosotros }, { "ellos", ellos }
                }));
            }

            AddVerb("aprender", "aprendo", "aprendes", "aprende", "aprende", "aprendemos", "aprenden");
            AddVerb("buscar", "busco", "buscas", "busca", "busca", "buscamos", "buscan");
            AddVerb("calificar", "califico", "calificas", "califica", "califica", "calificamos", "califican");
            AddVerb("caminar", "camino", "caminas", "camina", "camina", "caminamos", "caminan");
            AddVerb("compartir", "comparto", "compartes", "comparte", "comparte", "compartimos", "comparten");
            AddVerb("comprar", "compro", "compras", "compra", "compra", "compramos", "compran");
            AddVerb("enseñar", "enseño", "enseñas", "enseña", "enseña", "enseñamos", "enseñan");
            AddVerb("escribir", "escribo", "escribes", "escribe", "escribe", "escribimos", "escriben");
            AddVerb("hablar", "hablo", "hablas", "habla", "habla", "hablamos", "hablan");
            AddVerb("leer", "leo", "lees", "lee", "lee", "leemos", "leen");
            AddVerb("participar", "participo", "participas", "participa", "participa", "participamos", "participan");
            AddVerb("pasar", "paso", "pasas", "pasa", "pasa", "pasamos", "pasan");
            AddVerb("recibir", "recibo", "recibes", "recibe", "recibe", "recibimos", "reciben");
            AddVerb("sacar", "saco", "sacas", "saca", "saca", "sacamos", "sacan");
            AddVerb("terminar", "termino", "terminas", "termina", "termina", "terminamos", "terminan");
            AddVerb("tomar", "tomo", "tomas", "toma", "toma", "tomamos", "toman");
        }

        void ShowNextQuestion()
        {
            inputField.text = "";
            feedbackText.text = "";

            var randomVerb = verbs[Random.Range(0, verbs.Count)];
            currentSubject = subjects[Random.Range(0, subjects.Count)];
            currentVerb = randomVerb.verb;
            correctAnswer = randomVerb.conjugations[currentSubject];

            promptText.text = $"Conjugate **{currentVerb}** for **{currentSubject}**:";
        }

        void CheckAnswer()
        {
            string playerAnswer = inputField.text.Trim().ToLower();
            if (playerAnswer == correctAnswer)
            {
                feedbackText.text = "Correct!";
                correctCount++;

                if (correctCount >= requiredCorrect)
                {
                    feedbackText.text = "Great job! You've finished the mini-game.";
                    submitButton.interactable = false;
                }
                else
                {
                    Invoke(nameof(ShowNextQuestion), 1f); // Short delay
                }
            }
            else
            {
                feedbackText.text = "Try again.";
            }
            
            if (correctCount >= requiredCorrect)
            {
                feedbackText.text = "Time to go!";
                submitButton.interactable = false;

                // Wait a second, then close the mini-game
                Invoke(nameof(EndMiniGame), 1.5f);
            }
        }
        
        void EndMiniGame()
        {
            feedbackText.text = "Great job! You've finished the mini-game.";
            mgr.CloseGame();
        }
    }
}
