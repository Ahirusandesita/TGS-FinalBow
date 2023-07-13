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
public class GroundEnemyDataTable : EnemyDataTable
{
    [Header("地上雑魚のスポーン位置"), Tooltip("地上雑魚のスポーン位置")]
    public Transform _groundEnemySpawnPlace;
}