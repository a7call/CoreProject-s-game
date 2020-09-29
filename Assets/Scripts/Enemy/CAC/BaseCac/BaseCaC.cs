using System.Collections;
using UnityEngine;
/// <summary>
/// Classe héritière de Cac.cs 
/// Elle contient les même fonctions que Cac.cs
/// </summary>
public class BaseCaC : Cac
{

    
    private void Start()
    {
        currentState = State.Chasing;
        // Get player référence
        FindPlayer();
        // Set target
        targetPoint = target;
        // Set data
        SetData();
        // Vie initiale
        SetMaxHealth();
    }


    protected void Update()
    {
        switch (currentState) {
        default: 
        case State.Chasing:
                Aggro();
                isInRange();
                MoveToPath();

            break;

        case State.Attacking:
                GetPlayerPos();
                BaseAttack();
                isInRange();
                break;
           
        }

    }

    // Find player to follow
    private void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //Mouvement

    // Override fonction Aggro ( Enemy.cs)  => aggro à l'initialisation
    protected override void Aggro()
    {
            targetPoint = target;
    }


    // Voir Enemy.cs (héritage)
    protected override void SetData()
    {
        base.SetData();
    }




    //Health

    // Voir Enemy.cs (héritage)
    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    // Voir Enemy.cs (héritage)
    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    // Voir Enemy.cs (héritage)
    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();

    }



    //Attack

    // Voir Cac.cs (héritage)
    protected override void isInRange()
    {
        base.isInRange();
    }

    // Voir Cac.cs (héritage)
    protected override void BaseAttack()
    {
        base.BaseAttack();
    }

    // Voir Enemy.cs (héritage)
    protected override void GetPlayerPos()
    {
        base.GetPlayerPos();
    }


}
