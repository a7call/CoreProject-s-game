using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Assets.Scripts.UI
{
    public class TestCanvasManager : Singleton<TestCanvasManager>
    {
        List<CanvasController> canvasControllersList;
        CanvasController lastActiveCanvas;

        protected override void Awake()
        {
            base.Awake();

            canvasControllersList = GetComponentsInChildren<CanvasController>().ToList();
        }

        private void Start()
        {
            canvasControllersList.ForEach(x => x.gameObject.SetActive(false));
            SwitchCanvas(CanvasType.PlayerUI);
        }

        public void SwitchCanvas(CanvasType _type)
        {
            if (lastActiveCanvas != null)
                lastActiveCanvas.gameObject.SetActive(false);

            var desiredCanvas = canvasControllersList.Find(x => x.canvasType == _type);

            if (desiredCanvas != null)
            {
                desiredCanvas.gameObject.SetActive(true);
                lastActiveCanvas = desiredCanvas;
            }
            else
            {
                Debug.LogWarning("the desired canvas was not found !");
            }

        }
    }
}
