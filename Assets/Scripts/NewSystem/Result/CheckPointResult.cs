// --------------------------------------------------------- 
// CheckPointResult.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CheckPointResult : MonoBehaviour
{
    #region variable 
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI killScoreText;
    public TextMeshProUGUI numberOfCombosText;
    public TextMeshProUGUI clearTimeText;
    public TextMeshProUGUI sumScoreText;

    public List<TMP_FontAsset> fontAssets = new List<TMP_FontAsset>();

    public struct ResultStruct
    {
        public int NumberOfKills;
        public int KillsScore;
        public int NumberOfCombos;
        public float ClearTime;
        public int SumScore;
    }
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        //text = this.GetComponent<TextMeshProUGUI>();
    }

    public void Result(ResultStruct resultStruct)
    {
        killCountText.text = "���j��" + resultStruct.NumberOfKills;
        killScoreText.text = "���j�X�R�A" + resultStruct.KillsScore;
        numberOfCombosText.text = "�R���{�X�R�A" + resultStruct.NumberOfCombos;
        clearTimeText.text = "�N���A�^�C��" + resultStruct.ClearTime.ToString();
        sumScoreText.text = "���v�X�R�A" + resultStruct.SumScore.ToString();

    }
    private void Update()
    {
        int nowint = default;
        int lastInt = default;
        while (nowint == lastInt)
            nowint = Random.Range(0, fontAssets.Count - 1);

        lastInt = nowint;

        killCountText.font = fontAssets[nowint];
        killScoreText.font = fontAssets[nowint];
        numberOfCombosText.font = fontAssets[nowint];
        clearTimeText.font = fontAssets[nowint];
        sumScoreText.font = fontAssets[nowint];
    }
    #endregion
}