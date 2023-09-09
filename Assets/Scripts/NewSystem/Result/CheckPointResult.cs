// --------------------------------------------------------- 
// CheckPointResult.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CheckPointResult : MonoBehaviour
{
    #region variable 
    private Text text;

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
        text = this.GetComponent<Text>();
    }

    public void Result(ResultStruct resultStruct)
    {
        string killsToString = resultStruct.NumberOfKills.ToString();
        text.text = "撃破数" + killsToString + "   撃破スコア" + resultStruct.KillsScore + "\n" + "コンボスコア" + resultStruct.NumberOfCombos.ToString() + "\n" + "クリアタイム" + resultStruct.ClearTime.ToString() + "\n" + "合計スコア" + resultStruct.SumScore.ToString();
    }
    #endregion
}