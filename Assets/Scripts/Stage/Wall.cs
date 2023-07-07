// --------------------------------------------------------- 
// Wall.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

/// <summary>
/// ステージ２の壁に使用するクラス
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