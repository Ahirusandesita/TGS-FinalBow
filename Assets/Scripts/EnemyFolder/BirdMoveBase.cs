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
    protected float _linearMovementSpeed = 0.15f;

    [Tooltip("プレイヤーの方向を向く速度")]
    protected float _rotateToPlayerSpeed = 150f;

    [Tooltip("移動終了（ゴールに到達）")]
    protected bool _isFinishMovement = false;


    #region 各MoveSequenceのオプション項目
    [Tooltip("進行方向を向いて移動する（falseの場合は正面を向いて移動する）")]
    protected bool _needRotateTowardDirectionOfTravel = default;
    #endregion
    #endregion

    public Vector3 GoalPosition
    {
        set
        {
            _goalPosition = value;
        }
    }

    #region method

    private void Awake()
    {
        _offsetReverse = UnityEngine.Random.Range(0, OFFSET_TIME_RANGE);
    }

    protected virtual void OnEnable()
    {
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

        _childSpawner = _transform.GetChild(2).transform;

        _birdAttack = GameObject.FindWithTag("EnemyController").GetComponent<BirdAttack>();

        _stageManager = GameObject.FindWithTag("StageController").GetComponent<StageManager>();
    }

    public void MoveSelect()
    {
        if (bird.Get_isParalysis)
        {
            Paralysing();
        }
        else
        {
            LinearMovement(_startPosition + Vector3.right * 50f);
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
        animator.speed = 0;
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
    /// 変数の初期化を行う
    /// <para>_needRotateTowardDirectionOfTravel</para>
    /// </summary>
    protected abstract void InitializeVariables();

    /// <summary>
    /// 各ウェーブの敵の一連の挙動（イベントとして進行をまとめる）
    /// <para>ManagerのUpdateで呼ばれる</para>
    /// </summary>
    public abstract void MoveSequence();

    /// <summary>
    /// 直線移動（Updateで呼ぶ）
    /// </summary>
    /// <param name="goalPosition">ゴールの位置</param>
    protected void LinearMovement(Vector3 goalPosition)
    {
        // 移動が完了したら抜ける
        if (_interpolationRatio >= 1f)
        {
            X_Debug.Log("鳥の移動完了");
            _isFinishMovement = true;

            return;
        }

        // 自身の座標を線形補完により更新
        _interpolationRatio += Time.deltaTime * _linearMovementSpeed;
        _transform.position = Vector3.Lerp(_startPosition, goalPosition, _interpolationRatio);

        if (_needRotateTowardDirectionOfTravel)
        {
            // 進行方向を向く（「目標位置」から「自分の位置」を減算したベクトルの方向を向く）
            // goalPositionだけだとなぜかちょっとずれた
            _transform.rotation = Quaternion.LookRotation(goalPosition - _transform.position);
        }
    }

    /// <summary>
    /// プレイヤーの方向を向く（Updateで呼ぶ）
    /// </summary>
    protected void RotateToPlayer(float rotateSpeed)
    {
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _childSpawner.rotation, Time.deltaTime * rotateSpeed);
    }

    protected void SetGoalPosition()
    {
        //_goalPosition = _stageManager._enemySpawnerTable._scriptableESpawnerInformation[]
    }
}