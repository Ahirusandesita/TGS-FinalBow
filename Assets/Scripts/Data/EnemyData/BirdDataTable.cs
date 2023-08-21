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
public class BirdDataTable : ScriptableObject
{
    [Header("���G���̎��"), Tooltip("���G���̎��")]
    public BirdType _birdType;

    [Header("�o���e�̐��i�����F��j"), Tooltip("�o���e�̐��i�����F��j")]
    public int _bullet;

    [Header("�U���Ԋu"), Tooltip("�U���Ԋu")]
    public float _attackInterval_s;

    [Header("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j"), Tooltip("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j")]
    public float _spawnDelay_s;

    [Header("���G���̃X�|�[���ʒu"), Tooltip("���G���̃X�|�[���ʒu")]
    public Transform _birdSpawnPlace;

    [Header("���G���̃S�[���ʒu"), Tooltip("���G���̃S�[���ʒu")]
    public List<BirdGoalInformation> _birdGoalPlaces = new();
}

[System.Serializable]
public class BirdGoalInformation
{
    [Tooltip("�����̎��")]
    public MoveType _moveType;

    [Tooltip("���G���̃S�[���ʒu")]
    public Transform _birdGoalPlace;

    [Tooltip("���̃S�[���܂ł̈ړ��X�s�[�h")]
    public float _speed;

    [Tooltip("���̃S�[���Œ�~���čU������b���i���F�S�[���������ݒ肳�ꂽ�ꍇ�A�Ō�̃S�[���̂��̕ϐ��͖��������j")]
    public float _stayTime_s;

    [HideInInspector, Tooltip("�ʂ̍���")]
    public float _arcHeight;

    [HideInInspector, Tooltip("�ʂ̌���")]
    public ArcMoveDirection _arcMoveDirection;
}