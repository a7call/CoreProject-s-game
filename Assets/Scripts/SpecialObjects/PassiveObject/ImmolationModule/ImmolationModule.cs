using System.Collections;
using UnityEngine;

public class ImmolationModule : PassiveObjects
{
    [SerializeField] public static float imolationDotTimer;
    [SerializeField] private  float imolationDotT;
    [SerializeField] public static int imolationDotDamage;
    [SerializeField] private int imolationDotD;
    private void Awake()
    {
        imolationDotDamage = imolationDotD;
        imolationDotTimer = imolationDotT;
    }
    private void Start()
    {
        PlayerProjectiles.isImolationModule = true;
    }

    public static IEnumerator ImolationDotCo(Enemy enemy)
    {
        while (true)
        {

            yield return new WaitForSeconds(imolationDotTimer);
            if (enemy == null) yield break;
            enemy.TakeDamage(imolationDotDamage);
        }
    }
}
