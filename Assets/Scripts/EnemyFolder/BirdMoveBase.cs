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

    [Tooltip("直線移動のスピード")]
    protected float _linearMovementSpeed = 0.3f;

    [Tooltip("プレイヤーの方向を向く速度")]
    protected float _rotateSpeed = 100f;

    [Tooltip("移動終了（ゴールに到達）")]
    protected bool _isFinishMovement = false;

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
        catch (Exception)
        {
            _transform = this.transform;
            _startPosition = _transform.position;
        }

        // リセット
        _interpolationRatio = 0f;
    }

    protected virtual void Start()
    {
        bird = GetComponent<BirdStats>();

        animator = GetComponent<Animator>();

        // 子オブジェクトの「SpawnPosition」
        _childSpawner = _transform.GetChild(2).transform;

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
    private void Paralysing()
    {
        if (bird.Get_isParalysis)
        {
            animator.speed = 0;
        }
        animator.speed = 1;
    }

    private void Test()
    {
        // 左右の移動
        _idleMove = Vector3.right * (Mathf.Sin((Time.time + _offsetReverse)
            * IDLE_PENDULUM_REVERSE_SPEED) * _idleMoveSpeedY * Time.deltaTime);

        // 正面の移動
        _idleMove += IDLE_MOVE_Z_SPEED * Time.deltaTime * Vector3.forward;

        transform.Translate(_idleMove);
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
        // 移動が完了したら抜ける
        if (_interpolationRatio >= 1f)
        {
            X_Debug.Log("鳥の移動完了");
            IsFinishMovement = true;

            return;
        }

        // 自身の座標を線形補完により更新
        _interpolationRatio += Time.deltaTime * _linearMovementSpeed;
        _transform.position = Vector3.Lerp(_startPosition, _goalPosition, _interpolationRatio);

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
    /// <param name="howManyTimes">この関数を呼ぶのは何回目？（推奨：名前付き引数）</param>
    protected virtual void SetGoalPosition(WaveType zakoWaveNumber, int howManyTimes = 1)
    {
        try
        {
            _goalPosition = _stageManager._enemySpawnerTable._scriptableESpawnerInformation[(int)zakoWaveNumber]._birdGoalPlaces[howManyTimes - 1].position;
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
}