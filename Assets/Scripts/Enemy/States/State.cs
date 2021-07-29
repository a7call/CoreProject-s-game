﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public interface IState
{
    public abstract IEnumerator StartState();

    public abstract void UpdateState();
}
