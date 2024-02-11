using System.Collections.Generic;
using UnityEngine;

namespace Zen
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        protected State _currentState;

        public void SetState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void InitializeStateMachine(State initialState, Dictionary<State, Dictionary<Transition, State>> states)
        {
            IncludeStates(states);
            SetState(initialState);
        }

        public void IncludeStates(Dictionary<State, Dictionary<Transition, State>> states)
        {
            foreach (var state in states)
            {
                foreach (var transition in state.Value)
                {
                    transition.Key.Callback = () => SetState(transition.Value);
                    state.Key.AddTransition(transition.Key);
                }
            }
        }
    }

    public class StateMachine : StateMachineBase
    {
        private void Update()
        {
            _currentState?.Update();
        }
    }

    public class PhysicsStateMachine : StateMachine
    {
        private void FixedUpdate()
        {
            _currentState?.Update();
        }
    }
}
