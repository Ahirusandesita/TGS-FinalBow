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
/// ���G���̋����̊��
/// </summary>
public abstract class BirdMoveBase : EnemyMoveBase
{
    #region variable 

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
    #region �Vmove�ϐ�
    [Tooltip("�ړ��̃X�^�[�g�ʒu")]
    protected Vector3 _startPosition = default;

    [Tooltip("�ړ��̃S�[���ʒu")]
    protected Vector3 _goalPosition = default;

    [Tooltip("�ړ��X�s�[�h")]
    protected float _movementSpeed = default;

    [Tooltip("���̓���")]
    protected MoveType _moveType = default;

    [Tooltip("�J�[�u�������̏c���̈ړ����x")]
    protected float _moveSpeedArc = default;

    [Tooltip("�J�[�u�������̌ʂ�`������")]
    protected ArcMoveDirection _arcMoveDirection = default;


    [Tooltip("���g�̓G�̎��")]
    private CashObjectInformation _cashObjectInformation = default;

    [Tooltip("�q�I�u�W�F�N�g�ɂ���X�|�i�[���擾")]
    private Transform _childSpawner = default;

    [Tooltip("�擾����EAttackSpawner�N���X")]
    private EAttackSpawner _eAttackSpawner = default;

    [Tooltip("�擾����BirdAttack�N���X")]
    private BirdAttack _birdAttack = default;

    [Tooltip("�擾����BirdStats�N���X")]
    private BirdStats _birdStats = default;

    [Tooltip("�X�^�[�g�ƃS�[���Ԃ̋��� = �ڕW�ړ���")]
    private float _startToGoalDistance = default;

    [Tooltip("����������")]
    private float _movedDistance = 0f;

    [Tooltip("�v���C���[�̕������������x")]
    private float _rotateSpeed = 100f;

    [Tooltip("�ړ��I���i�S�[���ɓ��B�j")]
    private bool _isFinishMovement = false;

    [Tooltip("���ł�����i�|���Ȃ������ꍇ�j")]
    private bool _needDespawn = false;

    [Tooltip("�o����/���Ŏ��̑傫��")]
    private Vector3 _spawn_And_DespawnSize = default;

    [Tooltip("�ʏ�̑傫��")]
    private Vector3 _normalSize = default;

    [Tooltip("Scale�ς���Ƃ��̃u���C�N")]
    // ���ꂪ�Ȃ��ƃ��[�v���������ĖڂɌ����Ȃ�
    private WaitForSeconds _changeSizeWait = new WaitForSeconds(0.01f);

    [Tooltip("Scale�̕ύX������")]
    private bool _isCompleteChangeScale = false;

    [Tooltip("�o���e�̐�")]
    private int _numberOfBullet = default;

    [Tooltip("�U���̕p�x")]
    private float _attackIntervalTime = 2f;

    [Tooltip("���݂̌o�ߎ��ԁi�U���܂ł̕p�x�Ɏg���j")]
    private float _currentTime = 0f;

    [Tooltip("���݂̌o�ߎ��ԁi�Ăѓ����o���܂ł̎��ԂɎg���j")]
    private float _currentTime2 = 0f;

    [Tooltip("�s���i�S�[���ݒ�j�̌J��Ԃ��J�E���g")]
    private int _repeatCount = 0;

    [Tooltip("�U�����@�̎��")]
    private BirdAttackType _birdAttackType = default;

    [Tooltip("�U�����s���^�C�~���O")]
    private float _attackTiming = default;

    [Tooltip("�A���U����")]
    private int _attackTimes = default;

    [Tooltip("�A���U���N�[���^�C��")]
    private float _cooldownTime = default;

    [Tooltip("�A���U���Ԋu")]
    private float _consecutiveIntervalTime = default;

    [Tooltip("���[�v����")]
    private bool _needRoop = default;

    [Tooltip("���[�v��̃S�[���ԍ�")]
    private int _goalIndexOfRoop = default;

    [Tooltip("�f�X�|�[������")]
    private float _despawnTime_s = default;

    [Tooltip("�e�X�e�[�W�̃X�^�[�g�n�_")]
    private Transform _stageTransform = default;

    [Tooltip("�S�[�����X�g")]
    private List<Vector3> _goalPositions = new List<Vector3>();

