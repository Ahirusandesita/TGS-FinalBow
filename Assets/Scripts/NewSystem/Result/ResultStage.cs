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

    //bool���Ď�
    public IReActiveProperty<bool> readOnlyStateProperty => stagePropery;
    private ReActiveProperty<bool> stagePropery = new ReActiveProperty<bool>();

    private CheckPointResult checkPointResult;
    private CheckPointResult.ResultStruct resultStruct;
    private ResultString resultString;
    private bool isOne = true;
    private ScoreFrameMaganer scoreFrameMaganer;
    InputManagement inputManagement = default;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        inputManagement = GameObject.FindObjectOfType<InputManagement>();
        checkPointResult = GameObject.FindObjectOfType<CheckPointResult>();
        if (checkPointResult == null) return;
        resultString = checkPointResult.gameObject.transform.GetChild(7).gameObject.GetComponent<ResultString>();
        scoreFrameMaganer = checkPointResult.gameObject.transform.GetChild(0).gameObject.GetComponent<ScoreFrameMaganer>();
        stagePropery.Subject.Subscribe(
            isResult =>
            {
                if (!isResult) DeleteResultScreen();
            }
            );
        stagePropery.Subject.Subscribe(
            isResult =>
            {
                if (isResult)
                {
                    checkPointResult.gameObject.SetActive(true);
                    scoreFrameMaganer.OpenFrame();
                }
            }
            );
    }

    public void Result() => stagePropery.Value = true;
    public void EndStageResult() => stagePropery.Value = false;

    public void ResultScreenScore(ScoreNumber.Score score)
    {
        //Debug.LogError($"���v�X�R�A{score.SumScore}");
        //Debug.LogError($"�m�[�}�����j��{score.valueNormalEnemy}�B�m�[�}���X�R�A{score.scoreNormalEnemy}");
        // Debug.LogError($"�R���{�X�R�A{score.scoreComboBonus}");
        resultStruct.NumberOfKills = score.valueNormalEnemy;
        resultStruct.NumberOfCombos = score.scoreComboBonus;
        resultStruct.KillsScore = score.scoreNormalEnemy;
        resultStruct.SumScore = score.SumScore;
    }
    public void ResultScreenTime(float time)
    {
        //Debug.LogError($"�N���A�^�C��{time}");
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
                    checkPointResult.Result(resultStruct);
                    resultString.Result();
                    isOne = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (stagePropery.Value)
                stagePropery.Value = false;
        }

        if (inputManagement.ButtonLeftDownTrigger() || inputManagement.ButtonRightDownTrigger())
        {
            if (stagePropery.Value)
                stagePropery.Value = false;
        }
    }

    #endregion
}