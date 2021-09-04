using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class CanvasSwitcher : ButtonListner
    {
        public CanvasType desiredCanvasType;

        CanvasManager canvasManager;

        private void Start()
        {
            canvasManager = CanvasManager.GetInstance();
        }

        protected override void OnButtonClicked()
        {
            canvasManager.SwitchCanvas(desiredCanvasType);
        }
    }
}