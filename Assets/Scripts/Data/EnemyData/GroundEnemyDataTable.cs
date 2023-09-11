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

// Assets > Create > Scriptables > CreateGroundEnemyDataTableでアセット化
[CreateAssetMenu(fileName = "GroundEnemyData", menuName = "Scriptables/CreateGroundEnemyDataTable")]
public class GroundEnemyDataTable : ScriptableObject
{
    [Header("スポーンディレイ（注：ステージ開始時からの時間）"), Tooltip("スポーンディレイ（注：ステージ開始時からの時間）")]
    public float _spawnDelay_s;

    [Header("地上雑魚のスポーン位置"), Tooltip("地上雑魚のスポーン位置")]
    public Transform _groundEnemySpawnPlace;

    [Header("砂埃のスポーン位置"), Tooltip("砂埃のスポーン位置")]
    public Transform _cloudOfDustPlace;

    [Header("出現間隔"), Tooltip("出現間隔")]
    public float _appearanceInterval_s;

    [Header("出現時間"), Tooltip("出現時間")]
    public float _appearanceKeep_s;

    [Header("デスポーンまでの秒数"), Tooltip("デスポーンまでの秒数")]
    public float _despawnTime_s = 10f;

    [Header("出現の仕方"), Tooltip("出現の仕方")]
    public WormType _wormType;

    //[Header("攻撃する"), Tooltip("攻撃する")]
    //public bool _needAttack;

    //[HideInInspector, Tooltip("攻撃の種類")]
    //public GroundEnemyMoveBase.AttackType _attackType;

    //[HideInInspector, Tooltip("攻撃間隔")]
    //public float _reAttackTime_s = 2f;
}