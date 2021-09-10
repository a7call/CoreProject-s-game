using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "new Player", menuName = "Player")]
public class PlayerScriptableObjectScript: ScriptableObject
{

    //Data container for Player;

    public int maxHealth;
    public float moveForce;
    public float maxAcceleration;
    public float dashForce;
    public int maxDashNumber;
    public float dashReloadTime;
    public int damage;

    public Image image1;
    //[HideInInspector]
    public Image image2;
    //[HideInInspector]
    public Image image3;
    //[HideInInspector]
    public Image image4;
    //[HideInInspector]
    public Sprite emptyHearth;
    //[HideInInspector]
    public Sprite halfHearth;
    //[HideInInspector]
    public Sprite fullHearth;

    // Pour le UI de l'armor
    //[HideInInspector]
    public Image imageArmor;
    //[HideInInspector]
    public Sprite halfArmor;
    //[HideInInspector]
    public Sprite fullArmor;
}
