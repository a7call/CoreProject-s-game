using System.Collections;
using UnityEngine;
/// <summary>
/// Classe héritière de Cac.cs 
/// Elle contient les même fonctions que Cac.cs
/// </summary>
public class BaseCaC : Cac
{   
    protected override void Update()
    {
        StateR.UpdateState();
    }
    public override void DoChasingState()
    {
        isInAttackRange(attackRange);
    }

    public override void DoAttackingState()
    {
        isOutOfAttackRange(attackRange);
    }

    public override void DoPatrollingState()
    {
        isInChasingRange(inSight);
    }
}