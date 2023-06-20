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
    [Tooltip("�A�j���[�V�������x")]
    [SerializeField] float _animationSpeed = 0.4f;

    [Tooltip("�ʏ펞���ړ����x")]
    [SerializeField] float _idleMoveYSpeed = 1f;

    [Tooltip("�ʏ펞�̑O�ړ����x")]
    [SerializeField] float _idleMoveZSpeed = 0f;

    [Tooltip("�U�����̉�]�̑傫��")]
    [SerializeField] float _attackMoveSize = 50f;

    [Tooltip("�������x")]
    [SerializeField] float _backMoveSpeed = 15f;

    [Tooltip("����������̊p�x")]
    [SerializeField] float _backMoveAngle = 30f;

    [Tooltip("���������A�j���[�V�����ɂ����鎞��")]
    [SerializeField] float _animationBackEndTime = 0.6f;

    [Tooltip("�����̎���")]
    [SerializeField] float _animationRunEndTime = 0.2f;

    [Tooltip("�U���A�j���[�V�����ɂ����鎞��")]
    [SerializeField] float _animationAttackEndTime = 1.2f;
    Animator animator = default;

    Transform _myTransform = default;

    Transform _childTransform = default;

    Quaternion _myTmpRotation = default;

    Vector3 _idleMove = default;

    float _movingValueY = default;

    float _tmpMovingValueY = default;

    float endAngle = 0;

    [Tooltip("�U����Ԃ��ǂ���")]
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
    /// �ʏ펞
    /// </summary>
    private void IdleMove()
    {
        _idleMove = Time.deltaTime * WingMove() * Vector3.up;

        _idleMove += _idleMoveZSpeed * Time.deltaTime * Vector3.forward;

        _myTransform.Translate(_idleMove);

        _tmpMovingValueY = _movingValueY;
    }

    /// <summary>
    /// �A�j���[�V�����ړ��ʂ���Y�ړ��ʂ��Ƃ߂�
    /// </summary>
    private float WingMove()
    {
        _movingValueY = _childTransform.rotation.eulerAngles.y;

        return (_movingValueY * _idleMoveYSpeed - _tmpMovingValueY * _idleMoveYSpeed) * _idleMoveYSpeed * Time.deltaTime;

    }

    /// <summary>
    /// �G����������鎞�ɌĂ΂��
    /// </summary>
    private void SetEnemy()
    {
        print("�Ă΂ꂽ" + _myTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// �������
    /// </summary>
    private void Attack()
    {
        _myTmpRotation = _myTransform.rotation;

        StartCoroutine(BackAnimationCoroutine());

    }

    /// <summary>
    /// ������σA�j���[�V����
    /// </summary>
    private IEnumerator AttackCoroutine()
    {
        float startTime = Time.time;

        float angle = GetTurnVector(startTime);

        float runAngle = _backMoveAngle / _animationRunEndTime;
       
        // ����
        while (Time.time < startTime + _animationRunEndTime)
        {
            _myTransform.Rotate(Vector3.forward, -runAngle * Time.deltaTime);
            // ��뉺���������O�Ɉړ�
            _myTransform.Translate(_backMoveSpeed / _animationRunEndTime * Time.deltaTime * Vector3.left);

            yield return null;

        }

        float nextStartTime = Time.time;

        SetEnemy();

        // ��]���ړ�
        while (Time.time < nextStartTime + _animationAttackEndTime)
        {
            _myTransform.Rotate(Vector3.forward, angle * Time.deltaTime);

            _myTransform.Translate(_attackMoveSize * Time.deltaTime * Vector3.left);

            yield return null;
        }

        // ���̊p�x�ɂ��ǂ�����
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

        // �U���A�j���[�V�����ڍs
        animator.SetBool("Attacking", true);

        StartCoroutine(AttackCoroutine());
    }

    /// <summary>
    /// ������ς̊p�x�v�Z
    /// </summary>
    private float GetTurnVector(float startTime)
    {
        // ��b������̉�]�p�x�v�Z
        float sourceAngle = -360f / _animationAttackEndTime;

        return sourceAngle;
    }
}
