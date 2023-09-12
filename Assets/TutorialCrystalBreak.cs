using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCrystalBreak : MonoBehaviour
{
    private EnemyGimmickStats _stats;

    private void Awake()
    {
        _stats = this.GetComponentInChildren<EnemyGimmickStats>();
    }

    public IEnumerator Break()
    {
        yield return new WaitForSeconds(0.5f);

        _stats.TakeDamage(1);
    }
}
