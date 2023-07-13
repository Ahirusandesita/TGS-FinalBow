// --------------------------------------------------------- 
// EnemyDataTable.cs 
// 
// CreateDay: 2023/07/07
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

// Assets > Create > Scriptables > CreateBirdDataTableでアセット化
[CreateAssetMenu(fileName = "BirdData", menuName = "Scriptables/CreateBirdDataTable")]
[System.Serializable]
public class BirdDataTable : EnemyDataTable
{
    [Header("鳥雑魚の種類"), Tooltip("鳥雑魚の種類")]
    public BirdType _birdType;

    [Tooltip("動きの種類")]
    public MoveType _moveType;

    [Header("弧の高さ"), Tooltip("弧の高さ")]
    [HideInInspector]
    public float _arcHeight = 10f;

    [Header("弧の向き"), Tooltip("弧の向き")]
    [HideInInspector]
    public ArcMoveDirection _arcMoveDirection;

    [Header("出す弾の数（推奨：奇数）"), Tooltip("出す弾の数（推奨：奇数）")]
    public int _bullet;

    [Header("攻撃間隔"), Tooltip("攻撃間隔")]
    public float _attackInterval_s;

    [Header("スポーンディレイ（注意：Wave開始からの秒数）"), Tooltip("スポーンディレイ（注意：Wave開始からの秒数）")]
    public float _spawnDelay_s;

    [Header("鳥雑魚のスポーン位置"), Tooltip("鳥雑魚のスポーン位置")]
    public Transform _birdSpawnPlace;

    [Header("鳥雑魚のゴール位置"), Tooltip("鳥雑魚のゴール位置")]
    public List<BirdGoalInformation> _birdGoalPlaces = new List<BirdGoalInformation>();
}

[System.Serializable]
public class BirdGoalInformation
{
    [Header("鳥雑魚のゴール位置"), Tooltip("鳥雑魚のゴール位置")]
    public Transform _birdGoalPlace;

    [Header("このゴールまでの移動スピード"), Tooltip("このゴールまでの移動スピード")]
    public float _speed;

    [Header("このゴールで停止して攻撃する秒数"), Tooltip("このゴールで停止して攻撃する秒数（注：ゴールが複数設定された場合、最後のゴールのこの変数は無視される）")]
    public float _stayTime_s;
}