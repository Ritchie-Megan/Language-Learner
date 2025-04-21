using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool paused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        //escape brings up pause menu
        if(Input.GetKeyDown(KeyCode.Escape)) {
            pause();
        }
    }

    public void pause() {
        if(!paused) {
            pauseMenu.SetActive(true);
            paused = true;
        }
        else {
            pauseMenu.SetActive(false);
            paused = false;
        }
    }

}