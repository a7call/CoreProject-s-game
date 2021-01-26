using UnityEngine;

namespace Edgar.Unity
{
    public class FogOfWarAdditionalCamera : MonoBehaviour
    {
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            FogOfWar.Instance?.OnRenderImage(source, destination, GetComponent<Camera>());
        }
    }
}