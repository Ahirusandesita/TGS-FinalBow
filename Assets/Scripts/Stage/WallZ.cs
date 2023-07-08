// --------------------------------------------------------- 
// WallZ.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class WallZ : ColliderObjectBase
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method
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
        return _hitZone.Z_Forward();
    }
    public override float PositionAdjustment2()
    {
        return _hitZone.Z_Back();
    }

    public override AdjustmentPosint PositionAdjustmentPoint()
    {
        AdjustmentPosint adjustmentPosint = default;
        adjustmentPosint.onePoint = _hitZone.Z_Forward();
        adjustmentPosint.twoPoint = _hitZone.Z_Back();
        return adjustmentPosint;
    }
    #endregion
}