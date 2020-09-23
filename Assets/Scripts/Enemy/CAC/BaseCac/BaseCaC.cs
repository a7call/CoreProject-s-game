using System.Collections;
using UnityEngine;

public class BaseCaC : Cac
{

    
    private void Start()
    {
        // Get player référence
        FindPlayer();
        // Set target
        targetPoint = target;
        // Set data
        SetData();
        // Vie iniitial
        SetMaxHealth();
    }


    protected void Update()
    {
        // Check la distance et aggro si à distance
        Aggro();
        // Check la distance et attaque si à distance
        isInRange();
        // récupère la posotion de joueur
        GetPlayerPos();
        // Folllow player ou patrouille si n'attaque pas 
        if(!isInAttackRange)MoveToPath();
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
            isPatroling = false;
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
