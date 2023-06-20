// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public class BossMove : MonoBehaviour
{
    [Tooltip("アニメーション速度")]
    [SerializeField] float _animationSpeed = 0.4f;

    [Tooltip("通常時横移動速度")]
    [SerializeField] float _idleMoveYSpeed = 1f;

    [Tooltip("通常時の前移動速度")]
    [SerializeField] float _idleMoveZSpeed = 0f;

    [Tooltip("攻撃時の回転の大きさ")]
    [SerializeField] float _attackMoveSize = 50f;

    [Tooltip("助走速度")]
    [SerializeField] float _backMoveSpeed = 15f;

    [Tooltip("助走下がりの角度")]
    [SerializeField] float _backMoveAngle = 30f;

    [Tooltip("助走準備アニメーションにかかる時間")]
    [SerializeField] float _animationBackEndTime = 0.6f;

    [Tooltip("助走の時間")]
    [SerializeField] float _animationRunEndTime = 0.2f;

    [Tooltip("攻撃アニメーションにかかる時間")]
    [SerializeField] float _animationAttackEndTime = 1.2f;
    Animator animator = default;

    Transform _myTransform = default;

    Transform _childTransform = default;

    Quaternion _myTmpRotation = default;

    Vector3 _idleMove = default;

    float _movingValueY = default;

    float _tmpMovingValueY = default;

    float endAngle = 0;

    [Tooltip("攻撃状態かどうか")]
    private bool _isAttack = false;

    public bool IsAttack
    {
        set
        {
            _isAttack = value;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        animator.speed = _animationSpeed;

        _myTransform = transform;

        _childTransform = _myTransform.GetChild(0).GetChild(0).
            GetChild(0).GetChild(2).GetChild(0);

        _movingValueY = _childTransform.rotation.eulerAngles.y;

        _tmpMovingValueY = _movingValueY;

    }

    public void MoveSelect()
    {
        if (_isAttack)
        {
            print("atk");
            _isAttack = false;
            animator.speed = 1f;
            Attack();
        }
        else
        {
            animator.speed = _animationSpeed;
            IdleMove();
        }
    }

    /// <summary>
    /// 通常時
    /// </summary>
    private void IdleMove()
    {
        _idleMove = Time.deltaTime * WingMove() * Vector3.up;

        _idleMove += _idleMoveZSpeed * Time.deltaTime * Vector3.forward;

        _myTransform.Translate(_idleMove);

        _tmpMovingValueY = _movingValueY;
    }

    /// <summary>
    /// アニメーション移動量からY移動量もとめる
    /// </summary>
    private float WingMove()
    {
        _movingValueY = _childTransform.rotation.eulerAngles.y;

        return (_movingValueY * _idleMoveYSpeed - _tmpMovingValueY * _idleMoveYSpeed) * _idleMoveYSpeed * Time.deltaTime;

    }

    /// <summary>
    /// 雑魚召喚される時に呼ばれる
    /// </summary>
    private void SetEnemy()
    {
        print("呼ばれた" + _myTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// くるりんぱ
    /// </summary>
    private void Attack()
    {
        _myTmpRotation = _myTransform.rotation;

        StartCoroutine(BackAnimationCoroutine());

    }

    /// <summary>
    /// くるりんぱアニメーション
    /// </summary>
    private IEnumerator AttackCoroutine()
    {
        float startTime = Time.time;

        float angle = GetTurnVector(startTime);

        float runAngle = _backMoveAngle / _animationRunEndTime;
       
        // 助走
        while (Time.time < startTime + _animationRunEndTime)
        {
            _myTransform.Rotate(Vector3.forward, -runAngle * Time.deltaTime);
            // 後ろ下がった分前に移動
            _myTransform.Translate(_backMoveSpeed / _animationRunEndTime * Time.deltaTime * Vector3.left);

            yield return null;

        }

        float nextStartTime = Time.time;

        SetEnemy();

        // 回転＆移動
        while (Time.time < nextStartTime + _animationAttackEndTime)
        {
            _myTransform.Rotate(Vector3.forward, angle * Time.deltaTime);

            _myTransform.Translate(_attackMoveSize * Time.deltaTime * Vector3.left);

            yield return null;
        }

        // 元の角度にもどすため
        //_myTransform.rotation = _myTmpRotation;

        animator.SetBool("Attacking", false);

    }

    private IEnumerator BackAnimationCoroutine()
    {
        float startTime = Time.time;

        endAngle = _backMoveAngle / _animationBackEndTime;

        
        animator.SetBool("Backing", true);

        while (Time.time < startTime + _animationBackEndTime)
        {
            _myTransform.Rotate(Vector3.forward, endAngle * Time.deltaTime);

            _myTransform.Translate(_backMoveSpeed / _animationBackEndTime * Time.deltaTime * Vector3.right);

            yield return null;

        }

        animator.SetBool("Backing", false);

        // 攻撃アニメーション移行
        animator.SetBool("Attacking", true);

        StartCoroutine(AttackCoroutine());
    }

    /// <summary>
    /// くるりんぱの角度計算
    /// </summary>
    private float GetTurnVector(float startTime)
    {
        // 一秒あたりの回転角度計算
        float sourceAngle = -360f / _animationAttackEndTime;

        return sourceAngle;
    }
}
