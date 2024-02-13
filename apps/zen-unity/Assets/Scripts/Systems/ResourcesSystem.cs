using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Zen
{
    public class ResourcesSystem : Singleton<ResourcesSystem>
    {
        public List<ScriptableUnit> Units { get; private set; }
        private Dictionary<UnitType, ScriptableUnit> _unitsDict;

        protected override void Awake()
        {
            base.Awake();
            AssembleResources();
        }

        private void AssembleResources()
        {
            Units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
            _unitsDict = Units.ToDictionary(x => x.UnitType, x => x);
        }

        public ScriptableUnit GetUnit(UnitType type) => _unitsDict[type];
        public ScriptableUnit GetRandomUnit() => Units[Random.Range(0, Units.Count)];
    }
}
