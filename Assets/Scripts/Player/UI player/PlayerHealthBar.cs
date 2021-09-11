using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image fillFrontImage;
    public Image fillBackImage;
    public float chipSpeed = 0.005f;
    public void UpdateHealthUI(float health, float maxHealth)
    {
        float fillF = fillFrontImage.fillAmount;
        float fillB = fillBackImage.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            fillFrontImage.fillAmount = hFraction;
            fillBackImage.fillAmount -= chipSpeed;
            if (fillBackImage.fillAmount < hFraction)
            {
                fillBackImage.fillAmount = hFraction;
            }
                
        }
    }

}
