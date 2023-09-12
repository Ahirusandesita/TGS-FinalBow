// --------------------------------------------------------- 
// ResultString.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public struct TextMeshProUGUI_ResultStruct
{
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI killScoreText;
    public TextMeshProUGUI numberOfCombosText;
    public TextMeshProUGUI clearTimeText;
    public TextMeshProUGUI sumScoreText;


}

public class ResultString : MonoBehaviour
{
    #region variable 
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI killScoreText;
    public TextMeshProUGUI numberOfCombosText;
    public TextMeshProUGUI clearTimeText;
    public TextMeshProUGUI sumScoreText;

    private float alphaPlus = 1f;
    private float nowAlpha = 0f;

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

    public void Result()
    {
        nowAlpha = 0f;
        killCountText.alpha = nowAlpha;
        killScoreText.alpha = nowAlpha;
        numberOfCombosText.alpha = nowAlpha;
        clearTimeText.alpha = nowAlpha;
        sumScoreText.alpha = nowAlpha;


        killCountText.text = "撃破数";
        killScoreText.text = "撃破スコア";
        numberOfCombosText.text = "コンボスコア";
        clearTimeText.text = "クリアタイム";
        sumScoreText.text = "合計スコア";
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

        nowAlpha += alphaPlus * Time.deltaTime;
        killCountText.alpha = nowAlpha;
        killScoreText.alpha = nowAlpha;
        numberOfCombosText.alpha = nowAlpha;
        clearTimeText.alpha = nowAlpha;
        sumScoreText.alpha = nowAlpha;

    }
    #endregion
}