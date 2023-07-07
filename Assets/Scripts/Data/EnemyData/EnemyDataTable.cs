// --------------------------------------------------------- 
// EnemyDataTable.cs 
// 
// CreateDay: 2023/07/07
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

// Assets > Create > Scriptables > CreateEnemyDataTableでアセット化
[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptables/CreateEnemyDataTable")]
[System.Serializable]
public class EnemyDataTable : ScriptableObject
{
    [Header("敵の種類"), Tooltip("敵の種類")]
    public EnemyType _enemyType;

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