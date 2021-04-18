using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScale : MonoBehaviour
{
    // Scroll main texture based on time

    public GameObject zone;
    public GameObject zoneIns;
    bool isDOne;
    public float time;
  

    void Update()
    {
        test();
    } 

    void  test()
    {
        if (!isDOne)
        {
            
            zoneIns = Instantiate(zone, transform.position, transform.rotation);
         
            
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("tes21");

        if (collision.CompareTag("Finish"))
        {
            isDOne = true;
            print("tes");
        }
        else
        {
            isDOne = false;
        }
    }
}
