using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Selon la room, on spawn X TP pr�d�finis, dont les emplacements varient selon les rooms
/// Lorsque le joueur passe dans sur un TP. Pendant T1 secondes, le jeu se ralenti (tout le jeu). 
/// La cam�ra se d�zoom, les points de TP s'aggrandissent et deviennent plus visibles. 
/// Le joueur peut continuer de se d�placer autour du TP pendant T1. 
/// Si il n'utilise pas un autre TP, il perd des HP
/// Si il clique sur un TP, il y est t�l�port�
/// Les T�l�porteurs sont d�sactiv�s pendant T2 secondes apr�s l'utilisation (�vite le spam) 
/// </summary>

public class Teleportation : MonoBehaviour
{
    // Joueur
    private Player player;
    private PlayerHealth playerHealth;
    private PlayerMouvement playerMouvement;
    //public GameObject portal;

    // Cam�ra
    private CameraZoom cameraUnzoom;

    // Timer
    public TimeManager timeManager;

    // Contient les diff�rents TP de la map
    private GameObject[] teleporteurs;


    private float reloadDelay = 3f;
    [SerializeField] private static bool canTp = true;


    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();

        teleporteurs = GameObject.FindGameObjectsWithTag("TP");
        timeManager = FindObjectOfType<TimeManager>();

        cameraUnzoom = FindObjectOfType<CameraZoom>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canTp)
        {
            print("HI");
            timeManager.DoSlowMotion();
            cameraUnzoom.isUnzoomed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraUnzoom.isUnzoomed = false;
        }
    }

    //private IEnumerator Teleporte()
    //{
    //    Teleportation.canTp = false;
    //    player.transform.position = portal.transform.position;
    //    yield return new WaitForSeconds(reloadDelay);
    //    Teleportation.canTp = true;
    //}
}
