using System.Collections;
using UnityEditor;
using UnityEngine;
using Wanderer.Utils;

public class SpawningState : AIState
{
    public SpawningState(Enemy enemy) : base(enemy)
    {
        AICharacter = enemy;
    }

    public override IEnumerator EndState()
    {
        yield return null;
    }

    public override IEnumerator StartState()
    {
        AICharacter.AIMouvement.ShouldMove = false;
        yield return ActivateMonster();
        AICharacter.SetState(new PatrollingState(this.AICharacter));
    }

    public override void UpdateState()
    {
    }

    IEnumerator ActivateMonster()
    {
        float spawnAnimationDuration = Utils.GetAnimationClipDurantion(EnemyConst.SPAWN_ANIMATION_NAME, AICharacter.animator);
        var collider = AICharacter.GetComponent<BoxCollider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(spawnAnimationDuration);
        collider.enabled = true;
    }
}
