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
        if (Input.GetKeyDown(KeyCode.N))
        {
            stagePropery.Value = false;
        }
    }

    #endregion
}