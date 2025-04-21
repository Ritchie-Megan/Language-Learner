using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{

    [Header("Task Panel")] 
    public GameObject TaskItemPrefab;
    public Transform TaskPanel;

    [Header("Input Panel")] 
    public InputField AnswerInput;
    public Button SubmitButton;

    [Header("Clues")] public Text ClueText;

    [Header("Feedback")] public Text FeedbackText;
    
    public struct Task
    {
        public string description;
        public string correctAnswer;

        public Task(string desc, string correctAnswer)
        {
            description = desc;
            this.correctAnswer = correctAnswer;
        }
    }

    private List<Task> tasks;
    private int currTask;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeTasks();
        PopulateTasksUI();
        UpdateClueText();
        
        SubmitButton.onClick.AddListener(checkAnswer);
    }


    void InitializeTasks()
    {
        tasks = new List<Task>()
        {
            new Task("Encuentras la biblioteca y escribe 'la biblioteca", "biblioteca"),
            new Task("Corre y traeme pan del mercado", "compro pan")
        };
    }

    void PopulateTasksUI()
    {
        foreach (var task in tasks)
        {
            GameObject item = Instantiate(TaskItemPrefab, TaskPanel);
            item.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = task.description;

        }
        
        Canvas.ForceUpdateCanvases();
    }

    void UpdateClueText()
    {
        ClueText.text = tasks[currTask].description;
    }

    void checkAnswer()
    {

        string input = AnswerInput.text.Trim().ToLower();
        string correctAnswer = tasks[currTask].correctAnswer.ToLower();

        if (input.Equals(correctAnswer))
        {
            FeedbackText.text = "Correcto! Muy Bien";
            FeedbackText.color = Color.green;
            currTask++;

            if (currTask < tasks.Count)
            {
                UpdateClueText();
            }
            else
            {
                ClueText.text = "Felicitaciones! Has completado todos de mis tareas!";
                SubmitButton.interactable = false;
            }
        }
        else
        {
            FeedbackText.text = "Incorrecto! Intentalo de nuevo";
            FeedbackText.color = Color.red;
        }

        AnswerInput.text = "";
        AnswerInput.ActivateInputField();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
