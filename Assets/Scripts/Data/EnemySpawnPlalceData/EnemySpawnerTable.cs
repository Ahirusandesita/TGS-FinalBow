// --------------------------------------------------------- 
// EnemySpawnerTable.cs 
// 
// CreateDay: 2023/06/22
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// �G�̓����̎��
/// </summary>
public enum MoveType
{
    /// <summary>
    /// �����ړ�
    /// </summary>
    linear,
    /// <summary>
    /// �Ȑ��ړ�
    /// </summary>
    curve
}


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
/// �e�E�F�[�u�̓G�̏��
/// </summary>
// Inspector�ŕύX�����l���A�Z�b�g�Ƃ��ĕۑ������
[System.Serializable]
public class WaveEnemyInformation
{
    [Tooltip("�E�F�[�u��")]
    public string _wave;

    [Tooltip("�����̎��")]
    public MoveType _moveType;

    public List<EnemySpawnerInformation> _enemysSpawner = new List<EnemySpawnerInformation>();
}

/// <summary>
/// �G�̃X�|�i�[�̏��
/// </summary>
[System.Serializable]
public class EnemySpawnerInformation
{
    [Header("�ړ��X�s�[�h�iLinear�̂݁j"), Tooltip("�����ړ��̃X�s�[�h")]
    public float _speed;

    [Header("��~���čU������b���iLinear�̂݁j"), Tooltip("��~���čU������b��")]
    public float _waitTime_s;

    [Header("�X�|�[���f�B���C�i���ӁF�O�̓G����̕b���A�@�����F���b���x�j"), Tooltip("�X�|�[���f�B���C�i���ӁF�O�̓G����̕b���A�@�����F���b���x�j")]
    // �啝�ȃf�B���C��t���������ꍇ�́AWave�𕪂���i�S���|���Ȃ��Ǝ��̃E�F�[�u�ɐi�܂Ȃ����߁j
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