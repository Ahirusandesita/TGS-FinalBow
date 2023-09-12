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

    private CheckPointResult checkPointResult;
    private CheckPointResult.ResultStruct resultStruct;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        checkPointResult = GameObject.FindObjectOfType<CheckPointResult>();
        stagePropery.Subject.Subscribe(
            isResult =>
            {
                if (!isResult) DeleteResultScreen();
            }
            );
        stagePropery.Subject.Subscribe(
            isResult =>
            {
                if(isResult)
                checkPointResult.gameObject.SetActive(true);
            }
            );
    }

    public void Result()
    {
        stagePropery.Value = true;
        checkPointResult.Result(resultStruct);
    }
    public void EndStageResult() => stagePropery.Value = false;

    public void ResultScreenScore(ScoreNumber.Score score)
    {
        //Debug.LogError($"合計スコア{score.SumScore}");
        //Debug.LogError($"ノーマル撃破数{score.valueNormalEnemy}。ノーマルスコア{score.scoreNormalEnemy}");
        // Debug.LogError($"コンボスコア{score.scoreComboBonus}");
        resultStruct.NumberOfKills = score.valueNormalEnemy;
        resultStruct.NumberOfCombos = score.scoreComboBonus;
        resultStruct.KillsScore = score.scoreNormalEnemy;
        resultStruct.SumScore = score.SumScore;
    }
    public void ResultScreenTime(float time)
    {
        //Debug.LogError($"クリアタイム{time}");
        resultStruct.ClearTime = time;
    }


    private void DeleteResultScreen()
    {
        checkPointResult.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            stagePropery.Value = false;
        }
    }

    #endregion
}