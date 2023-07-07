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
    #endregion
    #region property
    #endregion
    #region method
    public GameObject c;

    private void Start()
    {
        HitScaleSizeSetting();


        //当たり判定ゾーンの頂点にオブジェクトを出して当たり判定ゾーンを可視化する
        Vector3[] vecs = _hitZone.GetHitZoneVertexPositions();

        for (int i = 0; i < vecs.Length; i++)
        {
            Instantiate(c, vecs[i], Quaternion.identity);
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
    /// 貫通や埋まりこみを防ぐための座標を取得する
    /// </summary>
    /// <returns></returns>
    public virtual float PositionAdjustment()
    {
        return _hitZone.Y_Max();
    }
    #endregion
}