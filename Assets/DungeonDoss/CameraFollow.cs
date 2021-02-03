using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public Color color1 = Color.black;
    public Color color2 = Color.white;
    public Camera cam;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.Lerp(color1, color2, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y,-10);
    }
}
