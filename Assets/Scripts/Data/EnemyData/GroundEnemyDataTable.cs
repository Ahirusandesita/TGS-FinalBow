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
    [Header("���[��/�����̃X�|�[���ʒu"), Tooltip("���[��/�����̃X�|�[���ʒu")]
    public Transform _groundEnemySpawnPlace;

    [Header("�X�|�[������܂ł̎���"), Tooltip("�X�|�[������܂ł̎���")]
    public float _spawnDelay_s;

    [Header("�����̎���"), Tooltip("�����̎���")]
    public float _spawnTime_s;

    [Header("�����̂ݏo��"), Tooltip("�����̂ݏo��")]
    public bool _onlySandDust;

    [HideInInspector, Tooltip("����܂ł̎���")]
    public float _appearanceKeep_s;

    [HideInInspector, Tooltip("�o���̎d��")]
    public WormType _wormType;

    //[Header("�U������"), Tooltip("�U������")]
    //public bool _needAttack;

    //[HideInInspector, Tooltip("�U���̎��")]
    //public GroundEnemyMoveBase.AttackType _attackType;

    //[HideInInspector, Tooltip("�U���Ԋu")]
    //public float _reAttackTime_s = 2f;
}