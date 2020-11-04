using System.Collections;
using UnityEngine;

public class NuclearExplosionModule : PassiveObjects
{
    [SerializeField] private int explosionDamageMultiplier;
    [SerializeField] public static float nuclearDotTimer;
    [SerializeField] public static int nuclearDotDamage;
    [SerializeField] private int nuclearDotD;
    [SerializeField] private float nuclearDotT;


    private void Awake()
    {
        nuclearDotDamage = nuclearDotD;
        nuclearDotTimer = nuclearDotT;
    }
    private void Start()
    {
        PlayerProjectiles.isNuclearExplosionModule = true;
        PlayerProjectiles.explosionDamageMultiplier = explosionDamageMultiplier;
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
