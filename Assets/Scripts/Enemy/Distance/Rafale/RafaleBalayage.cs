using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafaleBalayage : RafaleDistance
{
    [SerializeField] protected int angleTotalBalayage;
    protected int i = 0; //compteur pour le while
    protected float decalage;

    //protected override IEnumerator ShootCO()
    //{

    //    yield return StartCoroutine(intervalleTir());

    //}

    protected override IEnumerator intervalleTir()
    {
        
        decalage = - angleTotalBalayage / 2;
        for (int i = 0; i < nbTir; i++)
        {
            
            isAttacking = true;
            yield return StartCoroutine(ShootBalayage());
            yield return new WaitForSeconds(timeIntervale);
            decalage += angleTotalBalayage / (nbTir - 1); 
            n++;
        }
        n = 0;
        isAttacking = false;
    }

    protected virtual IEnumerator ShootBalayage()
    {


        //decalage +=  Random.Range(-dispersion, dispersion);

        if (attackPoint != null)
        {
            GameObject myProjectile = Instantiate(projetile, attackPoint.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
            Projectile ScriptProj = myProjectile.GetComponent<Projectile>();
            ScriptProj.dispersion = decalage;

        }
        else
        {
            GameObject myProjectile = Instantiate(projetile, transform.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
            Projectile ScriptProj = myProjectile.GetComponent<Projectile>();
            ScriptProj.dispersion = decalage;
        }
        yield return new WaitForEndOfFrame();
    }
}
