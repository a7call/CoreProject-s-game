using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreSpawner : MonoBehaviour
{
    
    [SerializeField] protected float chanceToGetCoffre1 = 0.4f;
    [SerializeField] protected float chanceToGetCoffre2 = 0.3f;
    [SerializeField] protected float chanceToGetCoffre3 = 0.2f;
    [SerializeField] protected float chanceToGetCoffre4 = 0.1f;
    protected float chanceToDrop = 0;
    public int nbC1 = 0;
    public int nbC2 = 0;
    public int nbC3 = 0;
    public int nbC4 = 0;
    public int nbC = 0;

    public GameObject Coffre1 = null;
    public GameObject Coffre2 = null;
    public GameObject Coffre3 = null;
    public GameObject Coffre4 = null;


    void Start()
    {
        DropCoffre();
    }

    private void DropCoffre()
    {
        chanceToDrop = Random.Range(0f, 1f);
        nbC++;
        if (chanceToDrop <= chanceToGetCoffre4)
        {
            GameObject.Instantiate(Coffre4, transform.position,Quaternion.identity);
            nbC4++;
            print("Coffre 4");
           
         }
        else if (chanceToDrop > chanceToGetCoffre4 && chanceToDrop <= chanceToGetCoffre2)
        {
            GameObject.Instantiate(Coffre3, transform.position, Quaternion.identity);
            nbC3++;
            print("Coffre 3");
        }
        else if (chanceToDrop > chanceToGetCoffre3 && chanceToDrop <= 1 - chanceToGetCoffre1)
        {
            GameObject.Instantiate(Coffre2, transform.position, Quaternion.identity);
            nbC2++;
            print("Coffre 2");
        }
        else if (chanceToDrop > 1 - chanceToGetCoffre1)
        {
            GameObject.Instantiate(Coffre1, transform.position, Quaternion.identity);
            nbC1++;
            print("Coffre 1");
        }

        Destroy(gameObject);

    }

    
   
}
