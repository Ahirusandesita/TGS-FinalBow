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

    [Tooltip("�擾����BirdAttack�N���X")]
    private BirdAttack _birdAttack = default;

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

    [Tooltip("���̈ړ������ŏ����I��")]
    private bool _isLastMove = false;

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

    [Tooltip("�U���̕p�x���X�g�i�ړ����j")]
    private List<float> _attackIntervalTimes_moving = new();

    [Tooltip("�U���̕p�x���X�g�i��~���j")]
    private List<float> _attackIntervalTimes_stopping = new();

    private bool _isAttackCompleted_moving = false;
    private bool _isAttackCompleted_stopping = false;
    private Coroutine _activeCoroutine_moving = default;
    private Coroutine _activeCoroutine_stopping = default;
    private WaitForSeconds _waitTimeOfConsecutiveAttack = default;


    [Tooltip("Scale�̉��Z/���Z�l")]
    private readonly Vector3 CHANGE_SCALE_VALUE = new Vector3(0.05f, 0.05f, 0.05f);   // �������ς��

    [Tooltip("���ʂ̊p�x")]
    private readonly Quaternion FRONT_ANGLE = Quaternion.Euler(new Vector3(0f, 180f, 0f));

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
    /// �U���̕p�x�i�ړ����j
    /// </summary>
    public float AttackIntervalTimes_Moving
    {
        set
        {
            _attackIntervalTimes_moving.Add(value);
        }
    }

    /// <summary>
    /// �U���̕p�x�i��~���j
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
    #endregion


    #region method

    protected override void Start()
    {
        base.Start();

        // Transform���̎擾
        _startPosition = _transform.position;
        _goalPosition = _goalPositions[_repeatCount];
        _normalSize = _transform.localScale;    // �L���b�V��

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

        bird = GetComponent<BirdStats>();

        animator = GetComponent<Animator>();

        // �q�I�u�W�F�N�g�́uSpawnPosition�v
        _childSpawner = _transform.GetChild(2).transform;

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        _birdAttack = GameObject.FindWithTag("EnemyController").GetComponent<BirdAttack>();


        // �X�|�[�����ɑ傫������
        StartCoroutine(LargerAtSpawn());
    }


    /// <summary>
    /// ��ე���
    /// </summary>
    protected void Paralysing()
    {
        if (bird.Get_isParalysis)
        {
            animator.speed = 0;
            return;
        }
        else
        {
            animator.speed = 1;
        }
    }

    protected override void MoveSequence()
    {
        // ��჏�Ԃ����肷��i��Ⴢ������瓮���Ȃ��j
        Paralysing();

        _currentTime += Time.deltaTime;

        // �ړ������i�ړ����������Ă����炱�̃u���b�N�͖��������j-----------------------------------------------------------

        if (!_isFinishMovement)
        {
            // �ړ�����
            EachMovement(ref _movedDistance);

            if (!_isAttackCompleted_moving)
            {
                _activeCoroutine_moving = StartCoroutine(AttackCoroutine());
                _isAttackCompleted_moving = true;
            }

            return;
        }
        else
        {
            _isAttackCompleted_moving = false;

            if (_activeCoroutine_moving != null)
                StopCoroutine(_activeCoroutine_moving);

            // �Ō�̈ړ����I��������A�f�X�|�[��������
            if (_isLastMove)
            {
                _needDespawn = true;

                return;
            }
        }

        // �U�������i���Ԋu�Ŏ��s�����j-----------------------------------------------------------------------------------

        if (!_isAttackCompleted_stopping)
        {
            InitializeForAttack();
            _activeCoroutine_stopping = StartCoroutine(AttackCoroutine());
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
            if (_activeCoroutine_stopping != null)
                StopCoroutine(_activeCoroutine_stopping);

            // �ݒ肳�ꂽ�S�[���̐���1�̂Ƃ��A���̍s�����Ō�
            if (_goalPositions.Count == 1)
            {
                // ���̈ړ����Ō�
                _isLastMove = true;

                return;
            }

            // �Ĉړ��O�ɐ��ʂ�����
            if (_transform.rotation != FRONT_ANGLE)
            {
                RotateToFront();
                return;
            }

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
            X_Debug.Log("���̈ړ�����");
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

        // �s���񐔂� �ݒ肳�ꂽ�S�[���̐�����������A���[�v����
        if (_repeatCount >= _goalPositions.Count)
        {
            // ���[�v���Index��ݒ�
            _repeatCount = 0;
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
                _attackIntervalTime = _attackIntervalTimes_moving[_repeatCount];
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

                _attackTiming = _attackIntervalTimes_stopping[MAX_ATTACK_TIMES_FOR_SPECIFY_SECONDS * _repeatCount];
                break;

            case BirdAttackType.consecutive:

                _attackTimes = _attackTimesList_stopping[_repeatCount];
                _cooldownTime = _cooldownTimeList_stopping[_repeatCount];
                _attackIntervalTime = _attackIntervalTimes_stopping[_repeatCount];
                break;

            default:
                break;
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
                    }

                    yield return null;
                }

            // �A���U��
            case BirdAttackType.consecutive:

                _waitTimeOfConsecutiveAttack = new WaitForSeconds(_attackIntervalTime);

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
    /// �v���C���[�̕����������iUpdate�ŌĂԁj
    /// </summary>
    protected void RotateToPlayer()
    {
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _childSpawner.rotation, Time.deltaTime * _rotateSpeed);
    }

    /// <summary>
    /// ���[���h���ʂ������iUpdate�ŌĂԁj
    /// </summary>
    protected void RotateToFront()
    {
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, FRONT_ANGLE, Time.deltaTime * _rotateSpeed);
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
        // Scale�����Ŏ��̃T�C�Y���傫���ԁA����������i����̂��߂�x��p����j
        while (_transform.localScale.x > _spawn_And_DespawnSize.x)
        {
            _transform.localScale -= CHANGE_SCALE_VALUE;

            yield return _changeSizeWait;
        }

        _isCompleteChangeScale = true;

        yield break;
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