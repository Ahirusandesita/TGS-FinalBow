// --------------------------------------------------------- 
// StageManager.cs 
// 
// CreateDay: 2023/06/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using System;
using System.Collections;
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

/// <summary>
/// �E�F�[�u�̎��
/// </summary>
public enum WaveType
{
    zakoWave1,
    zakoWave2,
    zakoWave3,
    zakoWave4,
    zakoWave5,
    boss
}

public class StageManager : MonoBehaviour, IStageSpawn
{
    #region �ϐ�
    [Tooltip("�^�O�̖��O")]
    public TagObject _PoolSystemTagData = default;

    [Tooltip("�G�̃X�|�[�����W�e�[�u��")]
    public WaveManagementTable _waveManagementTable = default;

    [Tooltip("�{�X�̃v���n�u")]
    public GameObject _bossPrefab;


    [Tooltip("�擾����ObjectPoolSystem�N���X")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("���݂̎G��/�I�̐�")]
    private int _currentNumberOfObject = 0;

    [Tooltip("���݂̃E�F�[�u��")]
    private WaveType _currentWave = WaveType.zakoWave1;     // �����l0

    [Tooltip("���ݎ��s���̃R���[�`��")]
    private Coroutine _currentActiveCoroutine = default;
    #endregion


    private void Start()
    {
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();

        // �Q�[���X�^�[�g
        _currentActiveCoroutine = StartCoroutine(WaveStart());
    }


    public void WaveExecution()
    {
        // �R���[�`���������Ă�����~�߂�
        //StopCoroutine(_currentActiveCoroutine);

        try
        {
            if (_currentWave == WaveType.boss)
            {
                // �{�X���X�|�[��
                Instantiate(_bossPrefab);
                return;
            }

            // EnemySpawnerTable�Őݒ肵���X�|�i�[�̐���ݒ�
            _currentNumberOfObject += _waveManagementTable._waveInformation[(int)_currentWave]._enemysData.Count;

            // EnemySpawnerTable�Őݒ肵���X�|�i�[�̐������G�����X�|�[��������
            for (int i = 0; i < _waveManagementTable._waveInformation[(int)_currentWave]._enemysData.Count; i++)
            {
                // �v�[������Ăяo��
                StartCoroutine(Spawn(listIndex: i));
            }

            // ����Wave�̋������s�܂ł̃J�E���g�_�E�����J�n����
            //_currentActiveCoroutine = StartCoroutine(WaveStart());
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

        if (_currentNumberOfObject <= 0)
        {
            _currentWave++;
            // �E�F�[�u��i�߂�
            WaveExecution();
        }
    }

    /// <summary>
    /// Wave�J�n�in�b�҂��Ă���X�|�[��������j
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaveStart()
    {
        // �ݒ肳�ꂽ�b�����o�߂�����A�����I�ɃE�F�[�u��i�s������
        float waitTime = _waveManagementTable._waveInformation[(int)_currentWave]._startWaveTime_s;
        yield return new WaitForSeconds(waitTime);

        WaveExecution();
    }

    /// <summary>
    /// �G���o��������R���[�`��
    /// </summary>
    /// <param name="listIndex">���X�g�̓Y�����ifor����i�����̂܂ܓn���j</param>
    /// <returns></returns>
    private IEnumerator Spawn(int listIndex)
    {
        PoolEnum.PoolObjectType selectedPrefab;
        BirdMoveBase baseMove;
        BirdDataTable enemyDataPath = _waveManagementTable._waveInformation[(int)_currentWave]._enemysData[listIndex];

        // �ݒ肳�ꂽ�b�������ҋ@����
        yield return new WaitForSeconds(enemyDataPath._spawnDelay_s);


        // �ǂ̓G���X�|�[�������邩����iScriptable����擾�j
        switch (enemyDataPath._birdType)
        {
            // �ȉ�Enum�̕ϊ�����
            case BirdType.normalBird:
                selectedPrefab = PoolEnum.PoolObjectType.normalBird;
                break;

            case BirdType.bombBird:
                selectedPrefab = PoolEnum.PoolObjectType.bombBird;
                break;

            case BirdType.penetrateBird:
                selectedPrefab = PoolEnum.PoolObjectType.penetrateBird;
                break;

            case BirdType.thunderBird:
                selectedPrefab = PoolEnum.PoolObjectType.thunderBird;
                break;

            case BirdType.bombBirdBig:
                selectedPrefab = PoolEnum.PoolObjectType.bombBirdBig;
                break;

            case BirdType.thunderBirdBig:
                selectedPrefab = PoolEnum.PoolObjectType.thunderBirdBig;
                break;

            case BirdType.penetrateBirdBig:
                selectedPrefab = PoolEnum.PoolObjectType.penetrateBirdBig;
                break;

            // ��O����
            default:
                selectedPrefab = PoolEnum.PoolObjectType.normalBird;
                break;
        }


        // �G�����v�[������Ăяo���A�Ăяo�����e�G���̃f���Q�[�g�ϐ��Ƀf�N�������g�֐���o�^
        GameObject temporaryObject = _objectPoolSystem.CallObject(selectedPrefab, enemyDataPath._birdSpawnPlace.position).gameObject;
        temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;


        // Scriptable�̐ݒ�ɉ����āA�A�^�b�`���鋓���X�N���v�g��ς���
        switch (enemyDataPath._moveType)
        {
            case MoveType.linear:
                //�Ăяo�����G���ɃR���|�[�l���g��t�^
                baseMove = temporaryObject.AddComponent<BirdMoveFirst>();
                break;

            case MoveType.curve:
                BirdMoveSecond subMove = temporaryObject.AddComponent<BirdMoveSecond>();

                // �ʂ̍���/������ݒ�
                subMove.MoveSpeedArc = enemyDataPath._arcHeight;
                subMove.ArcMoveDirection = enemyDataPath._arcMoveDirection;

                baseMove = subMove;
                break;

            default:
                baseMove = null;
                break;
        }

        // �Ăяo�����G���̕ϐ��ɐݒ�
        baseMove.NumberOfBullet = enemyDataPath._bullet;

        for (int i = 0; i < enemyDataPath._birdGoalPlaces.Count; i++)
        {
            baseMove.GoalPositions = enemyDataPath._birdGoalPlaces[i]._birdGoalPlace.position;
            baseMove.MovementSpeeds = enemyDataPath._birdGoalPlaces[i]._speed;
            baseMove.ReAttackTimes = enemyDataPath._birdGoalPlaces[i]._stayTime_s;
        }
    }
}