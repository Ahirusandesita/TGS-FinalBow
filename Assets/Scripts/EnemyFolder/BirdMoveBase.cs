// --------------------------------------------------------- 
// BirdMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 鳥雑魚の挙動の基盤
/// </summary>
public abstract class BirdMoveBase : EnemyMoveBase
{
    #region variable 

    //const float IDLE_MOVE_Z_SPEED = 0f;

    //const int FOR_INFINITY = 2;

    //const int OFFSET_VALUE = 180;

    //const float IDLE_PENDULUM_REVERSE_SPEED = 12f;

    //const int OFFSET_TIME_RANGE = 1000;

    IFCommonEnemyGetParalysis bird = default;

    Animator animator = default;

    //private Vector3 _idleMove = Vector3.zero;

    //private float _offsetReverse = 0f;

    //private float _moveAngle = 0f;

    //private float _time = 0f;
    #endregion
    #region 新move変数
    [Tooltip("移動のスタート位置")]
    protected Vector3 _startPosition = default;

    [Tooltip("移動のゴール位置")]
    protected Vector3 _goalPosition = default;

    [Tooltip("移動スピード")]
    protected float _movementSpeed = default;

    [Tooltip("鳥の動き")]
    protected MoveType _moveType = default;

    [Tooltip("カーブ挙動時の縦軸の移動速度")]
    protected float _moveSpeedArc = default;

    [Tooltip("カーブ挙動時の弧を描く方向")]
    protected ArcMoveDirection _arcMoveDirection = default;


    [Tooltip("自身の敵の種類")]
    private CashObjectInformation _cashObjectInformation = default;

    [Tooltip("子オブジェクトにあるスポナーを取得")]
    private Transform _childSpawner = default;

    [Tooltip("取得したEAttackSpawnerクラス")]
    private EAttackSpawner _eAttackSpawner = default;

    [Tooltip("取得したBirdAttackクラス")]
    private BirdAttack _birdAttack = default;

    [Tooltip("取得したBirdStatsクラス")]
    private BirdStats _birdStats = default;

    [Tooltip("スタートとゴール間の距離 = 目標移動量")]
    private float _startToGoalDistance = default;

    [Tooltip("動いた距離")]
    private float _movedDistance = 0f;

    [Tooltip("プレイヤーの方向を向く速度")]
    private float _rotateSpeed = 100f;

    [Tooltip("移動終了（ゴールに到達）")]
    private bool _isFinishMovement = false;

    [Tooltip("消滅させる（倒せなかった場合）")]
    private bool _needDespawn = false;

    [Tooltip("出現時/消滅時の大きさ")]
    private Vector3 _spawn_And_DespawnSize = default;

    [Tooltip("通常の大きさ")]
    private Vector3 _normalSize = default;

    [Tooltip("Scale変えるときのブレイク")]
    // これがないとループが速すぎて目に見えない
    private WaitForSeconds _changeSizeWait = new WaitForSeconds(0.01f);

    [Tooltip("Scaleの変更が完了")]
    private bool _isCompleteChangeScale = false;

    [Tooltip("出す弾の数")]
    private int _numberOfBullet = default;

    [Tooltip("攻撃の頻度")]
    private float _attackIntervalTime = 2f;

    [Tooltip("現在の経過時間（攻撃までの頻度に使う）")]
    private float _currentTime = 0f;

    [Tooltip("現在の経過時間（再び動き出すまでの時間に使う）")]
    private float _currentTime2 = 0f;

    [Tooltip("行動（ゴール設定）の繰り返しカウント")]
    private int _repeatCount = 0;

    [Tooltip("攻撃方法の種類")]
    private BirdAttackType _birdAttackType = default;

    [Tooltip("攻撃を行うタイミング")]
    private float _attackTiming = default;

    [Tooltip("連続攻撃回数")]
    private int _attackTimes = default;

    [Tooltip("連続攻撃クールタイム")]
    private float _cooldownTime = default;

    [Tooltip("連続攻撃間隔")]
    private float _consecutiveIntervalTime = default;

    [Tooltip("ループする")]
    private bool _needRoop = default;

    [Tooltip("ループ先のゴール番号")]
    private int _goalIndexOfRoop = default;

    [Tooltip("デスポーン時間")]
    private float _despawnTime_s = default;

    [Tooltip("各ステージのスタート地点")]
    private Transform _stageTransform = default;

