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

    // Caméra
    private CameraZoom cameraUnzoom;

    // Timer
    private TimeManager timeManager;

    // Contient les différents TP de la map
    [SerializeField] private GameObject[] teleporteurs;

    // Taille des TP [JE CROIS QUE J EN AI PLUS BESOIN, géré depuis scaleOverTime]
    private Vector2 initialTpScale;
    private float scaleUpTpSize = 20f;
    private float smooth = 5f; // A voir

    // Timer
    [SerializeField] private bool isInRange = false;
    [SerializeField] private bool isContact = false;

    // TP Color
    private Color activeColor = Color.white;
    private Color disableColor = Color.black ;

    [SerializeField] private LayerMask layer;
    private static bool canTp = true;
    private float reloadDelay = 3f;


    private void Start()
    {
        // Joueur
        player = FindObjectOfType<Player>();
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
        Test();
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
            isContact = false;
            this.gameObject.GetComponent<SpriteRenderer>().color = activeColor;

            foreach (GameObject tp in teleporteurs)
            {
                if (tp.name != this.gameObject.name)
                {
                    StartCoroutine(tp.GetComponent<ScaleOverTime>().Decrease());
                }
            }
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            isContact = true;
            cameraUnzoom.isUnzoomed = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timeManager.DoSlowMotion();
        }
    }

    //void Test()
    //{
    //    if (isInRange && canTp && Input.GetMouseButtonDown(0))
    //    {
    //        RaycastHit hit;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    //        Vector2 maPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        print(maPos);
    //        Debug.DrawRay(player.transform.position, maPos, Color.yellow, 10f);

    //        if (Physics2D.Raycast(player.transform.position, maPos, layer))
    //        //if(Physics.Raycast(ray, out hit))
    //        {
    //            print("marche");
    //            //if (hit.transform.CompareTag("TP"))
    //            //{
    //            //    print("Hello");
    //            //}
    //        }
    //    }
    //}
    void Test()
    {
        if (isInRange && canTp && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;+
            print(mousePos);
            Debug.DrawRay(player.transform.position, mousePos, Color.yellow, 10f);

            if(Physics2D.Raycast(player.transform.position, (mousePos-player.transform.position).normalized, layer))
            {
                print("hello");
            }
        }
    }

    //void OnMouseDown()
    //{
    //    if (isInRange)
    //    {
    //        Debug.Log(gameObject.name);
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
