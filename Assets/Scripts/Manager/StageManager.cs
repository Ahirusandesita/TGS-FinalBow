// --------------------------------------------------------- 
// StageManager.cs 
// 
// CreateDay: 2023/06/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using System.Collections.Generic;
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
    zakoWave4,
    wave5,
    boss
}

public class StageManager : MonoBehaviour, IStageSpawn
{
    #region �ϐ�
    [Tooltip("�^�O�̖��O")]
    public TagObject _PoolSystemTagData = default;

    [Tooltip("�G�̃X�|�[�����W�e�[�u��")]
    public EnemySpawnerTable _enemySpawnerTable = default;

    [Header("�G���̏o���ʒu���X�g")]
    [Tooltip("Wave1�̎G���̏o���ʒu")]
    public List<Transform> _birdSpawnPlaces_Wave1 = new List<Transform>();

    [Tooltip("Wave1�̎G���̃S�[���ʒu")]
    public List<Transform> _birdGoalPlaces_Wave1 = new List<Transform>();

    [Tooltip("Wave2�̎G���̏o���ʒu")]
    public List<Transform> _birdSpawnPlaces_Wave2 = new List<Transform>();

    [Tooltip("Wave3�̎G���̏o���ʒu")]
    public List<Transform> _birdSpawnPlaces_Wave3 = new List<Transform>();

    [Tooltip("Wave4�̎G���̏o���ʒu")]
    public List<Transform> _birdSpawnPlaces_Wave4 = new List<Transform>();

    [Tooltip("Wave5�̎G���̏o���ʒu")]
    public List<Transform> _birdSpawnPlaces_Wave5 = new List<Transform>();


    [Tooltip("�擾����ObjectPoolSystem�N���X")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("���݂̎G��/�I�̐�")]
    private int _currentNumberOfObject = default;

    [Tooltip("���݂̃E�F�[�u��")]
    private WaveType _waveType = WaveType.zakoWave1;     // �����l0�itutorial1)
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
        //GameManager���Ăԁ@TimeManager�ɂ͌Ă΂��Ȃ�

        // �X�|�[�������i���j
        switch (_waveType)
        {
            case WaveType.zakoWave1:
                // Inspector��ŃA�^�b�`�����X�|�[���ʒu�̐������G�����X�|�[��������
                for (int i = 0; i < _birdSpawnPlaces_Wave1.Count; i++)
                {
                    // �X�|�[���������G���̐���ݒ�
                    _currentNumberOfObject = _birdSpawnPlaces_Wave1.Count;

                    // �G�����v�[������Ăяo���A�Ăяo�����e�G���̃f���Q�[�g�ϐ��Ƀf�N�������g�֐���o�^
                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _birdSpawnPlaces_Wave1[i].position).gameObject;
                    temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;

                    temporaryObject.AddComponent<BirdMoveFirst>();
                }

                break;

            case WaveType.zakoWave2:
                for (int i = 0; i < _birdSpawnPlaces_Wave2.Count; i++)
                {
                    _currentNumberOfObject = _birdSpawnPlaces_Wave2.Count;

                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _birdSpawnPlaces_Wave2[i].position).gameObject;
                    temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;
                }

                break;

            case WaveType.zakoWave3:
                for (int i = 0; i < _birdSpawnPlaces_Wave3.Count; i++)
                {
                    _currentNumberOfObject = _birdSpawnPlaces_Wave3.Count;

                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _birdSpawnPlaces_Wave3[i].position).gameObject;
                    temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;
                }

                break;

            case WaveType.zakoWave4:
                for (int i = 0; i < _birdSpawnPlaces_Wave4.Count; i++)
                {
                    _currentNumberOfObject = _birdSpawnPlaces_Wave4.Count;

                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _birdSpawnPlaces_Wave4[i].position).gameObject;
                    temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;
                }

                break;

            case WaveType.wave5:
                for (int i = 0; i < _birdSpawnPlaces_Wave5.Count; i++)
                {
                    _currentNumberOfObject = _birdSpawnPlaces_Wave5.Count;

                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _birdSpawnPlaces_Wave5[i].position).gameObject;
                    temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;
                }

                break;

            case WaveType.boss:
                // �{�X���o��������

                break;

            default:
                X_Debug.LogError("�������Ă���E�F�[�u�𒴂��Ă��܂�");
                break;
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