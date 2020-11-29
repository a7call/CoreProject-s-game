using UnityEngine;

public class GravityBombModule : ActiveObjects
{
    [SerializeField] private GameObject gravityBomb;
     private bool spawned;
    // Start is called before the first frame update

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            SpawnBomb();
           
            UseModule = false;
        }

        
    }

    private void SpawnBomb()
    {
        spawned = true;
        Instantiate(gravityBomb, transform.position, Quaternion.identity);
    }
}
