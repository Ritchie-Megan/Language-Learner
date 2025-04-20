using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Events01 : MonoBehaviour
{
    //text box + attached things
    public TextMeshProUGUI textBox;
    public TextMeshProUGUI speakerName;
    public GameObject nextButton;
    
    //stage screen ui
    public GameObject mainStage;
    public GameObject[] contestants;
    public GameObject[] heartButtons;
    public GameObject lighting;
    public Image[] faces; //face images (1-3 are for contestants, 4 is for player)
    public Sprite[] face; //different images for faces (mouth closed, mouth open, frown)

    //question builder screen
    public GameObject questionBuilder;
    public TextMeshProUGUI questionTextBox;
    public TextMeshProUGUI[] optionButtons;
    public TextMeshProUGUI hint;
    public GameObject hintHider;

    //final stats screen
    public GameObject finalStats;
    public TextMeshProUGUI mistakesText;
    public TextMeshProUGUI elimText;
    public TextMeshProUGUI hintText;

    //pause menu
    public GameObject pauseMenu;
    bool paused = false;

    //other variables
    private bool proceed = false; //true after clicking next button in general
    private bool altProceed = false; //true after clicking button in question builder
    private int choice = 0; //which option the player clicks in question builder
    private int firstElim = -1; //tracks which player is eliminated in the first round
    private int correctElims = 0; //tracks correct eliminations on stage
    private int mistakes = 0; //tracks mistakes in question builder
    List<int> questionNums; //stores randomly generated question numbers
    private int hintsUsed = 0;

    //question set
    List<Question> questionSet = QuestionBank.Questions;
    //character introductions
    List<string> intros = new List<string>() {
        "¡Hola! Me llamo Ana y me encanta leer. Siempre tengo un libro conmigo.",
        "¡Buenas! Soy Carlos y me gusta mucho el cine. Puedo ver películas todo el día.",
        "¡Hola! Me llamo Sofía y amo a los animales. Tengo dos perros y un gato en casa.",
        "¡Hola a todos! Soy Miguel y me gusta tomar fotos. Siempre llevo mi cámara a todas partes.",
        "¡Buenas! Me llamo Elena y me encanta bailar. La música siempre me pone de buen humor.",
        "¡Hola! Soy Andrés y me gusta caminar por la naturaleza. Me relaja mucho ver montañas y ríos.",
        "¡Buenas! Me llamo Valeria y disfruto probar comidas nuevas. Me encanta cocinar para mis amigos.",
        "¡Hola a todos! Soy Ricardo y me gustan los videojuegos. Juego con mis amigos casi todos los días.",
        "¡Hola! Soy Lucía y me gusta pintar. Los colores me ayudan a expresar mis sentimientos.",
        "¡Buenas! Me llamo Fernando y me encanta hacer ejercicio. Correr y nadar son mis deportes favoritos.",
        "¡Hola! Soy Gabriela y me gusta la moda. Siempre busco ropa con colores y estilos interesantes.",
        "¡Buenas! Me llamo Tomás y disfruto cocinar. Mi especialidad es la pasta con salsa casera.",
        "¡Hola a todos! Soy Mariana y me encanta viajar. Quiero conocer muchos países en el futuro.",
        "¡Hola! Soy Javier y me gustan los libros de ciencia ficción. Me encanta imaginar otros mundos.",
        "¡Buenas! Me llamo Patricia y me gusta actuar. Me divierte mucho interpretar diferentes personajes.",
        "¡Hola! Soy Alejandro y la música es mi pasión. Toco la guitarra y canto en mi tiempo libre.",
        "¡Buenas! Me llamo Camila y practico yoga. Me ayuda a relajarme y sentirme bien.",
        "¡Hola a todos! Soy Sebastián y me gustan los acertijos. Siempre busco juegos de lógica para resolver.",
        "¡Hola! Me llamo Natalia y me encanta el espacio. Paso las noches mirando las estrellas.",
        "¡Buenas! Soy Hugo y me gusta la aventura. Siempre busco algo emocionante para hacer."
    };
    //question builder instructional sentences
    List<string> instructions = new List<string>() {
        "Ayuda a nuestro jugador a hacer una pregunta.",
        "Inténtalo otra vez.",
        "Correcto. ¡Buen trabajo!"
    };
    //intro scene script
    List<string> introStart = new List<string>() {
        "Bienvenido a Cartas de Amor, donde ayudamos a las personas a encontrar el amor.",
        "Vamos a conocer a nuestros concursantes.",
    };
    List<string> introEnd = new List<string>() {
        "¡Qué grupo tan interesante!",
        "Ahora es el momento de que nuestro jugador escriba su pregunta.",
        "¡En Cartas de Amor, cada palabra es importante!"
    };
    //first elimination script
    List<string> elim1Start = new List<string>() {
        "Bienvenido de nuevo, jugador.",
        "Es hora de escuchar a nuestros concursantes. ¡Vamos, haz tu pregunta!"
    };
    List<string> elim1Mid = new List<string>() {
        "Nuestro jugador debe eliminar a uno de los concursantes.",
        "¡Adelante!"
    };
    List<string> elim1End = new List<string>() {
        "¡Así es el juego! Unas cartas se quedan en la mesa, y otras se descartan.",
        "Felicitaciones a los otras jugadores.",
        "¡Es hora de otra pregunta!"
    };
    //second elimination script
    List<string> elim2Start = new List<string>() {
        "¡Tiempo terminado!",
        "Nuestro jugador hará su pregunta a los concursantes.",
    };
    List<string> elim2Mid = new List<string>() {
        "¡Llegamos a la última eliminación!",
        "Ahora es momento de tomar la decisión final."
    };
    List<string> elim2End = new List<string>() {
        "¡Y tenemos un ganador!",
        "Gracias a todos por acompañarnos en Cartas de Amor.",
        "Nos vemos la próxima vez, donde una nueva historia de amor estará por escribirse.",
        "¡Hasta pronto!"
    };


    void Start()
    {
        //generate questions from set
        int one = UnityEngine.Random.Range(0, questionSet.Count);
        int two = UnityEngine.Random.Range(0, questionSet.Count);
        while(one==two) {
            two = UnityEngine.Random.Range(0, questionSet.Count);
        }
        questionNums = new List<int> {one, two};

        //begin intro scene
        StartCoroutine(IntroScene());
    }

    void Update()
    {
        //space continues dialogue
        if(Input.GetKeyDown(KeyCode.Space)) {
            buttonPress();
        }
        //escape brings up pause menu
        if(Input.GetKeyDown(KeyCode.Escape)) {
            pause();
        }
    }

    //introduction scene
    IEnumerator IntroScene() {
        //enable stage scene
        mainStage.SetActive(true);
        //disable final stats screen
        finalStats.SetActive(false);
        //disable question builder
        questionBuilder.SetActive(false);
        //disable pause menu
        pauseMenu.SetActive(false);
        //disable heart buttons
        foreach(GameObject i in heartButtons) {
            i.SetActive(false);
        }
        //disable dramatic lighting
        lighting.SetActive(false);

        //host intro sentences
        foreach(string i in introStart) {
            yield return DisplaySentence(i);
        }

        //3 random contestant introductions
        List<int> introNums = threeInt(intros.Count);
        for (int i = 0; i < 3; i++)
        {
            int rand = introNums[i];
            faces[i].sprite = face[1];
            yield return DisplaySentence(intros[rand], "Concursante " + (i + 1));
            faces[i].sprite = face[0];
        }

        //host transitions to next scene
        foreach(string i in introEnd) {
            yield return DisplaySentence(i);
        }

        //start question builder first round
        StartCoroutine(QuestionBuilder(0));
    }

    //first elimination round
    IEnumerator EliminationOne() {
        //disable question builder
        questionBuilder.SetActive(false);

        //question intro
        foreach(string i in elim1Start) {
            yield return DisplaySentence(i);
        }

        //randomize which player has a bad response
        int badResponse = UnityEngine.Random.Range(0, 2);

        //retrieve question info for this round
        Question q1 = questionSet[questionNums[0]];

        //player asks question (mouth opens)
        faces[3].sprite = face[1];
        yield return DisplaySentence(q1.getFull(), "Jugador");
        faces[3].sprite = face[0];
        
        //contestants respond
        bool firstGood = true; //true if no good responses have been given
        for(int i = 0; i < 3; i++) {
            faces[i].sprite = face[1];
            if(i == badResponse) {
                yield return DisplaySentence(q1.getBad(), "Concursante " + (i + 1));
            }
            else if(firstGood) {
                yield return DisplaySentence(q1.getGood1(), "Concursante " + (i + 1));
                firstGood = false;
            }
            else {
                yield return DisplaySentence(q1.getGood2(), "Concursante " + (i + 1));
            }
            faces[i].sprite = face[0];
        }

        //elimination intro
        foreach(string i in elim1Mid) {
            yield return DisplaySentence(i);
        }

        //enable dramatic lighting
        lighting.SetActive(true);
        //enable heart buttons
        foreach(GameObject i in heartButtons) {
            i.SetActive(true);
        }
        //disable regular button
        nextButton.SetActive(false);
        //prompt player to eliminate contestant
        speakerName.text = "Presentador";
        textBox.text = "Presiona uno de los corazones para eliminar a un concursante.";
        //wait for heart button press
        yield return new WaitUntil(() => altProceed);
        altProceed = false;

        //track the first eliminated contestant
        firstElim = choice;
        //if the contestant eliminated gave the correct response, increment stats
        if(badResponse == choice) {
            correctElims++;
        }

        //disable dramatic lighting
        lighting.SetActive(false);
        //eliminated contestant frowny face
        faces[choice].sprite = face[2];
        //disable heart buttons
        foreach(GameObject i in heartButtons) {
            i.SetActive(false);
        }
        //enable regular button
        nextButton.SetActive(true);

        //announce which contestant was eliminated
        yield return DisplaySentence("¡Concursante "+(choice+1)+" queda eliminado!");
        //transition to next scene
        foreach(string i in elim1End) {
            yield return DisplaySentence(i);
        }

        //question builder second round
        StartCoroutine(QuestionBuilder(1));
    }

    //second elimination round
    IEnumerator EliminationTwo() {
        //disable question builder
        questionBuilder.SetActive(false);
        //disable first eliminated player
        contestants[firstElim].SetActive(false);

        //question intro
        foreach(string i in elim2Start) {
            yield return DisplaySentence(i);
        }

        //randomize whether the bad response will appear first or second
        int badResponse = UnityEngine.Random.Range(0, 1);

        //retrieve question info for this round
        Question q1 = questionSet[questionNums[1]];

        //player asks question (mouth opens)
        faces[3].sprite = face[1];
        yield return DisplaySentence(q1.getFull(), "Jugador");
        faces[3].sprite = face[0];
        
        int responseNum = 0;
        int badContestant = -1;
        //contestants respond
        for(int i = 0; i < 3; i++) {
            //if the contestant has not been eliminated already
            if(i != firstElim) {
                //mouth open
                faces[i].sprite = face[1];
                //if the current response number (will be 0 for the first reponse given, and 1 for second)
                if(responseNum == badResponse) {
                    yield return DisplaySentence(q1.getBad(), "Concursante " + (i + 1));
                    responseNum++;
                    //track which contestant gave the bad response
                    badContestant = i;
                }
                else {
                    yield return DisplaySentence(q1.getGood1(), "Concursante " + (i + 1));
                    responseNum++;
                }
                faces[i].sprite = face[0];
            }
        }

        //elimination intro
        foreach(string i in elim2Mid) {
            yield return DisplaySentence(i);
        }

        //enable dramatic lighting
        lighting.SetActive(true);
        //enable heart buttons
        for(int i = 0; i < 3; i++) {
            if(i != firstElim) {
                heartButtons[i].SetActive(true);
            }
        }
        //disable regular button
        nextButton.SetActive(false);

        //prompt player to eliminate contestant
        speakerName.text = "Presentador";
        textBox.text = "Presiona uno de los corazones para eliminar a un concursante.";
        //wait for heart button press
        yield return new WaitUntil(() => altProceed);
        altProceed = false;

        //if the contestant eliminated gave the worst response, increment stats
        if(badContestant == choice) {
            correctElims++;
        }

        //disable dramatic lighting
        lighting.SetActive(false);
        //eliminated contestant frowny face
        faces[choice].sprite = face[2];
        //disable heart buttons
        foreach(GameObject i in heartButtons) {
            i.SetActive(false);
        }
        //enable regular button
        nextButton.SetActive(true);

        //announce which contestant was eliminated
        yield return DisplaySentence("¡Concursante "+(choice+1)+" queda eliminado!");
        //transition to next scene
        foreach(string i in elim2End) {
            yield return DisplaySentence(i);
        }

        //bring up final stats screen
        finalStats.SetActive(true);
        mistakesText.text = "Mistakes in question builder: " + mistakes;
        elimText.text = "Correct eliminations: " + correctElims + "/2";
        hintText.text = "Hints used: " + hintsUsed + "/2";
    }

    //question builder (input round 0 or 1)
    IEnumerator QuestionBuilder(int round) {
        //enable question builder
        questionBuilder.SetActive(true);
        //enable hint hider
        hintHider.SetActive(true);
        //disable next button
        nextButton.SetActive(false);

        //set speaker name
        speakerName.text = "Presentador";
        //display sentence
        textBox.text = instructions[0];
    
        //retrieve question 1 info
        Question q1 = questionSet[questionNums[round]];
        //display question text
        questionTextBox.text = q1.getPartial();
        //set hint to translation
        hint.text = q1.getTranslation();

        //randomly choose a correct number
        int correctNum = UnityEngine.Random.Range(0, 3);
        //set the corresponding button to the correct answer's text
        optionButtons[correctNum].text = q1.getAnswer();
    
        //set the other buttons to the incorrect answers
        if(correctNum == 0) {
            optionButtons[1].text = q1.getI1();
            optionButtons[2].text = q1.getI2();
            optionButtons[3].text = q1.getI3();
        }
        else if(correctNum == 1) {
            optionButtons[0].text = q1.getI1();
            optionButtons[2].text = q1.getI2();
            optionButtons[3].text = q1.getI3();
        }
        else if(correctNum == 2) {
            optionButtons[1].text = q1.getI1();
            optionButtons[0].text = q1.getI2();
            optionButtons[3].text = q1.getI3();
        }
        else {
            optionButtons[1].text = q1.getI1();
            optionButtons[2].text = q1.getI2();
            optionButtons[0].text = q1.getI3();
        }

        //becomes true when right answer chosen
        bool correct = false;
        while(!correct) {
            //wait for option button press
            yield return new WaitUntil(() => altProceed);
            altProceed = false;

            //if they press the right choice
            if(choice == correctNum) {
                //fill in question blank
                questionTextBox.text = q1.getFull();
                //show word definition
                hintHider.SetActive(false);
                //display congratulations
                textBox.text = instructions[2];

                //enable next button
                nextButton.SetActive(true);
                
                //wait for next button press
                yield return new WaitUntil(() => proceed);
                proceed = false;

                //go to next scene
                if(round == 0) {
                    StartCoroutine(EliminationOne());
                }
                else {
                    StartCoroutine(EliminationTwo());
                }
            }
            //if they're wrong
            else {
                mistakes++;
                //display incorrect choice text
                textBox.text = instructions[1];
            }
        }
    }

    //display sentence and wait for button press
     IEnumerator DisplaySentence(string sentence, string speaker = "") {
        //set speaker name
        speakerName.text = string.IsNullOrEmpty(speaker) ? "Presentador" : speaker;
        //display sentence
        textBox.text = sentence;
        //wait for button press
        yield return new WaitUntil(() => proceed);
        proceed = false;
    }

    //select three random integers within the range (no repeats)
    List<int> threeInt(int range) {
        int one = UnityEngine.Random.Range(0, range);
        int two = UnityEngine.Random.Range(0, range);
        int three = UnityEngine.Random.Range(0, range);

        while(one == two) {
            two = UnityEngine.Random.Range(0, range);
        }
        while(one==three || two==three) {
            three = UnityEngine.Random.Range(0, range);
        }

        return new List<int> {one, two, three};
    }

    //advance text (space key or next button)
     public void buttonPress() {
        proceed = true;
    }
    public void accessHint() {
        hintHider.SetActive(false);
        hintsUsed++;
    }
    //bring up pause menu or exit pause menu
    public void pause() {
        if(!paused) {
            pauseMenu.SetActive(true);
            paused = true;
        }
        else {
            pauseMenu.SetActive(false);
            paused = false;
        }
    }
    //track which button clicked (question builder options or heart buttons)
    public void option1() {
        choice = 0;
        altProceed = true;
    }
    public void option2() {
        choice = 1;
        altProceed = true;
    }
    public void option3() {
        choice = 2;
        altProceed = true;
    }
    public void option4() {
        choice = 3;
        altProceed = true;
    }
}
