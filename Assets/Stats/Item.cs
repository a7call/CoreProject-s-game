using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.CharacterStats;


    public abstract class Item : ScriptableObject
    {
        public virtual void Equip(Player p) { }
        public virtual void Unequip(Player p){}
    }

