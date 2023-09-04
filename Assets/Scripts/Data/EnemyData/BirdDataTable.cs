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

    [Header("���[�v��̃S�[���ԍ��i���X�g�̃C���f�b�N�X�j"), Tooltip("���[�v��̃S�[���ԍ�")]
    public int _goalIndexOfRoop;
}

[System.Serializable]
public class BirdGoalInformation
{
    [Tooltip("�����̎��")]
    public MoveType _moveType;

    [Tooltip("�ʂ̍���")]
    public float _arcHeight;

    [Tooltip("�ʂ̌���")]
    public ArcMoveDirection _arcMoveDirection;

    [Tooltip("���G���̃S�[���ʒu")]
    public Transform _birdGoalPlace;

    [Tooltip("���̃S�[���܂ł̈ړ��X�s�[�h")]
    public float _speed;

    [Tooltip("���̃S�[���Œ�~���čU������b���i���F�S�[���������ݒ肳�ꂽ�ꍇ�A�Ō�̃S�[���̂��̕ϐ��͖��������j")]
    public float _stayTime_s;

    [Tooltip("�U�����@�̎��")]
    public BirdAttackType _birdAttackType_a;
    public BirdAttackType _birdAttackType_b;

    [Tooltip("�U���Ԋu")]
    public float _attackInterval_s_a;
    public float _attackInterval_s_b;

    [Tooltip("�U�����s���^�C�~���O�i�b�j")]
    public float _attackTiming_s1_a;
    public float _attackTiming_s2_a;
    public float _attackTiming_s3_a;
    public float _attackTiming_s4_a;
    public float _attackTiming_s5_a;
    public float _attackTiming_s1_b;
    public float _attackTiming_s2_b;
    public float _attackTiming_s3_b;
    public float _attackTiming_s4_b;
    public float _attackTiming_s5_b;

    [Tooltip("�A���U����")]
    public int _attackTimes_a;
    public int _attackTimes_b;

    [Tooltip("�A���U���N�[���^�C���i�b�j")]
    public float _cooldownTime_s_a;
    public float _cooldownTime_s_b;

    [Tooltip("�ړ����ɂǂ̕�����������")]
    public DirectionType_AtMoving _directionType_moving;

    [Tooltip("��~���ɂǂ̕�����������")]
    public DirectionType_AtStopping _directionType_stopping;

    [Tooltip("�U�����ɂǂ̕�����������")]
    public DirectionType_AtAttack _directionType_attack;
}