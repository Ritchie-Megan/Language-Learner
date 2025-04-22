using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    public List<int> visitedMiniGames = new List<int>();
    public Text clueTextUI;
    public Text feedbackTextUI;
    public GameObject endScreen;

    private int currentIndex = 0;

    void Start()
    {
        //--choose a random room--
        int randRoom = Random.Range(0, miniGames.Length);
        //save it
        visitedMiniGames.Add(randRoom);
        currentIndex = randRoom;
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
        //if visited count = miniGame count that means the player has visited all games, thus end screen
        if (visitedMiniGames.Count != miniGames.Length) {
            //choose a random room
            int randRoom = Random.Range(0, miniGames.Length);
            while (visitedMiniGames.Contains(randRoom)) {
                randRoom = Random.Range(0, miniGames.Length);
            }
            visitedMiniGames.Add(randRoom);
            currentIndex = randRoom;
        }
        else {
            endGame();
        }
        
        ShowClue();
    }
    
    void ShowClue()
    {
        clueTextUI.text = "Clue: " + miniGames[currentIndex].clueText;
    }

    void endGame() {
        endScreen.SetActive(true);
    }
}