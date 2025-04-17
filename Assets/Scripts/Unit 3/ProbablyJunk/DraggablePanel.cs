using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DraggablePanel : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private bool dropped = false;
    //[HideInInspector] public Transform parentAfterDrag;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        //makke sure obj has canvas group
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        dropped = false;
        //original location
        originalParent = transform.parent;
        transform.SetParent(canvas.transform, true);

        //while moving
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        //parentAfterDrag = transform.parent;
        //transform.SetParent(transform.root);
        //transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        //set to original if no drop place
        if(!dropped) {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void isDropped() {
        Debug.Log("dropped = true");
        dropped = true;
    }

}
