using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ItemSlotNode : MonoBehaviour, IDropHandler
{
    public bool isTreeCanvas = false;

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null) {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            RectTransform draggedObj = eventData.pointerDrag.GetComponent<RectTransform>();
            //snap obj inside panel
            draggedObj.SetParent(transform);
            draggedObj.anchoredPosition = Vector2.zero;

            //if back to list, reset for list
            if(!isTreeCanvas) {
                draggedObj.localScale = Vector2.one;
            }
        }
    }
}
