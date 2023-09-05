// --------------------------------------------------------- 
// ResultStage.cs 
// 
// CreateDay: 
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;


public class ResultStage : MonoBehaviour
{
    #region variable 

    //boolを監視
    public IReActiveProperty<bool> readOnlyStateProperty => stagePropery;
    private ReActiveProperty<bool> stagePropery = new ReActiveProperty<bool>();

    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {

        stagePropery.Subject.Subscribe(
            isResult =>
            {
                if (!isResult) DeleteResultScreen();
            }
            );
    }

    public void Result()=> stagePropery.Value = true;

    public void ResultScreenScore(ScoreNumber.Score score)
    {
        X_Debug.Log("合計スコア：" + score.SumScore);
        X_Debug.Log("タイムスコア：" + score.scoreTimeBonus + "aaaa：" + score.valueTimeBonus);
        X_Debug.Log("Hp：" + score.scoreHpBonus + "AAAA：" + score.valueHpBonus);
    }
    public void ResultScreenTime(float time)
    {
        Debug.Log(time);
    }


    private void DeleteResultScreen()
    {

    }


    #endregion
}