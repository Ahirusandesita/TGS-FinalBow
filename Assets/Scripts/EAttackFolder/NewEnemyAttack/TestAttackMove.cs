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
        //�U���̋��������
        throw new System.NotImplementedException();
    }

    protected override void SetAttackStartTime()
    {
        //�\�b��ɃX�^�[�g����
        _attackStartTime = 10f;
    }

}