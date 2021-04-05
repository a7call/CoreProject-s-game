using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redbull : StacksObjects
{
    // Variables pour la vitesse de déplacements
    [SerializeField] private float newMoveSpeed = 300f;
    [SerializeField] private float timeMoveSpeed = 10f;

    protected override void Start()
    {
        base.Start();
        // pas cumulable (c'est chiant)
        timeMoveSpeed = cd + 1;
        player = FindObjectOfType<Player>();
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
        float moveSpeed = player.mooveSpeed;
        player.mooveSpeed = newMoveSpeed;
        yield return new WaitForSeconds(timeMoveSpeed);
        player.mooveSpeed = moveSpeed;
    }

}
