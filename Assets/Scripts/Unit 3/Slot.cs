using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Slot : MonoBehaviour, IDropHandler
{
    //Item that is curently being held in the slot
    public GameObject currentItem;

    public void OnDrop(PointerEventData eventData) {
        string parentName = transform.name;
        //UnityEngine.Debug.Log("Dropped in slot " + parentName);
    }
}
