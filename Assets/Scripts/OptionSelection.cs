using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class OptionSelection : MonoBehaviour
{
    public SpriteRenderer featureToChange;

    public void SetFeature()
    {
        Sprite sprite = GetComponentInChildren<Image>().sprite;
        featureToChange.sprite = sprite;
    }

    public void SetFeatureToChange(SpriteRenderer renderer)
    {
        featureToChange = renderer;
    }
    
}
