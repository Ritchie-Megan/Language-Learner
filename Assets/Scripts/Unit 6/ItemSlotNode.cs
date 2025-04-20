using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using FamilyTree;

public class ItemSlotNode : MonoBehaviour, IDropHandler
{
    public Node node;

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("Item dropped into: " + gameObject.name);

        if (eventData.pointerDrag != null)
        {
            GameObject droppedObject = eventData.pointerDrag;

            // Ensure the object has the DragDrop component
            DragDrop dragDropScript = droppedObject.GetComponent<DragDrop>();
            if (dragDropScript != null)
            {
                if (transform.childCount == 0) {
                    droppedObject.transform.SetParent(transform, false);
                    droppedObject.transform.SetAsLastSibling();
                    droppedObject.GetComponent<RectTransform>().position = Vector3.zero; 
                    

                    //check to see if child is in correct place
                    if (gameObject.name == droppedObject.name) {
                        Debug.Log("Matched pair!");
                        Sprite greenLeaf = Resources.Load<Sprite>("Unit6/greenLeaf");
                        droppedObject.GetComponent<Image>().sprite = greenLeaf;
                    }
                    else {
                        Debug.Log("Not matched pair!");
                        Sprite orangeLeaf = Resources.Load<Sprite>("Unit6/orangeLeaf");
                        droppedObject.GetComponent<Image>().sprite = orangeLeaf;
                    }
                }
            }
        }
    }
    
}
