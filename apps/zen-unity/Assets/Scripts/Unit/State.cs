using System.Collections.Generic;

namespace Zen
{
    public abstract class State
    {
        private List<Transition> _transitions = new List<Transition>();
        public void AddTransition(Transition transition) => _transitions.Add(transition);
        public virtual void Enter()
        {
            foreach (var transition in _transitions) transition.Enter();
        }

        public virtual void Exit()
        {
            foreach (var transition in _transitions) transition.Exit();
        }

        public virtual void Update()
        {
            foreach (var transition in _transitions) transition.Update();
        }
    }
}