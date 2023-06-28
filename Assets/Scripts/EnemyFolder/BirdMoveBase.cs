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
    [Tooltip("���g�̓G�̎��")]
    protected CashObjectInformation _cashObjectInformation = default;

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

    [Tooltip("�X�^�[�g�ƃS�[���Ԃ̋��� = �ڕW�ړ���")]
    protected float _startToGoalDistance = default;

    [Tooltip("����������")]
    protected float _movedDistance = default;

    [Tooltip("�����ړ��̃X�s�[�h")]
    protected float _linearMovementSpeed = 20f;

    [Tooltip("�v���C���[�̕������������x")]
    protected float _rotateSpeed = 100f;

    [Tooltip("�ړ��I���i�S�[���ɓ��B�j")]
    protected bool _isFinishMovement = false;

    [Tooltip("���ł�����i�|���Ȃ������ꍇ�j")]
    protected bool _needDespawn = false;

    [Tooltip("�o����/���Ŏ��̑傫��")]
    protected Vector3 _spawn_And_DespawnSize = default;

    [Tooltip("�ʏ�̑傫��")]
    protected Vector3 _normalSize = default;

    [Tooltip("Scale�ς���Ƃ��̃u���C�N")]
    // ���ꂪ�Ȃ��ƃ��[�v���������ĖڂɌ����Ȃ�
    protected WaitForSeconds _changeSizeWait = new WaitForSeconds(0.01f);

    [Tooltip("Scale�̕ύX������")]
    protected bool _isCompleteChangeScale = false;

    [Tooltip("���̃C���X�^���X�̃C���f�b�N�X")]
    // ex) ���E�F�[�u�ɓG��2�̂���ꍇ�AInstance_0 = 0, Instance_1 = 1���ݒ肳���
    protected int _thisInstanceIndex = default;

    [Tooltip("�ǂ̃E�F�[�u�ŃX�|�[��������")]
    protected WaveType _spawnedWave = default;

    [Tooltip("�ݒ肳�ꂽ�S�[���̐�")]
    protected int _numberOfGoal = default;

    [Tooltip("�Ăѓ����o���܂ł̎���")]
    protected float _reAttackTime = 10f;

    [Tooltip("Scale�̉��Z/���Z�l")]
    protected readonly Vector3 CHANGE_SCALE_VALUE = new Vector3(0.05f, 0.05f, 0.05f);   // �������ς��

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
    public float LinearMovementSpeed
    {
        set
        {
            _linearMovementSpeed = value;
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
        // �v�[�����������ꂽ�u�Ԃ�1�񂾂��͂�����������
        catch (Exception)
        {
            _transform = this.transform;
            _startPosition = _transform.position;
            _spawn_And_DespawnSize = _transform.localScale / 5f;
            _normalSize = _transform.localScale;    // �L���b�V��
        }

        // ���Z�b�g
        _interpolationRatio = 0f;
        _needDespawn = false;
        _isCompleteChangeScale = false;
        _movedDistance = 0f;

        // �X�|�[�����ɑ傫������
        StartCoroutine(LargerAtSpawn());
    }

    protected virtual void Start()
    {
        bird = GetComponent<BirdStats>();

        animator = GetComponent<Animator>();

        // �q�I�u�W�F�N�g�́uSpawnPosition�v
        _childSpawner = _transform.GetChild(2).transform;

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

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

    private void Test()
    {
        // ���E�̈ړ�
        _idleMove = Vector3.right * (Mathf.Sin((Time.time + _offsetReverse)
            * IDLE_PENDULUM_REVERSE_SPEED) * _idleMoveSpeedY * Time.deltaTime);

        // ���ʂ̈ړ�
        _idleMove += IDLE_MOVE_Z_SPEED * Time.deltaTime * Vector3.forward;

        _transform.Translate(_idleMove);
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
        // �ړ������������甲����i���ړ��ʂƖڕW�ړ��ʂ��r�j
        if (_movedDistance >= _startToGoalDistance)
        {
            X_Debug.Log("���̈ړ�����");
            IsFinishMovement = true;
            _movedDistance = 0f;

            return;
        }

        // �ړ�����i�ړ������̃x�N�g�� * �ړ����x�j
        _transform.Translate((_goalPosition - _startPosition).normalized * _linearMovementSpeed * Time.deltaTime, Space.World); �@// �������Ȃ��ƃo�O��
        // �ړ��ʂ����Z
        _movedDistance += ((_goalPosition - _startPosition).normalized * _linearMovementSpeed * Time.deltaTime).magnitude;


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
    /// <param name="spawnedNumber">�C���X�^���X�ԍ��i_spawnedNumber��n���j�i_spawn</param>
    /// <param name="howManyTimes">���̊֐����ĂԂ͉̂���ځH�i�����F���O�t�������j</param>
    protected virtual void SetGoalPosition(WaveType zakoWaveNumber, int spawnedNumber ,int howManyTimes = 1)
    {
        try
        {
            _goalPosition = _stageManager._enemySpawnerTable._scriptableWaveEnemy[(int)zakoWaveNumber]._enemysSpawner[spawnedNumber]._birdGoalPlaces[howManyTimes - 1].position;
            _startToGoalDistance = (_goalPosition - _startPosition).magnitude;
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
        }

        // ��O����
        return PoolEnum.PoolObjectType.normalBullet;
    }
}