    [Tooltip("ゴールリスト")]
    private List<Vector3> _goalPositions = new List<Vector3>();

    [Tooltip("ゴール間のスピードリスト")]
    private List<float> _movementSpeeds = new List<float>();

    [Tooltip("再び動き出すまでの時間リスト")]
    private List<float> _reAttackTimes = new List<float>();

    [Tooltip("ゴール間の鳥の動きリスト")]
    private List<MoveType> _moveTypes = new List<MoveType>();

    [Tooltip("カーブ挙動時の縦軸の移動速度リスト")]
    private List<float> _moveSpeedArcs = new();

    [Tooltip("カーブ挙動時の弧を描く方向リスト")]
    private List<ArcMoveDirection> _arcMoveDirections = new();

    [Tooltip("攻撃方法の種類リスト（移動中）")]
    private List<BirdAttackType> _birdAttackTypes_moving = new();

    [Tooltip("攻撃方法の種類リスト（停止中）")]
    private List<BirdAttackType> _birdAttackTypes_stopping = new();

    [Tooltip("攻撃の頻度リスト（移動中）")]
    private List<float> _attackIntervalTimes_moving = new();

    [Tooltip("攻撃の頻度リスト（停止中）")]
    private List<float> _attackIntervalTimes_stopping = new();

    [Tooltip("攻撃を行うタイミングリスト（移動中）")]
    private List<float> _attackTimings_moving = new();

    [Tooltip("攻撃を行うタイミングリスト（停止中）")]
    private List<float> _attackTimings_stopping = new();

    [Tooltip("連続攻撃回数リスト（移動中）")]
    private List<int> _attackTimesList_moving = new();

    [Tooltip("連続攻撃回数リスト（停止中）")]
    private List<int> _attackTimesList_stopping = new();

    [Tooltip("連続攻撃クールタイムリスト（移動中）")]
    private List<float> _cooldownTimeList_moving = new();

    [Tooltip("連続攻撃クールタイムリスト（停止中）")]
    private List<float> _cooldownTimeList_stopping = new();

    [Tooltip("連続攻撃間隔リスト（移動中）")]
    private List<float> _consecutiveIntervalTimes_moving = new();

    [Tooltip("連続攻撃間隔リスト（停止中）")]
    private List<float> _consecutiveIntervalTimes_stopping = new();

    [Tooltip("移動中の向きリスト")]
    private List<DirectionType_AtMoving> _directionTypes_moving = new();

    [Tooltip("停止中の向きリスト")]
    private List<DirectionType_AtStopping> _directionTypes_stopping = new();

    [Tooltip("攻撃の向きリスト")]
    private List<DirectionType_AtAttack> _directionTypes_attack = new();

    private bool _isAttackCompleted_moving = false;
    private bool _isAttackCompleted_stopping = false;
    private Coroutine _activeAttackCoroutine_moving = default;
    private Coroutine _activeAttackCoroutine_stopping = default;
    private WaitForSeconds _waitTimeOfConsecutiveAttack = default;

    private bool _isRotateCompleted_moving = false;
    private Coroutine _activeRotateCoroutine_moving = default;
    private Coroutine _activeRotateCoroutine_stopping = default;
    private Coroutine _activeRotateCoroutine_Attack = default;

    private float _spawnedTime = default;

    #region Rotate変数
    private Vector3 _prevPosition = default;
    private Vector3 _delta = default;
    private readonly Vector3 ZERO = Vector3.zero;
    private readonly Vector3 UP = Vector3.up;
    #endregion

    [Tooltip("Scaleの加算/減算値")]
    private Vector3 CHANGE_SCALE_VALUE = default;

    [Tooltip("正面の角度")]
    private Quaternion _frontAngle = default;

    [Tooltip("指定秒数攻撃の最大数")]
    private const int MAX_ATTACK_TIMES_FOR_SPECIFY_SECONDS = 5;
    #endregion


    #region property
    /// <summary>
    /// 消滅させる（倒せなかった場合）
    /// </summary>
    public bool NeedDespawn
    {
        get
        {
            return _needDespawn;
        }
    }

    /// <summary>
    /// Scaleの変更が完了
    /// </summary>
    public bool IsChangeScaleComplete
    {
        get
        {
            return _isCompleteChangeScale;
        }
    }

    /// <summary>
    /// 出す弾の数
    /// </summary>
    public int NumberOfBullet
    {
        set
        {
            _numberOfBullet = value;
        }
    }

