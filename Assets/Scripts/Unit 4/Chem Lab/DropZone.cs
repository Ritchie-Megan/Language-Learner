using UnityEngine;
using UnityEngine.EventSystems;

public class WordDropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var draggedWord = eventData.pointerDrag.GetComponent<DragAndDropWord>();
        if (draggedWord != null)
        {
            // 1. Re-parent to this drop zone
            draggedWord.transform.SetParent(transform, false); 
            // ^ false means "don't keep local offset from old parent"

            // 2. Convert the mouse position to local coordinates of the drop zone
            RectTransform dropZoneRect = transform as RectTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                dropZoneRect,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint
            );

            // 3. Place the dropped word exactly where the mouse is
            RectTransform draggedRect = draggedWord.GetComponent<RectTransform>();
            draggedRect.anchoredPosition = localPoint;
        }
    }
}