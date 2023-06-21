// --------------------------------------------------------- 
// TargeterSetParent.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TargeterSetParent : MonoBehaviour
{
    #region 変数
    private bool _isStart = false;
    private bool _isTargetArrival = true;
    private float _distance = default;
    private float _moveRadius = default;
    private float _functionTime = default;
    private float _moveValue_x = default;
    private float _moveValue_y = default;
    private float _attractPower = default;
    private const float PERIOD_VALUE = Mathf.PI * 2f;
    private const float COEFFICIENT_DISTANCExRADIUS = 10f;
    private Transform ParentObject = default;
    #endregion
    #region プロパティ

    /// <summary>
    /// 目標地点に到達しているかどうか
    /// </summary>
    public bool IsTargeterArrivel
    {
        get
        {
            return _isTargetArrival;
        }
    }

    /// <summary>
    /// 引き寄せる速度の設定
    /// </summary>
    public float SetAttractPower
    {
        set
        {
            _attractPower = value;
        }
    }

    #endregion
    #region メソッド

    /// <summary>
    /// 初期設定
    /// </summary>
    private void Start()
    {
        // ParentObjectの代入
        ParentObject = GameObject.Find("Player").transform;
    }

    /// <summary>
    /// 移動処理の実行
    /// </summary>
    private void Update()
    {
        // 開始フラグが立っていたら
        if (_isStart)
        {
            // 移動メソッドの呼び出し
            TargeterMovement();
        }
    }

    /// <summary>
    /// 呼び出し時の初期設定
    /// </summary>
    private void OnEnable()
    {
        // 親オブジェクトの設定
        this.transform.parent = ParentObject.transform;

        // 開始時間を設定
        _functionTime = -Time.time;

        // 目標地点とのローカルポジションＺを代入
        _distance = this.transform.localPosition.z;

        // 開始フラグを立てる
        _isStart = true;
    }

    /// <summary>
    /// 削除時の初期化設定
    /// </summary>
    private void OnDisable()
    {
        // 親子関係の解除
        this.transform.parent = null;

        // 開始フラグの初期化
        _isStart = false;

        // 到達フラグの初期化
        _isTargetArrival = true;
    }

    /// <summary>
    /// ターゲットの移動処理
    /// </summary>
    public void TargeterMovement()
    {
        // 到達していないか判定　到達していない間移動　到達したらフラグ変更
        if (_distance > 0)
        {
            // 周回軌道の半径を求める
            _moveRadius = _distance * COEFFICIENT_DISTANCExRADIUS;

            // ＸＹ軸それぞれの位置を求める
            _moveValue_x = Mathf.Sin((_functionTime + Time.time) * PERIOD_VALUE) * _moveRadius;
            _moveValue_y = Mathf.Cos((_functionTime + Time.time) * PERIOD_VALUE) * _moveRadius;

            // 各軸ごとの位置を代入
            this.transform.localPosition = new Vector3( _moveValue_x,   // Ｘ軸
                                                        _moveValue_y,   // Ｙ軸
                                                        _distance   );  // Ｚ軸

            // 距離を縮める
            _distance -= _attractPower * Time.deltaTime;
        }
        else
        {
            // 開始フラグを切る
            _isStart = false;

            // 到達フラグを立てる
            _isTargetArrival = false;
        }
    }
    #endregion
}