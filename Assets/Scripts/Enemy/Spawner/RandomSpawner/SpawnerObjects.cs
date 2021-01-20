using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObjects : MonoBehaviour
{
    private int[] spawner;
    public Vector3[] sp;
    private int nbSp = 3;
    private int element;
    
    void Start()
    {
        spawner = new int[nbSp];
        sp = new Vector3[nbSp];
        RandomTab();
        TabTransform();
    }

    private void TabTransform()
    {
        for (int i = 0; i < nbSp; i++)
        {
            sp[i] = transform.GetChild(spawner[i]).position;
        }
    }

    private int CalculRandomNumber()
    {
        element = Random.Range(0, transform.childCount);
        return element;
    }
    private void RandomTab()
    {
        for (int i = 0; i < nbSp; i++)
        {
            int myNb = CalculRandomNumber();
            for (int j = 0; j < i; j++)
            {
                if (spawner[j] == myNb)
                {
                    myNb = CalculRandomNumber();
                    j = 0;
                }
            }
            spawner[i] = myNb;
        }
    }
}
