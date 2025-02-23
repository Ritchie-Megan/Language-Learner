using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit5SceneChange : MonoBehaviour
{
    public void GoToStage() {
        SceneManager.LoadScene("Scenes/Unit 5/Stage");
    }
    public void GoToQuestion() {
        SceneManager.LoadScene("Scenes/Unit 5/Question Builder");
    }
    public void QuitMinigame() {
        SceneManager.LoadScene("Scenes/Unit 5");
    }
}
