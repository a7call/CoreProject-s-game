using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class TimeLineManager : MonoBehaviour
{
    public PlayableDirector playableDirector { get; private set; }
    public Queue<Transform> pointsToMove = new Queue<Transform>();
    public Transform pointsContainer;
    private Player player;
    public float speed;

    private void Awake()
    {
        foreach (Transform t in pointsContainer.transform)
            pointsToMove.Enqueue(t);

        playableDirector = GetComponent<PlayableDirector>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        SetClipVariable();
    }

    void SetClipVariable()
    {
        var timelineAsset = playableDirector.playableAsset as TimelineAsset;
        var track = timelineAsset.GetOutputTracks().FirstOrDefault(t => t.name == "Rigibody Track") as RigibodyTrack;
        playableDirector.SetGenericBinding(track, player.rb);     

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playableDirector.Play();
        }
    }
}
