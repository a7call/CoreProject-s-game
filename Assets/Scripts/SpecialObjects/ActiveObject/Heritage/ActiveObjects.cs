using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveObjects : MonoBehaviour
{
    public bool UseModule = false;
    protected bool ModuleAlreadyUse = false;
    [SerializeField] protected bool readyToUse = true;
    public float range;
    protected Vector3 MousePos;
    protected Camera Cam;
    protected Player player;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public void ToUseModule()
    {
        if (readyToUse && player.currentEtat == Player.EtatJoueur.normal)
        {
            UseModule = true;
            readyToUse = false;
            StartCoroutine(WayToReUse());
        }   
    }

    protected virtual IEnumerator WayToReUse()
    {
        // Voir selon l'object
        yield return null;
    }


    public virtual Vector3 GetMousePosition()
    {
        MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = 0;
        return MousePos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
    }

    public Vector3 GetDirection()
    {
        Vector3 direction = (GetMousePosition() - transform.position).normalized;
        return direction;
    }
}
