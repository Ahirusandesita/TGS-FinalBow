// --------------------------------------------------------- 
// StageManager.cs 
// 
// CreateDay: 2023/06/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�X�|�[���@�\�̒�`
/// </summary>
interface IStageSpawn
{
    /// <summary>
    /// �E�F�[�u�̎��s����
    /// </summary>
    void WaveExecution();
}

public class StageManager : MonoBehaviour, IStageSpawn
{
    /// <summary>
    /// �X�e�[�W���N���X
    /// </summary>
    [Serializable]
    public class StageInformation
    {
        [Tooltip("�X�e�[�W��")]
        public string _stageName;

        [Tooltip("�e�X�e�[�W�̃X�^�[�g�n�_")]
        public Transform _stageTransform;
    }

    #region �ϐ�
    [SerializeField, Tooltip("�^�O�̖��O")]
    private TagObject _PoolSystemTagData = default;

    [SerializeField, Tooltip("�e�X�e�[�W�̃X�^�[�g�n�_���X�g")]
    private List<StageInformation> _stageTransforms = new();

    [SerializeField, Tooltip("Wave�Ԃ̑҂�����")]
    private float _waveWait_s = 1.5f;

    [SerializeField, Tooltip("�G�̃X�|�[�����W�e�[�u��")]
    private List<StageDataTable> _stageDataTables = new();

    [SerializeField, Tooltip("�{�X�̃v���n�u")]
    private GameObject _bossPrefab;

    [SerializeField, Tooltip("�v���C���[")]
    private GameObject _player = default;

    [SerializeField, Tooltip("�f�o�b�O�J����")]
    private GameObject _debugPlayer = default;

    [SerializeField, Tooltip("���U���g�pCanvas")]
    private GameObject _resultCanvas = default;

    [SerializeField, Tooltip("�Q�[���X�^�[�g�pCanvas")]
    private GameObject _startCanvas = default;

    [SerializeField, Tooltip("Canvas�̈ʒu���v���C���[�̈ʒu����ǂꂾ��������")]
    private float _canvasPositionCorrectionValue = 50f;

    [SerializeField]
    private SceneObject _sceneObject = default;


    [Tooltip("�擾����ObjectPoolSystem�N���X")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("�擾�������U���g�p�N���X")]
    private ResultStage _resultStage = default;
    private GameProgress gameProgress;

    [Tooltip("���݂̎G��/�I�̐�")]
    private int _currentNumberOfObject = 0;

    [Tooltip("���݂̃X�e�[�W�ԍ�")]
    private int _currentStageIndex = 0; // �`���[�g���A���X�e�[�W

    [Tooltip("���݂̃E�F�[�u�ԍ�")]
    private int _currentWaveIndex = 0;  // �E�F�[�u1
    #endregion

    private void Awake()
    {
        gameProgress = GameObject.FindObjectOfType<GameProgress>();
        gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if (progressType == GameProgressType.ending)
                {
                    ProgressingTheStage();
                }
                if (progressType == GameProgressType.inGame)
                {
                    StartCoroutine(WaveStart());
                }
                if (progressType == GameProgressType.gamePreparation)
                {
                    MovingPlayer();
                    MovingStartCanvas();
                }
            }
            );

    }

    private void Start()
    {
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();
        _resultStage = this.GetComponent<ResultStage>();

        // �X�e�[�W�ԃ��U���g�̕\�����I�������Ƃ��AResultStage�N���X�ŃX�e�[�W�i�s�������Ă΂��
        //_resultStage.readOnlyStateProperty.Subject.Subscribe(isResult => { if (!isResult) { ProgressingTheStage(); } });

        // �Q�[���J�n���Ƀv���C���[��Transform���X�V����
        //MovingPlayer();

        // �Q�[���X�^�[�g
        //StartCoroutine(WaveStart());
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EnemyStats[] obj = FindObjectsOfType<EnemyStats>();

            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].TakeDamage(1);
            }
        }
    }
