using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    private Player playerMvt;
    public GameObject settingsWindow;
    public GameObject pauseMenuUI;

    public static bool pause = false;

    protected void Update()
    {
        if (pause)
        {
            pause = false;
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
        playerMvt = FindObjectOfType<Player>();
        playerMvt.rb.velocity = Vector2.zero;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsWindow.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        Resume();
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }
    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void BestiaireButton()
    {
        settingsWindow.SetActive(false);
        //Bestiaire.setActive(true);
    }

    
}
