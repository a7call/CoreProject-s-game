using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Classe mère des monstres lasers
/// Le monstre a été modifié du à des problème vis à vis du gameplay de la première version.
/// Cette variation (variation de base) ce comporte comme un distance de base mis à part la gestion des visuel (anim d'attaque + prpojectile différent)
/// L'ancien code ce trouve en bas de page
/// </summary>
public class DistanceNoGun : Distance
{
   
    private Transform attackPointFrontGO;
    private Transform attackPointBackGO;
    private Transform attackPointLeftGO;
    private Transform attackPointRightGO;
    protected override void Awake()
    {
        base.Awake();
        SetData();
        SetMaxHealth();
        findAttackPoints();
    }

    protected override void GetReference()
    {
        base.GetReference();
    }

   
    protected override void Update()
    {        
        base.Update();

        ChangeStateWithRange();

        switch (currentState)
        {
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                // suit le path créé et s'arrête pour tirer
                break;
            case State.Attacking:
                SetInitialAttackPosition();
                isInRange();
                PlayAttackAnim();
                break;
        }
       
    }

    // Methode lergerement modifié pour permettre la mise en place des animations d'attaque


   
    // Récupere en temps réel la position de l'attaque point en fonction de l'animation joué 
    public void SetInitialAttackPosition()
    {
        
        float lastMoveX = animator.GetFloat("lastMoveX");
        float lastMoveY = animator.GetFloat("lastMoveY");
        if (Mathf.Abs(lastMoveX) > Mathf.Abs(lastMoveY))
        {
            if (lastMoveX > 0)
            {
                attackPoint = attackPointRightGO;
            }
            else
            {
                attackPoint = attackPointLeftGO;
            }
        }
        else
        {
            if (lastMoveY > 0)
            {
                attackPoint = attackPointBackGO;
            }
            else
            {
                attackPoint = attackPointFrontGO;
            }
        }
        
    }


   //Recherche les position potentiel des attaques points 
    private void findAttackPoints()
    {

        foreach (Transform trans in transform)
        {
            if (trans.gameObject.name == "attackPoints")
            {
                Transform attackPointContainer = trans;
                foreach (Transform t in trans)
                {
                    if (t.gameObject.name == "attackFront") attackPointFrontGO = t;
                    if (t.gameObject.name == "attackBack") attackPointBackGO = t;
                    if (t.gameObject.name == "attackLeft") attackPointLeftGO = t;
                    if (t.gameObject.name == "attackRight") attackPointRightGO = t;
                }

            }
        }
    }
    
}