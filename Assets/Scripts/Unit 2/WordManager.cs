using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;


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

    private bool continueCutscene = false;
    public GameObject continueButton;

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
            remainingQuestions = new List<QuestionData>(questions);
            //return;
        }

        //randomize which question comes next
        System.Random rand = new System.Random();
        int index = rand.Next(0, remainingQuestions.Count);
        currentQuestion = remainingQuestions[index];
        remainingQuestions.RemoveAt(index); // make sure it won't repeat

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

            // Color YES or NO box accordingly
            yesBox.GetComponent<Image>().color = getResponseColor(hasTrait, true);
            noBox.GetComponent<Image>().color = getResponseColor(!hasTrait, false);
            
            yesBox.GetComponentInChildren<TMP_Text>().color = getResponseColor(hasTrait, true);
            noBox.GetComponentInChildren<TMP_Text>().color = getResponseColor(!hasTrait, false);
            
            StartCoroutine(NextQuestionCoroutine());
        }
        else
        {
            // Debug.Log("Try again!");
            // yesBox.SetActive(false);
            // noBox.SetActive(false);
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
        continueButton.SetActive(true);
        yield return new WaitUntil(CheckIfContinue);
        continueCutscene = false;
        continueButton.SetActive(false);

        ClearSentenceAndWordBank();
        SetupSentence();

        yesBox.GetComponent<Image>().color = getResponseColor(false, false);
        noBox.GetComponent<Image>().color = getResponseColor(false, false);

        yesBox.GetComponentInChildren<TMP_Text>().color = getResponseColor(false, false);
        noBox.GetComponentInChildren<TMP_Text>().color = getResponseColor(false, false);

    }

    private bool CheckIfContinue() {
        return continueCutscene;
    }

    public void ContinueButton() {
        continueCutscene = true;
    }

    private Color getResponseColor(bool hasTrait, bool yes)
    {
        if (hasTrait)
        {
            if (yes)
                return new Color32(100, 200, 100, 255);
            else
                return new Color32(200, 100, 100, 255);
        }
        else
        {
            return new Color32(180, 180, 180, 255);
        }
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