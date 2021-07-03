using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    /// <typeparam name="TGameManager">Actual type of the game manager</typeparam>
    public abstract class GameManagerBaseWanderer<TGameManager> : MonoBehaviour
          where TGameManager : Component
    {
        public static TGameManager Instance;
        
        private static bool m_applicationIsQuitting = false;

        public GameObject Canvas;
        public static TGameManager GetInstance()
        {
            if (m_applicationIsQuitting) { return null; }

            if (Instance == null)
            {
                Instance = FindObjectOfType<TGameManager>();
                if (Instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(TGameManager).Name;
                    Instance = obj.AddComponent<TGameManager>();
                }
            }
            return Instance;
        }
        public void Awake()
        {
            Canvas = GetCanvas();
            if (Instance == null)
            {
                Instance = this as TGameManager;
                DontDestroyOnLoad(gameObject);
            }
            else if (!ReferenceEquals(Instance, this))
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }

            if (Canvas != null)
            {
                Canvas.SetActive(true);
            }
        }

        protected virtual void SingletonAwake()
        {
            LoadNextLevel();
        }

        /// <summary>
        /// Load next level.
        /// </summary>
        public abstract void LoadNextLevel();

        /// <summary>
        /// Display information about the level like time to generate the level, etc.
        /// </summary>
        /// <param name="text"></param>
        protected void SetLevelInfo(string text)
        {
            var canvas = Canvas;
            var levelInfo = canvas.transform.Find("LevelInfo")?.gameObject.GetComponent<Text>();

            if (levelInfo != null)
            {
                levelInfo.text = text;
            }
        }

        /// <summary>
        /// Show loading screen with primary and secondary text.
        /// </summary>
        /// <param name="primaryText"></param>
        /// <param name="secondaryText"></param>
        protected void ShowLoadingScreen(string primaryText, string secondaryText)
        {
            var canvas = Canvas;
            var loadingImage = canvas.transform.Find("LoadingImage")?.gameObject;
            var primaryTextComponent = loadingImage?.transform.Find("PrimaryText")?.gameObject.GetComponent<Text>();
            var secondaryTextComponent = loadingImage?.transform.Find("SecondaryText")?.gameObject.GetComponent<Text>();

            if (loadingImage != null)
            {
                loadingImage.SetActive(true);
            }

            if (primaryTextComponent != null)
            {
                primaryTextComponent.text = primaryText;
            }

            if (secondaryTextComponent != null)
            {
                secondaryTextComponent.text = secondaryText;
            }
        }

        /// <summary>
        /// Get canvas game object.
        /// </summary>
        /// <returns></returns>
        protected GameObject GetCanvas()
        {
            var canvas = GameObject.FindGameObjectWithTag("DebugCanvas");

            if (canvas == null)
            {
                throw new InvalidOperationException($"Canvas was not found. Please set the {nameof(Canvas)} variable of the GameManager");
            }

            return canvas;
        }

        /// <summary>
        /// Hide loading screen.
        /// </summary>
        protected void HideLoadingScreen()
        {
            var canvas = Canvas;
            var loadingImage = canvas.transform.Find("LoadingImage")?.gameObject;

            if (loadingImage != null)
            {
                loadingImage.SetActive(false);
            }
        }

        private void OnDisable()
        {
            if (ReferenceEquals(this, Instance))
            {
                Instance = null;
            }
        }
    }
}