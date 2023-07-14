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

    private void Start()
    {
        _stats = this.GetComponent<GroundEnemyStats>();
    }

    private void Update()
    {
        if (_stats.HP <= 0)
        {
            _stats.Death();
        }
    }
}