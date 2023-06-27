// --------------------------------------------------------- 
// EnemySpawnerTable.cs 
// 
// CreateDay: 2023/06/22
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// 敵の動きの種類
/// </summary>
public enum MoveType
{
    /// <summary>
    /// 直線移動
    /// </summary>
    linear,
    /// <summary>
    /// 曲線移動
    /// </summary>
    curve
}


/// <summary>
/// 敵のスポナーを設定して保存するクラス
/// </summary>
// Assets > Create > Scriptables > CreateEnemySpawnerTableでアセット化
[CreateAssetMenu(fileName = "EnemySpawnerData", menuName = "Scriptables/CreateEnemySpawnerTable")]
public class EnemySpawnerTable : ScriptableObject
{
    [Header("マップ中心の座標"), Tooltip("マップ中心の座標")]
    public CentralInformation _centralInformation;
    [Header("雑魚の出現位置"), Tooltip("雑魚の出現位置")]
    public List<WaveEnemyInformation> _scriptableWaveEnemy = new List<WaveEnemyInformation>();
}

/// <summary>
/// 各ウェーブの敵の情報
/// </summary>
// Inspectorで変更した値がアセットとして保存される
[System.Serializable]
public class WaveEnemyInformation
{
    [Tooltip("ウェーブ名")]
    public string _wave;

    [Tooltip("動きの種類")]
    public MoveType _moveType;

    public List<EnemySpawnerInformation> _enemysSpawner = new List<EnemySpawnerInformation>();
}

/// <summary>
/// 敵のスポナーの情報
/// </summary>
[System.Serializable]
public class EnemySpawnerInformation
{
    [Header("雑魚のスポーン位置"), Tooltip("雑魚のスポーン位置")]
    public Transform _birdSpawnPlace;

    [Header("雑魚のゴール位置"), Tooltip("雑魚のゴール位置")]
    public List<Transform> _birdGoalPlaces = new List<Transform>();
}

[System.Serializable]
public class CentralInformation
{
    public Transform _centralTransform;
}