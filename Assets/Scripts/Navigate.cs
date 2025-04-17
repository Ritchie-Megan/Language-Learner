
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigate : MonoBehaviour
{
    public void GoToUnit1() {
        SceneManager.LoadScene("Scenes/Unit 1");
    }
    public void GoToUnit2() {
        SceneManager.LoadScene("Scenes/Unit 2");
    }
    public void GoToUnit3() {
        SceneManager.LoadScene("Scenes/Unit 3 Menu");
    }
    public void GoToUnit4() {
        SceneManager.LoadScene("Scenes/Unit 4");
    }
    public void GoToUnit5() {
        SceneManager.LoadScene("Scenes/Unit 5");
    }
    public void GoToUnit6() {
        SceneManager.LoadScene("Scenes/Unit 6");
    }
    public void GoToMenu() {
        SceneManager.LoadScene("Scenes/Main Menu");
    }
}
