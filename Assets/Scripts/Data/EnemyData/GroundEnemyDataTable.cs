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
    [Header("地上雑魚/砂埃のスポーン位置"), Tooltip("地上雑魚/砂埃のスポーン位置")]
    public Transform _groundEnemySpawnPlace;

    [Header("出現する時刻"), Tooltip("出現する時刻")]
    public float _spawnTime_s;

    [Header("砂埃のみ出現"), Tooltip("砂埃のみ出現")]
    public bool _onlySandDust;

    [Header("出現キープ時間"), Tooltip("出現キープ時間")]
    public float _appearanceKeep_s;

    [Header("出現の仕方"), Tooltip("出現の仕方")]
    public WormType _wormType;

    //[Header("攻撃する"), Tooltip("攻撃する")]
    //public bool _needAttack;

    //[HideInInspector, Tooltip("攻撃の種類")]
    //public GroundEnemyMoveBase.AttackType _attackType;

    //[HideInInspector, Tooltip("攻撃間隔")]
    //public float _reAttackTime_s = 2f;
}