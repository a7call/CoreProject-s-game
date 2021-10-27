using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class TimeLineManager : MonoBehaviour
{
    private PlayableDirector playableDirector;
    public Queue<Transform> pointsToMove = new Queue<Transform>();
    public Transform pointsContainer;
    public float distance;
    public GameObject player;
    public float speed;

    private void Awake()
    {
        foreach (Transform t in pointsContainer.transform)
            pointsToMove.Enqueue(t);

        playableDirector = GetComponent<PlayableDirector>();
        SetClipVariable();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            playableDirector.Play();
    }
    void SetClipVariable()
    {
        var timelineAsset = playableDirector.playableAsset as TimelineAsset;
        var track = timelineAsset.GetOutputTracks().FirstOrDefault(t => t.name == "Rigibody Track") as RigibodyTrack;

    }
}
