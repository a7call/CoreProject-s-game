using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffects : MonoBehaviour
{
    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject smoke;
    [SerializeField] private int maxNumberOfGhost;

    private SpriteRenderer playerSr;
   
    void Start()
    {
        PoolManager.GetInstance().CreatePool(ghost, 10);
        playerSr = GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator DashEffect(float delayBetweenGhosts, Vector2 mouvementVector)
    {
        GameObject currentSmoke = Instantiate(smoke, transform.position, transform.rotation);
        Destroy(currentSmoke, 1f);

        var currentGhostNumber = 0;
        while (currentGhostNumber <= maxNumberOfGhost )
        {
            currentGhostNumber++;
            GameObject currentGhost = PoolManager.GetInstance().ReuseObject(ghost, playerSr.transform.position, transform.rotation);
            var ghostSr = currentGhost.GetComponent<SpriteRenderer>();
            SetUpGhostSpriteRenderer(mouvementVector, ghostSr);
            yield return new WaitForSeconds(delayBetweenGhosts);
        }
    }

    private void SetUpGhostSpriteRenderer(Vector2 mouvementVector, SpriteRenderer ghostSr)
    {
        if (mouvementVector.y < 0)
            ghostSr.sortingOrder = -1;

        ghostSr.sprite = playerSr.sprite;
    }
}