    /// <summary>
    /// ループする
    /// </summary>
    public bool NeedRoop
    {
        set
        {
            _needRoop = value;
        }
    }

    /// <summary>
    /// ループ先のゴール番号
    /// </summary>
    public int GoalIndexOfRooop
    {
        set
        {
            _goalIndexOfRoop = value;
        }
    }

    /// <summary>
    /// デスポーン時間
    /// </summary>
    public float DespawnTime
    {
        set
        {
            _despawnTime_s = value;
        }
    }

    /// <summary>
    /// 攻撃の頻度リスト（移動中）
    /// </summary>
    public float AttackIntervalTimes_Moving
    {
        set
        {
            _attackIntervalTimes_moving.Add(value);
        }
    }

    /// <summary>
    /// 攻撃の頻度リスト（停止中）
    /// </summary>
    public float AttackIntervalTimes_Stopping
    {
        set
        {
            _attackIntervalTimes_stopping.Add(value);
        }
    }

    /// <summary>
    /// ゴールリスト
    /// </summary>
    public Vector3 GoalPositions
    {
        set
        {
            _goalPositions.Add(value);
        }
    }

    /// <summary>
    /// ゴール間のスピードリスト
    /// </summary>
    public float MovementSpeeds
    {
        set
        {
            _movementSpeeds.Add(value);
        }
    }

    /// <summary>
    /// ゴールの停止（攻撃）時間リスト
    /// </summary>
    public float ReAttackTimes
    {
        set
        {
            _reAttackTimes.Add(value);
        }
    }

    /// <summary>
    /// ゴール間の鳥の動きリスト
    /// </summary>
    public MoveType MoveTypes
    {
        set
        {
            _moveTypes.Add(value);
        }
    }

    /// <summary>
    /// カーブ挙動時の縦軸の移動速度リスト
    /// </summary>
    public float MoveSpeedArcs
    {
        set
        {
            _moveSpeedArcs.Add(value);
        }
    }

    /// <summary>
    /// カーブ挙動時の弧を描く方向リスト
    /// </summary>
    public ArcMoveDirection ArcMoveDirections
    {
        set
        {
            _arcMoveDirections.Add(value);
        }
    }

    /// <summary>
    /// 攻撃方法の種類リスト（移動中）
    /// </summary>
    public BirdAttackType BirdAttackTypes_Moving
    {
        set
        {
            _birdAttackTypes_moving.Add(value);
        }
    }

    /// <summary>
    /// 攻撃方法の種類リスト（停止中）
    /// </summary>
    public BirdAttackType BirdAttackTypes_Stopping
    {
        set
        {
            _birdAttackTypes_stopping.Add(value);
        }
    }

    /// <summary>
    /// 攻撃を行うタイミングリスト（移動中）
    /// </summary>
    public float AttackTimings_Moving
    {
        set
        {
            _attackTimings_moving.Add(value);
        }
    }

    /// <summary>
    /// 攻撃を行うタイミングリスト（停止中）
    /// </summary>
    public float AttackTimings_Stopping
    {
        set
        {
            _attackTimings_stopping.Add(value);
        }
    }

    /// <summary>
    /// 連続攻撃回数（移動中）
    /// </summary>
    public int AttackTimes_Moving
    {
        set
        {
            _attackTimesList_moving.Add(value);
        }
    }

    /// <summary>
    /// 連続攻撃回数（停止中）
    /// </summary>
    public int AttackTimes_Stopping
    {
        set
        {
            _attackTimesList_stopping.Add(value);
        }
    }

    /// <summary>
    /// 連続攻撃クールタイム（移動中）
    /// </summary>
    public float CooldownTime_Moving
    {
        set
        {
            _cooldownTimeList_moving.Add(value);
        }
    }

    /// <summary>
    /// 連続攻撃クールタイム（停止中）
    /// </summary>
    public float CooldownTime_Stopping
    {
        set
        {
            _cooldownTimeList_stopping.Add(value);
        }
    }

    /// <summary>
    /// 連続攻撃間隔（移動中）
    /// </summary>
    public float ConsecutiveIntervalTimes_Moving
    {
        set
        {
            _consecutiveIntervalTimes_moving.Add(value);
        }
    }

