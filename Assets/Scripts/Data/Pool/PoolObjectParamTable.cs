// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プールオブジェクトのパラメータを設定して保存するクラス
/// </summary>
// Assets > Create > Scriptables > CreatePoolObjectTableでアセット化
[CreateAssetMenu(fileName = "PoolObjectData", menuName = "Scriptables/CreatePoolObjectTable")]
public class PoolObjectParamTable : ScriptableObject
{
    // PoolInfomationクラスの内容を持ったリストを生成
    public List<PoolInformation> _scriptablePoolInformation = new List<PoolInformation>();

    // EffectInfomationクラスの内容を持ったリストを生成
    public List<EffectInformation> _scriptableEffectInformation = new List<EffectInformation>();
}

/// <summary>
/// プールオブジェクトの情報
/// </summary>
// Inspectorで変更した値がアセットとして保存される
[System.Serializable]
public class PoolInformation
{
    [Tooltip("オブジェクト名")]
    public string _name = default;
    [Tooltip("インスタンス化するプレハブ")]
    public CashObjectInformation _prefab = default;
    [Tooltip("キューの最大容量")]
    public int _queueMax = default;
    [Tooltip("プールオブジェクトの生成方法")]
    public CreateType _createType = CreateType.automatic;
}

/// <summary>
/// プールエフェクトの種類
/// </summary>
[System.Serializable]
public class EffectInformation
{
    [Tooltip("エフェクト名")]
    public string _name = default;

    [Tooltip("インスタンス化するエフェクトプレハブ")]
    public GameObject _prefab = default;

    [Tooltip("キューの最大容量")]
    public int _queueMax = default;
}