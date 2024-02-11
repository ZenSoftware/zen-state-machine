using UnityEngine.Events;

namespace Zen
{
    public interface IHealable
    {
        bool Healable { get; set; }
        void Heal(GameAction<int> damage);
        UnityEvent<int> OnCurrentHealthChanged { get; }
        UnityEvent<GameAction<int>> OnBeforeHealed { get; }
        UnityEvent<GameAction<int>> OnHealed { get; }
    }
}

