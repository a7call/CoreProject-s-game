using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Levels;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonListner : MonoBehaviour
    {
        Button menuButton;
        protected void Awake()
        {
            menuButton = GetComponent<Button>();
            menuButton.onClick.AddListener(OnButtonClicked);
        }

        protected abstract void OnButtonClicked();
    }
}