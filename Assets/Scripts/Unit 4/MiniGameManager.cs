using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MiniGame
{
    public string targetRoomName;
    public string clueText;
    public GameObject miniGamePanel; // the UI panel to show
}

public class MiniGameManager : MonoBehaviour
{
    public MiniGame[] miniGames;
    public Text clueTextUI;
    public Text feedbackTextUI;

    private int currentIndex = 0;

    void Start()
    {
        ShowClue();
    }

    public void CheckRoom(string roomName)
    {
        var current = miniGames[currentIndex];

        if (roomName == current.targetRoomName)
        {
            feedbackTextUI.text = "Correct! Starting game...";
            clueTextUI.gameObject.SetActive(false);
            feedbackTextUI.gameObject.SetActive(false);
            current.miniGamePanel.SetActive(true);
        }
        else
        {
            feedbackTextUI.text = "Wrong room. Try again.";
        }
    }

    public void CloseGame()
    {
        feedbackTextUI.text = "";
        var current = miniGames[currentIndex];
        current.miniGamePanel.SetActive(false);

        clueTextUI.gameObject.SetActive(true);
        feedbackTextUI.gameObject.SetActive(true);
        currentIndex++;
        ShowClue();
    }
    
    void ShowClue()
    {
        clueTextUI.text = "Clue: " + miniGames[currentIndex].clueText;
    }
}