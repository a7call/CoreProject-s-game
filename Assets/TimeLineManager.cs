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
    private Collider2D collider2d;
    public float speed;
    public bool isEnter = false;

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        collider2d = GetComponent<Collider2D>();

        foreach (Transform t in pointsContainer.transform)
            pointsToMove.Enqueue(t);

        SetClipVariable();
    }

    void SetClipVariable()
    {
        var timelineAsset = playableDirector.playableAsset as TimelineAsset;
        var rbTrack = timelineAsset.GetOutputTracks().FirstOrDefault(t => t.name == "Rigibody Track") as RigibodyTrack;
        playableDirector.SetGenericBinding(rbTrack, player.rb);     


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().currentState = PlayerState.NotInControl;
            StartCoroutine(CheckTimelinePlaying(collision));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") )
        {
            collider2d.enabled = false;
            if (!isEnter)
                playableDirector.Stop();
        }
    }

    IEnumerator CheckTimelinePlaying(Collider2D collision)
    {
        playableDirector.Play();
        if (isEnter)
        {
            while (playableDirector.state == PlayState.Playing)
                yield return null;
            playableDirector.Stop();
            collision.GetComponent<Player>().currentState = PlayerState.Normal;
        }
    }
}
