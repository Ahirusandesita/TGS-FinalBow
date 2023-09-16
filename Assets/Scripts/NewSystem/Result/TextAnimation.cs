// --------------------------------------------------------- 
// TextAnimation.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class TextAnimation : MonoBehaviour
{
    #region variable 
    private TextMeshProUGUI textMeshProUGUI;
    public TextMeshAssetFontData textMeshAssetFontData;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        textMeshProUGUI = this.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        int nowint = default;
        int lastInt = default;
        while (nowint == lastInt)
            nowint = Random.Range(0,textMeshAssetFontData.fontAssets.Count - 1);

        lastInt = nowint;

        textMeshProUGUI.font = textMeshAssetFontData.fontAssets[nowint];
    }
    #endregion
}