using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CorrectOrder : MonoBehaviour
{

    public Image grid;
    public GameObject slotPrefab;
    public GameObject wordPrefab;

    public Text feedback;
    public Button submitButton;

    private List<String> sentences;

    private int countWords;
    private String correctOrder;

    public GameObject canvas;

    private void setUpSentences()
    {
        sentences.Add("Yo como en la cafetería");
        sentences.Add("tengo que ducharme antes de cenar");
    }
    
    
    //Init variables and pass off to game logic.
    public void OnEnable()
    {
        // Set up the sentences
        sentences = new List<string>();
        setUpSentences(); // assumes it populates the list
        countWords = 0;
        
        setupBoard();
        
        submitButton.onClick.AddListener(checkAnswer);

    }

    private void checkAnswer()
    {
        //We need to check the second row of words.

        string answer = "";
        for (int i = countWords; i < countWords * 2; i++)
        {
            //Get the word in the slot
            GameObject slot = grid.transform.GetChild(i).gameObject;
            GameObject word = slot.transform.GetChild(0).gameObject;

            //Get the text of the word
            Text wordText = word.GetComponentInChildren<Text>();
            string text = wordText.text;
            answer += text + " ";
        }
        
        //Check if the answer is correct
        if (answer.Trim().Equals(correctOrder))
        {
            //Correct! Give feedback and make new board if there are
            //any sentences left, otherwise they win!
            feedback.text = "Correct!";
            if (sentences.Count > 0)
            {
                setupBoard();
            }
            else
            {
                feedback.text = "You win!";
                closeGame();
            }
        }
    }

    private void setupBoard()
    {
        
        // Choose a random sentence
        string sentence = sentences[Random.Range(0, sentences.Count)];

        correctOrder = sentence;

        sentences.Remove(sentence);

        // Split into words
        string[] words = sentence.Split(' ');
        int wordCount = words.Length;
        countWords = wordCount;

        // Shuffle words for the top row
        List<string> shuffledWords = new List<string>(words);
        for (int i = 0; i < shuffledWords.Count; i++)
        {
            int randIndex = Random.Range(i, shuffledWords.Count);
            (shuffledWords[i], shuffledWords[randIndex]) = (shuffledWords[randIndex], shuffledWords[i]);
        }

        // Get grid and layout
        GridLayoutGroup gridLayout = grid.GetComponent<GridLayoutGroup>();

        // Calculate size to fit in two rows
        float totalWidth = 890f;
        float totalHeight = 401f;

        float spacingX = 10f;
        float spacingY = 20f;

        int columns = wordCount;
        int rows = 2;


        float cellWidth = (totalWidth - spacingX * (columns - 1)) / columns;
        float cellHeight = (totalHeight - spacingY) / rows;

        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
        gridLayout.spacing = new Vector2(spacingX, spacingY);

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = columns;

        // Clear previous children just in case
        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }

        // First row (empty target slots for correct order)
        for (int i = 0; i < wordCount; i++)
        {
            // Create the slot for this word
            GameObject slot = Instantiate(slotPrefab, grid.transform);

            // Instantiate the wordPrefab inside the slot
            GameObject wordObject = Instantiate(wordPrefab, slot.transform);

            // Set the word text
            Text wordText = wordObject.GetComponentInChildren<Text>();
            if (wordText != null)
            {
                wordText.text = shuffledWords[i];
            }
            else
            {
                Debug.LogWarning("Text component not found in wordPrefab!");
            }
        }

        // Second row (shuffled draggable words)
        foreach (string word in shuffledWords)
        {
            GameObject wordSlot = Instantiate(slotPrefab, grid.transform);
        }
    }
    
    //When player wins!
    void closeGame()
    {
        // Clear all grid children
        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }

        // Reset state variables
        correctOrder = "";
        countWords = 0;

        // Clear feedback
        feedback.text = "";

        // Remove event listeners to prevent stacking them if re-enabled
        submitButton.onClick.RemoveListener(checkAnswer);

        // Optionally deactivate this minigame GameObject
        gameObject.SetActive(false);

        // Optional: Notify manager or transition elsewhere
        // e.g., GameManager.Instance.OnMinigameComplete();
        
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
