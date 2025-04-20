using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WordManager : MonoBehaviour
{
    public Transform wordBankPanel;
    public Transform sentencePanel;
    public GameObject wordPrefab;
    public string correctSentence = "Me gusta el helado";

    private List<string> allWords;

    void Start()
    {
        SetupSentence();
    }

    void SetupSentence()
    {
        List<string> correctWords = new List<string>(correctSentence.Split(' '));
        allWords = new List<string>(correctWords);

        // Add distractors
        allWords.AddRange(new List<string> { "rojo", "ella", "mal", "rápido" });
        Shuffle(allWords);

        foreach (string word in allWords)
        {
            GameObject newWord = Instantiate(wordPrefab, wordBankPanel);
            newWord.GetComponentInChildren<TMP_Text>().text = word;
        }
    }

    void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int rand = Random.Range(i, list.Count);
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    public void CheckAnswer()
    {
        string result = "";
        foreach (Transform child in sentencePanel)
        {
            result += child.GetComponentInChildren<TMP_Text>().text + " ";
        }

        result = result.Trim();
        Debug.Log("Your answer: " + result);

        if (result == correctSentence)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Try again!");
        }
    }
}