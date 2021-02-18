using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float normal; // Taille classique de la cam�ra
    private int unZoom = 15; // Taille de la cam�ra lorsqu'on d�zoom (A definir -> taille d'une room)
    private float smooth = 5;

    // Permet de savoir si la cam�ra est d�zoom�e ou non 
    public bool isUnzoomed = false;

    private void Start()
    {
        normal = GetComponent<Camera>().orthographicSize;
    }

    private void Update()
    {
        UnZoom();
    }

    private void UnZoom()
    {
        if (isUnzoomed)
        {
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, unZoom, Time.deltaTime * smooth);
        }
        else
        {
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, normal, Time.deltaTime * smooth);
        }
    }
}
