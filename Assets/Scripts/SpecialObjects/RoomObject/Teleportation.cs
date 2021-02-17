using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Selon la room, on spawn X TP prédéfinis, dont les emplacements varient selon les rooms
/// Lorsque le joueur passe dans sur un TP. Pendant T1 secondes, le jeu se ralenti (tout le jeu). 
/// La caméra se dézoom, les points de TP s'aggrandissent et deviennent plus visibles. 
/// Le joueur peut continuer de se déplacer autour du TP pendant T1. 
/// Si il n'utilise pas un autre TP, il perd des HP
/// Si il clique sur un TP, il y est téléporté
/// Les Téléporteurs sont désactivés pendant T2 secondes après l'utilisation (évite le spam) 
/// </summary>

public class Teleportation : MonoBehaviour
{
    // Joueur
    private Player player;
    private PlayerHealth playerHealth;
    //public GameObject portal;

    // Caméra
    private CameraZoom cameraUnzoom;

    // Timer
    private TimeManager timeManager;

    // Contient les différents TP de la map
    [SerializeField] private GameObject[] teleporteurs;

    // Taille des TP
    private float initialTpSize;
    private float bigTpSize = 10;

    // Timer
    private bool isTimerStarted = false;
    private float timer;
    private float maxTimer = 2f;

    private float reloadDelay = 3f;
    [SerializeField] private static bool canTp = true;


    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        teleporteurs = GameObject.FindGameObjectsWithTag("TP");
        initialTpSize = transform.localScale.x;

        timeManager = FindObjectOfType<TimeManager>();
        cameraUnzoom = FindObjectOfType<CameraZoom>();
    }

    private void Update()
    {
        Timer();
        DisableTp();
    }

    private void Timer()
    {
        if (isTimerStarted)
        {
            timer += Time.unscaledDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTimerStarted = true;
            cameraUnzoom.isUnzoomed = true;
            IncreaseTpSize();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canTp)
        {
            timeManager.DoSlowMotion();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTimerStarted = false;
            timer = 0;
            cameraUnzoom.isUnzoomed = false;
            RenitializeTpSize();
        }
    }

    private void DisableTp()
    {
        if (timer > maxTimer)
        {
            isTimerStarted = false;
            timer = 0;
            playerHealth.currentHealth -= 1;
        }
    }


    private void IncreaseTpSize()
    {
        foreach (GameObject tp in teleporteurs)
        {
            if(tp.name != this.gameObject.name)
            {
                tp.transform.localScale = new Vector2(bigTpSize, bigTpSize);
            }
        }
    }

    private void RenitializeTpSize()
    {
        foreach(GameObject tp in teleporteurs)
        {
            if(tp.name != this.gameObject.name)
            {
                tp.transform.localScale = new Vector2(initialTpSize, initialTpSize);
            }
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
