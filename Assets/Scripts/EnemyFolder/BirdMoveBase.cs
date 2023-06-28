// --------------------------------------------------------- 
// BirdMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using System;
using System.Collections;
using UnityEngine;


public abstract class BirdMoveBase : MonoBehaviour
{
    #region variable 
    [SerializeField] float _idleMoveSpeedX = 10f;

    [SerializeField] float _idleMoveSpeedY = 10f;

    [SerializeField] float _changeAngleSpeed = 1f;

    const float IDLE_MOVE_Z_SPEED = 0f;

    const int FOR_INFINITY = 2;

    const int OFFSET_VALUE = 180;

    const float IDLE_PENDULUM_REVERSE_SPEED = 12f;

    const int OFFSET_TIME_RANGE = 1000;

    IFCommonEnemyGetParalysis bird = default;

    Animator animator = default;

    private Vector3 _idleMove = Vector3.zero;

    private float _offsetReverse = 0f;

    private float _moveAngle = 0f;

    private float _time = 0f;
    #endregion

    #region protected変数
    [Tooltip("自身の敵の種類")]
    protected CashObjectInformation _cashObjectInformation = default;

    [Tooltip("取得したStageManager")]
    protected StageManager _stageManager = default;

    [Tooltip("子オブジェクトにあるスポナーを取得")]
    protected Transform _childSpawner = default;

    [Tooltip("取得したBirdAttackクラス")]
    protected BirdAttack _birdAttack = default;

    [Tooltip("自身のTransformをキャッシュ")]
    protected Transform _transform = default;

    [Tooltip("移動のスタート位置")]
    protected Vector3 _startPosition = default;

    [Tooltip("移動のゴール位置位置")]
    protected Vector3 _goalPosition = default;

    [Tooltip("線形補完の割合")]
    protected float _interpolationRatio = 0f;

    [Tooltip("スタートとゴール間の距離 = 目標移動量")]
    protected float _startToGoalDistance = default;

    [Tooltip("動いた距離")]
    protected float _movedDistance = default;

    [Tooltip("直線移動のスピード")]
    protected float _linearMovementSpeed = 20f;

    [Tooltip("プレイヤーの方向を向く速度")]
    protected float _rotateSpeed = 100f;

    [Tooltip("移動終了（ゴールに到達）")]
    protected bool _isFinishMovement = false;

    [Tooltip("消滅させる（倒せなかった場合）")]
    protected bool _needDespawn = false;

    [Tooltip("出現時/消滅時の大きさ")]
    protected Vector3 _spawn_And_DespawnSize = default;

    [Tooltip("通常の大きさ")]
    protected Vector3 _normalSize = default;

    [Tooltip("Scale変えるときのブレイク")]
    // これがないとループが速すぎて目に見えない
    protected WaitForSeconds _changeSizeWait = new WaitForSeconds(0.01f);

    [Tooltip("Scaleの変更が完了")]
    protected bool _isCompleteChangeScale = false;

    [Tooltip("このインスタンスのインデックス")]
    // ex) 同ウェーブに敵が2体いる場合、Instance_0 = 0, Instance_1 = 1が設定される
    protected int _thisInstanceIndex = default;

    [Tooltip("どのウェーブでスポーンしたか")]
    protected WaveType _spawnedWave = default;

    [Tooltip("設定されたゴールの数")]
    protected int _numberOfGoal = default;

    [Tooltip("再び動き出すまでの時間")]
    protected float _reAttackTime = 10f;

    [Tooltip("Scaleの加算/減算値")]
    protected readonly Vector3 CHANGE_SCALE_VALUE = new Vector3(0.05f, 0.05f, 0.05f);   // 少しずつ変わる

    [Tooltip("正面の角度")]
    protected readonly Quaternion FRONT_ANGLE = Quaternion.Euler(new Vector3(0f, 180f, 0f));
    #endregion


