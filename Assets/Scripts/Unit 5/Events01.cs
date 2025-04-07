using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Events01 : MonoBehaviour
{
    //UI stuff
    public TextMeshProUGUI textBox;
    public TextMeshProUGUI speakerName;
    public GameObject nextButton;
    public GameObject mainStage;
    public GameObject questionBuilder;

    //other variables
    private bool proceed = false;
    
    //question builder sentences
    List<string> instructions = new List<string>() {
        "Ayuda a nuestro jugador a hacer una pregunta.",
        "Inténtalo otra vez."
    };
    //character introductions
    List<string> intros = new List<string>() {
        "Hola! Me llamo Andrea.",
        "Hola! Me llamo Miguel."
    };
    //intro scene
    List<string> script1 = new List<string>() {
        "Bienvenido a Carta de Amor, donde ayudamos a las personas a encontrar el amor.",
        "Vamos a conocer a nuestras jugadores.",
    };
    List<string> script2 = new List<string>() {
        "Looks like there's some competition!"
    };
    

    void Start()
    {
        StartCoroutine(IntroScene());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            buttonPress();
        }
    }

    IEnumerator IntroScene() {
        //enable stage scene
        mainStage.SetActive(true);
        //disable question builder
        questionBuilder.SetActive(false);

        //intro sentences
        foreach(string i in script1) {
            yield return DisplaySentence(i);
        }

        //contestants introduce themselves
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, intros.Count-1);
            yield return DisplaySentence(intros[rand], "Jugador " + (i + 1));
        }

        //transition into next scene
        foreach(string i in script2) {
            yield return DisplaySentence(i);
        }
        
        StartCoroutine(QuestionOne());
    }

    IEnumerator QuestionOne() {
        //enable question builder
        questionBuilder.SetActive(true);
        //disable stage scene
        mainStage.SetActive(false);

        //display text prompt
        yield return DisplaySentence(instructions[0]);
    }

     IEnumerator DisplaySentence(string sentence, string speaker = "") {
        //set speaker name
        speakerName.text = string.IsNullOrEmpty(speaker) ? "" : speaker;
        //display sentence
        textBox.text = sentence;
        //wait for button press
        yield return new WaitUntil(() => proceed);
        proceed = false;
    }

     public void buttonPress()
    {
        proceed = true;
    }
}
