using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;


    public void SetMaxEnergy(int _energy)
    {

        slider.maxValue = _energy;
        slider.value = _energy;
    }
    public void SetEnergy(int _energy)
    {
        slider.value = _energy;
    }
}