    /// <summary>
    /// 移動終了（ゴールに到達）
    /// </summary>
    protected bool IsFinishMovement
    {
        get
        {
            return _isFinishMovement;
        }
        set
        {
            _isFinishMovement = value;

            // falseに設定されたとき、線形補完も同時にリセット
            if (!_isFinishMovement)
            {
                _interpolationRatio = 0f;
            }
        }
    }

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
    public float LinearMovementSpeed
    {
        set
        {
            _linearMovementSpeed = value;
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


    #region method

    private void Awake()
    {
        _offsetReverse = UnityEngine.Random.Range(0, OFFSET_TIME_RANGE);
    }

    protected virtual void OnEnable()
    {
        _stageManager = GameObject.FindWithTag("StageController").GetComponent<StageManager>();

        // 移動のスタート位置を設定
        try
        {
            _startPosition = _transform.position;
        }
        // プールが生成された瞬間の1回だけはこっちが動く
        catch (Exception)
        {
            _transform = this.transform;
            _startPosition = _transform.position;
            _spawn_And_DespawnSize = _transform.localScale / 5f;
            _normalSize = _transform.localScale;    // キャッシュ
        }

        // リセット
        _interpolationRatio = 0f;
        _needDespawn = false;
        _isCompleteChangeScale = false;
        _movedDistance = 0f;

        // スポーン時に大きくする
        StartCoroutine(LargerAtSpawn());
    }

    protected virtual void Start()
    {
        bird = GetComponent<BirdStats>();

        animator = GetComponent<Animator>();

        // 子オブジェクトの「SpawnPosition」
        _childSpawner = _transform.GetChild(2).transform;

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        _birdAttack = GameObject.FindWithTag("EnemyController").GetComponent<BirdAttack>();
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
    /// いつもの動き
    /// </summary>
    private void IdleMove()
    {
        animator.speed = 1;

        _time += Time.deltaTime * _changeAngleSpeed;

        _idleMove = _idleMoveSpeedY * Mathf.Sin(_time) * Vector3.up;

        transform.Translate(_idleMove * Time.deltaTime);
    }

    private void InfinityMode()
    {
        _moveAngle += Time.deltaTime * _changeAngleSpeed;

        _idleMove = new Vector3(
            Mathf.Sin(_moveAngle) * _idleMoveSpeedY,
            Mathf.Sin((_moveAngle + OFFSET_VALUE) * FOR_INFINITY) * _idleMoveSpeedY,
            0);

        transform.Translate(Time.deltaTime * _idleMove);
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

    private void Test()
    {
        // 左右の移動
        _idleMove = Vector3.right * (Mathf.Sin((Time.time + _offsetReverse)
            * IDLE_PENDULUM_REVERSE_SPEED) * _idleMoveSpeedY * Time.deltaTime);

        // 正面の移動
        _idleMove += IDLE_MOVE_Z_SPEED * Time.deltaTime * Vector3.forward;

        _transform.Translate(_idleMove);
    }
    #endregion


    /// <summary>
    /// 各ウェーブの敵の一連の挙動（イベントとして進行をまとめる）
    /// <para>Updateで呼ばれる</para>
    /// </summary>
    public abstract void MoveSequence();

    /// <summary>
    /// 直線移動（Updateで呼ぶ）
    /// </summary>
    protected virtual void LinearMovement()
    {
        // 移動が完了したら抜ける（実移動量と目標移動量を比較）
        if (_movedDistance >= _startToGoalDistance)
        {
            X_Debug.Log("鳥の移動完了");
            IsFinishMovement = true;
            _movedDistance = 0f;

            return;
        }

        // 移動する（移動方向のベクトル * 移動速度）
        _transform.Translate((_goalPosition - _startPosition).normalized * _linearMovementSpeed * Time.deltaTime, Space.World); 　// 第二引数ないとバグる
        // 移動量を加算
        _movedDistance += ((_goalPosition - _startPosition).normalized * _linearMovementSpeed * Time.deltaTime).magnitude;


        // 進行方向を向く（「目標位置」から「自分の位置」を減算したベクトルの方向を向く）
        // goalPositionだけだとなぜかちょっとずれた
        //_transform.rotation = Quaternion.LookRotation(_goalPosition - _transform.position);
    }

    /// <summary>
    /// プレイヤーの方向を向く（Updateで呼ぶ）
    /// </summary>
    protected void RotateToPlayer()
    {
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _childSpawner.rotation, Time.deltaTime * _rotateSpeed);
    }

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
            _goalPosition = _stageManager._enemySpawnerTable._scriptableWaveEnemy[(int)zakoWaveNumber]._enemysSpawner[spawnedNumber]._birdGoalPlaces[howManyTimes - 1].position;
            _startToGoalDistance = (_goalPosition - _startPosition).magnitude;
        }
        catch (Exception)
        {
            X_Debug.LogError("ゴール座標がEnemySpawnerTable(Scriptable)に設定されていないか、呼び出し可能回数を超えています");
        }
    }

    /// <summary>
    /// _goalPosition変数にマップ中央の座標を代入する処理
    /// </summary>
    protected void SetGoalPositionCentral()
    {
        try
        {
            _goalPosition = _stageManager._enemySpawnerTable._centralInformation._centralTransform.position;
        }
        catch (Exception)
        {
            X_Debug.LogError("中心座標がEnemySpawnerTable(Screptable)に設定されていません");
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
        }

        // 例外処理
        return PoolEnum.PoolObjectType.normalBullet;
    }
}