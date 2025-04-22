using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class OptionSelection : MonoBehaviour
{
    public GameObject targetFeature;
    public CharacterFeature characterFeature;
    public string featureDescription;
    public bool useCrop = true;
    public UnityEvent<string> onOptionSelected;
    

    void Start()
    {
        if (characterFeature != null && useCrop)
            transform.GetChild(0).GetComponent<Image>().sprite = characterFeature.croppedSprite;
    }

    public void SetTargetSprite()
    {
        if (characterFeature != null)
        {
            Image targetImage = targetFeature.GetComponent<Image>();
            targetImage.sprite = characterFeature.fullSprite;

            if (targetFeature.transform.childCount > 0 && characterFeature.subSprites.Count > 0)
            {
                for (int i = 0; i < targetFeature.transform.childCount; i++)
                {
                    GameObject subFeature = targetFeature.transform.GetChild(i).gameObject;
                    subFeature.GetComponent<Image>().sprite = characterFeature.subSprites[i];
                }
            }

            UpdateGameManager();
        }
    }

    public void UpdateGameManager()
    {
        onOptionSelected.Invoke(featureDescription);
    }
}
