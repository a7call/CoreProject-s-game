using UnityEngine;

public class VoidGrenadeModule : ActiveObjects
{
    [SerializeField] private GameObject GrenadeModule;

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
        Instantiate(GrenadeModule, transform.position, Quaternion.identity);
    }
}
