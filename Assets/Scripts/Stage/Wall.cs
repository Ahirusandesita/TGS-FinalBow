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

    private void Awake()
    {
        ContainObject.walls.Add(this);
    }

    protected override void HitScaleSizeSetting()
    {
        Bounds bounds = GetComponent<Renderer>().bounds;
        Vector3 size = bounds.size;
        //_hitDistanceScale._hitDistanceX = 95.35f;
        //_hitDistanceScale._hitDistanceY = 23.3f;
        //_hitDistanceScale._hitDistanceZ = 96.41f;

        _hitDistanceScale._hitDistanceX = size.x / 2f;
        _hitDistanceScale._hitDistanceY = size.y / 2f;
        _hitDistanceScale._hitDistanceZ = size.z / 2f;

        Vector3 vector = default;
        vector.x = X;
        vector.y = Y;
        vector.z = Z;
        _hitZone = new HitZone(_hitDistanceScale, this.transform.position + vector);
    }
    public override float PositionAdjustment()
    {
        return _hitZone.X_Right();
    }
    public override float PositionAdjustment2()
    {
        return _hitZone.X_Left();
    }

    public override AdjustmentPosint PositionAdjustmentPoint()
    {
        AdjustmentPosint adjustmentPosint = default;
        adjustmentPosint.onePoint = _hitZone.X_Right();
        adjustmentPosint.twoPoint = _hitZone.X_Left();
        return adjustmentPosint;
    }

    #endregion
}