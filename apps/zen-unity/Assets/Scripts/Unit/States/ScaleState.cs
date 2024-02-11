using UnityEngine;
using PrimeTween;

namespace Zen.States
{
    public abstract class ScaleState : State
    {
        protected Transform _transform;
        protected Collider _collider;
        protected ScaleState(Transform transform, Collider collider)
        {
            _transform = transform;
            _collider = collider;
        }
        protected void ScaleTo(Vector3 endValue, Ease ease)
        {
            Tween.Scale(_transform, endValue, 0.3f, ease);
        }
    }

    public class VisibleState : ScaleState
    {
        public VisibleState(Transform transform, Collider collider) : base(transform, collider)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _collider.enabled = true;
            ScaleTo(new Vector3(1, 1, 1), Ease.OutBack);
        }
    }

    public class HiddenState : ScaleState
    {
        public HiddenState(Transform transform, Collider collider) : base(transform, collider)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _collider.enabled = false;
            ScaleTo(new Vector3(0, 0, 0), Ease.InBack);
        }
    }
}