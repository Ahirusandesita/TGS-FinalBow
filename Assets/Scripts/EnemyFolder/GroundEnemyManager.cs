// --------------------------------------------------------- 
// GroundEnemyManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GroundEnemyStats), typeof(CashObjectInformation))]
public class GroundEnemyManager : MonoBehaviour
{
    private GroundEnemyStats _stats = default;
    private GroundEnemyMoveBase _move = default;

    private void Start()
    {
        _stats = this.GetComponent<GroundEnemyStats>();
        _move = this.GetComponent<GroundEnemyMoveBase>();
    }

    private void Update()
    {
        if (_stats.HP <= 0)
        {
            _stats.Death();
        }
        else if (_move._needDespawn)
        {
            _stats.Despawn();
        }
    }
}