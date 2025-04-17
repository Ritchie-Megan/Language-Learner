using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CharacterCard : MonoBehaviour
{
    public UnityEvent<bool, string> onCardToggle;
    private bool cardActive;
    private string cardName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardActive = true;
        cardName = transform.GetChild(1).GetComponent<TMP_Text>().text;
        
    }

    // Update is called once per frame
    public void toggleCard()
    {
        cardActive = !cardActive;
        onCardToggle.Invoke(cardActive, cardName);
    }
}