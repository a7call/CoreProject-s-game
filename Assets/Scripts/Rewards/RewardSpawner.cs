using UnityEngine;

public class RewardSpawner : MonoBehaviour
{
    [SerializeField] private int minimumCoin = 2;
    [SerializeField] private int maximumCoin = 10;
    [SerializeField] private GameObject coin;



    

    private int MinimumCoins()
    {
        return this.minimumCoin;
    }
    private int MaximumCoins()
    {
        return this.maximumCoin;
    }

    public void RandomCoinReward(GameObject deadEnemy)
    {
        int numberOfCoinToSpawn = Random.Range(this.MinimumCoins(), this.MaximumCoins());
        for(int i=0; i< numberOfCoinToSpawn; i++)
        {
            Instantiate(coin, deadEnemy.transform.position + RandomVector(Vector3.zero, Vector3.one), Quaternion.identity);
        }
    }

    private Vector3 RandomVector( Vector3 min, Vector3 max)
    {
       Vector3 myVector = new Vector3(Random.Range(min.x, max.x),Random.Range(min.y, max.y));
       return myVector;
    }


    [SerializeField] private GameObject key;
    [SerializeField] private float chanceToGetKey;


    private float ChanceToDrop()
    {
        return Random.Range(0f, 1f);
    }
    public void SpawnKeyReward(GameObject deadEnemy)
    {
        if (ChanceToDrop() >= 1 - chanceToGetKey)
        {
            Instantiate(key, deadEnemy.transform.position, Quaternion.identity);
        }
    }


    [SerializeField] private GameObject fullHeart;
    [SerializeField] private GameObject halfHeart;
    [SerializeField] private float chanceToGetHearts;
    [SerializeField] private float chanceToGetFullHeart;


    public void SpawnHeartReward(GameObject deadEnemy)
    {
        if (ChanceToDrop() >= 1 - chanceToGetHearts)
        {
            if (ChanceToDrop() >= 1 - chanceToGetFullHeart)
            {
                Instantiate(fullHeart, deadEnemy.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(halfHeart, deadEnemy.transform.position, Quaternion.identity);
            }
           
        }
    }

    [SerializeField] private GameObject amoCase;
    [SerializeField] private float chanceToGetAmoCase;



    public void SpawnAmoReward(GameObject deadEnemy)
    {
        if (ChanceToDrop() >= 1 - chanceToGetAmoCase)
        {
            Instantiate(amoCase, deadEnemy.transform.position, Quaternion.identity);
        }
    }

}
