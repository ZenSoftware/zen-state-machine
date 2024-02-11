using System.Collections;
using UnityEngine;

namespace Zen
{
    public class StandardBlock : Unit
    {
        public override void Die()
        {
            base.Die();
            StartCoroutine(ExecuteDeath());
        }

        IEnumerator ExecuteDeath()
        {
            UnitManager.Instance.SpawnUnit(UnitType.StandardBlock, new Vector3(5, 3, 18));
            Destroy(gameObject);
            yield return null;
        }
    }
}
