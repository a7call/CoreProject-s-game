using System.Collections;
using UnityEngine;

public class ImmolationModule : PassiveObjects
{
    [SerializeField] public static float imolationDotTimer = 0f;
    [SerializeField] private  float imolationDotT = 0f;
    [SerializeField] public static int imolationDotDamage;
    [SerializeField] private int imolationDotD = 0;
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
