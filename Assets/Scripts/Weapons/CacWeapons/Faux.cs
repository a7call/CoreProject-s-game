using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script de l'attaque en arc de cercle arme de type (faux).
/// Le script peut aussi etre ultilisé pour le shotgun
/// et peut être facilement adapté à un script de tourbillons sur soit même !
/// </summary>

public class Faux : CacWeapons
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isCleaving)
        {
            AngleCalcule();
            isCleaving = true;
        }
        if (isCleaving)
        {
            Cleave();
            EnnemiDectectionCleave();
        }
        if (!isCleaving) GetAttackDirection();
    }

    [SerializeField]
    float rotationRadius = 0.5f, angularSpeed = 2f, rotationTime;
    float posX, posY;
    float angle;

    bool isCleaving;

    // rotation de attack point selon l'angle
    private void Cleave()
    {
        posX = transform.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = transform.position.y + Mathf.Sin(angle) * rotationRadius;
        attackPoint.position = new Vector2(posX, posY);

        angle = angle + Time.deltaTime * angularSpeed;

    }


    // calcule de l'angle
    private void AngleCalcule()
    {
        Vector3 dir = (attackPoint.position - transform.position).normalized;
        StartCoroutine(CleavingTime());
        Vector3 faceVector = new Vector3(0, Mathf.Abs(transform.position.y + 1), 0);
        angle = Mathf.Deg2Rad * (Vector3.Angle(dir, faceVector));
        if (dir.x > 0)
        {
            angle = -angle;
        }

    }
    // Coroutine du temps de cleave
    private IEnumerator CleavingTime()
    {
        yield return new WaitForSeconds(rotationTime);
        isCleaving = false;

    }

    // Detecte les ennemis touché pas le cleave ( et leurs fait des trucs)
    void EnnemiDectectionCleave()
    {
        Vector3 dir = (attackPoint.position - transform.position).normalized;
        float distance = (attackPoint.position - transform.position).sqrMagnitude;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, distance, enemyLayer);

        foreach (RaycastHit2D enemy in hits)
        {
            if (enemy.collider != null)
            {
                // Script de vie de l'enemi
            }


        }

    }

}
