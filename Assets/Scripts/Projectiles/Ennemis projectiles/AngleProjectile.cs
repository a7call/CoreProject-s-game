using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>


public class AngleProjectile : Projectile
{
   [SerializeField] float angleDecalage;
    
    Vector3 directionTir;
    private float decalageXl;
    private float decalageYl;
    private float decalageX;
    private float decalageY;
    private float angle1;
    private float angle2;
    private float X;
    private float Y;

    // private float dist;
    //private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        GetDirection();
        distance = Vector3.Distance(target.position, transform.position);
        DirTir();
        
    }

    // Update is called once per frame
    void Update()
    {
        Lauch();
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        // Damage
        
    }

    protected override void GetDirection()
    {
        base.GetDirection();
        //modifier angle
        
    }

    protected override void Lauch()
    {
        transform.Translate(directionTir.normalized * base.speed * Time.deltaTime);
    }

    protected void DirTir()
    {
        //target = GameObject.FindGameObjectWithTag("Player");
        angle1 = Vector3.Angle(base.dir,Vector3.right);
        angle2 = Vector3.Angle(base.dir, Vector3.up);

        //dist = Vector3.Distance(transform.position, target.transform.position);

        //decalageXl = Mathf.Sin(angleDecalage) * distance;
        //decalageYl = Mathf.Cos(angleDecalage) * distance;

        //decalageX =  decalageXl/ Mathf.Sin(angle1);
        //decalageY = Mathf.Cos(angle2) * decalageYl;
        angleDecalage = Mathf.Rad2Deg*angleDecalage;

        X = distance * Mathf.Cos(angle1 + angleDecalage);
        Y = distance * Mathf.Sin(angle1 + angleDecalage);

        //print(distance);
        print(dir.x);
        print(dir.y);
        print(X);
        print(Y);

        directionTir.x = X;
        directionTir.y = Y;

        //print(Quaternion.AngleAxis(angleDecalage, dir));

        

        //print(directionTir);
        //print(base.dir);
        //transform.rotation.ToAngleAxis(out angleDecalage, out directionTir);
        //print(directionTir);
    }
}
