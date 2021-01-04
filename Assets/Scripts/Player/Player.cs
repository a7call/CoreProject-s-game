using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    public PlayerScriptableObjectScript playerData;
    public EtatJoueur currentEtat ;

    protected Transform HealthContent;
    protected GameObject canvas;

    //[HideInInspector]
    protected Image image1;
    //[HideInInspector]
    protected Image image2;
    //[HideInInspector]
    protected Image image3;
    //[HideInInspector]
    protected Image image4;
    //[HideInInspector]
    protected Sprite emptyHearth;
    //[HideInInspector]
    protected Sprite halfHearth;
    //[HideInInspector]
    protected Sprite fullHearth;

    // Pour le UI de l'armor
    //[HideInInspector]
    protected Image imageArmor;
    //[HideInInspector]
    protected Sprite halfArmor;
    //[HideInInspector]
    protected Sprite fullArmor;

    //public PlayerManager playerManager;

    public enum EtatJoueur
    {
        normal,
        fear,
        shopping,
        Grapping,
        Dashing,
    }

    [HideInInspector]
    public int maxHealth;
    [HideInInspector]
    public int maxStacks;
    [HideInInspector]
    public float mooveSpeed;
    [HideInInspector]
    public float dashForce;
    protected float DashTime;
    [HideInInspector]
    public float stacksReloadTime;

    protected void SetData()
    {
        maxHealth = playerData.maxHealth;
        maxStacks = playerData.maxStacks;
        mooveSpeed = playerData.mooveSpeed;
        dashForce = playerData.dashForce;
        stacksReloadTime = playerData.stacksReloadTime;
        DashTime = playerData.DashTime;

        
        emptyHearth = playerData.emptyHearth;
        halfHearth = playerData.halfHearth;
        fullHearth = playerData.fullHearth;
        imageArmor = playerData.imageArmor;
        halfArmor = playerData.halfArmor;
        fullArmor = playerData.fullArmor;
    }

    protected virtual void Awake()
    {
        SetData();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canvas = GameObject.FindGameObjectWithTag("PlayerCanvas");
        HealthContent = canvas.transform.Find("HealthContent");
        image1 = HealthContent.Find("ImageFirstHP").GetComponent<Image>();
        image2 = HealthContent.Find("ImageSecondHP").GetComponent<Image>();
        image3 = HealthContent.Find("ImageThirdHP").GetComponent<Image>();
        Transform ArmorContent = canvas.transform.Find("ArmorContent");
        imageArmor = ArmorContent.Find("ImageArmor").GetComponent<Image>();
    }
   
}
