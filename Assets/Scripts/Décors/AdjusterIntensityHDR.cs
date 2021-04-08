using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjusterIntensityHDR : MonoBehaviour
{

    public Material textureMaterial;
    [SerializeField] private float intensitySpeed = 1f;
    [SerializeField] private float minIntensity = 5f;
    [SerializeField] private float maxIntensity = 5f;



    // Start is called before the first frame update
    void Start()
    {
        // textureMaterial = gameObject.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        float intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity) + minIntensity;
        textureMaterial.SetFloat("_Intensity", intensity);
    }
}
