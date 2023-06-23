// --------------------------------------------------------- 
// StraightMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class StraightMove : EnemyAttackBase
{
    protected override void AttackMove()
    {
        // ZŽ²•ûŒü‚Ö”ò‚Î‚·
        _transform.Translate(Vector3.forward * _attackMoveSpeed * Time.deltaTime);
    }
}