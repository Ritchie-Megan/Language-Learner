using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OrderSentences : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform grid;
    public GameObject slotPrefab;
    public GameObject wordPrefab;
    public Text promptText;
    public Text feedback;
    public Button submitButton;

    [Header("Game References")]
    //This will need to be updated when we have the
    //core gameplay mgr. public MiniGameManager mgr;

    private List<(string prompt, string sentence)> sentencePairs;
    private string correctSentence;
    private int wordCount;

    private void setUpSentences()
    {
        sentencePairs = new List<(string, string)>
        {
            ("Ask: What color is your hair?", "¿Qué color es tu cabello?"),
            ("Say: What color are your eyes?", "¿Qué color son tus ojos?"),
            ("Say: Are you a boy or a girl?", "¿Eres un niño o una niña?")
        };
    }

    public void OnEnable()
    {
        setUpSentences();
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(checkAnswer);
        setupBoard();
    }

    private void setupBoard()
    {
        if (sentencePairs.Count == 0)
        {
            feedback.text = "No more puzzles!";
            return;
        }

        // Choose and remove a random sentence pair
        int index = Random.Range(0, sentencePairs.Count);
        (string prompt, string sentence) = sentencePairs[index];
        sentencePairs.RemoveAt(index);

        correctSentence = sentence.Trim();
        string[] words = correctSentence.Split(' ');
        wordCount = words.Length;

        // Set prompt text
        if (promptText != null)
            promptText.text = prompt;

        // Build shuffled word list including extra distractors
        List<string> wordBank = new List<string>(words);

        // Example distractors (can be smarter later)
        string[] distractors = { "y", "pero", "él", "ella", "nosotros", "ellos", "a", "en", "por", "muy", "rápidamente" };
        while (wordBank.Count < wordCount + 3) // Add 3 distractors
        {
            string distractor = distractors[Random.Range(0, distractors.Length)];
            if (!wordBank.Contains(distractor)) wordBank.Add(distractor);
        }

        // Shuffle wordBank
        for (int i = 0; i < wordBank.Count; i++)
        {
            int randIndex = Random.Range(i, wordBank.Count);
            (wordBank[i], wordBank[randIndex]) = (wordBank[randIndex], wordBank[i]);
        }

        // Clear previous grid
        foreach (Transform child in grid)
        {
            Destroy(child.gameObject);
        }

        // Top row: draggable word buttons
        foreach (string word in wordBank)
        {
            GameObject wordObj = Instantiate(wordPrefab, grid);
            Text wordText = wordObj.GetComponentInChildren<Text>();
            if (wordText != null)
                wordText.text = word;
        }

        // Bottom row: empty drop slots for correct sentence
        for (int i = 0; i < wordCount; i++)
        {
            Instantiate(slotPrefab, grid);
        }

        feedback.text = "";
    }

    private void checkAnswer()
    {
        string builtSentence = "";

        // Get drop slots (last 'wordCount' children of grid)
        int start = grid.childCount - wordCount;
        for (int i = start; i < grid.childCount; i++)
        {
            Transform slot = grid.GetChild(i);
            if (slot.childCount == 0)
            {
                feedback.text = "Incomplete!";
                return;
            }

            Text wordText = slot.GetComponentInChildren<Text>();
            if (wordText != null)
                builtSentence += wordText.text + " ";
        }

        builtSentence = builtSentence.Trim();

        if (builtSentence.Equals(correctSentence))
        {
            feedback.text = "Correct!";
            Invoke(nameof(notifyManager), 1f);
        }
        else
        {
            feedback.text = "Try again!";
        }
    }

    private void notifyManager()
    {
        feedback.text = "";
        //mgr.OnPuzzleComplete(); //This will be updated when mgr. is complete.
        gameObject.SetActive(false);
    }
}