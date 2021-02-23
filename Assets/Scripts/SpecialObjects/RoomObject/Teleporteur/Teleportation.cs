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

// A LIRE IMPERATIVEMENT : QUAND ON EST EN ONTRIGGER STAY PUIS QU ON PASSE EN EXIT, LA COROUTINE DE DISABLE SE LANCE DEUX FOIS

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

    // Timer
    private float maxTime;
    private Vector2 maxVector;
    [SerializeField] private bool isInRange = false;
    [SerializeField] private bool isContact = false;

    // TP Color
    private Color activeColor = Color.white;
    private Color disableColor = Color.black ;

    [SerializeField] private LayerMask layer;
    private static bool canTp;
    private bool isPrio = false;
    private float reloadDelay = 5f;


    private void Start()
    {
        // Joueur
        player = FindObjectOfType<Player>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        // TP
        teleporteurs = GameObject.FindGameObjectsWithTag("TP");
        maxTime = FindObjectOfType<ScaleOverTime>().growTime;
        maxVector = FindObjectOfType<ScaleOverTime>().maxScale;

        // TimeManager & Camera
        timeManager = FindObjectOfType<TimeManager>();
        cameraUnzoom = FindObjectOfType<CameraZoom>();

        // Initilisation canTp
        canTp = true;
    }

    private void Update()
    {
        if(canTp && isInRange && isContact) StartCoroutine(EnableTp());
        if(!canTp && !isInRange && isContact &&!isPrio) StartCoroutine(DisableTp());
        
        //if (isContact)
        //{
        //    print("isContact true at " + Time.deltaTime);
        //}
        //Test();
    }

    private IEnumerator EnableTp()
    {
        print("EnableTp");
        isContact = false;
        canTp = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = disableColor;

        foreach (GameObject tp in teleporteurs)
        {
            if (tp.name != this.gameObject.name)
            {
                StartCoroutine(tp.GetComponent<ScaleOverTime>().Grow());
            }

        }

        yield return new WaitForSeconds(GetComponent<ScaleOverTime>().growTime);
        canTp = false;
    }

    private IEnumerator DisableTp()
    {
        print("DisableTp");
        isContact = false;
        //this.gameObject.GetComponent<Collider2D>().enabled = false;

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
            tp.GetComponent<ScaleOverTime>().growTime = maxTime;
            tp.GetComponent<ScaleOverTime>().maxScale = maxVector;
        }

        //this.gameObject.GetComponent<Collider2D>().enabled = true;
        canTp = true;
    }

    //private IEnumerator DisableAllTp()
    //{
    //    print("DisableAllTp");
    //    if (canTp)
    //    {
    //        canTp = false;
    //        foreach (GameObject tp in teleporteurs)
    //        {
    //            tp.GetComponent<SpriteRenderer>().color = disableColor;
    //            tp.GetComponent<Collider2D>().enabled = false;
    //        }

    //        yield return new WaitForSeconds(reloadDelay);

    //        foreach (GameObject tp in teleporteurs)
    //        {
    //            tp.GetComponent<SpriteRenderer>().color = activeColor;
    //            tp.GetComponent<Collider2D>().enabled = true;
    //        }

    //        isLoosingHp = false;
    //        canTp = true;
    //    }
    //    yield return null;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isContact = true;
            print("isContact doit être true et il est : " + isContact);
            isInRange = true;
            cameraUnzoom.isUnzoomed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.LogWarning("OnTriggerExit");
            //canTp = false;
            isInRange = false;
            isContact = true;
            cameraUnzoom.isUnzoomed = false;

            foreach (GameObject tp in teleporteurs)
            {
                if(tp.GetComponent<ScaleOverTime>().timer <= tp.GetComponent<ScaleOverTime>().growTime && tp.name != this.gameObject.name)
                {
                    tp.GetComponent<ScaleOverTime>().growTime = tp.GetComponent<ScaleOverTime>().timer;
                    tp.GetComponent<ScaleOverTime>().maxScale = tp.transform.localScale;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.LogWarning("OnTriggerStay");
            timeManager.DoSlowMotion();
            //gameObject.GetComponent<ScaleOverTime>().growTime += Time.unscaledDeltaTime;

            for (int i = 0; i < teleporteurs.Length; i++)
            {
                float timer = teleporteurs[i].GetComponent<ScaleOverTime>().timer;
                float growTime = teleporteurs[i].GetComponent<ScaleOverTime>().growTime;

                if (!isLoosingHp && timer > growTime - 0.02f)
                {
                    isPrio = true;
                    isLoosingHp = true;
                    playerHealth.currentHealth -= 1;
                    StartCoroutine(DisableTp());
                    isPrio = false;
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
