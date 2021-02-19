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
    private PlayerHealth playerHealth;

    // Cam�ra
    private CameraZoom cameraUnzoom;

    // Timer
    private TimeManager timeManager;

    // Contient les diff�rents TP de la map
    [SerializeField] private GameObject[] teleporteurs;

    // Taille des TP [JE CROIS QUE J EN AI PLUS BESOIN, g�r� depuis scaleOverTime]
    private Vector2 initialTpScale;
    private float scaleUpTpSize = 20f;
    private float smooth = 5f; // A voir

    // Timer
    [SerializeField] private bool isInRange = false;
    [SerializeField] private bool isContact = false;

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
        initialTpScale = transform.localScale;

        // TimeManager & Camera
        timeManager = FindObjectOfType<TimeManager>();
        cameraUnzoom = FindObjectOfType<CameraZoom>();
    }

    private void Update()
    {
        if(isInRange && isContact) EnableTp();
        if(!isInRange && isContact) DisableTp();
    }

    private void EnableTp()
    {
        isContact = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = disableColor;

        foreach (GameObject tp in teleporteurs)
        {
            if (tp.name != this.gameObject.name)
            {
                StartCoroutine(tp.GetComponent<ScaleOverTime>().Grow());
            }
        }
    }

    private void DisableTp()
    {
        if (!isInRange && isContact)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = activeColor;
            //StartCoroutine(scaleOverTime.Decrease());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            isContact = true;
            cameraUnzoom.isUnzoomed = true;
        }
    }
    private IEnumerator TemporisationCoroutine()
    {
        yield return new WaitForEndOfFrame();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //IncreaseTpSize();
            timeManager.DoSlowMotion();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            isContact = true;
            //scaleOverTime.timer = 0f;
            cameraUnzoom.isUnzoomed = false;
           // RenitializeTpSize();
        }
    }

    //private void DisableTp()
    //{
    //    if (timer > scaleOverTime.growTime)
    //    {
    //        isTimerStarted = false;
    //        timer = 0;
    //        playerHealth.currentHealth -= 1;
    //    }
    //}


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

    //private void RenitializeTpSize()
    //{
    //    foreach(GameObject tp in teleporteurs)
    //    {
    //        if(tp.name != this.gameObject.name)
    //        {
    //            tp.transform.localScale = initialTpScale;
    //        }
    //    }
    //}
    
    //private IEnumerator Teleporte()
    //{
    //    Teleportation.canTp = false;
    //    player.transform.position = portal.transform.position;
    //    yield return new WaitForSeconds(reloadDelay);
    //    Teleportation.canTp = true;
    //}
}
