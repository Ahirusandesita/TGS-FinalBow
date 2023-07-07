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
    [Header("雑魚の出現位置"), Tooltip("雑魚の出現位置")]
    public List<WaveEnemyInformation> _scriptableWaveEnemy = new List<WaveEnemyInformation>();
}

/// <summary>
/// 各Waveの敵の情報
/// </summary>
// Inspectorで変更した値がアセットとして保存される
[System.Serializable]
public class WaveEnemyInformation
{
    [Tooltip("Wave名")]
    public string _wave;

    [Header("Waveの開始時間（前Waveからの時間）"), Tooltip("Waveの開始時間（ex: Wave1に1.5を設定すると、ゲーム開始後1.5s後にWave1の敵がスポーンする")]
    public float _startWaveTime_s;

    public List<EnemySpawnerInformation> _enemysSpawner = new List<EnemySpawnerInformation>();
}

/// <summary>
/// 敵のスポナーの情報
/// </summary>
[System.Serializable]
public class EnemySpawnerInformation
{
    [Header("敵の種類"), Tooltip("敵の種類")]
    public BirdType _enemyType;

    [Tooltip("動きの種類")]
    public MoveType _moveType;

    [Header("移動スピード（Linearのみ）"), Tooltip("直線移動のスピード")]
    public float _speed;

    [Header("出す弾の数（推奨：奇数）"), Tooltip("出す弾の数（推奨：奇数）")]
    public int _bullet;

    [Header("停止して攻撃する秒数（Linearのみ）"), Tooltip("停止して攻撃する秒数")]
    public float _waitTime_s;

    [Header("スポーンディレイ（注意：Wave開始からの秒数）"), Tooltip("スポーンディレイ（注意：Wave開始からの秒数）")]
    public float _spawnDelay_s;

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