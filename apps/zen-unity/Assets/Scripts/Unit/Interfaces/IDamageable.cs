using UnityEngine.Events;

namespace Zen
{
    public interface IDamageable
    {
        bool Damagable { get; set; }
        void Damage(GameAction<int> damage);
        UnityEvent<int> OnCurrentHealthChanged { get; }
        UnityEvent<GameAction<int>> OnBeforeDamaged { get; }
        UnityEvent<GameAction<int>> OnDamaged { get; }
        UnityEvent<int> OnDied { get; }
    }
}