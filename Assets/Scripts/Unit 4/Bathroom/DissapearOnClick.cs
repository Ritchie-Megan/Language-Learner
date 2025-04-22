using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using TMPro;

public class DissapearOnClick : MonoBehaviour, IPointerClickHandler
{
    [Header("Word Info")]
    public TMP_Text wordText; // drag the Text object here in the prefab
    public bool isCorrect; // set this when spawning
    public int mistakes;
    private ClickWrong gameManager;
    private List<String> fontColors = new List<String> {
        "#AB5252", // dull red
        "#EC8F25", //orange 
        "#F0F332", // yellow
        "#0A5703", //dark green
        "#45A6A1", //teal
        "#315CF3", // blue
        "#AC31F3", //purple
        "#E05EDA" //pink
    };
    
    

    public void Initialize(string spanish, string english, bool correct, ClickWrong manager)
    {
        wordText.text = $"{spanish} - {english}";
        isCorrect = correct;
        gameManager = manager;

        //randomize the font and color
        int randFont = Random.Range(0,4);
        //  color
        string hexColor = fontColors[Random.Range(0, fontColors.Count)];
        Color newHexColor;
        ColorUtility.TryParseHtmlString(hexColor, out newHexColor);
        wordText.color = newHexColor;
        //  font
        TMP_FontAsset newFont = null;
        switch(randFont) {
            case 0:
                newFont = Resources.Load<TMP_FontAsset>("Unit4/beatstreet/beatstreet");
                break;
            case 1:
                newFont = Resources.Load<TMP_FontAsset>("Unit4/conviva/Conviva");
                break;
            case 2:
                newFont = Resources.Load<TMP_FontAsset>("Unit4/janitor/Janitor");
                break;
            case 3:
                newFont = Resources.Load<TMP_FontAsset>("Unit4/orcascript/OrcaScript");
                break;
        }
        
        if (newFont != null) {
            wordText.font = newFont;
            Debug.Log("Font: " + newFont.name);
        }
        else {
            Debug.Log("No font loaded Error on case: "+ randFont);
        }
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isCorrect)
        {
            // Player clicked a correct word (bad!)
            gameManager.WordMissed();
            gameManager.getLivesText().text = "Lives: " + (gameManager.GetMaxMisses() - gameManager.GetMisses());
            Destroy(gameObject);
        }
        else
        {
            // Correctly clicked an incorrect pair — remove it
            gameManager.addCorrect();
            gameManager.getCorrectText().text = "Correct: " + gameManager.getCorrect();
            Destroy(gameObject);
        }
    }
}
