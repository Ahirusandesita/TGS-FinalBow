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
public class BirdDataTable : ScriptableObject
{
    [Header("鳥雑魚の種類"), Tooltip("鳥雑魚の種類")]
    public BirdType _birdType;

    [Header("出す弾の数（推奨：奇数）"), Tooltip("出す弾の数（推奨：奇数）")]
    public int _bullet;

    [Header("スポーンディレイ（注意：Wave開始からの秒数）"), Tooltip("スポーンディレイ（注意：Wave開始からの秒数）")]
    public float _spawnDelay_s;

    [Header("鳥雑魚のスポーン位置"), Tooltip("鳥雑魚のスポーン位置")]
    public Transform _birdSpawnPlace;

    [Header("鳥雑魚のゴール位置"), Tooltip("鳥雑魚のゴール位置")]
    public List<BirdGoalInformation> _birdGoalPlaces = new();

    [Header("ループ先のゴール番号（リストのインデックス）"), Tooltip("ループ先のゴール番号")]
    public int _goalIndexOfRoop;
}

[System.Serializable]
public class BirdGoalInformation
{
    [Tooltip("動きの種類")]
    public MoveType _moveType;

    [Tooltip("弧の高さ")]
    public float _arcHeight;

    [Tooltip("弧の向き")]
    public ArcMoveDirection _arcMoveDirection;

    [Tooltip("鳥雑魚のゴール位置")]
    public Transform _birdGoalPlace;

    [Tooltip("このゴールまでの移動スピード")]
    public float _speed;

    [Tooltip("このゴールで停止して攻撃する秒数（注：ゴールが複数設定された場合、最後のゴールのこの変数は無視される）")]
    public float _stayTime_s;

    [Tooltip("攻撃方法の種類")]
    public BirdAttackType _birdAttackType_a;
    public BirdAttackType _birdAttackType_b;

    [Tooltip("攻撃間隔")]
    public float _attackInterval_s_a;
    public float _attackInterval_s_b;

    [Tooltip("攻撃を行うタイミング（秒）")]
    public float _attackTiming_s1_a;
    public float _attackTiming_s2_a;
    public float _attackTiming_s3_a;
    public float _attackTiming_s4_a;
    public float _attackTiming_s5_a;
    public float _attackTiming_s1_b;
    public float _attackTiming_s2_b;
    public float _attackTiming_s3_b;
    public float _attackTiming_s4_b;
    public float _attackTiming_s5_b;

    [Tooltip("連続攻撃回数")]
    public int _attackTimes_a;
    public int _attackTimes_b;

    [Tooltip("連続攻撃クールタイム（秒）")]
    public float _cooldownTime_s_a;
    public float _cooldownTime_s_b;

    [Tooltip("移動時にどの方向を向くか")]
    public DirectionType_AtMoving _directionType_moving;

    [Tooltip("停止時にどの方向を向くか")]
    public DirectionType_AtStopping _directionType_stopping;

    [Tooltip("攻撃時にどの方向を向くか")]
    public DirectionType_AtAttack _directionType_attack;
}