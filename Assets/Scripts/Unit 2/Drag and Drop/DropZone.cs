using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableWord word = eventData.pointerDrag.GetComponent<DraggableWord>();
        if (word != null)
        {
            word.transform.SetParent(transform);
        }
    }
}