using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Game;

namespace Assets.Scripts.UI
{
    public class RestartButton : ButtonListner
    {
        protected override void OnButtonClicked()
        {
            Restart();
        }

        private void Restart()
        {
            PauseMenu.Resume();
            SceneManager.LoadScene("MainMenu");
        }
    }
}