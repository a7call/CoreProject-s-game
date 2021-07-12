using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorObj : MonoBehaviour
{
    // Start is called before the first frame update
    Animator doorAnimator;
    Collider2D DoorCollider;
    public bool isForeGroundDoor = false;

    [HideInInspector]
    public SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingLayerName = "BackGround";
        sr.sortingOrder = 100;
        doorAnimator = GetComponent<Animator>();
        DoorCollider = GetComponent<Collider2D>();
        DoorCollider.enabled = false;
    }
    public void AnimateDoors(string trigger)
    {
        var doorAnimator = GetComponent<Animator>();
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger(trigger);
        }
    }

    public void ManageCollider(bool isenabled)
    {
        DoorCollider.enabled = isenabled;
    }
}
