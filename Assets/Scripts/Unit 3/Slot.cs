using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Slot : MonoBehaviour, IDropHandler
{
    //Item that is curently beign held in the slot
    public GameObject currentItem;

    public void OnDrop(PointerEventData eventData) {

    }
}
