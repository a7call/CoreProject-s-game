using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private int normal = 10; // Taille classique
    // Dézoomer sur la salle pendant que le joueur utilisera le TP (Récupérer la taille de la salle)
    private int unZoom = 30;
    private float smooth = 5;

    // Permet de savoir si elle est unZoomed
    public bool isUnzoomed = false;

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
