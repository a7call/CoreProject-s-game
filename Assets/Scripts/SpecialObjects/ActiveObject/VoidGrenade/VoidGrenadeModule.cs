using UnityEngine;

public class VoidGrenadeModule : StacksObjects
{
    [SerializeField] private GameObject GrenadeModule;

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
        Instantiate(GrenadeModule, transform.position, Quaternion.identity);
    }
}
