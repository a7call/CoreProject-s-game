using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public RoomConnectionType roomConnectionType;

    public bool isForeGroundDoor = false;

    Collider2D doorCollider;

    [HideInInspector]
    public Transform pointToSpawn;

    private TimeLineManager timeLineManager;

    public bool isAssigned { get; set; }

    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
        timeLineManager = GetComponentInChildren<TimeLineManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Transition(collision.gameObject));
        }

    }

    IEnumerator Transition(GameObject player)
    {
        DungeonManager.GetInstance().DoRoomTransition();
        yield return new WaitForSeconds(0.5f);
        //timeLineManager.playableDirector.Stop();
        //player.transform.position = pointToSpawn.position;
    }

    public void ManageCollider(bool isTrigger)
    {
        doorCollider.isTrigger = isTrigger; 
    }

    public void AnimateDoors(string trigger)
    {
        var doorAnimator = GetComponent<Animator>();

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger(trigger);
        }
    }
}
