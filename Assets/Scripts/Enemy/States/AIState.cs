using UnityEditor;
using UnityEngine;


public abstract class AIState : IState
{
    protected Enemy AICharacter;
    public AIState(Enemy enemy) 
    {
        AICharacter = enemy;
    }

    public abstract void StartState();

    public abstract void UpdateState();
}
