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
        text.text = "���j��" + killsToString + "   ���j�X�R�A" + resultStruct.KillsScore + "\n" + "�R���{�X�R�A" + resultStruct.NumberOfCombos.ToString() + "\n" + "�N���A�^�C��" + resultStruct.ClearTime.ToString() + "\n" + "���v�X�R�A" + resultStruct.SumScore.ToString();
    }
    #endregion
}