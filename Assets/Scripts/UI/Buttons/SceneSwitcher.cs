using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Levels;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{

    public class SceneSwitcher : ButtonListner
    {
        public string desiredScene;
        SceneManagementWanderer sceneManager;


        private  void Start()
        {
            sceneManager = SceneManagementWanderer.GetInstance();
        }
        protected override void OnButtonClicked()
        {
            sceneManager.SwitchScene(desiredScene);
        }
    }
}