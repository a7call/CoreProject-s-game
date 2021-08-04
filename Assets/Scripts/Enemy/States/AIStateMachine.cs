using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public abstract class AIStateMachine : MonoBehaviour
    {
        protected AIState State { get; private set; }

        public void SetAIState(AIState state)
        {
            State = state;
            State.StartState();
        }
    }
}