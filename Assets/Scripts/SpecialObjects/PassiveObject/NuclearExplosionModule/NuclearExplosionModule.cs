using System.Collections;
using UnityEngine;

public class NuclearExplosionModule : PassiveObjects
{
    [SerializeField] private float explosionDamageMultiplier = 0;
    [SerializeField] public static float nuclearDotTimer;
    [SerializeField] public static float nuclearDotDamage;
    [SerializeField] private float nuclearDotD = 0;
    [SerializeField] private float nuclearDotT = 0f;


    private void Awake()
    {
        nuclearDotDamage = nuclearDotD;
        nuclearDotTimer = nuclearDotT;
    }
    public static IEnumerator NuclearDotCo(Enemy enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(nuclearDotTimer);
            if (enemy == null) yield break;
            enemy.TakeDamage(nuclearDotDamage);
        }
    }

}
