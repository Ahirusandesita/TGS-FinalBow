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
    private bool _isTargetArrival = false;
    private float _distance = default;
    private float _moveRadius = default;
    private float _timeCount = default;
    private float _moveValue_x = default;
    private float _moveValue_y = default;
    private float _attractPower = default;
    private const float PERIOD_VALUE = Mathf.PI * 2f;
    private const float COEFFICIENT_DISTANCExRADIUS = 10f;
    private Transform ParentObject = default;

    // ObjectPoolSystemの代入用変数
    private ObjectPoolSystem PoolManager = default;

    // CashObjectInformationの代入用変数
    private CashObjectInformation Cash = default;
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
        ParentObject = GameObject.FindGameObjectWithTag("PlayerController").transform;

        // PoolManagerの代入
        PoolManager = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();

        // CashObjectInformationの代入
        Cash = this.GetComponent<CashObjectInformation>();
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
        //スタートの取得前に代入するのを防ぐため
        if(ParentObject != null)
        {
            // 親オブジェクトの設定
            this.transform.parent = ParentObject.transform;
        }

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
        _isTargetArrival = false;

        // 時間の初期化
        _timeCount = default;
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
            _moveValue_x = Mathf.Sin(_timeCount * PERIOD_VALUE) * _moveRadius;
            _moveValue_y = Mathf.Cos(_timeCount * PERIOD_VALUE) * _moveRadius;

            // 各軸ごとの位置を代入
            this.transform.localPosition = new Vector3( _moveValue_x,   // Ｘ軸
                                                        _moveValue_y,   // Ｙ軸
                                                        _distance   );  // Ｚ軸

            // 距離を縮める
            _distance -= _attractPower * Time.deltaTime;

            // 時間経過
            _timeCount += Time.deltaTime;
        }
        else
        {
            print("ローカル　" + this.transform.localPosition) ;
            print("ワールド　" + this.transform.position);
            print("親　" + this.transform.parent.position);
            // 開始フラグを切る
            _isStart = false;

            // 到達フラグを立てる
            _isTargetArrival = true;
        }
    }

    public void ReSetTargeter()
    {
        PoolManager.ReturnObject(Cash);
    }

    #endregion
}