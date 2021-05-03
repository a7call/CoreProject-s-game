using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{

    bool IsPoisoned { get; set; }
    bool IsBurned { get; set; }
    int MaxHealth { get; set; }
    int CurrentHealth { get;  set; }
    void TakeDamage(float damage);
}
