using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SentenceReorderGame : MonoBehaviour
{
    [System.Serializable]
    public class Sentence
    {
        public string fullSentence;
        public List<string> words;
    }

    [Header("UI References")]
    public Transform wordBankPanel;      // Where scrambled words spawn
    public Transform dropZonePanel;      // Where player drags words to form the sentence
    public GameObject wordPrefab;        // A prefab containing a Text + DragAndDropWord script
    public Text feedbackText;
    public Button submitButton;

    [Header("Game Config")]
    public List<Sentence> allSentences = new List<Sentence>(); 
    public bool autoGenerateSentences = false;
    public int numberOfAutoSentences = 5;

    private List<string> currentCorrectOrder;

    void Start()
    {
        // If toggled, generate random sentences in code.
        if (autoGenerateSentences)
        {
            GenerateSentencesDynamically();
        }
        else
        {
            allSentences.Add(new Sentence
            {
                fullSentence = "Yo como en la cafetería",
                words = "Yo como en la cafetería".Split(' ').ToList()
            });

            allSentences.Add(new Sentence
            {
                fullSentence = "Tengo que ducharme antes de cenar",
                words = "Tengo que ducharme antes de cenar".Split(' ').ToList()
            });
        }

        // Hook up the Submit button and load the first puzzle
        submitButton.onClick.AddListener(CheckAnswer);
        LoadRandomSentence();
    }

    /// <summary>
    /// Loads a random sentence from allSentences, scrambles it, and spawns word buttons.
    /// </summary>
    void LoadRandomSentence()
    {
        ClearPanels();

        // Pick a random sentence
        var selected = allSentences[Random.Range(0, allSentences.Count)];
        // This is the correct order we'll compare against
        currentCorrectOrder = new List<string>(selected.words);

        // Scramble the word list
        List<string> scrambled = currentCorrectOrder.OrderBy(_ => Random.value).ToList();

        // Create a draggable UI button for each scrambled word
        foreach (string word in scrambled)
        {
            GameObject wordObj = Instantiate(wordPrefab, wordBankPanel);
            wordObj.GetComponentInChildren<Text>().text = word;

            // If your wordPrefab already has a DragAndDropWord script, you're good to go!
            // Otherwise, add it dynamically:
            // wordObj.AddComponent<DragAndDropWord>();
        }

        feedbackText.text = "";
    }

    /// <summary>
    /// Checks if the order of words in the dropZonePanel matches currentCorrectOrder exactly.
    /// </summary>
    void CheckAnswer()
    {
        // Build a list of words from left to right in the drop zone
        List<string> playerOrder = new List<string>();
        foreach (Transform child in dropZonePanel)
        {
            playerOrder.Add(child.GetComponentInChildren<Text>().text.Trim());
        }

        // Compare the player's order to the correct one
        if (playerOrder.SequenceEqual(currentCorrectOrder))
        {
            feedbackText.text = "✅ Correct!";

            // Wait 1.5 seconds, then load a new sentence
            Invoke(nameof(LoadRandomSentence), 1.5f);
        }
        else
        {
            feedbackText.text = "❌ Try again.";
        }
    }

    /// <summary>
    /// Removes existing word objects from both panels.
    /// </summary>
    void ClearPanels()
    {
        foreach (Transform child in wordBankPanel)
            Destroy(child.gameObject);
        foreach (Transform child in dropZonePanel)
            Destroy(child.gameObject);
    }

    /// <summary>
    /// Dynamically generate a set of random sentences using your vocab lists.
    /// Example approach only — tailor it for your needs.
    /// </summary>
    void GenerateSentencesDynamically()
    {
        allSentences.Clear();

        // Simple data for random combos
        List<string> subjects = new List<string> { "Yo", "Tú", "Él", "Ella", "Nosotros", "Ellos" };
        List<string> verbs = new List<string> { "camino", "hablo", "leo", "participo", "busco", "enseño", "termino" };
        List<string> places = new List<string> 
        {
            "en la biblioteca",
            "en la cafetería",
            "en el laboratorio",
            "en el apartamento",
            "en la clase"
        };
        List<string> adjectives = new List<string>
        {
            "nuevo", 
            "sucio", 
            "viejo", 
            "moderno", 
            "organizado"
        };

        // Generate some random sentences
        for (int i = 0; i < numberOfAutoSentences; i++)
        {
            // Example structure: "{Subject} {Verb} {Place}. Está {Adjective}."
            // e.g. "Él leo en la cafetería. Está nuevo."

            string subject = subjects[Random.Range(0, subjects.Count)];
            string verb = verbs[Random.Range(0, verbs.Count)];
            string place = places[Random.Range(0, places.Count)];
            string adj = adjectives[Random.Range(0, adjectives.Count)];

            string firstPart = $"{subject} {verb} {place}.";
            string secondPart = $"Está {adj}.";

            string full = firstPart + " " + secondPart;

            // Split into words for reorder
            List<string> words = full.Split(' ').ToList();

            // Add to allSentences
            allSentences.Add(new Sentence
            {
                fullSentence = full,
                words = words
            });
        }
    }
}