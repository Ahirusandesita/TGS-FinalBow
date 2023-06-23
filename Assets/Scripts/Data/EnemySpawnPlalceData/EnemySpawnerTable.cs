// --------------------------------------------------------- 
// EnemySpawnerTable.cs 
// 
// CreateDay: 2023/06/22
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 敵のスポナーを設定して保存するクラス
/// </summary>
// Assets > Create > Scriptables > CreateEnemySpawnerTableでアセット化
[CreateAssetMenu(fileName = "EnemySpawnerData", menuName = "Scriptables/CreateEnemySpawnerTable")]
public class EnemySpawnerTable : ScriptableObject
{
    [Header("マップ中心の座標"), Tooltip("マップ中心の座標")]
    public CentralInformation _centralInformation;
    [Header("雑魚の出現位置リスト"), Tooltip("雑魚の出現位置リスト")]
    public List<EnemySpawnerInformation> _scriptableESpawnerInformation = new List<EnemySpawnerInformation>();
}

/// <summary>
/// 敵のスポナーの情報
/// </summary>
// Inspectorで変更した値がアセットとして保存される
[System.Serializable]
public class EnemySpawnerInformation
{
    [Tooltip("ウェーブ名")]
    public string _wave;

    [Tooltip("雑魚のスポーン位置")]
    public List<Transform> _birdSpawnPlaces = new List<Transform>();

    [Tooltip("雑魚のゴール位置")]
    public List<Transform> _birdGoalPlaces = new List<Transform>();
}

[System.Serializable]
public class CentralInformation
{
    public Transform _centralTransform;
}