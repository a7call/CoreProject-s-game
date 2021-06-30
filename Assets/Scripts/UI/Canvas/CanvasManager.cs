using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;




namespace Assets.Scripts.UI
{
    public enum CanvasType
    {
        MainMenu,
        GameUI,
        EndScreen
    }

    public class CanvasManager : Singleton<CanvasManager>
    {
        List<CanvasController> canvasControllersList;
        CanvasController lastActiveCanvas;

        protected override void Awake()
        {
            base.Awake();

            canvasControllersList = GetComponentsInChildren<CanvasController>().ToList();
            canvasControllersList.ForEach(x => x.gameObject.SetActive(false));
            SwitchCanvas(CanvasType.MainMenu);
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