    [Tooltip("�S�[���Ԃ̃X�s�[�h���X�g")]
    private List<float> _movementSpeeds = new List<float>();

    [Tooltip("�Ăѓ����o���܂ł̎��ԃ��X�g")]
    private List<float> _reAttackTimes = new List<float>();

    [Tooltip("�S�[���Ԃ̒��̓������X�g")]
    private List<MoveType> _moveTypes = new List<MoveType>();

    [Tooltip("�J�[�u�������̏c���̈ړ����x���X�g")]
    private List<float> _moveSpeedArcs = new();

    [Tooltip("�J�[�u�������̌ʂ�`���������X�g")]
    private List<ArcMoveDirection> _arcMoveDirections = new();

    [Tooltip("�U�����@�̎�ރ��X�g�i�ړ����j")]
    private List<BirdAttackType> _birdAttackTypes_moving = new();

    [Tooltip("�U�����@�̎�ރ��X�g�i��~���j")]
    private List<BirdAttackType> _birdAttackTypes_stopping = new();

    [Tooltip("�U���̕p�x���X�g�i�ړ����j")]
    private List<float> _attackIntervalTimes_moving = new();

    [Tooltip("�U���̕p�x���X�g�i��~���j")]
    private List<float> _attackIntervalTimes_stopping = new();

    [Tooltip("�U�����s���^�C�~���O���X�g�i�ړ����j")]
    private List<float> _attackTimings_moving = new();

    [Tooltip("�U�����s���^�C�~���O���X�g�i��~���j")]
    private List<float> _attackTimings_stopping = new();

    [Tooltip("�A���U���񐔃��X�g�i�ړ����j")]
    private List<int> _attackTimesList_moving = new();

    [Tooltip("�A���U���񐔃��X�g�i��~���j")]
    private List<int> _attackTimesList_stopping = new();

    [Tooltip("�A���U���N�[���^�C�����X�g�i�ړ����j")]
    private List<float> _cooldownTimeList_moving = new();

    [Tooltip("�A���U���N�[���^�C�����X�g�i��~���j")]
    private List<float> _cooldownTimeList_stopping = new();

    [Tooltip("�A���U���Ԋu���X�g�i�ړ����j")]
    private List<float> _consecutiveIntervalTimes_moving = new();

    [Tooltip("�A���U���Ԋu���X�g�i��~���j")]
    private List<float> _consecutiveIntervalTimes_stopping = new();

    [Tooltip("�ړ����̌������X�g")]
    private List<DirectionType_AtMoving> _directionTypes_moving = new();

    [Tooltip("��~���̌������X�g")]
    private List<DirectionType_AtStopping> _directionTypes_stopping = new();

    [Tooltip("�U���̌������X�g")]
    private List<DirectionType_AtAttack> _directionTypes_attack = new();

    private bool _isAttackCompleted_moving = false;
    private bool _isAttackCompleted_stopping = false;
    private Coroutine _activeAttackCoroutine_moving = default;
    private Coroutine _activeAttackCoroutine_stopping = default;
    private WaitForSeconds _waitTimeOfConsecutiveAttack = default;

    private bool _isRotateCompleted_moving = false;
    private Coroutine _activeRotateCoroutine_moving = default;
    private Coroutine _activeRotateCoroutine_stopping = default;
    private Coroutine _activeRotateCoroutine_Attack = default;

    private float _spawnedTime = default;

    #region Rotate�ϐ�
    private Vector3 _prevPosition = default;
    private Vector3 _delta = default;
    private readonly Vector3 ZERO = Vector3.zero;
    private readonly Vector3 UP = Vector3.up;
    #endregion

    [Tooltip("Scale�̉��Z/���Z�l")]
    private Vector3 CHANGE_SCALE_VALUE = default;

    [Tooltip("���ʂ̊p�x")]
    private Quaternion _frontAngle = default;

    [Tooltip("�w��b���U���̍ő吔")]
    private const int MAX_ATTACK_TIMES_FOR_SPECIFY_SECONDS = 5;
    #endregion


    #region property
    /// <summary>
    /// ���ł�����i�|���Ȃ������ꍇ�j
    /// </summary>
    public bool NeedDespawn
    {
        get
        {
            return _needDespawn;
        }
    }

