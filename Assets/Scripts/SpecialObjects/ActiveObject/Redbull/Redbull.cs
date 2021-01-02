using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redbull : StacksObjects
{

    private PlayerMouvement playerMouvement;
    private PlayerEnergy playerEnergy;

    // Variables pour la vitesse de déplacements
    [SerializeField] private float newMoveSpeed = 300f;
    [SerializeField] private float timeMoveSpeed = 10f;

    protected override void Start()
    {
        playerMouvement = FindObjectOfType<PlayerMouvement>();
        playerEnergy = FindObjectOfType<PlayerEnergy>();
    }

    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            StartCoroutine(MoveCoroutine());
            UseModule = false;
        }
    }

    private IEnumerator MoveCoroutine()
    {
        UseModule = true;
        float moveSpeed = playerMouvement.mooveSpeed;
        playerMouvement.mooveSpeed = newMoveSpeed;
        // A voir avec l'équilibrage
        // playerEnergy.SpendEnergy(0);
        yield return new WaitForSeconds(timeMoveSpeed);
        playerMouvement.mooveSpeed = moveSpeed;
    }

}
