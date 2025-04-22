using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Unit1Manager : MonoBehaviour
{
    public ProfileGenerator profGenerator;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject submitMessage;
    public List<GameObject> genderedFeatures;

    private string status;
    private string eyeColor;
    private string hairType;
    private string hairColor;
    private string gender;
    private int count;

    void Start()
    {
        status = "content*";
        eyeColor = "negros";
        hairType = "corto, liso";
        hairColor = "negro";
        gender = "male";
        count = 0;
    }

    public void checkSelections()
    {
        bool wonGame = true;
        string message = "";
        if (profGenerator.status == "sueño" && status == "cansad*")
        {
            status = "sueño";
        }

        if (GenderWords(status) != profGenerator.status)
        {
            wonGame = false;
            message += "Estado correcto: " + profGenerator.status + "\n";
            message += "Estado enviado: " + GenderWords(status) + "\n\n";
        }
        if (eyeColor != profGenerator.eyeColor)
        {
            wonGame = false;
            message += "Color de ojos correcto: " + profGenerator.eyeColor + "\n";
            message += "Color de ojos enviado: " + eyeColor + "\n\n";
        }
        if (hairType != profGenerator.hairType)
        {
            wonGame = false;
            message += "Tipo de cabello correcto: " + profGenerator.hairType + "\n";
            message += "Tipo de cabello enviado: " + hairType + "\n\n";
        }
        if (hairColor != profGenerator.hairColor)
        {
            wonGame = false;
            message += "Color de cabello correcto: " + profGenerator.hairColor + "\n";
            message += "Color de cabello enviado: " + hairColor + "\n\n";
        }
        if (gender != profGenerator.gender)
        {
            wonGame = false;
            message += "Género correcto: " + profGenerator.hairColor + "\n";
            message += "Género de cabello enviado: " + hairColor + "\n\n";
        }

        if (wonGame)
        {
            submitMessage.SetActive(false);
            winScreen.SetActive(true);
        }
        else
        {
            submitMessage.SetActive(false);
            TMP_Text loseMessage = loseScreen.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            loseScreen.SetActive(true);
            loseMessage.text = message;
            LayoutRebuilder.ForceRebuildLayoutImmediate(loseScreen.transform.GetChild(0).gameObject.GetComponent<RectTransform>());
        }
    }
    
    public void setStatus(string _status)
    {
        status = _status;
    }

    public void setEyeColor(string _eyeColor)
    {
        eyeColor = _eyeColor;
    }

    public void setHairType(string _hairType)
    {
        hairType = _hairType;
    }

    public void setHairColor(string _hairColor)
    {
        hairColor = _hairColor;
    }

    public void setGender(string _gender)
    {
        if (_gender != gender)
        {
            toggleGenderedFeatures();
            gender = _gender;
        }
    }

    public void toggleGenderedFeatures()
    {
        foreach (GameObject genderedObject in genderedFeatures)
        {
            genderedObject.SetActive(!genderedObject.activeSelf);
        }
    }

    string GenderWords(string phrase)
    {
        bool male = profGenerator.gender == "male";
        string[] words = phrase.Split(' ');
        string genderedPhrase = "";

        foreach (string word in words)
        {
            if (word == "trabajador*")
                genderedPhrase += male ? "trabajador" : word.Replace('*', 'a');
            else
                genderedPhrase += male ? word.Replace('*', 'o') : word.Replace('*', 'a');
        }

        return genderedPhrase;
    }
}
