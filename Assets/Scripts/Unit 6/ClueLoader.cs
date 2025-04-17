using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClueLoader : MonoBehaviour
{
    public GameObject draggablePrefab;
    public Transform contentPanel;

    public void AddItem(string clue) {
        //create new instance of item
        GameObject newItem = Instantiate(draggablePrefab, contentPanel);

        //set name
        newItem.GetComponentInChildren<TMP_Text>().text = clue;

    }
}