    /// <summary>
    /// Scale�̕ύX������
    /// </summary>
    public bool IsChangeScaleComplete
    {
        get
        {
            return _isCompleteChangeScale;
        }
    }

    /// <summary>
    /// �o���e�̐�
    /// </summary>
    public int NumberOfBullet
    {
        set
        {
            _numberOfBullet = value;
        }
    }

    /// <summary>
    /// ���[�v����
    /// </summary>
    public bool NeedRoop
    {
        set
        {
            _needRoop = value;
        }
    }

    /// <summary>
    /// ���[�v��̃S�[���ԍ�
    /// </summary>
    public int GoalIndexOfRooop
    {
        set
        {
            _goalIndexOfRoop = value;
        }
    }

    /// <summary>
    /// �f�X�|�[������
    /// </summary>
    public float DespawnTime
    {
        set
        {
            _despawnTime_s = value;
        }
    }

    /// <summary>
    /// �U���̕p�x���X�g�i�ړ����j
    /// </summary>
    public float AttackIntervalTimes_Moving
    {
        set
        {
            _attackIntervalTimes_moving.Add(value);
        }
    }

    /// <summary>
    /// �U���̕p�x���X�g�i��~���j
    /// </summary>
    public float AttackIntervalTimes_Stopping
    {
        set
        {
            _attackIntervalTimes_stopping.Add(value);
        }
    }

    /// <summary>
    /// �S�[�����X�g
    /// </summary>
    public Vector3 GoalPositions
    {
        set
        {
            _goalPositions.Add(value);
        }
    }

    /// <summary>
    /// �S�[���Ԃ̃X�s�[�h���X�g
    /// </summary>
    public float MovementSpeeds
    {
        set
        {
            _movementSpeeds.Add(value);
        }
    }

    /// <summary>
    /// �S�[���̒�~�i�U���j���ԃ��X�g
    /// </summary>
    public float ReAttackTimes
    {
        set
        {
            _reAttackTimes.Add(value);
        }
    }

    /// <summary>
    /// �S�[���Ԃ̒��̓������X�g
    /// </summary>
    public MoveType MoveTypes
    {
        set
        {
            _moveTypes.Add(value);
        }
    }

    /// <summary>
    /// �J�[�u�������̏c���̈ړ����x���X�g
    /// </summary>
    public float MoveSpeedArcs
    {
        set
        {
            _moveSpeedArcs.Add(value);
        }
    }

    /// <summary>
    /// �J�[�u�������̌ʂ�`���������X�g
    /// </summary>
    public ArcMoveDirection ArcMoveDirections
    {
        set
        {
            _arcMoveDirections.Add(value);
        }
    }

    /// <summary>
    /// �U�����@�̎�ރ��X�g�i�ړ����j
    /// </summary>
    public BirdAttackType BirdAttackTypes_Moving
    {
        set
        {
            _birdAttackTypes_moving.Add(value);
        }
    }

    /// <summary>
    /// �U�����@�̎�ރ��X�g�i��~���j
    /// </summary>
    public BirdAttackType BirdAttackTypes_Stopping
    {
        set
        {
            _birdAttackTypes_stopping.Add(value);
        }
    }

    /// <summary>
    /// �U�����s���^�C�~���O���X�g�i�ړ����j
    /// </summary>
    public float AttackTimings_Moving
    {
        set
        {
            _attackTimings_moving.Add(value);
        }
    }

    /// <summary>
    /// �U�����s���^�C�~���O���X�g�i��~���j
    /// </summary>
    public float AttackTimings_Stopping
    {
        set
        {
            _attackTimings_stopping.Add(value);
        }
    }

    /// <summary>
    /// �A���U���񐔁i�ړ����j
    /// </summary>
    public int AttackTimes_Moving
    {
        set
        {
            _attackTimesList_moving.Add(value);
        }
    }

    /// <summary>
    /// �A���U���񐔁i��~���j
    /// </summary>
    public int AttackTimes_Stopping
    {
        set
        {
            _attackTimesList_stopping.Add(value);
        }
    }

    /// <summary>
    /// �A���U���N�[���^�C���i�ړ����j
    /// </summary>
    public float CooldownTime_Moving
    {
        set
        {
            _cooldownTimeList_moving.Add(value);
        }
    }

