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
/// 

// A LIRE : Probleme quand tu rentres et que tu sors vite
// A LIRE : Vrai PROBLEME => Quand tu restes dans le TP avec le ontriggerStay2D
// Faire une fonction test qui l'oblige l'utilisateur à sortir du TP

public class Teleportation : MonoBehaviour
{
    // Joueur
    private Player player;
    private PlayerHealth playerHealth;
    private bool isLoosingHp = false;

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
    private static bool canTp;
    private float reloadDelay = 5f;


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
        if(canTp && isInRange && isContact) EnableTp();
        if(!canTp && !isInRange && isContact) StartCoroutine(DisableTp());
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

    private IEnumerator DisableTp()
    {
        canTp = false;
        isContact = false;
        this.gameObject.GetComponent<Collider2D>().enabled = false;

        foreach (GameObject tp in teleporteurs)
        {
            if (tp.name != this.gameObject.name)
            {
                tp.GetComponent<SpriteRenderer>().color = disableColor;
                StopCoroutine("tp.GetComponent<ScaleOverTime>().Grow");
                StartCoroutine(tp.GetComponent<ScaleOverTime>().Decrease());
            }
        }

        yield return new WaitForSeconds(reloadDelay);
        

        foreach (GameObject tp in teleporteurs)
        {
            tp.GetComponent<SpriteRenderer>().color = activeColor;
        }

        this.gameObject.GetComponent<Collider2D>().enabled = true;
        canTp = true;
    }

    private IEnumerator DisableAllTp()
    {
        if (canTp)
        {
            canTp = false;
            foreach (GameObject tp in teleporteurs)
            {
                tp.GetComponent<SpriteRenderer>().color = disableColor;
                tp.GetComponent<Collider2D>().enabled = false;
            }

            yield return new WaitForSeconds(reloadDelay);

            foreach (GameObject tp in teleporteurs)
            {
                tp.GetComponent<SpriteRenderer>().color = activeColor;
                tp.GetComponent<Collider2D>().enabled = true;
            }

            isLoosingHp = false;
            canTp = true;
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTp = true;
            isInRange = true;
            isContact = true;
            cameraUnzoom.isUnzoomed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTp = false;
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

            for (int i = 0; i < teleporteurs.Length; i++)
            {
                float timer = teleporteurs[i].GetComponent<ScaleOverTime>().timer;
                float growTime = teleporteurs[i].GetComponent<ScaleOverTime>().growTime;
                print(timer);

                if (!isLoosingHp && timer > growTime - 0.02f)
                {
                    isLoosingHp = true;
                    playerHealth.currentHealth -= 1;
                    StartCoroutine(DisableAllTp());
                    Debug.LogWarning("Lance DisabelAllTp");
                }
            }
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

    // Si la distance entre le joueur et la souris est trop faible
    // Ca veut dire qu'il clique sur le TP sur lequel il est
    // De ce fait, il ne peut pas TP
    private void Test()
    {
        if (isInRange && canTp && Input.GetMouseButtonDown(0))
        {
            //canTp = false;
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;
            print(mousePos);
            Debug.DrawRay(player.transform.position, mousePos, Color.yellow, 10f);

            if(Physics2D.Raycast(player.transform.position, (mousePos-player.transform.position).normalized, layer))
            {
                foreach(GameObject tp in teleporteurs)
                {
                    
                }
                print("hello");
            }

            //yield return new WaitForSeconds(reloadDelay);
            //canTp = true;
        }
    }

    //void OnMouseDown()
    //{
    //    if (isInRange)
    //    {
    //        Debug.Log(gameObject.name);
    //    }
    //}

}
