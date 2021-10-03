using System.Collections;
using UnityEngine;
using Wanderer.Utils;


public class DeathState : AIState
{
    public DeathState(Enemy enemy) : base(enemy)
    {
    }
    public override IEnumerator EndState()
    {
        yield return null;
    }

    public override IEnumerator StartState()
    {
        yield return null;
        AICharacter.PlayDeathEffect();
        AICharacter.sr.material = AICharacter.BaseMaterial;
        AICharacter.EnemyDeath(AICharacter.gameObject);
        AICharacter.AIMouvement.ShouldMove = false;
        AICharacter.AIMouvement.ShouldSearch = false;
        DisableAllColliders();
        DisableComponents();
        DestroyChildren(AICharacter.transform);      
    }

    public override void UpdateState()
    {

    }

    void DisableAllColliders()
    {
        var collider = AICharacter.GetComponent<Collider2D>();
        collider.enabled = false;
        //if (collider.enabled)
        //{
        //    foreach (Transform child in AICharacter.transform)
        //    {
        //        if (child.gameObject.GetComponent<Collider2D>())
        //        {
        //            child.gameObject.GetComponent<Collider2D>().enabled = false;
        //        }
        //    }
           
        //}
    }

    void DisableComponents()
    {

        AICharacter.animator.SetBool(EnemyConst.DEATH_BOOL_CONST, true);
        AICharacter.animator.SetBool(EnemyConst.ATTACK_BOOL_CONST, false);
        AICharacter.GetComponent<SpriteRenderer>().sortingOrder = -1;
        AICharacter.AIMouvement.enabled = false;
        AICharacter.enabled = false;
    }

    void DestroyChildren(Transform transform)
    {
        foreach (Transform t in transform)
        {
            if (t.GetComponent<Collider2D>())
                continue;

            GameObject.Destroy(t.gameObject);
        }
    }


}
