// --------------------------------------------------------- 
// BirdMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using System;
using System.Collections;
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

    [Tooltip("移動のスピード")]
    protected float _movementSpeed = default;


    [Tooltip("自身の敵の種類")]
    private CashObjectInformation _cashObjectInformation = default;

    [Tooltip("取得したStageManager")]
    private StageManager _stageManager = default;

    [Tooltip("子オブジェクトにあるスポナーを取得")]
    private Transform _childSpawner = default;

    [Tooltip("取得したBirdAttackクラス")]
    private BirdAttack _birdAttack = default;

    [Tooltip("スタートとゴール間の距離 = 目標移動量")]
    private float _startToGoalDistance = default;

    [Tooltip("動いた距離")]
    private float _movedDistance = default;

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

    [Tooltip("このインスタンスのインデックス")]
    // ex) 同ウェーブに敵が2体いる場合、Instance_0 = 0, Instance_1 = 1が設定される
    private int _thisInstanceIndex = default;

    [Tooltip("どのウェーブでスポーンしたか")]
    private WaveType _spawnedWave = default;

    [Tooltip("設定されたゴールの数")]
    private int _numberOfGoal = default;

    [Tooltip("再び動き出すまでの時間")]
    private float _reAttackTime = 10f;

    [Tooltip("出す弾の数")]
    private int _numberOfBullet = default;

    [Tooltip("攻撃の頻度")]
    private float _attackIntervalTime = 2f;

    [Tooltip("現在の経過時間（攻撃までの頻度に使う）")]
    private float _currentTime = 0f;

    [Tooltip("現在の経過時間（再び動き出すまでの時間に使う）")]
    private float _currentTime2 = 0f;

    [Tooltip("次の移動完了で処理終了")]
    private bool _isLastMove = default;

    [Tooltip("何回目のゴール設定か")]
    private int _howTimesSetGoal = 1;  // 初期値1


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
    /// このインスタンスのインデックス
    /// </summary>
    public int ThisInstanceIndex
    {
        set
        {
            _thisInstanceIndex = value;
        }
    }

    /// <summary>
    /// どのウェーブでスポーンしたか
    /// </summary>
    public WaveType SpawnedWave
    {
        set
        {
            _spawnedWave = value;
        }
    }

    /// <summary>
    /// 設定されたゴールの数
    /// </summary>
    public int NumberOfGoal
    {
        set
        {
            _numberOfGoal = value;
        }
    }

    /// <summary>
    /// 直線移動のスピード
    /// </summary>
    public float MovementSpeed
    {
        set
        {
            _movementSpeed = value;
        }
    }

    /// <summary>
    /// 再び動き出すまでの時間
    /// </summary>
    public float ReAttackTime
    {
        set
        {
            _reAttackTime = value;
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
        _spawn_And_DespawnSize = _transform.localScale / 5f;
        _normalSize = _transform.localScale;    // キャッシュ


        bird = GetComponent<BirdStats>();

        animator = GetComponent<Animator>();

        // 子オブジェクトの「SpawnPosition」
        _childSpawner = _transform.GetChild(2).transform;

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        _birdAttack = GameObject.FindWithTag("EnemyController").GetComponent<BirdAttack>();

        _stageManager = GameObject.FindWithTag("StageController").GetComponent<StageManager>();


        // 変数の初期化
        _isFinishMovement = false;
        _needDespawn = false;
        _isCompleteChangeScale = false;
        _movedDistance = 0f;
        _isLastMove = false;
        _currentTime = 0f;
        _currentTime2 = 0f;


        // 初期のゴールを設定
        SetGoalPosition(_spawnedWave, _thisInstanceIndex, howManyTimes: _howTimesSetGoal);
        _howTimesSetGoal++;


        // もしInspectorで設定ミスがあったら仮設定する
        if (_numberOfBullet < 0)
        {
            _numberOfBullet = 3;
            X_Debug.LogError("EnemySpawnPlaceData.Bullet が設定されてません");
        }

        if (_movementSpeed == 0f)
        {
            _movementSpeed = 20f;
            X_Debug.LogError("EnemySpawnPlaceData.Speed が設定されてません");
        }

        if (_reAttackTime == 0f)
        {
            _reAttackTime = 5f;
            X_Debug.LogError("EnemySpawnPlaceData.StayTime_s が設定されてません");
        }

        if (_attackIntervalTime == 0f)
        {
            _attackIntervalTime = 2f;
            X_Debug.LogError("EnemySpawnPlaceData.AttackInterval_S が設定されてません");
        }

        // スポーン時に大きくする
        StartCoroutine(LargerAtSpawn());
    }

    private void Update()
    {
        MoveSequence();
    }


    public void MoveSelect()
    {
        if (bird.Get_isParalysis)
        {
            Paralysing();
        }
        else
        {
            //IdleMove();
        }
    }

    /// <summary>
    /// 麻痺中
    /// </summary>
    protected bool Paralysing()
    {
        if (bird.Get_isParalysis)
        {
            animator.speed = 0;

            return true;
        }

        animator.speed = 1;
        return false;
    }

    protected override void MoveSequence()
    {
        // 麻痺状態か判定する（麻痺だったら動かない）
        if (Paralysing())
        {
            return;
        }

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

                // クラスをはがす
                Destroy(this);

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
            _birdAttack.NormalAttack(_childSpawner, ConversionToBulletType(), _numberOfBullet);
            _currentTime = 0f;
        }

        _currentTime2 += Time.deltaTime;

        // 再移動のためのリセット処理（攻撃がスタートしてから一定時間後に実行）-----------------------------------------------

        // 攻撃が終了
        if (_currentTime2 >= _reAttackTime)
        {
            // 設定されたゴールの数が1のとき AND すべてのゴールが設定されたら、次の行動が最後
            if (_numberOfGoal == 1 && _howTimesSetGoal > _numberOfGoal)
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
            if (_howTimesSetGoal > _numberOfGoal)
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

        // スタート位置とゴール位置の再設定
        _startPosition = _transform.position;
        SetGoalPosition(_spawnedWave, _thisInstanceIndex, howManyTimes: _howTimesSetGoal);
        _howTimesSetGoal++;
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
    /// _goalPosition変数の代入処理
    /// </summary>
    /// <param name="zakoWaveNumber">どのウェーブの敵の動きかenumで指定（ex: BirdMoveFirstはzakoWave1）</param>
    /// <param name="spawnedNumber">インスタンス番号（_spawnedNumberを渡す）（_spawn</param>
    /// <param name="howManyTimes">この関数を呼ぶのは何回目？（推奨：名前付き引数）</param>
    protected virtual void SetGoalPosition(WaveType zakoWaveNumber, int spawnedNumber ,int howManyTimes = 1)
    {
        try
        {
            _goalPosition = _stageManager._waveManagementTable._waveInformation[(int)zakoWaveNumber]._enemysData[spawnedNumber]._birdGoalPlaces[howManyTimes - 1]._birdGoalPlace.position;
            _startToGoalDistance = (_goalPosition - _startPosition).magnitude;
        }
        catch (Exception)
        {
            X_Debug.LogError("ゴール座標がEnemySpawnerTable(Scriptable)に設定されていないか、呼び出し可能回数を超えています");
        }
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