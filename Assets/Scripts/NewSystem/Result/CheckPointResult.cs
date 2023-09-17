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
    public TextMeshProUGUI HitAverageText;
    public TextMeshProUGUI KillCountText;
    public TextMeshProUGUI ClearTimeText;
    public TextMeshProUGUI AttractGetValueText;
    public TextMeshProUGUI SumScoreText;



    [SerializeField]
    private float _waitTime = 0.3f;

    [SerializeField]
    private float _upSpeed = 5f;

    public struct ResultStruct
    {
        public int HitAverage;
        public int KillCount;
        public float ClearTime;
        public int AttractValue;
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

    public void Result(ref ResultStruct resultStruct)
    {
        HitAverageText.text = resultStruct.HitAverage.ToString();
        KillCountText.text = resultStruct.KillCount.ToString();
        ClearTimeText.text = resultStruct.ClearTime.ToString();
        AttractGetValueText.text = resultStruct.AttractValue.ToString();
        SumScoreText.text = resultStruct.SumScore.ToString();

    }
    private void Update()
    {
        //int nowint = default;
        //int lastInt = default;
        //while (nowint == lastInt)
        //    nowint = Random.Range(0, fontAssets.Count - 1);

        //lastInt = nowint;

        //killCountText.font = fontAssets[nowint];
        //killScoreText.font = fontAssets[nowint];
        //numberOfCombosText.font = fontAssets[nowint];
        //clearTimeText.font = fontAssets[nowint];
        //sumScoreText.font = fontAssets[nowint];
    }

    IEnumerator DestroyCount()
    {
        yield return new WaitForEndOfFrame();

    }
    #endregion
}