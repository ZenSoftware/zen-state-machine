using UnityEngine;

namespace Zen.Transitions
{
    public abstract class DistanceTransitionBase : Transition
    {
        protected float _maxRadius;
        private Transform _transformA;
        private Transform _transformB;

        protected DistanceTransitionBase(float maxRadius, Transform transformA, Transform transformB)
        {
            _maxRadius = maxRadius;
            _transformA = transformA;
            _transformB = transformB;
        }

        protected float _distance => Vector3.Distance(_transformA.position, _transformB.position);
    }

    public class DistanceGreaterThan : DistanceTransitionBase
    {
        public DistanceGreaterThan(float maxRadius, Transform origin, Transform player)
            : base(maxRadius, origin, player)
        {
            _checkConditionFunc = () => _distance > _maxRadius;
        }
    }

    public class DistanceLessThan : DistanceTransitionBase
    {
        public DistanceLessThan(float maxRadius, Transform origin, Transform player)
            : base(maxRadius, origin, player)
        {
            _checkConditionFunc = () => _distance < _maxRadius;
        }
    }
}