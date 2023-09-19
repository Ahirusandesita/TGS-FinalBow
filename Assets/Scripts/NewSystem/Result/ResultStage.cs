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
    private bool isOne = false;
    private bool isScore = false;
    private bool isTime = false;
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
                if (progressType == GameProgressType.gamePreparation)
                {
                    checkPointResult.gameObject.SetActive(false);
                }
                if (progressType == GameProgressType.result)
                {
                    isOne = true;
                    checkPointResult.gameObject.SetActive(true);
                }
                if (progressType == GameProgressType.ending)
                {
                    DeleteResultScreen();
                }
            }
            );
        if (checkPointResult == null) return;
        //resultString = checkPointResult.gameObject.transform.GetChild(7).gameObject.GetComponent<ResultString>();
        //scoreFrameMaganer = checkPointResult.gameObject.transform.GetChild(0).gameObject.GetComponent<ScoreFrameMaganer>();
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
        if (score.shotCount <= 0) resultStruct.HitAverage = 0.00f;
        else
            resultStruct.HitAverage = Mathf.Floor(score.hitCount / score.shotCount * 10000f) / 100f;
        resultStruct.AttractValue = score.valueAttractBonus;
        resultStruct.KillCount = score.valueNormalEnemy;
        resultStruct.SumScore = score.SumScore + (int)(resultStruct.HitAverage * 100f);
        isScore = true;
    }
    public void ResultScreenTime(float time)
    {
        //Debug.LogError($"クリアタイム{time}");
        resultStruct.ClearTime = time;
        isTime = true;
    }


    private void DeleteResultScreen()
    {
        checkPointResult.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isScore && isTime)
            if (isOne)
            {
                checkPointResult.Result(ref resultStruct);
                isOne = false;
                isScore = false;
                isTime = false;
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