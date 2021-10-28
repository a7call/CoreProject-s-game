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
        yield return new WaitForSeconds(0.1f);
        StartRoomTransition();
        yield return new WaitForSeconds(1f);
        EndRoomTransition();
    }

    public void StartRoomTransition()
    {
        animator.SetTrigger("StartTransition");
    }

    public void EndRoomTransition()
    {
        animator.SetTrigger("EndTransition");
    }
}
