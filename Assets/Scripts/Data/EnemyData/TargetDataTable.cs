// --------------------------------------------------------- 
// TargetDataTable.cs 
// 
// CreateDay: 2023/08/30
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TargetData", menuName = "Scriptables/CreateTargetDataTable")]
public class TargetDataTable : ScriptableObject
{
    [Header("スポーンディレイ（注意：Wave開始からの秒数）"), Tooltip("スポーンディレイ（注意：Wave開始からの秒数）")]
    public float _spawnDelay_s;

    [Header("スポーンする位置"), Tooltip("スポーンする位置")]
    public Transform _spawnPlace;

    [Header("動きの種類"), Tooltip("動きの種類")]
    public MoveType _moveType;

    [Header("ループの種類（forward：順行,　reverse：逆行）"), Tooltip("ループの種類")]
    public RoopType _roopType;

    [Header("的の挙動の詳細リスト"), Tooltip("的の挙動の詳細リスト")]
    public List<TargetMoveInformation> _targetMoveInformations = new();
}

[System.Serializable]
public class TargetMoveInformation
{
    [Header("ゴールの位置"), Tooltip("ゴールの位置")]
    public Transform _goalPlace;

    [Header("移動スピード"), Tooltip("移動スピード")]
    public float _speed;

    [Header("停止時間"), Tooltip("停止時間")]
    public float _stayTime_s;
}