    /// <summary>
    /// 連続攻撃間隔（停止中）
    /// </summary>
    public float ConsecutiveIntervalTimes_Stopping
    {
        set
        {
            _consecutiveIntervalTimes_stopping.Add(value);
        }
    }

    /// <summary>
    /// 移動中の向きリスト
    /// </summary>
    public DirectionType_AtMoving DirectionTypes_Moving
    {
        set
        {
            _directionTypes_moving.Add(value);
        }
    }

    /// <summary>
    /// 停止中の向きリスト
    /// </summary>
    public DirectionType_AtStopping DirectionTypes_Stopping
    {
        set
        {
            _directionTypes_stopping.Add(value);
        }
    }

    /// <summary>
    /// 攻撃の向きリスト
    /// </summary>
    public DirectionType_AtAttack DirectionTypes_Attack
    {
        set
        {
            _directionTypes_attack.Add(value);
        }
    }

    /// <summary>
    /// 各ステージのスタート地点
    /// </summary>
    public Transform StageTransform
    {
        set
        {
            _stageTransform = value;
        }
    }
    #endregion


    #region method
    protected override void Start()
    {
        _normalSize = _transform.localScale;    // キャッシュ

        // 子オブジェクトの「SpawnPosition」
        _childSpawner = _transform.GetChild(2).transform;
        _eAttackSpawner = _childSpawner.GetComponent<EAttackSpawner>();

        bird = GetComponent<BirdStats>();
        animator = GetComponent<Animator>();
        _cashObjectInformation = this.GetComponent<CashObjectInformation>();
        _birdAttack = GameObject.FindWithTag("EnemyController").GetComponent<BirdAttack>();
        _birdStats = this.GetComponent<BirdStats>();

        base.Start();
    }


    /// <summary>
    /// 鳥雑魚が出現したときの初期化関数
    /// </summary>
    public virtual void BirdEnable()
    {
        // Transform情報の取得
        _startPosition = _transform.position;
        _prevPosition = _transform.position;
        _goalPosition = _goalPositions[_repeatCount];
        CHANGE_SCALE_VALUE = _normalSize / 45f;

        // ステージ正面（ステージ地点の正面の逆数 = 敵にとっての正面）
        _frontAngle = Quaternion.Euler(_stageTransform.forward);
        _eAttackSpawner.StageFrontPosition = -_stageTransform.forward;

        // 変数の初期化
        _startToGoalDistance = (_goalPosition - _startPosition).magnitude;
        _spawn_And_DespawnSize = _normalSize / 5f;
        _movementSpeed = _movementSpeeds[_repeatCount];

        _moveType = _moveTypes[_repeatCount];
        _birdAttackType = _birdAttackTypes_moving[_repeatCount];

        if (_moveType == MoveType.curve)
        {
            _moveSpeedArc = _moveSpeedArcs[_repeatCount];
            _arcMoveDirection = _arcMoveDirections[_repeatCount];
        }

        switch (_birdAttackType)
        {
            case BirdAttackType.equalIntervals:

                _attackIntervalTime = _attackIntervalTimes_moving[5 * _repeatCount];
                break;

            case BirdAttackType.specifySeconds:

                _attackTiming = _attackTimings_moving[_repeatCount];
                break;

            case BirdAttackType.consecutive:

                _attackTimes = _attackTimesList_moving[_repeatCount];
                _cooldownTime = _cooldownTimeList_moving[_repeatCount];
                _attackIntervalTime = _attackIntervalTimes_moving[_repeatCount];
                break;

            default:
                break;
        }

        _needDespawn = false;

        _spawnedTime = Time.time;

        // 方向の初期化
        _transform.LookAt(-_stageTransform.forward);

        // スポーン時に大きくする
        StartCoroutine(LargerAtSpawn());
    }

    private void OnDisable()
    {
        // リストの初期化
        _goalPositions.Clear();
        _movementSpeeds.Clear();
        _reAttackTimes.Clear();
        _moveTypes.Clear();
        _moveSpeedArcs.Clear();
        _arcMoveDirections.Clear();
        _birdAttackTypes_moving.Clear();
        _birdAttackTypes_stopping.Clear();
        _attackIntervalTimes_moving.Clear();
        _attackIntervalTimes_stopping.Clear();
        _attackTimings_moving.Clear();
        _attackTimings_stopping.Clear();
        _attackTimesList_moving.Clear();
        _attackTimesList_stopping.Clear();
        _cooldownTimeList_moving.Clear();
        _cooldownTimeList_stopping.Clear();
        _consecutiveIntervalTimes_moving.Clear();
        _consecutiveIntervalTimes_stopping.Clear();
        _directionTypes_moving.Clear();
        _directionTypes_stopping.Clear();
        _directionTypes_attack.Clear();
    }

