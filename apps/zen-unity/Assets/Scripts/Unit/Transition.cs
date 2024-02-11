using System;

namespace Zen
{
    public class Transition
    {
        public Action Callback;
        protected Func<bool> _checkConditionFunc;

        public Transition() { }
        public Transition(Func<bool> checkConditionFunc)
        {
            _checkConditionFunc = checkConditionFunc;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update()
        {
            if (!_checkConditionFunc()) return;

            if (Callback != null)
            {
                Callback.Invoke();
            }
        }
    }
}