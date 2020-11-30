using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : ActiveObjects
{
    private PlayerEnergy playerEnergy;
    private PlayerMouvement playerMouvement;
    [SerializeField] private float timeUsing = 2f;
    public static bool interrupt = false;

    protected override void Start()
    {
        playerEnergy = FindObjectOfType<PlayerEnergy>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();
    }

    protected override void Update()
    {

        if (ModuleAlreadyUse)
        {
            interrupt = false;
            DestoyEnergyDrink();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UseModule = true;
        }

        if (UseModule)
        {
            UseModule = false;
            StartCoroutine(UseEnergyDring());
        }
    }

    private IEnumerator UseEnergyDring()
    {
        playerMouvement.canDash = false;
        yield return new WaitForSeconds(timeUsing);
        RefillEnergy();
        playerMouvement.canDash = true;
        ModuleAlreadyUse = true;
    }

    private void RefillEnergy()
    {
        playerEnergy.currentEnergy = playerEnergy.maxEnergy;
        playerEnergy.energyIsReloading = false;
        interrupt = true;
    }

    private void DestoyEnergyDrink()
    {
        Destroy(gameObject);
    }

}
