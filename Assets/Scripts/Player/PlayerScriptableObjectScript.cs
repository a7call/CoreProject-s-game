using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Player", menuName = "Player")]
public class PlayerScriptableObjectScript: ScriptableObject
{

    //Data container for Player;

    public int maxHealth;
    public int maxEnergy;
    public float mooveSpeed;
    public float dashForce;
    public float attackRadius;
    public int dashEnergyCost;
    public int damage;
    public int energyReloadNumber;
}
