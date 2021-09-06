using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimationsBehaviour : StateMachineBehaviour
{
    private string[] hitTriggers = { "Hit1", "Hit2" };

    private void TriggerRandomly(Animator animator)
    {
        System.Random rdn = new System.Random();
        int randomTrigger = rdn.Next(hitTriggers.Length);
        string trigger = hitTriggers[randomTrigger];
        animator.SetTrigger(trigger);
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TriggerRandomly(animator) ;
    }

}
