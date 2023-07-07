// --------------------------------------------------------- 
// Floor.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ステージ２の床に使用するクラス
/// </summary>
public class Floor : ColliderObjectBase
{
    #region method

    private void Awake()
    {
        ContainObject.floors.Add(this);
    }

    protected override void HitScaleSizeSetting()
    {

        _hitDistanceScale._hitDistanceX = 95.35f;
        _hitDistanceScale._hitDistanceY = 23.3f;
        _hitDistanceScale._hitDistanceZ = 96.41f;
        Vector3 vector = default;
        vector.y = 25f;
        _hitZone = new HitZone(_hitDistanceScale, this.transform.position + vector);
    }
    #endregion
}