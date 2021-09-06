using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    private Player playerMvt;
    public static GameObject pauseMenuUI;

    private void Start()
    {
        pauseMenuUI = transform.Find("PauseMenu").gameObject;
    }
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                PauseGame();
            }
        }
    }

    protected void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
    }
    public static void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        PauseMenu.isGamePaused = false;
    }
    public void Quit()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
}
