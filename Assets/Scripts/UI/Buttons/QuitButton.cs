using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class QuitButton : ButtonListner
    {
        protected override void OnButtonClicked()
        {
            Application.Quit();
        }
    }
}