// --------------------------------------------------------- 
// SoundParamTable.cs 
// 
// CreateDay: 2023/06/09
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンドのパラメータを設定して保存するクラス
/// </summary>
// Assets > Create > Scriptables > CreateSoundTableでアセット化
[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptables/CreateSoundTable")]
public class SoundParamTable : ScriptableObject
{
    // SoundInfomationクラスの内容を持ったリストを生成
    public List<SoundInformation> _scriptableSoundInformation = new List<SoundInformation>();
}

/// <summary>
/// サウンドの種類
/// </summary>
// Inspectorで変更した値がアセットとして保存される
[System.Serializable]
public class SoundInformation
{
    [Tooltip("サウンド名")]
    public string _name = default;

    [Tooltip("サウンドデータ")]
    public AudioClip _audioClip = default;
}