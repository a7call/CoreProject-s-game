using System;
using System.Collections;
using UnityEngine;


public class DungeonManager : Singleton<DungeonManager>
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void DoRoomTransition()
    {
        StartCoroutine(TransitionCo());
    }

    private IEnumerator TransitionCo()
    {
        StartRoomTransition();
        yield return new WaitForSeconds(0.5f);
        EndRoomTransition();
    }

    private void StartRoomTransition()
    {
        animator.SetTrigger("StartTransition");
    }

    private void EndRoomTransition()
    {
        animator.SetTrigger("EndTransition");
    }
}
