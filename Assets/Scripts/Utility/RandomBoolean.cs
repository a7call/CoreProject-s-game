using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomBoolean
{
    public static bool RandomBool(float trueChance)
    {
        return Random.Range(0f, 100f) <= trueChance;
    }

    public static bool RandomBool()
    {
        return RandomBool(50f);
    }
}