    /// <summary>
    /// 麻痺判定
    /// </summary>
    protected bool Paralysing()
    {
        if (bird.Get_isParalysis)
        {
            animator.speed = 0;
            return true;
        }
        else
        {
            animator.speed = 1;
            return false;
        }
    }

    protected override void MoveSequence()
    {
        if (_needDespawn)
            return;

        // デスポーン処理
        if (_needRoop && Time.time > _spawnedTime + _despawnTime_s)
        {
            //-----------------------------------------------------------------
            StartCoroutine(SmallerAtDespawn());// 撤退演出変えたい
            //-----------------------------------------------------------------
        }


        if (_birdStats.HP <= 0)
        {
            if (_activeAttackCoroutine_moving != null)
                StopCoroutine(_activeAttackCoroutine_moving);

            if (_activeAttackCoroutine_stopping != null)
                StopCoroutine(_activeAttackCoroutine_stopping);

            if (_activeRotateCoroutine_Attack != null)
                StopCoroutine(_activeRotateCoroutine_Attack);

            return;
        }

        // 麻痺状態か判定する（麻痺だったら動かない）
        if (Paralysing())
            return;

        // 移動処理（移動が完了していたらこのブロックは無視される）-----------------------------------------------------------

        if (!_isFinishMovement)
        {
            // 移動しながら指定方向に回転する（非同期）
            if (!_isRotateCompleted_moving)
            {
                _activeRotateCoroutine_moving = StartCoroutine(RotateCoroutine_Moving());
                _isRotateCompleted_moving = true;
            }

            // 移動処理
            EachMovement(ref _movedDistance);

            // 移動しながら指定方法で攻撃する（非同期）
            if (!_isAttackCompleted_moving)
            {
                _activeAttackCoroutine_moving = StartCoroutine(AttackCoroutine());
                _isAttackCompleted_moving = true;
            }

            return;
        }
        else
        {
            // 現在動いているコルーチンがあれば停止させる
            if (_activeRotateCoroutine_moving != null)
                StopCoroutine(_activeRotateCoroutine_moving);

            if (_activeAttackCoroutine_moving != null)
                StopCoroutine(_activeAttackCoroutine_moving);

            // 最後の移動が終了したら、デスポーンさせる
            //if (_isLastMove)
            //{
            //    _needDespawn = true;

            //    return;
            //}
        }

        // 攻撃処理-----------------------------------------------------------------------------------

        _currentTime += Time.deltaTime;

        if (!_isAttackCompleted_stopping)
        {
            // 停止後の初期化を行い、指定方法で攻撃を行う（非同期）
            InitializeForAttack();

            // 現在（停止時）のAttackTypeによって呼ぶコルーチンを変える
            if (_birdAttackType == BirdAttackType.none)
            {
                _activeRotateCoroutine_stopping = StartCoroutine(RotateCoroutine_Stopping());
            }
            else
            {
                _activeRotateCoroutine_Attack = StartCoroutine(RotateCoroutine_Attack());
            }

            // 攻撃する（非同期）
            _activeAttackCoroutine_stopping = StartCoroutine(AttackCoroutine());
            _isAttackCompleted_stopping = true;
        }
        //if (_currentTime >= _attackIntervalTime)
        //{
        //    // 攻撃前にプレイヤーの方向を向く
        //    if (_transform.rotation != _childSpawner.rotation)
        //    {
        //        RotateToPlayer();
        //        return;
        //    }

        //    //　攻撃を実行
        //    _birdAttack.NormalAttack(ConversionToBulletType(), _childSpawner, _numberOfBullet);
        //    _currentTime = 0f;
        //}

        _currentTime2 += Time.deltaTime;

        // 再移動のためのリセット処理（攻撃がスタートしてから一定時間後に実行）-----------------------------------------------

        // 攻撃が終了
        if (_currentTime2 >= _reAttackTimes[_repeatCount])
        {
            // 現在動いているコルーチンがあれば停止させる
            if (_activeRotateCoroutine_stopping != null)
                StopCoroutine(_activeRotateCoroutine_stopping);

            if (_activeAttackCoroutine_stopping != null)
                StopCoroutine(_activeAttackCoroutine_stopping);

            // 設定されたゴールの数が1のとき、次の行動が最後
            //if (_goalPositions.Count == 1)
            //{
            //    // 次の移動が最後
            //    _isLastMove = true;

            //    return;
            //}

            // 再移動前に正面を向く
            //if (_transform.rotation != _frontAngle)
            //{
            //    RotateToFront();
            //    return;
            //}

            // 初期化
            InitializeForRe_Movement();

            // すべてのゴールが設定されたら、次の行動が最後
            //if (_repeatCount + 1 >= _goalPositions.Count)
            //{
            //    _isLastMove = true;
            //}

            _currentTime2 = 0f;
        }
    }

