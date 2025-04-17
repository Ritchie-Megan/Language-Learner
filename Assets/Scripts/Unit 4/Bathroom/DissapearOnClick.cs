using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DissapearOnClick : MonoBehaviour, IPointerClickHandler
{
    [Header("Word Info")]
    public Text wordText; // drag the Text object here in the prefab
    public bool isCorrect; // set this when spawning
    private ClickWrong gameManager;
    
    

    public void Initialize(string spanish, string english, bool correct, ClickWrong manager)
    {
        wordText.text = $"{spanish} - {english}";
        isCorrect = correct;
        gameManager = manager;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isCorrect)
        {
            // Player clicked a correct word (bad!)
            gameManager.WordMissed();
            Destroy(gameObject);
        }
        else
        {
            // Correctly clicked an incorrect pair — remove it
            Destroy(gameObject);
        }
    }
}
