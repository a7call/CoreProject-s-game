using System.Collections;
using UnityEngine;


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
        AICharacter.AIMouvement.ShouldMove = false;
        AICharacter.AIMouvement.ShouldSearch = false;
        DisableAllColliders();
        DisableComponents();
        yield return null;
    }

    public override void UpdateState()
    {

    }

    void DisableAllColliders()
    {
        var collider = AICharacter.GetComponent<Collider2D>();
        if (collider.enabled)
        {
            foreach (Transform child in AICharacter.transform)
            {
                if (child.gameObject.GetComponent<Collider2D>())
                {
                    child.gameObject.GetComponent<Collider2D>().enabled = false;
                }
            }
            collider.enabled = false;
        }
    }

    void DisableComponents()
    {
        AICharacter.animator.SetBool(EnemyConst.DEATH_BOOL_CONST, true);
        AICharacter.animator.SetBool(EnemyConst.ATTACK_BOOL_CONST, false);
        AICharacter.GetComponent<SpriteRenderer>().sortingOrder = -1;
        AICharacter.AIMouvement.enabled = false;
        AICharacter.enabled = false;
    }
}
