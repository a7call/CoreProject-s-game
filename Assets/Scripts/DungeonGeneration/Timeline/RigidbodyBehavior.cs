using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class RigidbodyBehavior : PlayableBehaviour
{
    public Queue<Transform> pointsToMoveTo;
    public float speed;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Rigidbody2D rb = playerData as Rigidbody2D;

        if (pointsToMoveTo.Count == 0)
        {
            // TP Player
            return;
        }
            

        rb.velocity = (pointsToMoveTo.Peek().position - rb.transform.position).normalized * speed;

        if (Vector2.Distance(rb.transform.position, pointsToMoveTo.Peek().position) < 0.5f)
            pointsToMoveTo.Dequeue();

    }
}
