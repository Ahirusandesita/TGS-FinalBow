// --------------------------------------------------------- 
// ColliderObjectBase.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

/// <summary>
/// ステージなどの当たり判定ゾーンを作成するための抽象クラス
/// </summary>
public abstract class ColliderObjectBase : MonoBehaviour
{
    #region variable 
    protected HitZone _hitZone;
    protected HitZone.HitDistanceScale _hitDistanceScale = default;

    public float Y = 0f; //25f
    public float X = 0f;
    public float Z = 0f;
    public bool needVertexPoint = false;
    private Transform _myTransform;
    public bool _isDynamic = false;
    public struct AdjustmentPosint
    {
        /// <summary>
        /// Up Right Forward
        /// </summary>
        public float onePoint;
        /// <summary>
        /// Down Left Back
        /// </summary>
        public float twoPoint;
    }

    #endregion
    #region property
    #endregion
    #region method
    public GameObject c;

    private void Start()
    {
        HitScaleSizeSetting();

        //頂点を可視化するかどうか
        if (needVertexPoint)
        {
            //当たり判定ゾーンの頂点にオブジェクトを出して当たり判定ゾーンを可視化する
            Vector3[] vecs = _hitZone.GetHitZoneVertexPositions();

            for (int i = 0; i < vecs.Length; i++)
            {
                Instantiate(c, vecs[i], Quaternion.identity);
            }
        }
        _myTransform = this.transform;

    }

    private void Update()
    {
        if (_isDynamic)
        {
            _hitZone.SetPosition(_myTransform.position);
        }
    }

    /// <summary>
    /// 当たり判定ゾーンを作成する
    /// </summary>
    protected abstract void HitScaleSizeSetting();

    /// <summary>
    /// 当たり判定ゾーンにヒットしているか
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public bool IsHit(Vector3 a)
    {
        if (_hitZone.IsHit(a))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// まだ使ってない当たり判定
    /// </summary>
    /// <param name="mes"></param>
    /// <returns></returns>
    public bool IsHit2(Vector3[] mes)
    {
        if (_hitZone.IsHit2(mes))
        {
            return true;
        }
        return false;
    }

    public bool IsHit3(Vector3 point,Vector3 size)
    {
        if (_hitZone.IsHit3(point, size))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 貫通や埋まりこみを防ぐための座標を取得する
    /// </summary>
    /// <returns></returns>
    public virtual float PositionAdjustment()
    {
        return _hitZone.Y_Up();
    }

    public virtual float PositionAdjustment2()
    {
        return _hitZone.Y_Down();
    }

    public virtual AdjustmentPosint PositionAdjustmentPoint()
    {
        AdjustmentPosint adjustmentPosint = default;
        adjustmentPosint.onePoint = _hitZone.Y_Up();
        adjustmentPosint.twoPoint = _hitZone.Y_Down();
        return adjustmentPosint;
    }

    public HitZone.HitDistanceScale GetDistanceScale()
    {
        return _hitDistanceScale;
    }

    public float PushOutFromColliderX(Vector3 playerPosition)
    {
        if (_hitZone.X_Left() > playerPosition.x)
        {
            return _hitZone.X_Left();
        }

        if (_hitZone.X_Right() < playerPosition.x)
        {
            return _hitZone.X_Right();
        }
        return 0;
    }


    public float PushOutFromColliderY(Vector3 playerPosition)
    {
        if(_hitZone.Y_Up() < playerPosition.y)
        {
            return _hitZone.Y_Up();
        }

        if (_hitZone.Y_Down() > playerPosition.y)
        {
            return _hitZone.Y_Down();
        }
        return 0f;
    }

    public float PushOutFromColliderZ(Vector3 playerPosition)
    {
        if(_hitZone.Z_Back() > playerPosition.z)
        {
            return _hitZone.Z_Back();
        }

        if (_hitZone.Z_Forward() < playerPosition.z)
        {
            return _hitZone.Z_Forward();
        }
        return 0;
    }

    public int GetGameObjectLayer()
    {
        return this.gameObject.layer;
    }
    public string GetGameObjectLayerString()
    {
        return LayerMask.LayerToName(this.gameObject.layer);
    }

    #endregion
}