    /// <summary>
    /// �A���U���N�[���^�C���i��~���j
    /// </summary>
    public float CooldownTime_Stopping
    {
        set
        {
            _cooldownTimeList_stopping.Add(value);
        }
    }

    /// <summary>
    /// �A���U���Ԋu�i�ړ����j
    /// </summary>
    public float ConsecutiveIntervalTimes_Moving
    {
        set
        {
            _consecutiveIntervalTimes_moving.Add(value);
        }
    }

    /// <summary>
    /// �A���U���Ԋu�i��~���j
    /// </summary>
    public float ConsecutiveIntervalTimes_Stopping
    {
        set
        {
            _consecutiveIntervalTimes_stopping.Add(value);
        }
    }

    /// <summary>
    /// �ړ����̌������X�g
    /// </summary>
    public DirectionType_AtMoving DirectionTypes_Moving
    {
        set
        {
            _directionTypes_moving.Add(value);
        }
    }

    /// <summary>
    /// ��~���̌������X�g
    /// </summary>
    public DirectionType_AtStopping DirectionTypes_Stopping
    {
        set
        {
            _directionTypes_stopping.Add(value);
        }
    }

    /// <summary>
    /// �U���̌������X�g
    /// </summary>
    public DirectionType_AtAttack DirectionTypes_Attack
    {
        set
        {
            _directionTypes_attack.Add(value);
        }
    }

    /// <summary>
    /// �e�X�e�[�W�̃X�^�[�g�n�_
    /// </summary>
    public Transform StageTransform
    {
        set
        {
            _stageTransform = value;
        }
    }
    #endregion


    #region method
    protected override void Start()
    {
        _normalSize = _transform.localScale;    // �L���b�V��

        // �q�I�u�W�F�N�g�́uSpawnPosition�v
        _childSpawner = _transform.GetChild(2).transform;
        _eAttackSpawner = _childSpawner.GetComponent<EAttackSpawner>();

        bird = GetComponent<BirdStats>();
        animator = GetComponent<Animator>();
        _cashObjectInformation = this.GetComponent<CashObjectInformation>();
        _birdAttack = GameObject.FindWithTag("EnemyController").GetComponent<BirdAttack>();
        _birdStats = this.GetComponent<BirdStats>();

        base.Start();
    }


    /// <summary>
    /// ���G�����o�������Ƃ��̏������֐�
    /// </summary>
    public virtual void BirdEnable()
    {
        // Transform���̎擾
        _startPosition = _transform.position;
        _prevPosition = _transform.position;
        _goalPosition = _goalPositions[_repeatCount];
        CHANGE_SCALE_VALUE = _normalSize / 45f;

        // �X�e�[�W���ʁi�X�e�[�W�n�_�̐��ʂ̋t�� = �G�ɂƂ��Ă̐��ʁj
        _frontAngle = Quaternion.Euler(_stageTransform.forward);
        _eAttackSpawner.StageFrontPosition = -_stageTransform.forward;

        // �ϐ��̏�����
        _startToGoalDistance = (_goalPosition - _startPosition).magnitude;
        _spawn_And_DespawnSize = _normalSize / 5f;
        _movementSpeed = _movementSpeeds[_repeatCount];

        _moveType = _moveTypes[_repeatCount];
        _birdAttackType = _birdAttackTypes_moving[_repeatCount];

        if (_moveType == MoveType.curve)
        {
            _moveSpeedArc = _moveSpeedArcs[_repeatCount];
            _arcMoveDirection = _arcMoveDirections[_repeatCount];
        }

        switch (_birdAttackType)
        {
            case BirdAttackType.equalIntervals:

                _attackIntervalTime = _attackIntervalTimes_moving[5 * _repeatCount];
                break;

            case BirdAttackType.specifySeconds:

                _attackTiming = _attackTimings_moving[_repeatCount];
                break;

            case BirdAttackType.consecutive:

                _attackTimes = _attackTimesList_moving[_repeatCount];
                _cooldownTime = _cooldownTimeList_moving[_repeatCount];
                _attackIntervalTime = _attackIntervalTimes_moving[_repeatCount];
                break;

            default:
                break;
        }

        _needDespawn = false;

        _spawnedTime = Time.time;

        // �����̏�����
        _transform.LookAt(-_stageTransform.forward);

        // �X�|�[�����ɑ傫������
        StartCoroutine(LargerAtSpawn());
    }

