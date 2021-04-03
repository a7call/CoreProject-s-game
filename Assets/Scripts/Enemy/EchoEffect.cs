using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float timeBtwSpwans;
    public float startTimeBtwSpawn;
    private float animationFlaqueTime = 5.20f;

    public GameObject Echo;

    private void Update()
    {
        if(timeBtwSpwans <= 0)
        {
            GameObject instance = (GameObject)Instantiate(Echo, transform.position, Quaternion.identity);
            Destroy(instance, animationFlaqueTime);
            timeBtwSpwans = startTimeBtwSpawn; 
        } else
        {
            timeBtwSpwans -= Time.deltaTime;
        }
    }

}
