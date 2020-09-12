
using UnityEngine;


[CreateAssetMenu(fileName = "new Type1", menuName = "Type1Enemy")]
public class Type1ScriptableObject : EnemyScriptableObject
{
    public float chargeSpeed;
    public float loadDelay;
    public float restTime;
    public float chargeDelay;
    public float readyToChargeTimer;
}
