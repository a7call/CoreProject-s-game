using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjTest : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 directionTir;
    public GameObject player;
    public GameObject attackPoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attackPoint = GameObject.FindGameObjectWithTag("AttackPointPlayer");
        directionTir = (attackPoint.transform.position - player.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
        Launch(directionTir);
    }

    protected virtual void Launch(Vector3 directionTir)
    {

        transform.Translate(directionTir * 5 * Time.deltaTime, Space.World);

    }



}
