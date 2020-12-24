using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Classe mère des Healers et héritière de Enemy.cs
///  Elle contient  la liste des ennemis que le healer peut soigner
///  Une fonction permettant de trouver un ennemi à heal 
///  Une coroutine de heal 
///  Une fonction permettant de repérer la destruction d'un ennemi
///  Une fonction renvoyant un bool qui montre si le healer est à porté de heal 
/// </summary>
public class Healer : Enemy
{
    // Liste des ennemies que le healer peut heal
    [SerializeField] protected List<GameObject> ennemies;
    // Montant du heal;
    protected int amountToheal;
    // Boolean permettant de savoir si le healer est en range de heal
    private bool isInSight = false;
    // Boolean permettant de savoir si à/doit commencer à heal
    public bool startHealing = false;
    // Boolean permettant de fermer la bouble de GetEnnemitoHeal()
    public bool hasFinished = true;
    //  count du nombre d'ennemi n'ayant pas subis de dégats
    int count = 0;
    // Ennemi entrain de se faire heal
    GameObject EnnemiHealed;
    //Distance à laquel le healer peut soigner;
    [SerializeField] float healingDistance = 0f;
    // couroutine déclaration
    IEnumerator mycouroutine;
    // Cd de heal 
    [SerializeField] protected float healCd;

    private void Start()
    {
        SetMaxHealth();
        // Set the state to patrol
        currentState = State.Patrolling;
        // Initialise la liste
        List<GameObject> ennemies = new List<GameObject>();
        // Set point to lui même car pas de patrouille
        

    }

    // à replacer au bon endroit 
    protected override void Update()
    {
        base.Update();
        switch(currentState)
        {
            case State.Attacking :
                // voir détails
                GetEnnemiToHeal();
                // vérifi si l'énnemis en cours de heal est tué, si c'est le cas stop la couroutine de heal et recommence une boucle
                checkEnnemiDestroyed(EnnemiHealed);
                // vérifi si l'énnemi à heal est à distance 
                checkehealingDistance();
                break;
            case State.Chasing:
                // voir détails
                GetEnnemiToHeal();
                // vérifi si l'énnemis en cours de heal est tué, si c'est le cas stop la couroutine de heal et recommence une boucle
                checkEnnemiDestroyed(EnnemiHealed);
                // vérifi si l'énnemi à heal est à distance 
                checkehealingDistance();
                break;
            case State.Patrolling:
                checkEnnemiDestroyed(EnnemiHealed);
                // voir détails
                GetEnnemiToHeal();
                break;
        }
       
    }



    private void GetEnnemiToHeal()
    {
        // Lance 1 seule boucle. Pour lancer une boucle hasFinished n'est remis à true que une fois l'action en cours terminé. 
        if (hasFinished)
        {
        hasFinished = false;
        // Cast List to Array pour manipulation
        GameObject[] ennemiArray = ennemies.ToArray();

            // Loop sur les ennemis de la liste
            for (int i = 0; i < ennemiArray.Length; i++)
            {

                // Cleaning de l'array après destruction d'un ennemi
                if (ennemiArray[i] == null)
                {
                    ennemies.Remove(ennemiArray[i]);
                    hasFinished = true;
                    return;
                }

                // Verifie qu'un ennemi ait perdu des hp, si c'est le cas lance l'action du heal
                if (ennemiArray[i].GetComponent<Enemy>().currentHealth < ennemiArray[i].GetComponent<Enemy>().maxHealth)
                {
                    

                    // réinitialise le count 
                    count = 0;
                    startHealing = true;
                    // Stock cette ennemi
                    EnnemiHealed = ennemiArray[i];
                    mycouroutine = HealEnnemiCo(EnnemiHealed, 1);

                    // Si à porté..
                    if (isInSight)
                    {
                        // S'arrete
                        rb.velocity = Vector2.zero;
                        // Passe le state à attack
                        currentState = State.Attacking;
                        // lance la couroutine de heal
                        StartCoroutine(mycouroutine);
                    }
                    // sinon reboucle et commence a suivre l'ennemi à heal
                    else
                    {
                        // reboucle 
                        hasFinished = true;
                        // Passe l'etat à chase
                        currentState = State.Chasing;
                        // change target vers l'ennemi à heal
                        target = ennemiArray[i].GetComponent<Transform>().transform;
                    }




                }
                // Si l'ennemi n'a pas perdu des hps alors count ++
                else
                {
                    count++;
                    // Si tout les ennemis de la liste n'ont pas perdu des hps, relance la fonction 
                    if (count == ennemiArray.Length)
                    {

                        count = 0;
                        startHealing = false;
                        // Passe la condition de relance à true
                        hasFinished = true;
                        return;
                    }
                }
            }
        }
       
    }


    // Couroutine de heal
    private IEnumerator HealEnnemiCo(GameObject _ennemiToHeal, int _amountToHeal)
    {
        // Heal l'ennemi tant qu'il n'est pas full life et qu'il est à porté
        while (_ennemiToHeal.GetComponent<Enemy>().currentHealth < _ennemiToHeal.GetComponent<Enemy>().maxHealth && isInSight )
        {
            // bloque la fonction
            hasFinished = false;
            // heal l'ennemi
            _ennemiToHeal.GetComponent<Enemy>().currentHealth += _amountToHeal;
            // cd du heal 
            yield return new WaitForSeconds(healCd);
        }
        // relance la fontion 
        hasFinished = true;
        // stop la couroutine
        yield break;
    }

    
    private void checkEnnemiDestroyed(GameObject _ennemiToHeal)
    {
        if (_ennemiToHeal == null && startHealing)
        {
            // repasse en state patrolling 
            currentState = State.Patrolling;
            // reboucle
            hasFinished = true;
            // arrete la coroutine si en cour d'excution 
            StopCoroutine(mycouroutine);
            // s'arrete
            rb.velocity = Vector2.zero;
            target = transform;
        }
        else return;
    }

    // Check la distance entre le healer et l'ennemi à heal 
    private void  checkehealingDistance()
    {
       if(EnnemiHealed != null)
        {
            if (Vector2.Distance(transform.position, EnnemiHealed.GetComponent<Transform>().transform.position) < healingDistance)
            {
                isInSight = true;
            }
            else
            {
                isInSight = false;
            }
        }
        
    }
}
