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
    private List<string> labelOptions = new List<string>{"Hair", "Eyes", "Nose", "Clothes"};
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
            listIndex = 3;
        else
            listIndex = listIndex - 1;

        listLabel.text = labelOptions[listIndex];
        currentSprite = avatarSprites[listIndex];
        UpdateList();
    }

    public void CycleRight()
    {
        if (listIndex == 3)
            listIndex = 0;
        else
            listIndex = listIndex + 1;

        listLabel.text = labelOptions[listIndex];
        currentSprite = avatarSprites[listIndex];
        UpdateList();
    }

    private void UpdateList()
    {
        string filepath = "Unit1/" + labelOptions[listIndex];
        Sprite[] sprites = Resources.LoadAll<Sprite>(filepath);

        for(int i = 0; i < 9; i++)
        {
            GameObject option = listOptions.transform.GetChild(i).gameObject;
            GameObject optionImage = listOptions.transform.GetChild(i).GetChild(0).gameObject;
            Image image = optionImage.GetComponent<Image>();
            
            if (i==0)
            {
                option.SetActive(true);
                image.sprite = null;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            else if (i > 0 && i<= sprites.Length)
            {
                option.SetActive(true);
                image.sprite = sprites[i-1];
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            }
            else
            {  
                option.SetActive(false);
            }

            optionImage.GetComponent<OptionSelection>().SetFeatureToChange(currentSprite);
        }
    }
}