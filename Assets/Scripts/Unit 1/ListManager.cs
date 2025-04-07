using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ListManager : MonoBehaviour
{    
    public SpriteRenderer characterSprite;
    public GameObject listOptions;
    enum SpriteType {Body, Clothes, Hair, Emotions};
    [SerializeField] SpriteType spriteType;
    
    void Start() {
        UpdateList();
    }

    public void UpdateList()
    {
        string filepath = "Unit 1/Scriptable Objects/" + spriteType.ToString();
        CharacterFeature[] characterfeatures = Resources.LoadAll<CharacterFeature>(filepath);

        for(int i = 0; i < 9; i++)
        {
            GameObject option = listOptions.transform.GetChild(i).gameObject;
            GameObject optionImage = listOptions.transform.GetChild(i).GetChild(0).gameObject;
            Image image = optionImage.GetComponent<Image>();
            RectTransform rect = optionImage.GetComponent<RectTransform>();
            
            if (i < characterfeatures.Length)
            {
                option.SetActive(true);
                image.sprite = characterfeatures[i].spriteCrop;
                float scale = characterfeatures[i].scale;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                rect.localScale = new Vector3(scale, scale, scale);
                optionImage.GetComponent<OptionSelection>().SetFeatureToChange(characterSprite, characterfeatures[i]);
            }
            else
            {  
                option.SetActive(false);
            }

            
        }
    }
}