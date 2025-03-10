using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; 

public class ImageLoader : MonoBehaviour
{
    public GameObject namePrefab;
    public Transform contentPanel;

    public void LoadScrableNames(List<string> scrambledNames) {
        foreach (Transform child in contentPanel) {
            Destroy(child.gameObject);
        }
    }
}
