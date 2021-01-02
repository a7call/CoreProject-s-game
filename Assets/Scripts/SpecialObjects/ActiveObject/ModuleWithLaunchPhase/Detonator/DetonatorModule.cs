using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonatorModule : StacksObjects
{
    [SerializeField] private GameObject distortionGrenade;
    public bool readyToExplode = false ;
    // Start is called before the first frame update

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (UseModule && !readyToExplode)
        {
            StartCoroutine(SpawnBomb());
            UseModule = false;
        }
    }
    protected override IEnumerator WayToReUse()
    {
        if(!readyToExplode) numberOfUse--;
        //check number of use dans détonator
        if (!isOutOfUse)
        {
            yield return new WaitForSeconds(cd);
            readyToUse = true;
        }
    }
    private IEnumerator SpawnBomb()
    {
        
        Instantiate(distortionGrenade, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        readyToExplode = true;

    }
}
