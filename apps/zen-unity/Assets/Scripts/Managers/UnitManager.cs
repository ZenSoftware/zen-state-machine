using UnityEngine;

namespace Zen
{
    public class UnitManager : Singleton<UnitManager>
    {
        public void SpawnPlayer()
        {

        }

        public Unit SpawnUnit(UnitType type, Vector3 pos)
        {
            var scriptableUnit = ResourcesSystem.Instance.GetUnit(type);
            var spawned = Instantiate(scriptableUnit.Prefab, pos, Quaternion.identity);
            return spawned;
        }
    }
}
