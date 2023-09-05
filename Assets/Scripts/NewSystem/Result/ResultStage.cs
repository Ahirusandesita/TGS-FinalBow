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

    //bool‚ðŠÄŽ‹
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
        Debug.Log(score.SumScore);
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