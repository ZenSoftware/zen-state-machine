using R3;
using UnityEngine;
using UnityEngine.Events;

namespace Zen
{
    public class Unit : StateMachine, IDamageable, IHealable
    {
        #region Declarations
        public ScriptableUnit UnitData;
        public Stats Stats;
        public int CurrentHealth
        {
            get => UnitData.CurrentHealth;
            set { UnitData.CurrentHealth = value; }
        }
        public int MaxHealth { get => UnitData.BaseStats.Health; }
        [field: SerializeField] public bool Healable { get; set; } = true;
        [field: SerializeField] public bool Damagable { get; set; } = true;
        [field: SerializeField] public UnityEvent<GameAction<int>> OnBeforeHealed { get; private set; }
        [field: SerializeField] public UnityEvent<GameAction<int>> OnHealed { get; private set; }
        [field: SerializeField] public UnityEvent<GameAction<int>> OnBeforeDamaged { get; private set; }
        [field: SerializeField] public UnityEvent<GameAction<int>> OnDamaged { get; private set; }
        [field: SerializeField] public UnityEvent<int> OnDied { get; private set; }
        [field: SerializeField] public UnityEvent<int> OnHealthChangedAmount { get; private set; }
        [field: SerializeField] public UnityEvent<int> OnCurrentHealthChanged { get; private set; }
        #endregion

        protected virtual void Awake()
        {
            OnBeforeHealed = new UnityEvent<GameAction<int>>();
            OnHealed = new UnityEvent<GameAction<int>>();
            OnBeforeDamaged = new UnityEvent<GameAction<int>>();
            OnDamaged = new UnityEvent<GameAction<int>>();
            OnDied = new UnityEvent<int>();

            OnHealthChangedAmount = new UnityEvent<int>();
            OnHealed.AsObservable()
                .Select(heal => heal.Value)
                .Merge(OnDamaged.AsObservable().Select(hit => -hit.Value))
                .Where(amount => amount != 0)
                .Subscribe(amount => OnHealthChangedAmount.Invoke(amount))
                .AddTo(this);


            OnCurrentHealthChanged = new UnityEvent<int>();
            OnHealed.AsObservable()
                .Merge(OnDamaged.AsObservable())
                .Where(action => action.Value != 0)
                .Select(_ => CurrentHealth)
                .Subscribe(health => OnCurrentHealthChanged.Invoke(health))
                .AddTo(this);

            if (UnitData != null)
            {
                Stats = UnitData.BaseStats;
                CurrentHealth = Stats.Health;
            }
        }

        public void Heal(GameAction<int> heal)
        {
            if (Healable)
            {
                OnBeforeHealed.Invoke(heal);

                CurrentHealth += heal.Value;

                if (CurrentHealth > MaxHealth)
                {
                    var difference = CurrentHealth - MaxHealth;
                    CurrentHealth = MaxHealth;

                    var actualHeal = heal;
                    actualHeal.Value = heal.Value - difference;

                    OnHealed.Invoke(actualHeal);
                }
                else
                {
                    OnHealed.Invoke(heal);
                }
            }
        }

        public void Damage(GameAction<int> hit)
        {
            if (Damagable)
            {
                OnBeforeDamaged.Invoke(hit);

                CurrentHealth -= hit.Value;

                OnDamaged.Invoke(hit);

                if (CurrentHealth <= 0) Die();
            }
        }

        public virtual void Die()
        {
            OnDied.Invoke(CurrentHealth);
        }
    }
}
