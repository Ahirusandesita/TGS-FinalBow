// --------------------------------------------------------- 
// EnemyDataTable.cs 
// 
// CreateDay: 2023/07/07
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

// Assets > Create > Scriptables > CreateBirdDataTable�ŃA�Z�b�g��
[CreateAssetMenu(fileName = "BirdData", menuName = "Scriptables/CreateBirdDataTable")]
[System.Serializable]
public class BirdDataTable : EnemyDataTable
{
    [Header("���G���̎��"), Tooltip("���G���̎��")]
    public BirdType _birdType;

    [Tooltip("�����̎��")]
    public MoveType _moveType;

    [Header("�ʂ̍���"), Tooltip("�ʂ̍���")]
    [HideInInspector]
    public float _arcHeight = 10f;

    [Header("�ʂ̌���"), Tooltip("�ʂ̌���")]
    [HideInInspector]
    public ArcMoveDirection _arcMoveDirection;

    [Header("�o���e�̐��i�����F��j"), Tooltip("�o���e�̐��i�����F��j")]
    public int _bullet;

    [Header("�U���Ԋu"), Tooltip("�U���Ԋu")]
    public float _attackInterval_s;

    [Header("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j"), Tooltip("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j")]
    public float _spawnDelay_s;

    [Header("���G���̃X�|�[���ʒu"), Tooltip("���G���̃X�|�[���ʒu")]
    public Transform _birdSpawnPlace;

    [Header("���G���̃S�[���ʒu"), Tooltip("���G���̃S�[���ʒu")]
    public List<BirdGoalInformation> _birdGoalPlaces = new List<BirdGoalInformation>();
}

[System.Serializable]
public class BirdGoalInformation
{
    [Header("���G���̃S�[���ʒu"), Tooltip("���G���̃S�[���ʒu")]
    public Transform _birdGoalPlace;

    [Header("���̃S�[���܂ł̈ړ��X�s�[�h"), Tooltip("���̃S�[���܂ł̈ړ��X�s�[�h")]
    public float _speed;

    [Header("���̃S�[���Œ�~���čU������b��"), Tooltip("���̃S�[���Œ�~���čU������b���i���F�S�[���������ݒ肳�ꂽ�ꍇ�A�Ō�̃S�[���̂��̕ϐ��͖��������j")]
    public float _stayTime_s;
}