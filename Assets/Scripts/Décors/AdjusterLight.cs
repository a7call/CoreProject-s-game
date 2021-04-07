using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AdjusterLight : MonoBehaviour
{

    public Light2D myLight;

    // Intensity variables
    [SerializeField] private bool isChangeIntensity = false;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float intensitySpeed;

    // Color variables
    [SerializeField] private bool isChangeColor = false;
    [SerializeField] private float colorSpeed;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    private float startTime;


    // Start is called before the first frame update
    void Start()
    {
        myLight = gameObject.GetComponent<Light2D>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChangeIntensity)
        {
            myLight.intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity);
        }

        if (isChangeColor)
        {
            float t = (Mathf.Sin(Time.time - startTime * colorSpeed));
            myLight.color = Color.Lerp(startColor, endColor, t);
        }
    }
}
