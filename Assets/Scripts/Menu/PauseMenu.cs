using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    private PlayerMouvement playerMvt;
    public GameObject settingsWindow;
    public GameObject pauseMenuUI;

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
                settingsWindow.SetActive(false);
            }
            else
            {
                Paused();
            }
        }
    }


    protected void Paused()
    {
        playerMvt = FindObjectOfType<PlayerMouvement>();
        playerMvt.rb.velocity = Vector2.zero;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        
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
}
