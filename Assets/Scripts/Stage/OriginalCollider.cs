// --------------------------------------------------------- 
// Floor.cs 
// 
// CreateDay: 2023/07/14
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ステージ２の床に使用するクラス
/// </summary>
public class OriginalCollider : ColliderObjectBase
{
    #region method

    private void Awake()
    {
        //自クラスをAddする　同期の問題を解決するLockはまだ
        ContainObject.originalColliders.Add(this);
    }

    protected override void HitScaleSizeSetting()
    {

        //レンダラーに合わせたコライダーを作成する
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

        //コライダーをインスタンス
        _hitZone = new HitZone(_hitDistanceScale, this.transform.position + vector);
    }
    #endregion
}