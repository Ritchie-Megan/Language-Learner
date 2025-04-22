using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject submitMessage;
    public GameObject winScreen;
    public GameObject loseScreen;
    public int numCharacters = 16;
    public List<string> eliminated;
    public List<Character> characters;
    public TextMeshProUGUI loseText;

    private int activeCount;
    private bool updateOccured;
    [SerializeField]
    private Character guessWho;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eliminated = new List<string>();
        activeCount = numCharacters;
        guessWho = characters[Random.Range(0, characters.Count)];
        Debug.Log("Target Character: " + guessWho.characterName);
        loseText.text = "YOU LOSE...\nYour person was " + guessWho.characterName;
    }

    void Update()
    {
        if (activeCount == 1 && updateOccured)
            promptSubmit();
    }

    public void updateCount(bool increase, string name)
    {
        if (increase)
        {
            activeCount++;
            eliminated.Remove(name);
        }
        else
        {
            activeCount--;
            eliminated.Add(name);
        }
        
        updateOccured = true;
    
    }

    public void promptSubmit()
    {
        updateOccured = false;
        submitMessage.SetActive(true);
    }

    public void confirmSubmit()
    {
        submitMessage.SetActive(false);

        

        if (eliminated.Contains(guessWho.characterName))
            loseScreen.SetActive(true);
        else
            winScreen.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Unit 2 Menu");
    }
    
    public Character GetTargetCharacter()
    {
        return guessWho;
    }
}
