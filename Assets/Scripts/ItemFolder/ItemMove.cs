// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nishigaki
// --------------------------------------------------------- 
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    #region 変数

    #region　bool一覧

    [HideInInspector]
    // 移動を開始するフラグ　trueなら移動　falseなら停止　使うときにtrueにしてね
    public bool _isStart = false;

    [SerializeField, Tooltip("臨時版の使用の可否")]
    public bool _UseTemporary = true;

    [HideInInspector]
    public bool _endSetting = false;

    public ItemVibration itemVibration;

    #endregion

    #region　Unity変数一覧

    // アイテム自身のtransform
    private Transform _itemTransform = default;

    // ターゲットのtransform
    private Transform _targeterTransform = default;

    // プレイヤーのtransform
    private Transform _playerTransform = default;

    // アイテムから見たターゲットの向き
    private Vector3 _targetVector = default;

    // 追跡するターゲットのオブジェクト
    private GameObject _targeterObject = null;

    #endregion

    #region　float変数一覧　グロ注意

    // 引き寄せる速度の係数　速度は距離に応じて比例的に上昇するためその係数
    private float _attractSpeed = 150f;

    // 開始時の目標地点との直線距離
    private float _startDistance = default;

    // プレイヤーとアイテムの距離
    private float _playerDistance = default;
    #endregion

    #region　float定数一覧

    // 距離による速度加算値の上限値
    private const float SPEED_UP_MAXVARUE = 5f;

    // 距離による速度加算値の上昇係数
    private const float SPEED_UP_COEFFICIENT = 1f;

    // 到達判定距離　プレイヤーとの距離で判定
    private const float CHECK_ALLIVE_DISTANCE = 1f;
    #endregion

    #region クラスの代入用変数

    // ObjectPoolSystemの代入用変数
    private ObjectPoolSystem _poolManager = default;

    // CashObjectInformationの代入用変数
    private CashObjectInformation _itemCash = default;

    // CashObjectInformationの代入用変数
    private CashObjectInformation _targeterCash = default;

    // IFPlayerManagerEnchantParameterの代入用変数
    private IFPlayerManagerEnchantParameter _playerManager = default;

    // ItemStatusの代入用変数
    private ItemStatus _itemStatus = default;

    // BowManager
    private IFBowManager_GetStats _bowManager = default;

    // ScoreManagerの代入用変数
    private IFScoreManager_AllAttract _scoreManager = default;
    #endregion

    #endregion

    private AttractSE _attractSE = default;

    /// <summary>
    /// 初期の代入処理
    /// </summary>
    private void Start()
    {
        // アイテムのTransformの代入
        _itemTransform = this.gameObject.transform;

        // プレイヤーのTransformの代入
        _playerTransform = GameObject.Find("ArrowLookPoint").transform;

        // PoolManagerの代入
        _poolManager = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();

        // ItemのCashObjectInformationの代入
        _itemCash = this.gameObject.GetComponent<CashObjectInformation>();

        // PlayerManagerの代入
        _playerManager = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerManager>();

        // ItemStatusの代入
        _itemStatus = this.gameObject.GetComponent<ItemStatus>();

        // ScoreManagerの代入
        _scoreManager = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreManager>();

        // AttractSEの代入
        _attractSE = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<AttractSE>();

        // IFBowManager_GetStatsの代入
        _bowManager = GameObject.FindGameObjectWithTag("BowController").GetComponent<BowManager>();


    }

    /// <summary>
    /// 移動処理の実行
    /// </summary>
    private void Update()
    {
        // 開始のフラグ管理
        if (_isStart)
        {
            // 本処理と臨時処理の切り替え
            if (!_UseTemporary)
            {
                //移動処理実行
                ItemMovement();
            }
            else
            {
                //臨時
                ItemAttractTemporary();
            }

            /*
            if (itemVibration is null) return;
            itemVibration.canVibration = true;
            float distance = Vector3.Distance(_playerTransform.position, this.transform.position);
            distance = 20 - distance;
            if (distance < 0f)
            {
                distance = 0f;
            }
            itemVibration.vibrateSpeed = distance;
            */

        }

    }

    /// <summary>
    /// <para>移動開始時の設定メソッド</para>
    /// <para>ターゲットの設定やインスタンスと速度の設定などを行う</para>
    /// </summary>
    /// <param name="goalTransform">引き寄せる対象のオブジェクト</param>
    /// <param name="attractPower">引き寄せる力の大きさ</param>
    public void StartSetting()
    {
        _targeterObject = _poolManager.CallObject(PoolEnum.PoolObjectType.targeter, this.transform.position).gameObject;

        _targeterTransform = _targeterObject.transform;

        _targeterObject.GetComponent<TargeterMove>().StartSetting();

        _targeterCash = _targeterObject.GetComponent<CashObjectInformation>();

        _startDistance = Vector3.Distance(_playerTransform.position, _itemTransform.position);

        //設定完了
        _endSetting = true;
    }

    /// <summary>
    /// ターゲットを追従させる挙動メソッド
    /// </summary>
    private void ItemMovement()
    {
        if (!_endSetting)
        {
            StartSetting();
        }

        // ターゲットへのベクトルを取得する
        _targetVector = _targeterTransform.position - _itemTransform.position;

        // 自身をターゲットに向けて移動
        _itemTransform.position += _targetVector.normalized * Mathf.Clamp((_targetVector.magnitude * _attractSpeed * Time.deltaTime) ,-_targetVector.magnitude,_targetVector.magnitude);

        // プレイヤーとアイテムの距離を取得する
        _playerDistance = Vector3.Distance(_playerTransform.position , _itemTransform.position);

        // 距離でサイズ変更
        _itemTransform.localScale = startsize * Mathf.Clamp(_playerDistance / _startDistance , 0.15f , 1f);

        // '追跡するターゲットが目標地点に到達している' かつ '自身がターゲットに追いついている' かの判定
        if (_targetVector.magnitude < CHECK_ALLIVE_DISTANCE && _playerDistance < CHECK_ALLIVE_DISTANCE || !_bowManager.IsHolding)
        {
            // 追跡するターゲットの削除及びリセット
            ReSetAll();
        }
    }

    /// <summary>
    /// 追跡するターゲットの消去メソッド
    /// </summary>
    public void ReSetAll()
    {
        this.transform.localScale = startsize;
        _targeterObject.GetComponent<TargeterMove>().TargeterReSet();
        _poolManager.ReturnObject(_targeterCash);
        _poolManager.ReturnObject(_itemCash);
    }

    /// <summary>
    /// 消去時の初期化
    /// </summary>
    public void OnDisable()
    {
        // 移動処理を行っていた場合
        if (_isStart && _bowManager.IsHolding)
        {
            GameObject.FindObjectOfType<BowVibe>().GetComponent<BowVibe>().InhallVibe();


            // 削除するオブジェクトの属性をプレイヤーに渡す
            _playerManager.SetEnchantParameter(_itemStatus.GetState());

            // ダメージの加算処理とメーターの加算処理メソッド
            _playerManager.ArrowEnchantPlusDamage();

            // スコアの加算処理
            _scoreManager.NormalScore_EnchantScore();
            _scoreManager.BonusScore_AttractBonus();

            // 引き寄せた時のSEを呼ぶ
            _attractSE.PlayAttractSE();
        }


        // スタートフラグの初期化
        _isStart = false;

        // 追跡するターゲットの初期化
        _targeterObject = null;

        //設定フラグの初期化
        _endSetting = false;
    }



    /// <summary>
    /// オブジェクトを引き寄せる速度を設定するプロパティ
    /// てんぽらりーでしか使わない　メインは引数で
    /// </summary>
    /// <param name="set"></param>
    public void SetAttractPower(float set)
    {
        // 引き寄せ速度を設定
        _attractSpeed = set;


        //その他初期設定 後で消す
        MoveSetUp();
    }

    /// <summary>
    /// 目標地点のオブジェクトを設定するメソッド
    /// てんぽらりーでしか使わない　メインは引数で
    /// </summary>
    public Transform SetGoalPosition
    {
        get
        {
            return _goalTransform;
        }

        set
        {
            _goalTransform = value;
        }

    }


    #region 初代版　変数一覧　今は使わない　かも　てんぽらりーで使っている変数一部あり

    [SerializeField]
    //サイズの最小値　引き寄せ時に縮小する下限値
    private const float SIZE_MINIMUM = 0.001f;

    [SerializeField]
    private float _destroyDistance = 1f;

    float _sizeValue = default;

    Vector3 _tmpDistance = default;

    Vector3 startsize = default;

    private Transform _goalTransform = default;

    private float _tmpdif = default;

    private float _tmpStartDif = default;

    #endregion

    #region てんぽらりー
    public void ItemAttractTemporary()
    {
        _attractSpeed = 1000f;
        this.transform.Translate(_tmpDistance * _attractSpeed * Time.deltaTime);
        _tmpdif = _tmpdif - _attractSpeed * Time.deltaTime;

        _sizeValue = Mathf.Clamp(_tmpdif / _tmpStartDif, SIZE_MINIMUM , Mathf.Infinity);

        this.transform.localScale = startsize * _sizeValue;

        if (_tmpdif < _destroyDistance || !_bowManager.IsHolding)
        {

            this.transform.localScale = startsize;
            _poolManager.ReturnObject(_itemCash);
        }
    }
    #endregion

    #region 初代版　初期設定
    /// <summary>
    /// 周回運動の初期設定　最初に一回だけ呼ぶ
    /// </summary>
    protected void MoveSetUp()
    {
        //臨時用
        _tmpDistance = (_goalTransform.position - this.transform.position).normalized;
        _tmpdif = Vector3.Distance(this.transform.position, _goalTransform.position);
        _tmpStartDif = _tmpdif;
        startsize = this.transform.localScale;
    }
    #endregion
}