    private void OnDisable()
    {
        // ���X�g�̏�����
        _goalPositions.Clear();
        _movementSpeeds.Clear();
        _reAttackTimes.Clear();
        _moveTypes.Clear();
        _moveSpeedArcs.Clear();
        _arcMoveDirections.Clear();
        _birdAttackTypes_moving.Clear();
        _birdAttackTypes_stopping.Clear();
        _attackIntervalTimes_moving.Clear();
        _attackIntervalTimes_stopping.Clear();
        _attackTimings_moving.Clear();
        _attackTimings_stopping.Clear();
        _attackTimesList_moving.Clear();
        _attackTimesList_stopping.Clear();
        _cooldownTimeList_moving.Clear();
        _cooldownTimeList_stopping.Clear();
        _consecutiveIntervalTimes_moving.Clear();
        _consecutiveIntervalTimes_stopping.Clear();
        _directionTypes_moving.Clear();
        _directionTypes_stopping.Clear();
        _directionTypes_attack.Clear();
    }

    /// <summary>
    /// ��ე���
    /// </summary>
    protected bool Paralysing()
    {
        if (bird.Get_isParalysis)
        {
            animator.speed = 0;
            return true;
        }
        else
        {
            animator.speed = 1;
            return false;
        }
    }

    protected override void MoveSequence()
    {
        if (_needDespawn)
            return;

        // �f�X�|�[������
        if (_needRoop && Time.time > _spawnedTime + _despawnTime_s)
        {
            //-----------------------------------------------------------------
            StartCoroutine(SmallerAtDespawn());// �P�މ��o�ς�����
            //-----------------------------------------------------------------
        }


        if (_birdStats.HP <= 0)
        {
            if (_activeAttackCoroutine_moving != null)
                StopCoroutine(_activeAttackCoroutine_moving);

            if (_activeAttackCoroutine_stopping != null)
                StopCoroutine(_activeAttackCoroutine_stopping);

            if (_activeRotateCoroutine_Attack != null)
                StopCoroutine(_activeRotateCoroutine_Attack);

            return;
        }

        // ��჏�Ԃ����肷��i��Ⴢ������瓮���Ȃ��j
        if (Paralysing())
            return;

        // �ړ������i�ړ����������Ă����炱�̃u���b�N�͖��������j-----------------------------------------------------------

        if (!_isFinishMovement)
        {
            // �ړ����Ȃ���w������ɉ�]����i�񓯊��j
            if (!_isRotateCompleted_moving)
            {
                _activeRotateCoroutine_moving = StartCoroutine(RotateCoroutine_Moving());
                _isRotateCompleted_moving = true;
            }

            // �ړ�����
            EachMovement(ref _movedDistance);

            // �ړ����Ȃ���w����@�ōU������i�񓯊��j
            if (!_isAttackCompleted_moving)
            {
                _activeAttackCoroutine_moving = StartCoroutine(AttackCoroutine());
                _isAttackCompleted_moving = true;
            }

            return;
        }
        else
        {
            // ���ݓ����Ă���R���[�`��������Β�~������
            if (_activeRotateCoroutine_moving != null)
                StopCoroutine(_activeRotateCoroutine_moving);

            if (_activeAttackCoroutine_moving != null)
                StopCoroutine(_activeAttackCoroutine_moving);

            // �Ō�̈ړ����I��������A�f�X�|�[��������
            //if (_isLastMove)
            //{
            //    _needDespawn = true;

            //    return;
            //}
        }

        // �U������-----------------------------------------------------------------------------------

        _currentTime += Time.deltaTime;

        if (!_isAttackCompleted_stopping)
        {
            // ��~��̏��������s���A�w����@�ōU�����s���i�񓯊��j
            InitializeForAttack();

            // ���݁i��~���j��AttackType�ɂ���ČĂԃR���[�`����ς���
            if (_birdAttackType == BirdAttackType.none)
            {
                _activeRotateCoroutine_stopping = StartCoroutine(RotateCoroutine_Stopping());
            }
            else
            {
                _activeRotateCoroutine_Attack = StartCoroutine(RotateCoroutine_Attack());
            }

            // �U������i�񓯊��j
            _activeAttackCoroutine_stopping = StartCoroutine(AttackCoroutine());
            _isAttackCompleted_stopping = true;
        }
        //if (_currentTime >= _attackIntervalTime)
        //{
        //    // �U���O�Ƀv���C���[�̕���������
        //    if (_transform.rotation != _childSpawner.rotation)
        //    {
        //        RotateToPlayer();
        //        return;
        //    }

        //    //�@�U�������s
        //    _birdAttack.NormalAttack(ConversionToBulletType(), _childSpawner, _numberOfBullet);
        //    _currentTime = 0f;
        //}

        _currentTime2 += Time.deltaTime;

        // �Ĉړ��̂��߂̃��Z�b�g�����i�U�����X�^�[�g���Ă����莞�Ԍ�Ɏ��s�j-----------------------------------------------

        // �U�����I��
        if (_currentTime2 >= _reAttackTimes[_repeatCount])
        {
            // ���ݓ����Ă���R���[�`��������Β�~������
            if (_activeRotateCoroutine_stopping != null)
                StopCoroutine(_activeRotateCoroutine_stopping);

            if (_activeAttackCoroutine_stopping != null)
                StopCoroutine(_activeAttackCoroutine_stopping);

            // �ݒ肳�ꂽ�S�[���̐���1�̂Ƃ��A���̍s�����Ō�
            //if (_goalPositions.Count == 1)
            //{
            //    // ���̈ړ����Ō�
            //    _isLastMove = true;

            //    return;
            //}

            // �Ĉړ��O�ɐ��ʂ�����
            //if (_transform.rotation != _frontAngle)
            //{
            //    RotateToFront();
            //    return;
            //}

            // ������
            InitializeForRe_Movement();

            // ���ׂẴS�[�����ݒ肳�ꂽ��A���̍s�����Ō�
            //if (_repeatCount + 1 >= _goalPositions.Count)
            //{
            //    _isLastMove = true;
            //}

            _currentTime2 = 0f;
        }
    }

