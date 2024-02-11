using UnityEngine;

namespace Zen.Transitions
{
    public abstract class DistanceTransitionBase : Transition
    {
        private Transform _transformA;
        private Transform _transformB;
        protected float _maxRadius;

        protected DistanceTransitionBase(float maxRadius, Transform transformA, Transform transformB)
        {
            _transformA = transformA;
            _maxRadius = maxRadius;
            _transformB = transformB;
        }

        protected float _distance => Vector3.Distance(_transformA.position, _transformB.position);
    }

    public class DistanceGreaterThan : DistanceTransitionBase
    {
        public DistanceGreaterThan(float maxRadius, Transform origin, Transform playerTransform) : base(maxRadius, origin, playerTransform)
        {
            _checkConditionFunc = () => _distance > _maxRadius;
        }
    }

    public class DistanceLessThan : DistanceTransitionBase
    {
        public DistanceLessThan(float maxRadius, Transform origin, Transform playerTransform) : base(maxRadius, origin, playerTransform)
        {
            _checkConditionFunc = () => _distance < _maxRadius;
        }
    }
}