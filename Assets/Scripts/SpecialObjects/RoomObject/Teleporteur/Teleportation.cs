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
    private PlayerHealth playerHealth;
    //public GameObject portal;

    // Caméra
    private CameraZoom cameraUnzoom;

    // Timer
    private TimeManager timeManager;

    // Contient les différents TP de la map
    [SerializeField] private GameObject[] teleporteurs;

    // Taille des TP
    private Vector2 initialTpScale;
    private float scaleUpTpSize = 20f;
    private float smooth = 5f;

    // Timer
    private ScaleOverTime scaleOverTime;
    public bool isTimerStarted = false;
    private float timer;

    // TP Color
    private Color activeColor = Color.white;
    private Color disableColor = Color.black ; 

    private float reloadDelay = 3f;
    //[SerializeField] private static bool canTp = true;


    private void Start()
    {
        // Joueur
        playerHealth = FindObjectOfType<PlayerHealth>();

        // TP
        teleporteurs = GameObject.FindGameObjectsWithTag("TP");
        scaleOverTime = FindObjectOfType<ScaleOverTime>();
        initialTpScale = transform.localScale;

        // TimeManager & Camera
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
            this.gameObject.GetComponent<SpriteRenderer>().color = disableColor;
            this.gameObject.GetComponent<ScaleOverTime>().enabled = true;
            isTimerStarted = true;
            cameraUnzoom.isUnzoomed = true;
            //IncreaseTpSize();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTimerStarted)
        {
            //IncreaseTpSize();
            timeManager.DoSlowMotion();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // A mettre ailleurs, test;
            this.gameObject.GetComponent<SpriteRenderer>().color = activeColor;
            scaleOverTime.timer = 0f;
            isTimerStarted = false;
            timer = 0;
            cameraUnzoom.isUnzoomed = false;
            RenitializeTpSize();
        }
    }

    private void DisableTp()
    {
        if (timer > scaleOverTime.growTime)
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
                Vector2 scaleUpTp = new Vector2(scaleUpTpSize, scaleUpTpSize);
                tp.transform.localScale = Vector2.Lerp(initialTpScale, scaleUpTp, smooth*Time.deltaTime);
                print(tp.transform.localScale);
            }
        }
    }

    private void RenitializeTpSize()
    {
        foreach(GameObject tp in teleporteurs)
        {
            if(tp.name != this.gameObject.name)
            {
                tp.transform.localScale = initialTpScale;
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
