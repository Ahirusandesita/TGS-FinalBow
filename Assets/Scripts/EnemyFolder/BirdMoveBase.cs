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
    protected float _linearMovementSpeed = 0.3f;

    [Tooltip("�v���C���[�̕������������x")]
    protected float _rotateSpeed = 100f;

    [Tooltip("�ړ��I���i�S�[���ɓ��B�j")]
    protected bool _isFinishMovement = false;

    [Tooltip("���ʂ̊p�x")]
    protected readonly Quaternion FRONT_ANGLE = Quaternion.Euler(new Vector3(0f, 180f, 0f));
    #endregion


    /// <summary>
    /// �ړ��I���i�S�[���ɓ��B�j
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

            // false�ɐݒ肳�ꂽ�Ƃ��A���`�⊮�������Ƀ��Z�b�g
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

        // �q�I�u�W�F�N�g�́uSpawnPosition�v
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
        if (bird.Get_isParalysis)
        {
            animator.speed = 0;
        }
        animator.speed = 1;
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
    /// �e�E�F�[�u�̓G�̈�A�̋����i�C�x���g�Ƃ��Đi�s���܂Ƃ߂�j
    /// <para>Update�ŌĂ΂��</para>
    /// </summary>
    public abstract void MoveSequence();

    /// <summary>
    /// �����ړ��iUpdate�ŌĂԁj
    /// </summary>
    protected virtual void LinearMovement()
    {
        // �ړ������������甲����
        if (_interpolationRatio >= 1f)
        {
            X_Debug.Log("���̈ړ�����");
            IsFinishMovement = true;

            return;
        }

        // ���g�̍��W����`�⊮�ɂ��X�V
        _interpolationRatio += Time.deltaTime * _linearMovementSpeed;
        _transform.position = Vector3.Lerp(_startPosition, _goalPosition, _interpolationRatio);

        // �i�s�����������i�u�ڕW�ʒu�v����u�����̈ʒu�v�����Z�����x�N�g���̕����������j
        // goalPosition�������ƂȂ���������Ƃ��ꂽ
        //_transform.rotation = Quaternion.LookRotation(_goalPosition - _transform.position);
    }

    /// <summary>
    /// �v���C���[�̕����������iUpdate�ŌĂԁj
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
    /// _goalPosition�ϐ��̑������
    /// </summary>
    /// <param name="zakoWaveNumber">�ǂ̃E�F�[�u�̓G�̓�����enum�Ŏw��iex: BirdMoveFirst��zakoWave1�j</param>
    /// <param name="howManyTimes">���̊֐����ĂԂ͉̂���ځH�i�����F���O�t�������j</param>
    protected virtual void SetGoalPosition(WaveType zakoWaveNumber, int howManyTimes = 1)
    {
        try
        {
            _goalPosition = _stageManager._enemySpawnerTable._scriptableESpawnerInformation[(int)zakoWaveNumber]._birdGoalPlaces[howManyTimes - 1].position;
        }
        catch (Exception)
        {
            X_Debug.LogError("�S�[�����W��EnemySpawnerTable(Scriptable)�ɐݒ肳��Ă��Ȃ����A�Ăяo���\�񐔂𒴂��Ă��܂�");
        }
    }

    /// <summary>
    /// _goalPosition�ϐ��Ƀ}�b�v�����̍��W�������鏈��
    /// </summary>
    protected void SetGoalPositionCentral()
    {
        try
        {
            _goalPosition = _stageManager._enemySpawnerTable._centralInformation._centralTransform.position;
        }
        catch (Exception)
        {
            X_Debug.LogError("���S���W��EnemySpawnerTable(Screptable)�ɐݒ肳��Ă��܂���");
        }
    }
}