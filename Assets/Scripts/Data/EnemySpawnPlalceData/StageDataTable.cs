// --------------------------------------------------------- 
// EnemySpawnerTable.cs 
// 
// CreateDay: 2023/06/22
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// ステージ情報を保存するクラス
/// </summary>
// Assets > Create > Scriptables > CreateEnemySpawnerTableでアセット化
[CreateAssetMenu(fileName = "StageData", menuName = "Scriptables/CreateStageDataTable")]
public class StageDataTable : ScriptableObject
{
    [Tooltip("ウェーブリスト")]
    public List<WaveInformation> _waveInformation = new();
}

/// <summary>
/// 各Waveの敵の情報
/// </summary>
// Inspectorで変更した値がアセットとして保存される
[System.Serializable]
public class WaveInformation
{
    [Tooltip("Wave名")]
    public string _wave;

    [Header("Wave開始ディレイ"), Tooltip("Wave開始ディレイ")]
    public float _startDelay_s;

    [Header("鳥雑魚データ"), Tooltip("鳥雑魚データ")]
    public List<BirdDataTable> _birdsData = new();

    [Header("地上雑魚データ"), Tooltip("地上雑魚データ")]
    public List<GroundEnemyDataTable> _groundEnemysData = new();

    [Header("的のデータ"), Tooltip("的のデータ")]
    public List<TargetDataTable> _targetData = new();
}