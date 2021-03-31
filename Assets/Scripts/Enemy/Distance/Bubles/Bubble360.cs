using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bubble360 : Distance
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
        base.Update();
        switch (currentState)
        {
          
            case State.Chasing:
                isInRange();
                break;
            case State.Attacking:
                isInRange();
                PlayAttackAnim();
                break;
        }
        if (isDying)
        {
           GetComponent<SpriteRenderer>().material.SetColor("_Color", new Color(192, 2, 0, 1));
        }
    }
    #endregion


    #region Shoot

    [SerializeField] protected GameObject rayon;

    [SerializeField] private List<GameObject> differentRadius = new List<GameObject>();
    // Voir Enemy.cs (héritage)
    protected override IEnumerator ShootCO()
    {
        yield return new WaitForEndOfFrame();
        Instantiate(rayon, transform.position, Quaternion.identity);
        AddShoot();
      
    }

    private void AddShoot()                     
    {
        differentRadius.Insert(0,rayon);
    }

    #endregion

    #region Physics
    public override IEnumerator KnockCo(float knockBackForce, Vector3 dir, float knockBackTime, Enemy enemy)
    {
        yield return null;
        // pas de knockBack
    }
    #endregion
}
