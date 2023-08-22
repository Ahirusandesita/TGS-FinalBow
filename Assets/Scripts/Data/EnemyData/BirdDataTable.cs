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

    [HideInInspector, Tooltip("�ʂ̍���")]
    public float _arcHeight;

    [HideInInspector, Tooltip("�ʂ̌���")]
    public ArcMoveDirection _arcMoveDirection;

    [Tooltip("���G���̃S�[���ʒu")]
    public Transform _birdGoalPlace;

    [Tooltip("���̃S�[���܂ł̈ړ��X�s�[�h")]
    public float _speed;

    [Tooltip("���̃S�[���Œ�~���čU������b���i���F�S�[���������ݒ肳�ꂽ�ꍇ�A�Ō�̃S�[���̂��̕ϐ��͖��������j")]
    public float _stayTime_s;

    [Tooltip("�U�����@�̎��")]
    public BirdAttackType _birdAttackType;

    [Tooltip("�U���Ԋu")]
    public float _attackInterval_s;

    [Tooltip("�U�����s���^�C�~���O�i�b�j")]
    public float _attackTimings_s1;
    public float _attackTimings_s2;
    public float _attackTimings_s3;
    public float _attackTimings_s4;
    public float _attackTimings_s5;

    [Tooltip("�A���U����")]
    public int _attackTimes;

    [Tooltip("�U���N�[���^�C���i�b�j")]
    public float _cooldownTime_s;
}