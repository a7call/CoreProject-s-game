using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BubbleMS : Distance
{

    #region Unity Mono
    protected override void Awake()
    {
        base.Awake();
        SetData();
        SetMaxHealth();
    }

    protected override void Update()
    {
        
    }
    #endregion

    #region DeathObject
    public GameObject deathObject;
    protected override void DestroyEnemy()
    {
        base.DestroyEnemy();
        if (isDying)
        {
           Instantiate(deathObject, transform.position, Quaternion.identity);
        }
    }
    #endregion

    #region Physics
    public override IEnumerator KnockCo(float knockBackForce, Vector3 dir, float knockBackTime, Enemy enemy)
    {
        yield return null;
        // pas de knockBack
    }
    #endregion

    #region Shoot
    private bool firstShoot = true;
    protected override IEnumerator CanShootCO()
    {
        if (isReadytoShoot && firstShoot)
        {
            isReadytoShoot = false;
            firstShoot = false;
            yield return StartCoroutine(ShootCO());
            yield return new WaitForSeconds(restTime);
        }
    }

    // Voir Enemy.cs (héritage)
    protected override IEnumerator ShootCO()
    {
       
        GameObject monRayon = Instantiate(rayon, transform.position, Quaternion.identity);
        monRayon.transform.parent = gameObject.transform;
        AddShoot();
        yield return new WaitForEndOfFrame();
    }
    #endregion

    #region Rayon
    [SerializeField] protected GameObject rayon;

    [SerializeField] private List<GameObject> differentRadius = new List<GameObject>();
    private void AddShoot()
    {
        differentRadius.Insert(0, rayon);
    }

    private void EnabledRayon()
    {
        if (ParasiteIdol.parasiteIdolFear)
        {
            if(gameObject.transform.GetChild(1) != null) 
            {
                // gameObject.transform.GetChild(1).GetComponent<RadiusGrowUp>().enabled = false;
                GameObject myRayon = GameObject.FindGameObjectWithTag("RayonMS");
                Destroy(myRayon);
            }
            ParasiteIdol.parasiteIdolFear = false;
            firstShoot = true;
            isReadytoShoot = true;
        }
    }
    #endregion

}
