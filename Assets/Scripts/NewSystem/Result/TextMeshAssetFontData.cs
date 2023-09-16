// --------------------------------------------------------- 
// ResultTextMeshAssetFontData.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TextMeshAssetFontData", menuName = "TextMeshScriptables/CreateTextMeshAssetFontTable")]
public class TextMeshAssetFontData : ScriptableObject
{
    public List<TMP_FontAsset> fontAssets = new List<TMP_FontAsset>();
}