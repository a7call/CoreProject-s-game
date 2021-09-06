using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Assets.Scripts.UI
{
    public class ResumeButton : ButtonListner
    {
         public GameObject pauseMenuUI;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu.Resume();
            }

        }
        protected override void OnButtonClicked()
        {
            PauseMenu.Resume();
        }


       
    }
}
