// --------------------------------------------------------- 
// TargetDataTable.cs 
// 
// CreateDay: 2023/08/30
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;

[CreateAssetMenu(fileName = "TargetData", menuName = "Scriptables/CreateTargetDataTable")]
public class TargetDataTable : ScriptableObject
{
    [Header("スポーンディレイ（注意：Wave開始からの秒数）"), Tooltip("スポーンディレイ（注意：Wave開始からの秒数）"), Min(0)]
    public float _spawnDelay_s;

    [Header("スポーンする位置"), Tooltip("スポーンする位置")]
    public Transform _spawnPlace;

    [Header("的を動かす"), Tooltip("的を動かす")]
    public bool _needMove;

    [HideInInspector, Tooltip("ゴールの位置")]
    public Transform _goalPlace;

    [HideInInspector, Tooltip("移動スピード")]
    public float _speed;

    [HideInInspector, Tooltip("停止時間")]
    public float _stayTime_s;
}