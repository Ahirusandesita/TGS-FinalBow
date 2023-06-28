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
    /// �G�̃X�|�[������
    /// </summary>
    void Spawn();
}

/// <summary>
/// �E�F�[�u�̎��
/// </summary>
public enum WaveType
{
    zakoWave1_1,
    zakoWave1_2,
    zakoWave2,
    zakoWave3,
    boss
}

public class StageManager : MonoBehaviour, IStageSpawn
{
    #region �ϐ�
    [Tooltip("�^�O�̖��O")]
    public TagObject _PoolSystemTagData = default;

    [Tooltip("�G�̃X�|�[�����W�e�[�u��")]
    public EnemySpawnerTable _enemySpawnerTable = default;

    [Tooltip("�{�X�̃v���n�u")]
    public GameObject _bossPrefab;


    [Tooltip("�擾����ObjectPoolSystem�N���X")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("���݂̎G��/�I�̐�")]
    private int _currentNumberOfObject = default;

    [Tooltip("���݂̃E�F�[�u��")]
    private WaveType _waveType = WaveType.zakoWave1_1;     // �����l0
    #endregion


    private void Start()
    {
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();

        StartCoroutine(GameStart());
    }


    public void Spawn()
    {
        try
        {
            if (_waveType == WaveType.boss)
            {
                // �{�X���X�|�[��
                Instantiate(_bossPrefab);
                return;
            }

            // EnemySpawnerTable�Őݒ肵���X�|�i�[�̐���ݒ�
            _currentNumberOfObject = _enemySpawnerTable._scriptableWaveEnemy[(int)_waveType]._enemysSpawner.Count;

            // �v�[������Ăяo��
            StartCoroutine(SpawnAndDelay());
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
            // �E�F�[�u��i�߂�
            IncrementWave();
            Spawn();
        }
    }

    /// <summary>
    /// �E�F�[�u��i�s�����鏈��
    /// </summary>
    private void IncrementWave()
    {
        _waveType++;
    }

    /// <summary>
    /// �Q�[���X�^�[�g�i��b�҂��Ă���X�|�[��������j
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.5f);

        Spawn();
    }

    /// <summary>
    /// �ҋ@����
    /// </summary>
    /// <param name="wait">�ҋ@����b��</param>
    /// <returns></returns>
    private IEnumerator SpawnAndDelay()
    {
        PoolEnum.PoolObjectType selectedPrefab;
        BirdMoveBase temporaryMove;


        // �ݒ肵���������G�����X�|�[��������
        for (int i = 0; i < _currentNumberOfObject; i++)
        {
            // �ݒ肳�ꂽ�b�������ҋ@����
            yield return new WaitForSeconds(_enemySpawnerTable._scriptableWaveEnemy[(int)_waveType]._enemysSpawner[i]._spawnDelay_s);


            // �ǂ̓G���X�|�[�������邩����iScriptable����擾�j
            switch (_enemySpawnerTable._scriptableWaveEnemy[(int)_waveType]._enemysSpawner[i]._enemyType)
            {
                // �ȉ�Enum�̕ϊ�����
                case EnemyType.normalBird:
                    selectedPrefab = PoolEnum.PoolObjectType.normalBird;

                    break;

                case EnemyType.bombBird:
                    selectedPrefab = PoolEnum.PoolObjectType.bombBird;

                    break;

                case EnemyType.penetrateBird:
                    selectedPrefab = PoolEnum.PoolObjectType.penetrateBird;

                    break;

                case EnemyType.thunderBird:
                    selectedPrefab = PoolEnum.PoolObjectType.ThunderBird;

                    break;

                // ��O����
                default:
                    selectedPrefab = PoolEnum.PoolObjectType.normalBird;

                    break;
            }

            // �G�����v�[������Ăяo���A�Ăяo�����e�G���̃f���Q�[�g�ϐ��Ƀf�N�������g�֐���o�^
            GameObject temporaryObject = _objectPoolSystem.CallObject(selectedPrefab,
                _enemySpawnerTable._scriptableWaveEnemy[(int)_waveType]._enemysSpawner[i]._birdSpawnPlace.position).gameObject;
            temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;



            // Scriptabe�̐ݒ�ɉ����āA�A�^�b�`���鋓���X�N���v�g��ς���
            switch (_enemySpawnerTable._scriptableWaveEnemy[(int)_waveType]._moveType)
            {
                case MoveType.linear:
                    //�Ăяo�����G���ɃR���|�[�l���g��t�^
                    temporaryMove = temporaryObject.AddComponent<BirdMoveFirst>();

                    break;

                case MoveType.curve:
                    temporaryMove = temporaryObject.AddComponent<BirdMoveSecond>();

                    break;

                default:
                    temporaryMove = null;

                    break; ;
            }

            // �Ăяo�����G���̕ϐ��ɐݒ�
            temporaryMove.ThisInstanceIndex = i;
            temporaryMove.SpawnedWave = _waveType;
            temporaryMove.NumberOfGoal = _enemySpawnerTable._scriptableWaveEnemy[(int)_waveType]._enemysSpawner[i]._birdGoalPlaces.Count;
            temporaryMove.LinearMovementSpeed = _enemySpawnerTable._scriptableWaveEnemy[(int)_waveType]._enemysSpawner[i]._speed;
            temporaryMove.ReAttackTime = _enemySpawnerTable._scriptableWaveEnemy[(int)_waveType]._enemysSpawner[i]._waitTime_s;
        }
    }
}