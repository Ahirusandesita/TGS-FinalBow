// --------------------------------------------------------- 
// EnemyDataTable.cs 
// 
// CreateDay: 2023/07/07
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

// Assets > Create > Scriptables > CreateEnemyDataTable�ŃA�Z�b�g��
[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptables/CreateEnemyDataTable")]
[System.Serializable]
public class EnemyDataTable : ScriptableObject
{
    [Header("�G�̎��"), Tooltip("�G�̎��")]
    public EnemyType _enemyType;

    [Tooltip("�����̎��")]
    public MoveType _moveType;

    [Header("�o���e�̐��i�����F��j"), Tooltip("�o���e�̐��i�����F��j")]
    public int _bullet;

    [Header("�U���Ԋu"), Tooltip("�U���Ԋu")]
    public float _attackInterval_s;

    [Header("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j"), Tooltip("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j")]
    public float _spawnDelay_s;

    [Header("�G���̃X�|�[���ʒu"), Tooltip("�G���̃X�|�[���ʒu")]
    public Transform _birdSpawnPlace;

    [Header("�G���̃S�[���ʒu"), Tooltip("�G���̃S�[���ʒu")]
    public List<EnemyGoalInformation> _birdGoalPlaces = new List<EnemyGoalInformation>();
}

[System.Serializable]
public class EnemyGoalInformation
{
    [Header("�G���̃S�[���ʒu"), Tooltip("�G���̃S�[���ʒu")]
    public Transform _birdGoalPlace;

    [Header("���̃S�[���܂ł̈ړ��X�s�[�h"), Tooltip("���̃S�[���܂ł̈ړ��X�s�[�h")]
    public float _speed;

    [Header("���̃S�[���Œ�~���čU������b��"), Tooltip("���̃S�[���Œ�~���čU������b��")]
    public float _stayTime_s;
}