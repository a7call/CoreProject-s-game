using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redbull : StacksObjects
{

    private PlayerMouvement playerMouvement;
    // Variables pour la vitesse de déplacements
    [SerializeField] private float newMoveSpeed = 300f;
    [SerializeField] private float timeMoveSpeed = 10f;

    protected override void Start()
    {
        base.Start();
        // pas cumulable (c'est chiant)
        timeMoveSpeed = cd + 1;
        playerMouvement = FindObjectOfType<PlayerMouvement>();
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
        float moveSpeed = playerMouvement.mooveSpeed;
        playerMouvement.mooveSpeed = newMoveSpeed;
        yield return new WaitForSeconds(timeMoveSpeed);
        playerMouvement.mooveSpeed = moveSpeed;
    }

}
