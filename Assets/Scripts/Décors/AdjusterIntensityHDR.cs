using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjusterIntensityHDR : MonoBehaviour
{

    public Material textureMaterial;

    // Start is called before the first frame update
    void Start()
    {
        // textureMaterial = gameObject.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        float intensityValue = 0;
        print(textureMaterial.GetColor("_Color"));
        // textureMaterial.SetVector("_Color", new Vector4(0, 96, 191, intensityValue));
    }
}
