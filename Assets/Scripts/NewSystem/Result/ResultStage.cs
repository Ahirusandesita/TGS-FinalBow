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
        //Debug.LogError($"���v�X�R�A{score.SumScore}");
        //Debug.LogError($"�m�[�}�����j��{score.valueNormalEnemy}�B�m�[�}���X�R�A{score.scoreNormalEnemy}");
       // Debug.LogError($"�R���{�X�R�A{score.scoreComboBonus}");
    }
    public void ResultScreenTime(float time)
    {
        //Debug.LogError($"�N���A�^�C��{time}");
    }


    private void DeleteResultScreen()
    {

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