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
[System.Serializable]
public class GroundEnemyDataTable : ScriptableObject
{
    [Header("スポーンディレイ（注：ステージ開始時からの時間）"), Tooltip("スポーンディレイ（注：ステージ開始時からの時間）")]
    public float _spawnDelay_s;

    [Header("行動ディレイ"), Tooltip("行動ディレイ（スポーンしてからこの時間が経過するまで、何もしない）")]
    public float _moveDelay_s;

    [Header("地上雑魚のスポーン位置"), Tooltip("地上雑魚のスポーン位置")]
    public Transform _groundEnemySpawnPlace;

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