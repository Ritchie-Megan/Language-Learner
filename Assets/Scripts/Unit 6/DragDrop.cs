using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

//https://www.youtube.com/watch?v=kWRyZ3hb1Vc

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private ScrollRect scrollView;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Vector2 originalPosition;
    //[HideInInspector] public Transform parentAfterDrag;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        //make sure obj has canvas group
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        if (canvas == null) {
            canvas = FindObjectOfType<Canvas>();
        }
        
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        //original location
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
        transform.SetParent(canvas.transform, true);

        //while moving
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        //parentAfterDrag = transform.parent;
        //transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        if(scrollView != null) {
            scrollView.GetComponent<Mask>().enabled = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;


        //set to original if no drop place
        if(eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragDrop>() != null) {
            //transform.SetParent(eventData.pointerEnter.transform, false);
            //rectTransform.anchoredPosition = Vector2.zero;
            Debug.Log("Dropping Detected");
        }
        else {
            Debug.Log("Dropped in Invalid Zone");
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = originalPosition;
        }

        //reenable scrollview
        if (scrollView != null) {
            scrollView.GetComponent<Mask>().enabled = true;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

}
