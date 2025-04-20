using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollBarLoader : MonoBehaviour
{
    public GameObject draggablePrefab;
    public Transform contentPanel;
    public int itemCount = 0;

    public void AddItem(string itemName) {
        //create new instance of item
        GameObject newItem = Instantiate(draggablePrefab, contentPanel);
        newItem.name = itemName;

        //set name
        newItem.GetComponentInChildren<TMP_Text>().text = itemName;

    }
    
}
