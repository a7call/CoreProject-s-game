using System;
using System.Collections;
using UnityEngine;


public class DungeonManager : Singleton<DungeonManager>
{
    Animator animator;

    public RoomObject currentRoom { get; set; }
    Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void DoRoomTransition()
    {
        StartCoroutine(TransitionCo());
    }

    private IEnumerator TransitionCo()
    {
        StartRoomTransition();
        yield return new WaitForSeconds(1f);
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
