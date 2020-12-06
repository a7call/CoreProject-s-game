using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;

    public GameObject pauseMenuUI;

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }


    protected void Paused()
    {
        
        pauseMenuUI.SetActive(true);
        PlayerMouvement.instance = false;
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        PlayerMouvement.instance = true;
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        Resume();
    }
}
