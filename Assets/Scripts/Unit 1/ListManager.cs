using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ListManager : MonoBehaviour
{
    public SpriteRenderer[] avatarSprites;
    public SpriteRenderer currentSprite;
    public GameObject listOptions;
    private List<string> labelOptions = new List<string>{"Hair", "Eyes", "Nose", "Mouth", "Clothes"};
    private TMP_Text listLabel;
    int listIndex;
    

    void Awake() {
        listLabel = GetComponentInChildren<TMP_Text>();
        listIndex = 0;
        listLabel.text = labelOptions[listIndex];
        currentSprite = avatarSprites[listIndex];
    }

    void Start() {
        UpdateList();
    }

    public void CycleLeft()
    {
        if (listIndex == 0)
            listIndex = 4;
        else
            listIndex = listIndex - 1;

        listLabel.text = labelOptions[listIndex];
        currentSprite = avatarSprites[listIndex];
        UpdateList();
    }

    public void CycleRight()
    {
        if (listIndex == 4)
            listIndex = 0;
        else
            listIndex = listIndex + 1;

        listLabel.text = labelOptions[listIndex];
        currentSprite = avatarSprites[listIndex];
        UpdateList();
    }

    private void UpdateList()
    {
        string filepath = "Unit 1/Scriptable Objects/" + labelOptions[listIndex];
        CharacterFeature[] characterfeatures = Resources.LoadAll<CharacterFeature>(filepath);

        for(int i = 0; i < 9; i++)
        {
            GameObject option = listOptions.transform.GetChild(i).gameObject;
            GameObject optionImage = listOptions.transform.GetChild(i).GetChild(0).gameObject;
            Image image = optionImage.GetComponent<Image>();
            RectTransform rect = optionImage.GetComponent<RectTransform>();
            
            if (i==0)
            {
                option.SetActive(true);
                image.sprite = null;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
                //optionImage.GetComponent<OptionSelection>().SetFeatureToChange(currentSprite, avatarfeatures[i]);
            }
            else if (i > 0 && i<= characterfeatures.Length)
            {
                option.SetActive(true);
                image.sprite = characterfeatures[i-1].spriteCrop;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                //rect.localPosition = new Vector3(0, avatarfeatures[i-1].yPos, 0);
                float scale = characterfeatures[i-1].scale;
                rect.localScale = new Vector3(scale, scale, scale);
                optionImage.GetComponent<OptionSelection>().SetFeatureToChange(currentSprite, characterfeatures[i-1]);
            }
            else
            {  
                option.SetActive(false);
            }

            
        }
    }
}