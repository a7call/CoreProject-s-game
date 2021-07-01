using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Levels
{
    public class SceneManagementWanderer : Singleton<SceneManagementWanderer>
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