#endif


    public void WaveExecution()
    {
        try
        {
            // �ŏI�X�e�[�W��������A�{�X���X�|�[��
            //if (_currentStageIndex == _stageDataTables.Count)
            //{
            //    // �{�X���X�|�[��
            //    Instantiate(_bossPrefab);
            //    return;
            //}

            // EnemySpawnerTable�Őݒ肵���X�|�i�[�̐���ݒ�
            _currentNumberOfObject += _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._birdsData.Count;
            _currentNumberOfObject += _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._groundEnemysData.Count;

            // �e�E�F�[�u�Őݒ肳�ꂽ���A���G�����X�|�[��������
            for (int i = 0; i < _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._birdsData.Count; i++)
            {
                // �v�[������Ăяo��
                StartCoroutine(SpawnBird(listIndex: i));
            }

            // �e�E�F�[�u�Őݒ肳�ꂽ���A�n��G�����X�|�[��������
            for (int i = 0; i < _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._groundEnemysData.Count; i++)
            {
                StartCoroutine(SpawnGroundEnemy(listIndex: i));
            }
        }
        catch (Exception)
        {
            X_Debug.LogError("�������Ă���E�F�[�u�𒴂��Ă��邩�A�Q�Ɛ�̃A�^�b�`���O��Ă��܂�");
        }
    }

    /// <summary>
    /// �X�|�[���������I�u�W�F�N�g�̃f�N�������g����
    /// </summary>
    private void DecrementNumberOfObject()
    {
        _currentNumberOfObject--;

        if (_currentNumberOfObject == 0)
        {
            // ���̃E�F�[�u��
            ProgressingTheWave();
        }
    }

    /// <summary>
    /// �E�F�[�u��i�߂�
    /// </summary>
    private void ProgressingTheWave()
    {
        _currentWaveIndex++;
        _currentNumberOfObject = 0;
        X_Debug.Log("���̃E�F�[�u��");

        // ���ׂẴE�F�[�u���N���A = ���̃X�e�[�W�ɐi��
        if (_currentWaveIndex >= _stageDataTables[_currentStageIndex]._waveInformation.Count)
        {
            // ���̃X�e�[�W��
            X_Debug.Log("�X�e�[�W�N���A");
            ProgressingTheStage();
            return;
        }

        StartCoroutine(WaveStart());
    }

    /// <summary>
    /// �X�e�[�W��i�߂�
    /// </summary>
    private void ProgressingTheStage()
    {
        _currentStageIndex++;
        _currentWaveIndex = 0;
        X_Debug.Log("���̃X�e�[�W��");

        //-------------------------------------------------------------------
        // �{�X�����Ȃ��Ƃ��̓}�C�i�X1
        //-------------------------------------------------------------------
        if (_currentStageIndex > _stageDataTables.Count - 1)
        {
            // ���ׂẴX�e�[�W���N���A = �Q�[���I�[�o�[
            // �{�X�|������I�������炱�������Ȃ�����
            //FindObjectOfType<SceneManagement>().SceneLoadSpecifyMove(_sceneObject);
            MovingResultCanvas();
            gameProgress.InGameEnding();
            X_Debug.Log("�Q�[���I��");
            return;
        }

        MovingPlayer();
        StartCoroutine(WaveStart());
    }

    /// <summary>
    /// �E�F�[�u�J�n�i�Œ�b���҂��Ă���X�|�[���J�n�j
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaveStart()
    {
        // �ݒ肳�ꂽ�b�����o�߂�����A�X�e�[�W�X�^�[�g�i�ǂݍ��ݑ҂��j
        yield return new WaitForSeconds(_stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._startDelay_s);

        WaveExecution();
    }

    /// <summary>
    /// ���G�����o��������R���[�`��
    /// </summary>
    /// <param name="listIndex">���X�g�̓Y�����ifor����i�����̂܂ܓn���j</param>
    /// <returns></returns>
    private IEnumerator SpawnBird(int listIndex)
    {
        PoolEnum.PoolObjectType selectedPrefab;
        BirdDataTable birdDataPath = _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._birdsData[listIndex];

        // �ݒ肳�ꂽ�b�������ҋ@����
        yield return new WaitForSeconds(birdDataPath._spawnDelay_s);


        // �ǂ̓G���X�|�[�������邩����iScriptable����擾�j
        switch (birdDataPath._birdType)
        {
            // �ȉ�Enum�̕ϊ�����
            case BirdType.normalBird:
                selectedPrefab = PoolEnum.PoolObjectType.normalBird;
                break;

            case BirdType.bomberBug:
                selectedPrefab = PoolEnum.PoolObjectType.bomberBug;
                break;

            case BirdType.watcher:
                selectedPrefab = PoolEnum.PoolObjectType.watcher;
                break;

            case BirdType.giantBat:
                selectedPrefab = PoolEnum.PoolObjectType.giantBat;
                break;

            case BirdType.oceanDragon:
                selectedPrefab = PoolEnum.PoolObjectType.oceanDragon;
                break;

            case BirdType.desertDragon:
                selectedPrefab = PoolEnum.PoolObjectType.DesertDragon;
                break;

            // ��O����
            default:
                selectedPrefab = PoolEnum.PoolObjectType.normalBird;
                break;
        }


        // �G�����v�[������Ăяo���A�Ăяo�����e�G���̃f���Q�[�g�ϐ��Ƀf�N�������g�֐���o�^
        GameObject temporaryObject = _objectPoolSystem.CallObject(selectedPrefab, birdDataPath._birdSpawnPlace.position).gameObject;
        temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;

        BirdMoveComponents birdMove = temporaryObject.GetComponent<BirdMoveComponents>();

        // �Ăяo�����G���̕ϐ��ɐݒ�
        birdMove.NumberOfBullet = birdDataPath._bullet;
        birdMove.NeedRoop = birdDataPath._needRoop;
        birdMove.GoalIndexOfRooop = birdDataPath._goalIndexOfRoop;
        birdMove.DespawnTime = birdDataPath._despawnTime_s;

        try
        {
            birdMove.StageTransform = _stageTransforms[_currentStageIndex]._stageTransform;
        }
        catch (Exception)
        {
            X_Debug.LogError("StageController�ɃX�e�[�W��Transform���ݒ肳��Ă��܂���B");
            birdMove.StageTransform = null;
        }

        for (int i = 0; i < birdDataPath._birdGoalPlaces.Count; i++)
        {
            birdMove.MoveTypes = birdDataPath._birdGoalPlaces[i]._moveType;
            birdMove.GoalPositions = birdDataPath._birdGoalPlaces[i]._birdGoalPlace.position;
            birdMove.MovementSpeeds = birdDataPath._birdGoalPlaces[i]._speed;
            birdMove.ReAttackTimes = birdDataPath._birdGoalPlaces[i]._stayTime_s;

            birdMove.AttackIntervalTimes_Moving = birdDataPath._birdGoalPlaces[i]._attackInterval_s_a;
            birdMove.AttackIntervalTimes_Stopping = birdDataPath._birdGoalPlaces[i]._attackInterval_s_b;

            birdMove.BirdAttackTypes_Moving = birdDataPath._birdGoalPlaces[i]._birdAttackType_a;
            birdMove.BirdAttackTypes_Stopping = birdDataPath._birdGoalPlaces[i]._birdAttackType_b;

            birdMove.AttackTimings_Moving = birdDataPath._birdGoalPlaces[i]._attackTiming_s1_a;
            birdMove.AttackTimings_Stopping = birdDataPath._birdGoalPlaces[i]._attackTiming_s1_b;
            birdMove.AttackTimings_Moving = birdDataPath._birdGoalPlaces[i]._attackTiming_s2_a;
            birdMove.AttackTimings_Stopping = birdDataPath._birdGoalPlaces[i]._attackTiming_s2_b;
            birdMove.AttackTimings_Moving = birdDataPath._birdGoalPlaces[i]._attackTiming_s3_a;
            birdMove.AttackTimings_Stopping = birdDataPath._birdGoalPlaces[i]._attackTiming_s3_b;
            birdMove.AttackTimings_Moving = birdDataPath._birdGoalPlaces[i]._attackTiming_s4_a;
            birdMove.AttackTimings_Stopping = birdDataPath._birdGoalPlaces[i]._attackTiming_s4_b;
            birdMove.AttackTimings_Moving = birdDataPath._birdGoalPlaces[i]._attackTiming_s5_a;
            birdMove.AttackTimings_Stopping = birdDataPath._birdGoalPlaces[i]._attackTiming_s5_b;

            birdMove.AttackTimes_Moving = birdDataPath._birdGoalPlaces[i]._attackTimes_a;
            birdMove.AttackTimes_Stopping = birdDataPath._birdGoalPlaces[i]._attackTimes_b;

            birdMove.CooldownTime_Moving = birdDataPath._birdGoalPlaces[i]._cooldownTime_s_a;
            birdMove.CooldownTime_Stopping = birdDataPath._birdGoalPlaces[i]._cooldownTime_s_b;

            birdMove.ConsecutiveIntervalTimes_Moving = birdDataPath._birdGoalPlaces[i]._consecutiveAttackInterval_s_a;
            birdMove.ConsecutiveIntervalTimes_Stopping = birdDataPath._birdGoalPlaces[i]._consecutiveAttackInterval_s_b;

            birdMove.MoveSpeedArcs = birdDataPath._birdGoalPlaces[i]._arcHeight;
            birdMove.ArcMoveDirections = birdDataPath._birdGoalPlaces[i]._arcMoveDirection;

            birdMove.DirectionTypes_Moving = birdDataPath._birdGoalPlaces[i]._directionType_moving;
            birdMove.DirectionTypes_Stopping = birdDataPath._birdGoalPlaces[i]._directionType_stopping;
            birdMove.DirectionTypes_Attack = birdDataPath._birdGoalPlaces[i]._directionType_attack;
        }

        birdMove.BirdEnable();
    }

    private IEnumerator SpawnGroundEnemy(int listIndex)
    {
        GroundEnemyDataTable dataPath = _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._groundEnemysData[listIndex];

        yield return new WaitForSeconds(dataPath._spawnDelay_s);

        GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.groundEnemy, dataPath._groundEnemySpawnPlace.position).gameObject;

        GroundEnemyMoveBase temporaryMove = temporaryObject.GetComponent<GroundEnemyMoveBase>();
        temporaryMove.GroundEnemyData = dataPath;
        temporaryMove.InitializeOnEnable();

        temporaryObject.GetComponent<GroundEnemyStats>()._onDeathEnemy = DecrementNumberOfObject;
    }

    /// <summary>
    /// �v���C���[���ړ�������
    /// </summary>
    private void MovingPlayer()
    {
        _player.transform.position = _stageTransforms[_currentStageIndex]._stageTransform.position;
        _player.transform.rotation = _stageTransforms[_currentStageIndex]._stageTransform.rotation;
        _debugPlayer.transform.position = _stageTransforms[_currentStageIndex]._stageTransform.position;
        _debugPlayer.transform.rotation = _stageTransforms[_currentStageIndex]._stageTransform.rotation;
    }

    /// <summary>
    /// ���U���g�L�����o�X���ړ�������
    /// </summary>
    private void MovingResultCanvas()
    {
        _resultCanvas.transform.position = _stageTransforms[_currentStageIndex - 1]._stageTransform.position + _player.transform.forward * _canvasPositionCorrectionValue;
        _resultCanvas.transform.rotation = _player.transform.rotation;
    }

    private void MovingStartCanvas()
    {
        _startCanvas.transform.position = _stageTransforms[_currentStageIndex]._stageTransform.position + _player.transform.forward * _canvasPositionCorrectionValue;
        _startCanvas.transform.rotation = _player.transform.rotation;
    }
}