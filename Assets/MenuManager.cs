using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public void ClickToHome()
    {
        SceneManager.LoadScene(0);
    }
}
