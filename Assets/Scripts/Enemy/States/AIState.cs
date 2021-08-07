using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public abstract class AIState : IState
{
    protected Enemy AICharacter;
    public AIState(Enemy enemy) 
    {
        AICharacter = enemy;
    }

    public abstract IEnumerator EndState();

    public abstract IEnumerator StartState();

    public abstract void UpdateState();
}
