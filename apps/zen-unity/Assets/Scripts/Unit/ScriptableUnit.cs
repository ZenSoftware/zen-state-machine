using System;
using UnityEngine;

namespace Zen
{
    [CreateAssetMenu(fileName = "Unit Scriptable", menuName = "Zen/Unit Scriptable")]
    public class ScriptableUnit : ScriptableObject
    {
        public UnitType UnitType;
        public Unit Prefab;
        public int CurrentHealth;
        public int CurrentEssence;

        [SerializeField]
        private Stats _stats = new()
        {
            Health = 100,
            Essence = 100,
        };

        public Stats BaseStats => _stats;
    }

    [Serializable]
    public struct Stats
    {
        public int Health;
        public int Essence;
    }
}
