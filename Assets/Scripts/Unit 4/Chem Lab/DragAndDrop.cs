using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Make sure to add using UnityEngine.UI for events if needed.

public class DragAndDropWord : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform dragLayer; // Assign in Inspector (the top-level "DragLayer" object)
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(dragLayer, true); // Move out of any layout group
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragLayer as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );
        rectTransform.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // If the OnDrop never re-parented us, revert to original
        if (transform.parent == dragLayer)
        {
            transform.SetParent(originalParent, true);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}