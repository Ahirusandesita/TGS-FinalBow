// --------------------------------------------------------- 
// StageManager.cs 
// 
// CreateDay: 2023/06/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using System;
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
    zakoWave1,
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


    [Tooltip("�擾����ObjectPoolSystem�N���X")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("���݂̎G��/�I�̐�")]
    private int _currentNumberOfObject = default;

    [Tooltip("���݂̃E�F�[�u��")]
    private WaveType _waveType = WaveType.zakoWave1;     // �����l0


    public GameObject _bossTest;
    #endregion


    private void Start()
    {
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();
    }

    private void Update()
    {
        // �f�o�b�O�p
        if (Input.GetKeyDown(KeyCode.P))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        try
        {
            if (_waveType == WaveType.boss)
            {
                // �{�X���X�|�[��

                Instantiate(_bossTest);
                return;
            }

            // EnemySpawnerTable�Őݒ肵���X�|�i�[�̐������G�����X�|�[��������
            for (int i = 0; i < _enemySpawnerTable._scriptableESpawnerInformation[(int)_waveType]._birdSpawnPlaces.Count; i++)
            {
                // �X�|�[���������G���̐���ݒ�
                _currentNumberOfObject = _enemySpawnerTable._scriptableESpawnerInformation[(int)_waveType]._birdSpawnPlaces.Count;

                // �G�����v�[������Ăяo���A�Ăяo�����e�G���̃f���Q�[�g�ϐ��Ƀf�N�������g�֐���o�^
                GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird,
                    _enemySpawnerTable._scriptableESpawnerInformation[(int)_waveType]._birdSpawnPlaces[i].position).gameObject;
                temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;

                switch (_waveType)
                {
                    case WaveType.zakoWave1:
                        //�Ăяo�����G���ɃR���|�[�l���g��t�^
                        temporaryObject.AddComponent<BirdMoveFirst>();

                        break;

                    case WaveType.zakoWave2:
                        temporaryObject.AddComponent<BirdMoveFirst>();

                        break;

                    default:
                        temporaryObject.AddComponent<BirdMoveSecond>();

                        break;
                }
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
}