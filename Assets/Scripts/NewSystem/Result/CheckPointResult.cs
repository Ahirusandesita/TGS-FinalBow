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



    [SerializeField]
    private float _waitTime = 0.3f;

    [SerializeField]
    private float _upSpeed = 5f;

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

    public void Result(ref ResultStruct resultStruct)
    {
        killCountText.text = resultStruct.NumberOfKills.ToString();
        killScoreText.text = resultStruct.KillsScore.ToString();
        numberOfCombosText.text = resultStruct.NumberOfCombos.ToString();
        clearTimeText.text = resultStruct.ClearTime.ToString();
        sumScoreText.text = resultStruct.SumScore.ToString();

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