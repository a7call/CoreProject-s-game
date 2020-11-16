using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleManager : MonoBehaviour
{
    [SerializeField] private GameObject weapon = null;
    [SerializeField] private GameObject attackPoint = null;
    float angle;

    float CalculateAngle()
    {
        Vector2 dir = (attackPoint.transform.position - weapon.transform.position).normalized;
        angle = Vector2.Angle(Vector2.left, dir);
        if (dir.x > 0 && dir.y > 0 || dir.x < 0 && dir.y > 0)
        {
            angle = -angle;
        }
        return angle;
    }

    void createRotation()
    {
     
            transform.localRotation = Quaternion.Euler(CalculateAngle(), -90, 0);
      

    }
    private void Update()
    {
        createRotation();

    }
}
