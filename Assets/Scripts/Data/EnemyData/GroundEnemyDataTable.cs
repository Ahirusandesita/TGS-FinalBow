// --------------------------------------------------------- 
// GroundEnemyDataTable.cs 
// 
// CreateDay: 2023/07/13
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections.Generic;

// Assets > Create > Scriptables > CreateGroundEnemyDataTable�ŃA�Z�b�g��
[CreateAssetMenu(fileName = "GroundEnemyData", menuName = "Scriptables/CreateGroundEnemyDataTable")]
public class GroundEnemyDataTable : ScriptableObject
{
    [Header("�X�|�[���f�B���C�i���F�X�e�[�W�J�n������̎��ԁj"), Tooltip("�X�|�[���f�B���C�i���F�X�e�[�W�J�n������̎��ԁj")]
    public float _spawnDelay_s;

    [Header("�s���f�B���C"), Tooltip("�s���f�B���C�i�X�|�[�����Ă��炱�̎��Ԃ��o�߂���܂ŁA�������Ȃ��j")]
    public float _moveDelay_s;

    [Header("�n��G���̃X�|�[���ʒu"), Tooltip("�n��G���̃X�|�[���ʒu")]
    public Transform _groundEnemySpawnPlace;

    [Header("�U���̎��"), Tooltip("�U���̎��")]
    public GroundEnemyMoveBase.AttackType _attackType;

    [Header("�U���Ԋu"), Tooltip("�U���Ԋu")]
    public float _reAttackTime_s = 2f;

    [Header("�f�X�|�[���܂ł̕b��"), Tooltip("�f�X�|�[���܂ł̕b��")]
    public float _despawnTime_s = 10f;

    [Space]
    [Header("�s�����X�g"), Tooltip("�s�����X�g")]
    public List<GroundEnemyActionInformation> _groundEnemyActionInformation = new List<GroundEnemyActionInformation>();
}

[System.Serializable]
public class GroundEnemyActionInformation
{
    public GroundEnemyActionType _groundEnemyActionType;

    public GroundEnemyMoveBase.JumpDirectionState _jumpDirectionState;
}