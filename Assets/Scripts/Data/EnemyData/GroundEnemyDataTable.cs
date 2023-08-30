// --------------------------------------------------------- 
// GroundEnemyDataTable.cs 
// 
// CreateDay: 2023/07/13
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections.Generic;

// Assets > Create > Scriptables > CreateGroundEnemyDataTableでアセット化
[CreateAssetMenu(fileName = "GroundEnemyData", menuName = "Scriptables/CreateGroundEnemyDataTable")]
public class GroundEnemyDataTable : ScriptableObject
{
    [Header("スポーンディレイ（注：ステージ開始時からの時間）"), Tooltip("スポーンディレイ（注：ステージ開始時からの時間）")]
    public float _spawnDelay_s;

    [Header("行動ディレイ"), Tooltip("行動ディレイ（スポーンしてからこの時間が経過するまで、何もしない）")]
    public float _moveDelay_s;

    [Header("地上雑魚のスポーン位置"), Tooltip("地上雑魚のスポーン位置")]
    public Transform _groundEnemySpawnPlace;

    [Header("攻撃の種類"), Tooltip("攻撃の種類")]
    public GroundEnemyMoveBase.AttackType _attackType;

    [Header("攻撃間隔"), Tooltip("攻撃間隔")]
    public float _reAttackTime_s = 2f;

    [Header("デスポーンまでの秒数"), Tooltip("デスポーンまでの秒数")]
    public float _despawnTime_s = 10f;

    [Space]
    [Header("行動リスト"), Tooltip("行動リスト")]
    public List<GroundEnemyActionInformation> _groundEnemyActionInformation = new List<GroundEnemyActionInformation>();
}

[System.Serializable]
public class GroundEnemyActionInformation
{
    public GroundEnemyActionType _groundEnemyActionType;

    public GroundEnemyMoveBase.JumpDirectionState _jumpDirectionState;
}