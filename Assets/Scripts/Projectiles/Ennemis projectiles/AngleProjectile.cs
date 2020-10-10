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
        ConeShoot();


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
        transform.Translate(directionTir * base.speed * Time.deltaTime);
    }

    protected void ConeShoot()
    {
        directionTir = Quaternion.AngleAxis(angleDecalage, Vector3.forward) * dir;
    }





    protected void DirTir()
    {
        //target = GameObject.FindGameObjectWithTag("Player");
        angle1 = Vector3.Angle(Vector3.right, base.dir);
       // angle2 = Vector3.Angle(base.dir, Vector3.up);

        //dist = Vector3.Distance(transform.position, target.transform.position);

        //decalageXl = Mathf.Sin(angleDecalage) * distance;
        //decalageYl = Mathf.Cos(angleDecalage) * distance;

        //decalageX =  decalageXl/ Mathf.Sin(angle1);
        //decalageY = Mathf.Cos(angle2) * decalageYl;

        angleDecalage = Mathf.Deg2Rad*angleDecalage;
        angle1 = Mathf.Deg2Rad * angle1;

        X = distance * Mathf.Cos(angle1 - angleDecalage);
        Y = distance * Mathf.Sin(angle1 - angleDecalage);


        print(distance);
        print(angle1);
        print(angleDecalage);
        //print(dir.x);
        //print(dir.y);
        print(X);
        print(Y);

        print(Mathf.Cos(angle1 - angleDecalage));

        //directionTir.x = X;
        //directionTir.y = Y;

        // directionTir = new Vector3(X,Y,0).normalized;
        

        //print(Quaternion.AngleAxis(angleDecalage, dir));



        //print(directionTir);
        //print(base.dir);
        //transform.rotation.ToAngleAxis(out angleDecalage, out directionTir);
        //print(directionTir);
    }


   
}
