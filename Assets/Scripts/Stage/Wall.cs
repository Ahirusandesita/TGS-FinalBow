// --------------------------------------------------------- 
// Wall.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

/// <summary>
/// �X�e�[�W�Q�̕ǂɎg�p����N���X
/// </summary>
public class Wall : ColliderObjectBase
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method
    protected override void HitScaleSizeSetting()
    {
        
    }
    public override float PositionAdjustment()
    {
        return _hitZone.X_Right();
    }

    #endregion
}