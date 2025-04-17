
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit3Navigate : MonoBehaviour
{
    public void GoToMainMenu() {
        SceneManager.LoadScene("Scenes/Main Menu");
    }
    public void GoToMinigame() {
        SceneManager.LoadScene("Scenes/Unit 3");
    }
    public void GoToUnit3Menu() {
        SceneManager.LoadScene("Scenes/Unit 3 Menu");
    }
}