    /// <summary>
    /// �e�G�̓���
    /// <para>�I��������ς��Ȃ��ꍇ��base���Ă�</para>
    /// </summary>
    /// <param name="movedDistance">����������</param>
    protected virtual void EachMovement(ref float movedDistance)
    {
        // �ړ������������甲����i���ړ��ʂƖڕW�ړ��ʂ��r�j
        if (movedDistance >= _startToGoalDistance)
        {
            _isFinishMovement = true;
            movedDistance = 0f;

            return;
        }
    }


    /// <summary>
    /// 2��ڈȍ~�̈ړ������̂��߂̃��Z�b�g����
    /// </summary>
    protected virtual void InitializeForRe_Movement()
    {
        _repeatCount++;

        // �R���[�`���pbool�ϐ��̏�����
        _isAttackCompleted_moving = false;
        _isRotateCompleted_moving = false;

        // �s���񐔂� �ݒ肳�ꂽ�S�[���̐�����������A���[�v�܂��̓f�X�|�[������
        if (_repeatCount >= _goalPositions.Count)
        {
            // ���[�v����
            if (_needRoop)
            {
                // ���[�v���Index��ݒ�
                _repeatCount = _goalIndexOfRoop;
            }
            // ���[�v���Ȃ�
            else
            {
                //-----------------------------------------------------------------
                StartCoroutine(SmallerAtDespawn());// �P�މ��o�ς�����
                //-----------------------------------------------------------------

                return;
            }
        }

        _isFinishMovement = false;
        _isAttackCompleted_stopping = false;

        // �X�^�[�g�ʒu�ƃS�[���ʒu�̍Đݒ�
        _startPosition = _transform.position;
        _goalPosition = _goalPositions[_repeatCount];
        _movementSpeed = _movementSpeeds[_repeatCount];
        _startToGoalDistance = (_goalPosition - _startPosition).magnitude;
        _moveType = _moveTypes[_repeatCount];
        _birdAttackType = _birdAttackTypes_moving[_repeatCount];

        if (_moveType == MoveType.curve)
        {
            _moveSpeedArc = _moveSpeedArcs[_repeatCount];
            _arcMoveDirection = _arcMoveDirections[_repeatCount];
        }

        switch (_birdAttackType)
        {
            case BirdAttackType.equalIntervals:

                _attackIntervalTime = _attackIntervalTimes_moving[_repeatCount];
                break;

            case BirdAttackType.specifySeconds:

                _attackTiming = _attackTimings_moving[MAX_ATTACK_TIMES_FOR_SPECIFY_SECONDS * _repeatCount];
                break;

            case BirdAttackType.consecutive:

                _attackTimes = _attackTimesList_moving[_repeatCount];
                _cooldownTime = _cooldownTimeList_moving[_repeatCount];
                _consecutiveIntervalTime = _consecutiveIntervalTimes_moving[_repeatCount];
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// �U���p�ϐ��̏�����
    /// </summary>
    private void InitializeForAttack()
    {
        _birdAttackType = _birdAttackTypes_stopping[_repeatCount];

        switch (_birdAttackType)
        {
            case BirdAttackType.equalIntervals:

                _attackIntervalTime = _attackIntervalTimes_stopping[_repeatCount];
                break;

            case BirdAttackType.specifySeconds:

                _attackTiming = _attackTimings_stopping[MAX_ATTACK_TIMES_FOR_SPECIFY_SECONDS * _repeatCount];
                break;

            case BirdAttackType.consecutive:

                _attackTimes = _attackTimesList_stopping[_repeatCount];
                _cooldownTime = _cooldownTimeList_stopping[_repeatCount];
                _consecutiveIntervalTime = _consecutiveIntervalTimes_stopping[_repeatCount];
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// �I�����ꂽ�����ւƉ�]���s�������i�ړ����j
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateCoroutine_Moving()
    {
        switch (_directionTypes_moving[_repeatCount])
        {
            // �v���C���[�̕���������
            case DirectionType_AtMoving.player:
                _eAttackSpawner.AttackDirection = DirectionType_AtAttack.player;

                while (true)
                {
                    RotateToPlayer();
                    yield return null;
                }

            // ���[���h���ʂ�����
            case DirectionType_AtMoving.front:
                _eAttackSpawner.AttackDirection = DirectionType_AtAttack.front;

                while (_transform.rotation != _frontAngle)
                {
                    RotateToFront();
                    yield return null;
                }

                yield break;

            // �i�s����������
            case DirectionType_AtMoving.moveDirection:

                while (true)
                {
                    RotateToMoveDirection();
                    yield return null;
                }
        }
    }

    /// <summary>
    /// �I�����ꂽ�����ւƉ�]���s�������i��~���j
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateCoroutine_Stopping()
    {
        switch (_directionTypes_stopping[_repeatCount])
        {
            // ���݂̕������p��
            case DirectionType_AtStopping.continuation:
                yield break;

            // �v���C���[�̕���������
            case DirectionType_AtStopping.player:
                _eAttackSpawner.AttackDirection = DirectionType_AtAttack.player;

                while (true)
                {
                    RotateToPlayer();
                    yield return null;
                }

            // ���[���h���ʂ�����
            case DirectionType_AtStopping.front:
                _eAttackSpawner.AttackDirection = DirectionType_AtAttack.front;

                while (true)
                {
                    RotateToFront();
                    yield return null;
                }
        }
    }

    /// <summary>
    /// �I�����ꂽ�����ւƉ�]���s�������i�U���j
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateCoroutine_Attack()
    {
        _eAttackSpawner.AttackDirection = _directionTypes_attack[_repeatCount];

        // DirectionType_AtAttack�̒l�ɂ�����炸�A�X�|�i�[�̈ʒu�ɍ��킹��
        while (_transform.rotation != _childSpawner.rotation)
        {
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _childSpawner.rotation, Time.deltaTime * _rotateSpeed);
            yield return null;
        }
    }

    /// <summary>
    /// �U���R���[�`��
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCoroutine()
    {
        switch (_birdAttackType)
        {
            // ���Ԋu
            case BirdAttackType.equalIntervals:

                while (true)
                {
                    // �ݒ肳�ꂽ���ԊԊu�ōU��
                    if (_currentTime >= _attackIntervalTime)
                    {
                        // �U�������s
                        _birdAttack.NormalAttack(ConversionToBulletType(), _childSpawner, _numberOfBullet);
                        _currentTime = 0f;
                    }

                    yield return null;
                }

            // �w��b��
            case BirdAttackType.specifySeconds:

                _currentTime = 0f;
                int attackCount = 0;

                while (true)
                {
                    // �ݒ肳�ꂽ���ԂɒB������U��
                    if (_currentTime >= _attackTiming)
                    {
                        // �U�������s
                        _birdAttack.NormalAttack(ConversionToBulletType(), _childSpawner, _numberOfBullet);

                        attackCount++;

                        // ���̍U���^�C�~���O����
                        _attackTiming = _attackTimings_stopping[5 * _repeatCount + attackCount];

                        // �ő�U���������s�ς� OR �ݒ肳�ꂽ�b����0�ȉ��ŏI��
                        if (attackCount >= MAX_ATTACK_TIMES_FOR_SPECIFY_SECONDS || _attackTiming <= 0f)
                            yield break;

                        _currentTime = 0f;
                    }

                    yield return null;
                }

            // �A���U��
            case BirdAttackType.consecutive:

                _waitTimeOfConsecutiveAttack = new WaitForSeconds(_consecutiveIntervalTime);

                while (true)
                {
                    // �N�[���^�C���𒴂�����U��
                    if (_currentTime >= _cooldownTime)
                    {
                        for (int i = 0; i < _attackTimes; i++)
                        {
                            // �U�������s���āA�ݒ肳�ꂽ���ԑҋ@����
                            _birdAttack.NormalAttack(ConversionToBulletType(), _childSpawner, _numberOfBullet);
                            yield return _waitTimeOfConsecutiveAttack;
                        }

                        _currentTime = 0f;
                    }

                    yield return null;
                }

            default:
                yield break;
        }
    }

    /// <summary>
    /// �v���C���[�̕���������
    /// </summary>
    protected void RotateToPlayer()
    {
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _childSpawner.rotation, Time.deltaTime * _rotateSpeed);
    }

    /// <summary>
    /// ���[���h���ʂ�����
    /// </summary>
    protected void RotateToFront()
    {
        //_transform.rotation = Quaternion.RotateTowards(_transform.rotation, _frontAngle, Time.deltaTime * _rotateSpeed);
        _transform.rotation = Quaternion.LookRotation(-_stageTransform.forward, UP);
    }

    /// <summary>
    /// �i�s����������
    /// </summary>
    private void RotateToMoveDirection()
    {
        //_transform.rotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.Euler(_goalPosition - _startPosition), Time.deltaTime * _rotateSpeed);

        _delta = _transform.position - _prevPosition;

        _prevPosition = _transform.position;

        if (_delta == ZERO)
            return;

        _transform.rotation = Quaternion.LookRotation(_delta, UP);
    }

    /// <summary>
    /// �o�����ɂ��񂾂�傫���Ȃ鏈��
    /// </summary>
    protected IEnumerator LargerAtSpawn()
    {
        _transform.localScale = _spawn_And_DespawnSize;

        // Scale���ʏ�T�C�Y��菬�����ԁA�傫������i����̂��߂�x��p����j
        while (_transform.localScale.x < _normalSize.x)
        {
            _transform.localScale += CHANGE_SCALE_VALUE;

            yield return _changeSizeWait;
        }

        yield break;
    }

    /// <summary>
    /// ���Ŏ��ɂ��񂾂񏬂����Ȃ鏈��
    /// </summary>
    public IEnumerator SmallerAtDespawn()
    {
        _needDespawn = true;

        // Scale�����Ŏ��̃T�C�Y���傫���ԁA����������i����̂��߂�x��p����j
        while (_transform.localScale.x > _spawn_And_DespawnSize.x)
        {
            _transform.localScale -= CHANGE_SCALE_VALUE;

            yield return _changeSizeWait;
        }

        _birdStats.Despawn();
    }

    /// <summary>
    /// ���g�̓G�̎�ނ���A�Ή�����e�̎�ނ�Ԃ��ienum)
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

        // ��O����
        return PoolEnum.PoolObjectType.normalBullet;
    }


    /// <summary>
    /// �����̓���
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
    //    // ���E�̈ړ�
    //    _idleMove = Vector3.right * (Mathf.Sin((Time.time + _offsetReverse)
    //        * IDLE_PENDULUM_REVERSE_SPEED) * _idleMoveSpeedY * Time.deltaTime);

    //    // ���ʂ̈ړ�
    //    _idleMove += IDLE_MOVE_Z_SPEED * Time.deltaTime * Vector3.forward;

    //    _transform.Translate(_idleMove);
    //}
    #endregion
}