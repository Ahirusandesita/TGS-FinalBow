// --------------------------------------------------------- 
// EnemySpawnerTable.cs 
// 
// CreateDay: 2023/06/22
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// �G�̃X�|�i�[��ݒ肵�ĕۑ�����N���X
/// </summary>
// Assets > Create > Scriptables > CreateEnemySpawnerTable�ŃA�Z�b�g��
[CreateAssetMenu(fileName = "EnemySpawnerData", menuName = "Scriptables/CreateEnemySpawnerTable")]
public class EnemySpawnerTable : ScriptableObject
{
    [Header("�}�b�v���S�̍��W"), Tooltip("�}�b�v���S�̍��W")]
    public CentralInformation _centralInformation;
    [Header("�G���̏o���ʒu"), Tooltip("�G���̏o���ʒu")]
    public List<WaveEnemyInformation> _scriptableWaveEnemy = new List<WaveEnemyInformation>();
}

/// <summary>
/// �eWave�̓G�̏��
/// </summary>
// Inspector�ŕύX�����l���A�Z�b�g�Ƃ��ĕۑ������
[System.Serializable]
public class WaveEnemyInformation
{
    [Tooltip("Wave��")]
    public string _wave;

    [Header("Wave�̊J�n���ԁi�OWave����̎��ԁj"), Tooltip("Wave�̊J�n���ԁiex: Wave1��1.5��ݒ肷��ƁA�Q�[���J�n��1.5s���Wave1�̓G���X�|�[������")]
    public float _startWaveTime_s;

    public List<EnemySpawnerInformation> _enemysSpawner = new List<EnemySpawnerInformation>();
}

/// <summary>
/// �G�̃X�|�i�[�̏��
/// </summary>
[System.Serializable]
public class EnemySpawnerInformation
{
    [Header("�G�̎��"), Tooltip("�G�̎��")]
    public BirdType _enemyType;

    [Tooltip("�����̎��")]
    public MoveType _moveType;

    [Header("�ړ��X�s�[�h�iLinear�̂݁j"), Tooltip("�����ړ��̃X�s�[�h")]
    public float _speed;

    [Header("�o���e�̐��i�����F��j"), Tooltip("�o���e�̐��i�����F��j")]
    public int _bullet;

    [Header("��~���čU������b���iLinear�̂݁j"), Tooltip("��~���čU������b��")]
    public float _waitTime_s;

    [Header("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j"), Tooltip("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j")]
    public float _spawnDelay_s;

    [Header("�G���̃X�|�[���ʒu"), Tooltip("�G���̃X�|�[���ʒu")]
    public Transform _birdSpawnPlace;

    [Header("�G���̃S�[���ʒu"), Tooltip("�G���̃S�[���ʒu")]
    public List<Transform> _birdGoalPlaces = new List<Transform>();
}

[System.Serializable]
public class CentralInformation
{
    public Transform _centralTransform;
}