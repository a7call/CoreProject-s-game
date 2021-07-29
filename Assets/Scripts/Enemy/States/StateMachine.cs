using UnityEditor;
using UnityEngine;


public abstract class StateMachine : MonoBehaviour
{
    protected IState StateR { get; private set; }

    public void SetState(IState state)
    {
        StateR = state;
        StartCoroutine(StateR.StartState());
    }
}
