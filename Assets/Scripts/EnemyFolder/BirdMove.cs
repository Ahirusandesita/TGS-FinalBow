// --------------------------------------------------------- 
// BirdMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using System;
using System.Collections;
using UnityEngine;


public class BirdMove : MonoBehaviour
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


    #region LinaerMovement
    [Tooltip("自身のTransformをキャッシュ")]
    private Transform _transform = default;

    [Tooltip("線形補完のスタート位置")]
    private Vector3 _startPosition = default;

    [Tooltip("線形補完の割合")]
    private float _interpolationRatio = 0f;

    [Space, SerializeField, Tooltip("直線移動のスピード")]
    private float _linearMovementSpeed = 1f;

    [Tooltip("子オブジェクトにあるスポナーを取得")]
    public Transform _childSpawner = default;

    [Tooltip("スポナーを登録")]
    public Transform _attackSpawnPlace = default;

    public delegate void OnBeforeAttack(Transform spawnPosition);
    public OnBeforeAttack _onBeforeAttack;
    #endregion

    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        _offsetReverse = UnityEngine.Random.Range(0, OFFSET_TIME_RANGE);
    }

    private void OnEnable()
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

    private void Start()
    {
        bird = GetComponent<BirdStats>();

        animator = GetComponent<Animator>();
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

    private bool _isAttack = false;
    private float _currentTime = 0f;
    private const float ATTACK_INTERVAL_TIME = 2f;
    private float _currentTime2 = 0f;
    private const float AFTER_ROTATE_BREAK_TIME = 0.2f;


    /// <summary>
    /// 直線移動
    /// </summary>
    /// <param name="targetPosition">目標位置</param>
    private void LinearMovement(Vector3 targetPosition)
    {
        if (_currentTime >= ATTACK_INTERVAL_TIME)
        {
            if (_transform.rotation != _childSpawner.rotation)
            {
                RotateBeforeAttack();
                return;
            }

            if (_currentTime2 <= AFTER_ROTATE_BREAK_TIME)
            {
                _currentTime2 += Time.deltaTime;
                return;
            }

            _currentTime2 = 0f;

            _onBeforeAttack(_attackSpawnPlace);

            _currentTime2 = 0f;
            _currentTime = 0f;
        }

        // 移動が完了したら抜ける
        if (_interpolationRatio >= 1f)
        {
            X_Debug.Log("鳥の移動完了");

            // 移動完了後にプレイヤーの方向を向く
            _transform.rotation = _childSpawner.rotation;

            return;
        }

        // 自身の座標を線形補完により更新
        _interpolationRatio += Time.deltaTime * _linearMovementSpeed;
        _transform.position = Vector3.Lerp(_startPosition, targetPosition, _interpolationRatio);

        // 進行方向を向く（「目標位置」から「自分の位置」を減算したベクトルの方向を向く）
        // targetPositionだけだとなぜかちょっとずれた
        _transform.rotation = Quaternion.LookRotation(targetPosition - _transform.position);

        _currentTime += Time.deltaTime;
    }

    public void RotateBeforeAttack()
    {
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _childSpawner.rotation, Time.deltaTime * 100f);
    }
}