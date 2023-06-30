// --------------------------------------------------------- 
// CanDraw_BowManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
[RequireComponent(typeof(BowTransformControl))]
public abstract class CanDraw_BowManager : BowManager
{
    #region variable 

    protected Transform _drawObject = default;

    protected IFBowTransformControl _transformControl = default;

    #region パラメータ

    /// <summary>
    /// 弦掴める弦からの距離
    /// </summary>
    [SerializeField] protected float grapLimitDistance = 0.5f;

    /// <summary>
    /// 弦引ける限界角度
    /// </summary>
    [SerializeField] protected float drawLimitAngle = 90f;

    /// <summary>
    /// 弦引ける限界距離
    /// </summary>
    [SerializeField] protected float drawLimitDistance = 5f;

    /// <summary>
    /// 連射防止
    /// </summary>
    [SerializeField] protected float cantShotDistance = 0.01f;

    /// <summary>
    /// 引いた時にマックスパワーになる距離の最高距離の割合
    /// </summary>
    [SerializeField] protected float drawDistancePercentMaxPower = 0.9f;

    const int BACK = -1;
    #endregion

    #endregion
    #region property
    #endregion
    #region method

    // 初期化処理群
    protected override void Start()
    {


        _transformControl = GetComponent<BowTransformControl>();

        CastInterface();

        _drawObject = _transformControl.GetDrawObjectTransform;

        base.Start();
    }

    // 掴む時に呼ばれる
    protected override void ProcessOfGrapObject()
    {
        // 弦を掴める範囲内なら
        if (grapLimitDistance > Vector3.Distance(_drawObject.position, _transformControl.GetHandPosition))
        {
            BowStringGrap();
        }
    }
    /// <summary>
    /// 弓の弦を掴む処理群
    /// </summary>
    protected virtual void BowStringGrap()
    {

        BowGrapCreateArrow(_drawObject.position);
        _transformControl.GrapSetTransform(_arrow.transform);
    }

    // 弓の弦引く処理群
    protected virtual void BowStringHold()
    {
        // 引いている時のトランスフォームセット
        _transformControl.HoldingSetTransform();

        BowHoldingSetAttract();

        // 円錐範囲外まで引くと強制的に矢をうつ
        if (ConeDecision.ConeInObject(transform, _drawObject, drawLimitAngle, drawLimitDistance, BACK))
        {
            ProcessOfReleaseObjcect();
        }
    }

    /// <summary>
    /// 弓の撃つ処理群
    /// </summary>
    protected virtual void BowShotStart()
    {
        // 矢を撃つ
        BowShotSetting(_transformControl.GetBowFoward());

        // 矢を撃った後のトランスフォーム設定
        _transformControl.AfterShotResetTransform();
    }

    sealed protected override void BowShotArrow(Vector3 shotDirection)
    {
        X_Debug.LogError("as");
        // 連射防止
        if (_transformControl.GetDrawDistance() < cantShotDistance)
        {
            X_Debug.LogError("aaa");
            // キューにいれる(撃たれた瞬間に消滅)
            _poolSystem.ReturnObject(_arrow);
            return;
        }
        else
        {
            
            base.BowShotArrow(shotDirection);

        }
    }

    /// <summary>
    /// 追加行動
    /// </summary>
    protected abstract void AddBowShotProcess();

    /// <summary>
    /// インターフェース型を使用するためいキャストするためのメソッド
    /// ベースがゲットコンポーネント後に呼ばれる
    /// </summary>
    protected abstract void CastInterface();
    #endregion
}