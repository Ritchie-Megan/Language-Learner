using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class QuestionData
{
    public string sentence;
    public string characteristic; // like "HasGlasses"
}

public class WordManager : MonoBehaviour
{
    public Transform wordBankPanel;
    public Transform sentencePanel;
    public GameObject wordPrefab;
    public string correctSentence = "Me gusta el helado";

    private List<string> allWords;
    
    private List<QuestionData> remainingQuestions;
    
    public List<QuestionData> questions;
    public GameObject yesBox;
    public GameObject noBox;

    private QuestionData currentQuestion;

    void Start()
    {
        remainingQuestions = new List<QuestionData>(questions);
        SetupSentence();
    }

    void SetupSentence()
    {
        if (remainingQuestions.Count == 0)
        {
            Debug.Log("All questions completed!");
            // Optional: show an end screen, reset, or just return
            return;
        }

        currentQuestion = remainingQuestions[0];
        remainingQuestions.RemoveAt(0); // make sure it won't repeat

        correctSentence = currentQuestion.sentence;

        List<string> correctWords = new List<string>(correctSentence.Split(' '));
        allWords = new List<string>(correctWords);

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

    public GameManager gameManager; // assign in inspector

    public void CheckAnswer()
    {
        string result = "";
        int wordCount = 0;
        foreach (Transform child in sentencePanel)
        {
            result += child.GetComponentInChildren<TMP_Text>().text + " ";
            wordCount++;
        }
        
        if (wordCount < correctSentence.Split(' ').Length)
            return; 

        result = result.Trim();
        Debug.Log("Your answer: " + result);

        if (result == correctSentence)
        {
            Debug.Log("Correct Sentence!");

            // Now check if target character has the characteristic
            Character target = gameManager.GetTargetCharacter();
            bool hasTrait = CheckCharacterTrait(target, currentQuestion.characteristic);

            // Show YES or NO box
            yesBox.SetActive(hasTrait);
            noBox.SetActive(!hasTrait);
            
            StartCoroutine(NextQuestionCoroutine());
        }
        else
        {
            Debug.Log("Try again!");
            yesBox.SetActive(false);
            noBox.SetActive(false);
        }
    }
    
    private void ClearSentenceAndWordBank()
    {
        foreach (Transform child in sentencePanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in wordBankPanel)
        {
            Destroy(child.gameObject);
        }
    }
    
    private IEnumerator NextQuestionCoroutine()
    {
        yield return new WaitForSeconds(10f);

        ClearSentenceAndWordBank();
        SetupSentence();

        yesBox.SetActive(false);
        noBox.SetActive(false);
    }
    
    private bool CheckCharacterTrait(Character character, string traitName)
    {
        var type = typeof(Character);
        var field = type.GetField(traitName);
        if (field != null && field.FieldType == typeof(bool))
        {
            return (bool)field.GetValue(character);
        }
        else
        {
            Debug.LogWarning("Trait not found: " + traitName);
            return false;
        }
    }
}