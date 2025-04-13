using UnityEngine;
using UnityEngine.UI;

public class VocabQuizMgr : MonoBehaviour
{

    public GameObject popupPanel;

    public Text question;

    public InputField answerInput;

    public Button submitButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        popupPanel.SetActive(false);
    }

    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
