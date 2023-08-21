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
    [SerializeField] float _idleMoveSpeedX = 10f;

    [SerializeField] float _idleMoveSpeedY = 10f;

    [SerializeField] float _changeAngleSpeed = 1f;

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

    [Tooltip("取得したBirdAttackクラス")]
    private BirdAttack _birdAttack = default;

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

    [Tooltip("次の移動完了で処理終了")]
    private bool _isLastMove = false;

    [Tooltip("行動（ゴール設定）の繰り返しカウント")]
    private int _repeatCount = 0;

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


    [Tooltip("Scaleの加算/減算値")]
    private readonly Vector3 CHANGE_SCALE_VALUE = new Vector3(0.05f, 0.05f, 0.05f);   // 少しずつ変わる

    [Tooltip("正面の角度")]
    private readonly Quaternion FRONT_ANGLE = Quaternion.Euler(new Vector3(0f, 180f, 0f));
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
    /// 攻撃の頻度
    /// </summary>
    public float AttackIntervalTime
    {
        set
        {
            _attackIntervalTime = value;
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
    #endregion


    #region method

    //private void Awake()
    //{
    //    _offsetReverse = UnityEngine.Random.Range(0, OFFSET_TIME_RANGE);
    //}

    protected override void Start()
    {
        base.Start();

        // Transform情報の取得
        _startPosition = _transform.position;
        _goalPosition = _goalPositions[_repeatCount];
        _normalSize = _transform.localScale;    // キャッシュ

        // 変数の初期化
        _startToGoalDistance = (_goalPosition - _startPosition).magnitude;
        _spawn_And_DespawnSize = _normalSize / 5f;
        _movementSpeed = _movementSpeeds[_repeatCount];
        //_moveType = _moveTypes[_repeatCount];


        bird = GetComponent<BirdStats>();

        animator = GetComponent<Animator>();

        // 子オブジェクトの「SpawnPosition」
        _childSpawner = _transform.GetChild(2).transform;

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        _birdAttack = GameObject.FindWithTag("EnemyController").GetComponent<BirdAttack>();


        // スポーン時に大きくする
        StartCoroutine(LargerAtSpawn());
    }


    /// <summary>
    /// 麻痺判定
    /// </summary>
    protected void Paralysing()
    {
        if (bird.Get_isParalysis)
        {
            animator.speed = 0;
            return;
        }
        else
        {
            animator.speed = 1;
        }
    }

    protected override void MoveSequence()
    {
        // 麻痺状態か判定する（麻痺だったら動かない）
        Paralysing();

        _currentTime += Time.deltaTime;

        // 移動処理（移動が完了していたらこのブロックは無視される）-----------------------------------------------------------

        if (!_isFinishMovement)
        {
            // 移動処理
            EachMovement(ref _movedDistance);

            return;
        }
        else
        {
            // 最後の移動が終了したら、デスポーンさせる
            if (_isLastMove)
            {
                _needDespawn = true;

                return;
            }
        }

        // 攻撃処理（一定間隔で実行される）-----------------------------------------------------------------------------------

        if (_currentTime >= _attackIntervalTime)
        {
            // 攻撃前にプレイヤーの方向を向く
            if (_transform.rotation != _childSpawner.rotation)
            {
                RotateToPlayer();
                return;
            }

            //　攻撃を実行
            _birdAttack.NormalAttack(ConversionToBulletType(), _childSpawner, _numberOfBullet);
            _currentTime = 0f;
        }

        _currentTime2 += Time.deltaTime;

        // 再移動のためのリセット処理（攻撃がスタートしてから一定時間後に実行）-----------------------------------------------

        // 攻撃が終了
        if (_currentTime2 >= _reAttackTimes[_repeatCount])
        {
            // 設定されたゴールの数が1のとき、次の行動が最後
            if (_goalPositions.Count == 1)
            {
                // 次の移動が最後
                _isLastMove = true;

                return;
            }

            // 再移動前に正面を向く
            if (_transform.rotation != FRONT_ANGLE)
            {
                RotateToFront();
                return;
            }

            // 初期化
            InitializeForRe_Movement();

            // すべてのゴールが設定されたら、次の行動が最後
            if (_repeatCount + 1 >= _goalPositions.Count)
            {
                _isLastMove = true;
            }

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
            X_Debug.Log("鳥の移動完了");
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
        _isFinishMovement = false;
        _repeatCount++;

        // スタート位置とゴール位置の再設定
        _startPosition = _transform.position;
        _goalPosition = _goalPositions[_repeatCount];
        _movementSpeed = _movementSpeeds[_repeatCount];
        _startToGoalDistance = (_goalPosition - _startPosition).magnitude;
        _moveType = _moveTypes[_repeatCount];

        if (_moveType == MoveType.curve)
        {
            _moveSpeedArc = _moveSpeedArcs[_repeatCount];
            _arcMoveDirection = _arcMoveDirections[_repeatCount];
        }
    }

    /// <summary>
    /// プレイヤーの方向を向く（Updateで呼ぶ）
    /// </summary>
    protected void RotateToPlayer()
    {
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _childSpawner.rotation, Time.deltaTime * _rotateSpeed);
    }

    /// <summary>
    /// ワールド正面を向く（Updateで呼ぶ）
    /// </summary>
    protected void RotateToFront()
    {
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, FRONT_ANGLE, Time.deltaTime * _rotateSpeed);
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
        // Scaleが消滅時のサイズより大きい間、小さくする（判定のためにxを用いる）
        while (_transform.localScale.x > _spawn_And_DespawnSize.x)
        {
            _transform.localScale -= CHANGE_SCALE_VALUE;

            yield return _changeSizeWait;
        }

        _isCompleteChangeScale = true;

        yield break;
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