// --------------------------------------------------------- 
// TestAttackMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TestAttackMove : EnemyAttackBase
{
    #region variable 
    
    #endregion
    #region property
    #endregion
    #region method

    #endregion
    public override void AttackMove()
    {
        //攻撃の挙動を作る
        throw new System.NotImplementedException();
    }

    protected override void SetAttackStartTime()
    {
        //十秒後にスタートする
        _attackStartTime = 10f;
    }

}