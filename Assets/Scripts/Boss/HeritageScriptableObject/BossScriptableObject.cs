using UnityEngine;

[CreateAssetMenu(fileName = "new Boss", menuName = "Boss")]

public class BossScriptableObject : ScriptableObject
{
    public int maxHealth;
    public Material whiteMat;
    public Material defaultMat;

    public float attackRange;
    public float restTime;
    public float timeToSwich;

    public GameObject projectile;
    public GameObject eggProjectile;
    public GameObject eggRunner;
}

