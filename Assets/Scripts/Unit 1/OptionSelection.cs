using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class OptionSelection : MonoBehaviour
{
    public SpriteRenderer featureToChange;
    public CharacterFeature characterFeature;

    public void SetFeature()
    {
        Sprite sprite = GetComponentInChildren<Image>().sprite;
        featureToChange.sprite = characterFeature.featureSprite;
    }

    public void SetFeatureToChange(SpriteRenderer renderTarget, CharacterFeature feature)
    {
        featureToChange = renderTarget;
        characterFeature = feature;
    }
    
}
