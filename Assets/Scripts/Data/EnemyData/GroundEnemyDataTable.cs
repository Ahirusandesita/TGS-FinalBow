// --------------------------------------------------------- 
// GroundEnemyDataTable.cs 
// 
// CreateDay: 2023/07/13
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections.Generic;

public enum WormType
{
    low,
    hight,
    middle
}

// Assets > Create > Scriptables > CreateGroundEnemyDataTable�ŃA�Z�b�g��
[CreateAssetMenu(fileName = "GroundEnemyData", menuName = "Scriptables/CreateGroundEnemyDataTable")]
public class GroundEnemyDataTable : ScriptableObject
{
    [Header("�X�|�[���f�B���C�i���F�X�e�[�W�J�n������̎��ԁj"), Tooltip("�X�|�[���f�B���C�i���F�X�e�[�W�J�n������̎��ԁj")]
    public float _spawnDelay_s;

    [Header("�n��G���̃X�|�[���ʒu"), Tooltip("�n��G���̃X�|�[���ʒu")]
    public Transform _groundEnemySpawnPlace;

    [Header("�����̃X�|�[���ʒu"), Tooltip("�����̃X�|�[���ʒu")]
    public Transform _cloudOfDustPlace;

    [Header("�o���Ԋu"), Tooltip("�o���Ԋu")]
    public float _appearanceInterval_s;

    [Header("�o������"), Tooltip("�o������")]
    public float _appearanceKeep_s;

    [Header("�f�X�|�[���܂ł̕b��"), Tooltip("�f�X�|�[���܂ł̕b��")]
    public float _despawnTime_s = 10f;

    [Header("�o���̎d��"), Tooltip("�o���̎d��")]
    public WormType _wormType;

    //[Header("�U������"), Tooltip("�U������")]
    //public bool _needAttack;

    //[HideInInspector, Tooltip("�U���̎��")]
    //public GroundEnemyMoveBase.AttackType _attackType;

    //[HideInInspector, Tooltip("�U���Ԋu")]
    //public float _reAttackTime_s = 2f;
}