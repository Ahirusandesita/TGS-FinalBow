// --------------------------------------------------------- 
// BirdMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using System;
using System.Collections;
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

    [Tooltip("�ړ��̃X�s�[�h")]
    protected float _movementSpeed = default;


    [Tooltip("���g�̓G�̎��")]
    private CashObjectInformation _cashObjectInformation = default;

    [Tooltip("�擾����StageManager")]
    private StageManager _stageManager = default;

    [Tooltip("�q�I�u�W�F�N�g�ɂ���X�|�i�[���擾")]
    private Transform _childSpawner = default;

    [Tooltip("�擾����BirdAttack�N���X")]
    private BirdAttack _birdAttack = default;

    [Tooltip("�X�^�[�g�ƃS�[���Ԃ̋��� = �ڕW�ړ���")]
    private float _startToGoalDistance = default;

    [Tooltip("����������")]
    private float _movedDistance = default;

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

    [Tooltip("���̃C���X�^���X�̃C���f�b�N�X")]
    // ex) ���E�F�[�u�ɓG��2�̂���ꍇ�AInstance_0 = 0, Instance_1 = 1���ݒ肳���
    private int _thisInstanceIndex = default;

    [Tooltip("�ǂ̃E�F�[�u�ŃX�|�[��������")]
    private WaveType _spawnedWave = default;

    [Tooltip("�ݒ肳�ꂽ�S�[���̐�")]
    private int _numberOfGoal = default;

    [Tooltip("�Ăѓ����o���܂ł̎���")]
    private float _reAttackTime = 10f;

    [Tooltip("�o���e�̐�")]
    private int _numberOfBullet = default;

    [Tooltip("�U���̕p�x")]
    private float _attackIntervalTime = 2f;

    [Tooltip("���݂̌o�ߎ��ԁi�U���܂ł̕p�x�Ɏg���j")]
    private float _currentTime = 0f;

    [Tooltip("���݂̌o�ߎ��ԁi�Ăѓ����o���܂ł̎��ԂɎg���j")]
    private float _currentTime2 = 0f;

    [Tooltip("���̈ړ������ŏ����I��")]
    private bool _isLastMove = default;

    [Tooltip("����ڂ̃S�[���ݒ肩")]
    private int _howTimesSetGoal = 1;  // �����l1


    [Tooltip("Scale�̉��Z/���Z�l")]
    private readonly Vector3 CHANGE_SCALE_VALUE = new Vector3(0.05f, 0.05f, 0.05f);   // �������ς��

    [Tooltip("���ʂ̊p�x")]
    private readonly Quaternion FRONT_ANGLE = Quaternion.Euler(new Vector3(0f, 180f, 0f));
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
    /// ���̃C���X�^���X�̃C���f�b�N�X
    /// </summary>
    public int ThisInstanceIndex
    {
        set
        {
            _thisInstanceIndex = value;
        }
    }

    /// <summary>
    /// �ǂ̃E�F�[�u�ŃX�|�[��������
    /// </summary>
    public WaveType SpawnedWave
    {
        set
        {
            _spawnedWave = value;
        }
    }

    /// <summary>
    /// �ݒ肳�ꂽ�S�[���̐�
    /// </summary>
    public int NumberOfGoal
    {
        set
        {
            _numberOfGoal = value;
        }
    }

    /// <summary>
    /// �����ړ��̃X�s�[�h
    /// </summary>
    public float MovementSpeed
    {
        set
        {
            _movementSpeed = value;
        }
    }

    /// <summary>
    /// �Ăѓ����o���܂ł̎���
    /// </summary>
    public float ReAttackTime
    {
        set
        {
            _reAttackTime = value;
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
    /// �U���̕p�x
    /// </summary>
    public float AttackIntervalTime
    {
        set
        {
            _attackIntervalTime = value;
        }
    }
    #endregion


    #region method

    //private void Awake()
    //{
    //    _offsetReverse = UnityEngine.Random.Range(0, OFFSET_TIME_RANGE);
    //}

    protected override void Start()
    {
        base.Start();

        // Transform���̎擾
        _startPosition = _transform.position;
        _spawn_And_DespawnSize = _transform.localScale / 5f;
        _normalSize = _transform.localScale;    // �L���b�V��


        bird = GetComponent<BirdStats>();

        animator = GetComponent<Animator>();

        // �q�I�u�W�F�N�g�́uSpawnPosition�v
        _childSpawner = _transform.GetChild(2).transform;

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        _birdAttack = GameObject.FindWithTag("EnemyController").GetComponent<BirdAttack>();

        _stageManager = GameObject.FindWithTag("StageController").GetComponent<StageManager>();


        // �ϐ��̏�����
        _isFinishMovement = false;
        _needDespawn = false;
        _isCompleteChangeScale = false;
        _movedDistance = 0f;
        _isLastMove = false;
        _currentTime = 0f;
        _currentTime2 = 0f;


        // �����̃S�[����ݒ�
        SetGoalPosition(_spawnedWave, _thisInstanceIndex, howManyTimes: _howTimesSetGoal);
        _howTimesSetGoal++;


        // ����Inspector�Őݒ�~�X���������牼�ݒ肷��
        if (_numberOfBullet < 0)
        {
            _numberOfBullet = 3;
            X_Debug.LogError("EnemySpawnPlaceData.Bullet ���ݒ肳��Ă܂���");
        }

        if (_movementSpeed == 0f)
        {
            _movementSpeed = 20f;
            X_Debug.LogError("EnemySpawnPlaceData.Speed ���ݒ肳��Ă܂���");
        }

        if (_reAttackTime == 0f)
        {
            _reAttackTime = 5f;
            X_Debug.LogError("EnemySpawnPlaceData.StayTime_s ���ݒ肳��Ă܂���");
        }

        if (_attackIntervalTime == 0f)
        {
            _attackIntervalTime = 2f;
            X_Debug.LogError("EnemySpawnPlaceData.AttackInterval_S ���ݒ肳��Ă܂���");
        }

        // �X�|�[�����ɑ傫������
        StartCoroutine(LargerAtSpawn());
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
    /// ��გ�
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

    protected override void MoveSequence()
    {
        // ��჏�Ԃ����肷��i��Ⴢ������瓮���Ȃ��j
        if (Paralysing())
        {
            return;
        }

        _currentTime += Time.deltaTime;

        // �ړ������i�ړ����������Ă����炱�̃u���b�N�͖��������j-----------------------------------------------------------

        if (!_isFinishMovement)
        {
            // �ړ�����
            EachMovement(ref _movedDistance);

            return;
        }
        else
        {
            // �Ō�̈ړ����I��������A�f�X�|�[��������
            if (_isLastMove)
            {
                _needDespawn = true;

                // �N���X���͂���
                Destroy(this);

                return;
            }
        }

        // �U�������i���Ԋu�Ŏ��s�����j-----------------------------------------------------------------------------------

        if (_currentTime >= _attackIntervalTime)
        {
            // �U���O�Ƀv���C���[�̕���������
            if (_transform.rotation != _childSpawner.rotation)
            {
                RotateToPlayer();
                return;
            }

            //�@�U�������s
            _birdAttack.NormalAttack(_childSpawner, ConversionToBulletType(), _numberOfBullet);
            _currentTime = 0f;
        }

        _currentTime2 += Time.deltaTime;

        // �Ĉړ��̂��߂̃��Z�b�g�����i�U�����X�^�[�g���Ă����莞�Ԍ�Ɏ��s�j-----------------------------------------------

        // �U�����I��
        if (_currentTime2 >= _reAttackTime)
        {
            // �ݒ肳�ꂽ�S�[���̐���1�̂Ƃ� AND ���ׂẴS�[�����ݒ肳�ꂽ��A���̍s�����Ō�
            if (_numberOfGoal == 1 && _howTimesSetGoal > _numberOfGoal)
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
            if (_howTimesSetGoal > _numberOfGoal)
            {
                _isLastMove = true;
            }

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
        _isFinishMovement = false;

        // �X�^�[�g�ʒu�ƃS�[���ʒu�̍Đݒ�
        _startPosition = _transform.position;
        SetGoalPosition(_spawnedWave, _thisInstanceIndex, howManyTimes: _howTimesSetGoal);
        _howTimesSetGoal++;
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
    /// _goalPosition�ϐ��̑������
    /// </summary>
    /// <param name="zakoWaveNumber">�ǂ̃E�F�[�u�̓G�̓�����enum�Ŏw��iex: BirdMoveFirst��zakoWave1�j</param>
    /// <param name="spawnedNumber">�C���X�^���X�ԍ��i_spawnedNumber��n���j�i_spawn</param>
    /// <param name="howManyTimes">���̊֐����ĂԂ͉̂���ځH�i�����F���O�t�������j</param>
    protected virtual void SetGoalPosition(WaveType zakoWaveNumber, int spawnedNumber ,int howManyTimes = 1)
    {
        try
        {
            _goalPosition = _stageManager._waveManagementTable._waveInformation[(int)zakoWaveNumber]._enemysData[spawnedNumber]._birdGoalPlaces[howManyTimes - 1]._birdGoalPlace.position;
            _startToGoalDistance = (_goalPosition - _startPosition).magnitude;
        }
        catch (Exception)
        {
            X_Debug.LogError("�S�[�����W��EnemySpawnerTable(Scriptable)�ɐݒ肳��Ă��Ȃ����A�Ăяo���\�񐔂𒴂��Ă��܂�");
        }
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