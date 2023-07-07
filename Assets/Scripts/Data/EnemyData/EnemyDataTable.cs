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