using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.CharacterStats;


    public abstract class Item : ScriptableObject
    {
        public virtual void Equip(PlayerMouvement p) { }
        public virtual void Unequip(PlayerMouvement p){}
    }

