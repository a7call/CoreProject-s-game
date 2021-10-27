using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RigidbodyClip : PlayableAsset
{
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<RigidbodyBehavior>.Create(graph);

        RigidbodyBehavior rigidbodyBehavior = playable.GetBehaviour();
        rigidbodyBehavior.pointsToMoveTo = owner.GetComponent<TimeLineManager>().pointsToMove;
        rigidbodyBehavior.speed = owner.GetComponent<TimeLineManager>().speed;

        return playable;
    }
}
