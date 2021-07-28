using UnityEditor;
using UnityEngine;


public interface IState
{
    public abstract void StartState();

    public abstract void UpdateState();
}
