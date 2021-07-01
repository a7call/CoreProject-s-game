using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Levels;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class SceneSwitcher : MonoBehaviour
    {
        public string desiredScene;
        Button menuButton;
        SceneManagementWanderer sceneManager;
        

        private void Start()
        {
            menuButton = GetComponent<Button>();
            menuButton.onClick.AddListener(OnButtonClicked);
            sceneManager = SceneManagementWanderer.GetInstance();
        }

        void OnButtonClicked()
        {
            sceneManager.SwitchScene(desiredScene);
        }
    }
}