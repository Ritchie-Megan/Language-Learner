using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit6Navigate : MonoBehaviour
{
    public void GoToMainMenu() {
        SceneManager.LoadScene("Scenes/Main Menu");
    }
    public void GoToMinigame() {
        SceneManager.LoadScene("Scenes/Unit 6");
    }
    public void GoToUnit6Menu() {
        SceneManager.LoadScene("Scenes/Unit 6 Menu");
    }
}
