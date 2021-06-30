using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Levels
{
    public class LevelManager : Singleton<LevelManager>
    {     
        public void SwitchScene(string _name)
        {
            if (!string.IsNullOrEmpty(_name))
                SceneManager.LoadScene(_name);
            else
                Debug.LogWarning("Scene not specified");
        }
    }
}