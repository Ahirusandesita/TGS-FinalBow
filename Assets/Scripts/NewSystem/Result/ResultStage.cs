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
        X_Debug.Log("���v�X�R�A�F" + score.SumScore);
        X_Debug.Log("�^�C���X�R�A�F" + score.scoreTimeBonus + "aaaa�F" + score.valueTimeBonus);
        X_Debug.Log("Hp�F" + score.scoreHpBonus + "AAAA�F" + score.valueHpBonus);
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