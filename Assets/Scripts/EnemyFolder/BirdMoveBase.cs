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

    #region protected�ϐ�
    [Tooltip("�擾����StageManager")]
    protected StageManager _stageManager = default;

    [Tooltip("�q�I�u�W�F�N�g�ɂ���X�|�i�[���擾")]
    protected Transform _childSpawner = default;

    [Tooltip("�擾����BirdAttack�N���X")]
    protected BirdAttack _birdAttack = default;

    [Tooltip("���g��Transform���L���b�V��")]
    protected Transform _transform = default;

    [Tooltip("�ړ��̃X�^�[�g�ʒu")]
    protected Vector3 _startPosition = default;

    [Tooltip("�ړ��̃S�[���ʒu�ʒu")]
    protected Vector3 _goalPosition = default;

    [Tooltip("���`�⊮�̊���")]
    protected float _interpolationRatio = 0f;

    [Tooltip("�����ړ��̃X�s�[�h")]
    protected float _linearMovementSpeed = 0.15f;

    [Tooltip("�v���C���[�̕������������x")]
    protected float _rotateToPlayerSpeed = 150f;

    [Tooltip("�ړ��I���i�S�[���ɓ��B�j")]
    protected bool _isFinishMovement = false;


    #region �eMoveSequence�̃I�v�V��������
    [Tooltip("�i�s�����������Ĉړ�����ifalse�̏ꍇ�͐��ʂ������Ĉړ�����j")]
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
        // �ړ��̃X�^�[�g�ʒu��ݒ�
        try
        {
            _startPosition = _transform.position;
        }
        catch (Exception)
        {
            _transform = this.transform;
            _startPosition = _transform.position;
        }

        // ���Z�b�g
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
    /// �����̓���
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
    /// ��გ�
    /// </summary>
    private void Paralysing()
    {
        animator.speed = 0;
    }

    private void Test()
    {
        // ���E�̈ړ�
        _idleMove = Vector3.right * (Mathf.Sin((Time.time + _offsetReverse)
            * IDLE_PENDULUM_REVERSE_SPEED) * _idleMoveSpeedY * Time.deltaTime);

        // ���ʂ̈ړ�
        _idleMove += IDLE_MOVE_Z_SPEED * Time.deltaTime * Vector3.forward;

        transform.Translate(_idleMove);
    }
    #endregion


    /// <summary>
    /// �ϐ��̏��������s��
    /// <para>_needRotateTowardDirectionOfTravel</para>
    /// </summary>
    protected abstract void InitializeVariables();

    /// <summary>
    /// �e�E�F�[�u�̓G�̈�A�̋����i�C�x���g�Ƃ��Đi�s���܂Ƃ߂�j
    /// <para>Manager��Update�ŌĂ΂��</para>
    /// </summary>
    public abstract void MoveSequence();

    /// <summary>
    /// �����ړ��iUpdate�ŌĂԁj
    /// </summary>
    /// <param name="goalPosition">�S�[���̈ʒu</param>
    protected void LinearMovement(Vector3 goalPosition)
    {
        // �ړ������������甲����
        if (_interpolationRatio >= 1f)
        {
            X_Debug.Log("���̈ړ�����");
            _isFinishMovement = true;

            return;
        }

        // ���g�̍��W����`�⊮�ɂ��X�V
        _interpolationRatio += Time.deltaTime * _linearMovementSpeed;
        _transform.position = Vector3.Lerp(_startPosition, goalPosition, _interpolationRatio);

        if (_needRotateTowardDirectionOfTravel)
        {
            // �i�s�����������i�u�ڕW�ʒu�v����u�����̈ʒu�v�����Z�����x�N�g���̕����������j
            // goalPosition�������ƂȂ���������Ƃ��ꂽ
            _transform.rotation = Quaternion.LookRotation(goalPosition - _transform.position);
        }
    }

    /// <summary>
    /// �v���C���[�̕����������iUpdate�ŌĂԁj
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