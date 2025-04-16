using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DraggableActivities : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OnBeginDrag(PointerEventData eventData) {
        //origional parent save
        originalParent = transform.parent;
        //now clear the item from the slot, so the slot is "free" again
        if (originalParent.GetComponentInParent<Slot>() != null && originalParent.GetComponentInParent<Slot>().currentItem == gameObject) {
            originalParent.GetComponentInParent<Slot>().currentItem = null;
        }
        //make it above all other things
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        //transparent as drag
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData) {
        //follows mouse
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        //enables raycast again
        canvasGroup.blocksRaycasts = true;
        //no longer transparent
        canvasGroup.alpha = 1f;
        //get slot where the item is dropped
        Slot dropSlot = eventData.pointerEnter?.GetComponentInParent<Slot>();
        Transform dropBox = dropSlot?.transform.Find("DropBox");
        if (dropSlot != null && dropBox != null) {
            if (dropSlot.currentItem != null) {
                //if slot has an item, send drag item to original palce
                transform.SetParent(originalParent, false);
            }
            //else we want to drop it into the new slot
            else {
                transform.SetParent(dropBox.transform, false);
                dropSlot.currentItem = gameObject;
            }
        }
        //else if there is no slot underneath the mouse
        else {
            transform.SetParent(originalParent, false);
        }
        //center within the slot
        GetComponent<RectTransform>().anchorMin = GetComponent<RectTransform>().anchorMax = GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
