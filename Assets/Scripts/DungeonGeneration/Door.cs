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

    private SpriteRenderer sr;

    public bool isAssigned { get; set; }

    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
        timeLineManager = GetComponentInChildren<TimeLineManager>();
        sr = GetComponent<SpriteRenderer>();
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
        DungeonManager.GetInstance().StartRoomTransition();
        yield return new WaitForSeconds(1f);
        player.transform.position = pointToSpawn.position;
        yield return new WaitForSeconds(0.3f);
        DungeonManager.GetInstance().EndRoomTransition();
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

    public void ManageLayers(bool isClosing)
    {
        if (isForeGroundDoor && isClosing)
            sr.sortingOrder = 10;
        else if (isForeGroundDoor && !isClosing)
            sr.sortingOrder = -1;
    }
}
