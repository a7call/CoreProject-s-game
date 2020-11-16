using System.Collections;
using UnityEngine;

public class ModuleInertie : PassiveObjects
{
    [SerializeField] private float moveSpeedMulti = 0f;
    public static float moveSpeedMultiplier;
    [SerializeField] private float boostDur = 0f;
    public static float boostDuration;
    public static PlayerMouvement player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMouvement>();
    }
    void Start()
    {
        moveSpeedMultiplier = moveSpeedMulti;
        boostDuration = boostDur;
        PlayerMouvement.isModuleInertie = true;      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static IEnumerator InertieCo() 
    {
        float baseMoveSpeed = player.mooveSpeed;
        player.mooveSpeed *= moveSpeedMultiplier;
        yield return new WaitForSeconds(boostDuration);
        player.mooveSpeed = baseMoveSpeed;
    }
}
