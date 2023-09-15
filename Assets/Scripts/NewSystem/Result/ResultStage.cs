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
    private ResultString resultString;
    private bool isOne = true;
    private ScoreFrameMaganer scoreFrameMaganer;
    InputManagement inputManagement = default;

    private GameProgress gameProgress;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        inputManagement = GameObject.FindObjectOfType<InputManagement>();
        checkPointResult = GameObject.FindObjectOfType<CheckPointResult>();
        gameProgress = GameObject.FindObjectOfType<GameProgress>();
        gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if (progressType == GameProgressType.result)
                {
                    checkPointResult.gameObject.SetActive(true);
                    scoreFrameMaganer.OpenFrame();
                }
                if (progressType == GameProgressType.ending)
                {
                    DeleteResultScreen();
                }
            }
            );
        if (checkPointResult == null) return;
        resultString = checkPointResult.gameObject.transform.GetChild(7).gameObject.GetComponent<ResultString>();
        scoreFrameMaganer = checkPointResult.gameObject.transform.GetChild(0).gameObject.GetComponent<ScoreFrameMaganer>();
        //stagePropery.Subject.Subscribe(
        //    isResult =>
        //    {
        //        if (!isResult) DeleteResultScreen();
        //    }
        //    );
        //stagePropery.Subject.Subscribe(
        //    isResult =>
        //    {
        //        if (isResult)
        //        {
        //            checkPointResult.gameObject.SetActive(true);
        //            scoreFrameMaganer.OpenFrame();
        //        }
        //    }
        //    );


    }

    public void Result() => stagePropery.Value = true;
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


        if (scoreFrameMaganer != null)
        {
            if (scoreFrameMaganer._endOpen)
            {
                if (isOne)
                {
                    checkPointResult.Result(ref resultStruct);
                    resultString.Result();
                    isOne = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            gameProgress.ResultEnding();
            //if (stagePropery.Value)
            //    stagePropery.Value = false;
        }

        if (inputManagement.ButtonLeftDownTrigger() || inputManagement.ButtonRightDownTrigger())
        {
            //gameProgress.ResultEnding();
            //if (stagePropery.Value)
            //    stagePropery.Value = false;
        }
    }

    #endregion
}