    /// <summary>
    /// 各敵の動き
    /// <para>終了条件を変えない場合はbaseを呼ぶ</para>
    /// </summary>
    /// <param name="movedDistance">動いた距離</param>
    protected virtual void EachMovement(ref float movedDistance)
    {
        // 移動が完了したら抜ける（実移動量と目標移動量を比較）
        if (movedDistance >= _startToGoalDistance)
        {
            _isFinishMovement = true;
            movedDistance = 0f;

            return;
        }
    }


    /// <summary>
    /// 2回目以降の移動処理のためのリセット処理
    /// </summary>
    protected virtual void InitializeForRe_Movement()
    {
        _repeatCount++;

        // コルーチン用bool変数の初期化
        _isAttackCompleted_moving = false;
        _isRotateCompleted_moving = false;

        // 行動回数が 設定されたゴールの数を上回ったら、ループまたはデスポーンする
        if (_repeatCount >= _goalPositions.Count)
        {
            // ループする
            if (_needRoop)
            {
                // ループ先のIndexを設定
                _repeatCount = _goalIndexOfRoop;
            }
            // ループしない
            else
            {
                //-----------------------------------------------------------------
                StartCoroutine(SmallerAtDespawn());// 撤退演出変えたい
                //-----------------------------------------------------------------

                return;
            }
        }

        _isFinishMovement = false;
        _isAttackCompleted_stopping = false;

        // スタート位置とゴール位置の再設定
        _startPosition = _transform.position;
        _goalPosition = _goalPositions[_repeatCount];
        _movementSpeed = _movementSpeeds[_repeatCount];
        _startToGoalDistance = (_goalPosition - _startPosition).magnitude;
        _moveType = _moveTypes[_repeatCount];
        _birdAttackType = _birdAttackTypes_moving[_repeatCount];

        if (_moveType == MoveType.curve)
        {
            _moveSpeedArc = _moveSpeedArcs[_repeatCount];
            _arcMoveDirection = _arcMoveDirections[_repeatCount];
        }

        switch (_birdAttackType)
        {
            case BirdAttackType.equalIntervals:

                _attackIntervalTime = _attackIntervalTimes_moving[_repeatCount];
                break;

            case BirdAttackType.specifySeconds:

                _attackTiming = _attackTimings_moving[MAX_ATTACK_TIMES_FOR_SPECIFY_SECONDS * _repeatCount];
                break;

            case BirdAttackType.consecutive:

                _attackTimes = _attackTimesList_moving[_repeatCount];
                _cooldownTime = _cooldownTimeList_moving[_repeatCount];
                _consecutiveIntervalTime = _consecutiveIntervalTimes_moving[_repeatCount];
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// 攻撃用変数の初期化
    /// </summary>
    private void InitializeForAttack()
    {
        _birdAttackType = _birdAttackTypes_stopping[_repeatCount];

        switch (_birdAttackType)
        {
            case BirdAttackType.equalIntervals:

                _attackIntervalTime = _attackIntervalTimes_stopping[_repeatCount];
                break;

            case BirdAttackType.specifySeconds:

                _attackTiming = _attackTimings_stopping[MAX_ATTACK_TIMES_FOR_SPECIFY_SECONDS * _repeatCount];
                break;

            case BirdAttackType.consecutive:

                _attackTimes = _attackTimesList_stopping[_repeatCount];
                _cooldownTime = _cooldownTimeList_stopping[_repeatCount];
                _consecutiveIntervalTime = _consecutiveIntervalTimes_stopping[_repeatCount];
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// 選択された方向へと回転を行う処理（移動中）
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateCoroutine_Moving()
    {
        switch (_directionTypes_moving[_repeatCount])
        {
            // プレイヤーの方向を向く
            case DirectionType_AtMoving.player:
                _eAttackSpawner.AttackDirection = DirectionType_AtAttack.player;

                while (true)
                {
                    RotateToPlayer();
                    yield return null;
                }

            // ワールド正面を向く
            case DirectionType_AtMoving.front:
                _eAttackSpawner.AttackDirection = DirectionType_AtAttack.front;

                while (_transform.rotation != _frontAngle)
                {
                    RotateToFront();
                    yield return null;
                }

                yield break;

            // 進行方向を向く
            case DirectionType_AtMoving.moveDirection:

                while (true)
                {
                    RotateToMoveDirection();
                    yield return null;
                }
        }
    }

    /// <summary>
    /// 選択された方向へと回転を行う処理（停止中）
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateCoroutine_Stopping()
    {
        switch (_directionTypes_stopping[_repeatCount])
        {
            // 現在の方向を継続
            case DirectionType_AtStopping.continuation:
                yield break;

            // プレイヤーの方向を向く
            case DirectionType_AtStopping.player:
                _eAttackSpawner.AttackDirection = DirectionType_AtAttack.player;

                while (true)
                {
                    RotateToPlayer();
                    yield return null;
                }

            // ワールド正面を向く
            case DirectionType_AtStopping.front:
                _eAttackSpawner.AttackDirection = DirectionType_AtAttack.front;

                while (true)
                {
                    RotateToFront();
                    yield return null;
                }
        }
    }

    /// <summary>
    /// 選択された方向へと回転を行う処理（攻撃）
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateCoroutine_Attack()
    {
        _eAttackSpawner.AttackDirection = _directionTypes_attack[_repeatCount];

        // DirectionType_AtAttackの値にかかわらず、スポナーの位置に合わせる
        while (_transform.rotation != _childSpawner.rotation)
        {
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _childSpawner.rotation, Time.deltaTime * _rotateSpeed);
            yield return null;
        }
    }

    /// <summary>
    /// 攻撃コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCoroutine()
    {
        switch (_birdAttackType)
        {
            // 等間隔
            case BirdAttackType.equalIntervals:

                while (true)
                {
                    // 設定された時間間隔で攻撃
                    if (_currentTime >= _attackIntervalTime)
                    {
                        // 攻撃を実行
                        _birdAttack.NormalAttack(ConversionToBulletType(), _childSpawner, _numberOfBullet);
                        _currentTime = 0f;
                    }

                    yield return null;
                }

            // 指定秒数
            case BirdAttackType.specifySeconds:

                _currentTime = 0f;
                int attackCount = 0;

                while (true)
                {
                    // 設定された時間に達したら攻撃
                    if (_currentTime >= _attackTiming)
                    {
                        // 攻撃を実行
                        _birdAttack.NormalAttack(ConversionToBulletType(), _childSpawner, _numberOfBullet);

                        attackCount++;

                        // 次の攻撃タイミングを代入
                        _attackTiming = _attackTimings_stopping[5 * _repeatCount + attackCount];

                        // 最大攻撃数分実行済み OR 設定された秒数が0以下で終了
                        if (attackCount >= MAX_ATTACK_TIMES_FOR_SPECIFY_SECONDS || _attackTiming <= 0f)
                            yield break;

                        _currentTime = 0f;
                    }

                    yield return null;
                }

            // 連続攻撃
            case BirdAttackType.consecutive:

                _waitTimeOfConsecutiveAttack = new WaitForSeconds(_consecutiveIntervalTime);

                while (true)
                {
                    // クールタイムを超えたら攻撃
                    if (_currentTime >= _cooldownTime)
                    {
                        for (int i = 0; i < _attackTimes; i++)
                        {
                            // 攻撃を実行して、設定された時間待機する
                            _birdAttack.NormalAttack(ConversionToBulletType(), _childSpawner, _numberOfBullet);
                            yield return _waitTimeOfConsecutiveAttack;
                        }

                        _currentTime = 0f;
                    }

                    yield return null;
                }

            default:
                yield break;
        }
    }

    /// <summary>
    /// プレイヤーの方向を向く
    /// </summary>
    protected void RotateToPlayer()
    {
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _childSpawner.rotation, Time.deltaTime * _rotateSpeed);
    }

    /// <summary>
    /// ワールド正面を向く
    /// </summary>
    protected void RotateToFront()
    {
        //_transform.rotation = Quaternion.RotateTowards(_transform.rotation, _frontAngle, Time.deltaTime * _rotateSpeed);
        _transform.rotation = Quaternion.LookRotation(-_stageTransform.forward, UP);
    }

    /// <summary>
    /// 進行方向を向く
    /// </summary>
    private void RotateToMoveDirection()
    {
        //_transform.rotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.Euler(_goalPosition - _startPosition), Time.deltaTime * _rotateSpeed);

        _delta = _transform.position - _prevPosition;

        _prevPosition = _transform.position;

        if (_delta == ZERO)
            return;

        _transform.rotation = Quaternion.LookRotation(_delta, UP);
    }

    /// <summary>
    /// 出現時にだんだん大きくなる処理
    /// </summary>
    protected IEnumerator LargerAtSpawn()
    {
        _transform.localScale = _spawn_And_DespawnSize;

        // Scaleが通常サイズより小さい間、大きくする（判定のためにxを用いる）
        while (_transform.localScale.x < _normalSize.x)
        {
            _transform.localScale += CHANGE_SCALE_VALUE;

            yield return _changeSizeWait;
        }

        yield break;
    }

    /// <summary>
    /// 消滅時にだんだん小さくなる処理
    /// </summary>
    public IEnumerator SmallerAtDespawn()
    {
        _needDespawn = true;

        // Scaleが消滅時のサイズより大きい間、小さくする（判定のためにxを用いる）
        while (_transform.localScale.x > _spawn_And_DespawnSize.x)
        {
            _transform.localScale -= CHANGE_SCALE_VALUE;

            yield return _changeSizeWait;
        }

        _birdStats.Despawn();
    }

    /// <summary>
    /// 自身の敵の種類から、対応する弾の種類を返す（enum)
    /// </summary>
    /// <returns></returns>
    protected PoolEnum.PoolObjectType ConversionToBulletType()
    {
        switch (_cashObjectInformation._myObjectType)
        {
            case PoolEnum.PoolObjectType.normalBird:
                return PoolEnum.PoolObjectType.normalBullet;

            case PoolEnum.PoolObjectType.bombBird:
                return PoolEnum.PoolObjectType.bombBullet;

            case PoolEnum.PoolObjectType.penetrateBird:
                return PoolEnum.PoolObjectType.penetrateBullet;

            case PoolEnum.PoolObjectType.thunderBird:
                return PoolEnum.PoolObjectType.thunderBullet;

            case PoolEnum.PoolObjectType.bombBirdBig:
                return PoolEnum.PoolObjectType.bombBullet;

            case PoolEnum.PoolObjectType.thunderBirdBig:
                return PoolEnum.PoolObjectType.thunderBullet;

            case PoolEnum.PoolObjectType.penetrateBirdBig:
                return PoolEnum.PoolObjectType.penetrateBullet;
        }

        // 例外処理
        return PoolEnum.PoolObjectType.normalBullet;
    }


    /// <summary>
    /// いつもの動き
    /// </summary>
    //private void IdleMove()
    //{
    //    animator.speed = 1;

    //    _time += Time.deltaTime * _changeAngleSpeed;

    //    _idleMove = _idleMoveSpeedY * Mathf.Sin(_time) * Vector3.up;

    //    transform.Translate(_idleMove * Time.deltaTime);
    //}

    //private void InfinityMode()
    //{
    //    _moveAngle += Time.deltaTime * _changeAngleSpeed;

    //    _idleMove = new Vector3(
    //        Mathf.Sin(_moveAngle) * _idleMoveSpeedY,
    //        Mathf.Sin((_moveAngle + OFFSET_VALUE) * FOR_INFINITY) * _idleMoveSpeedY,
    //        0);

    //    transform.Translate(Time.deltaTime * _idleMove);
    //}

    //private void Test()
    //{
    //    // 左右の移動
    //    _idleMove = Vector3.right * (Mathf.Sin((Time.time + _offsetReverse)
    //        * IDLE_PENDULUM_REVERSE_SPEED) * _idleMoveSpeedY * Time.deltaTime);

    //    // 正面の移動
    //    _idleMove += IDLE_MOVE_Z_SPEED * Time.deltaTime * Vector3.forward;

    //    _transform.Translate(_idleMove);
    //}
    #endregion
}