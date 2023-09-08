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
        //Debug.LogError($"合計スコア{score.SumScore}");
        //Debug.LogError($"ノーマル撃破数{score.valueNormalEnemy}。ノーマルスコア{score.scoreNormalEnemy}");
       // Debug.LogError($"コンボスコア{score.scoreComboBonus}");
    }
    public void ResultScreenTime(float time)
    {
        //Debug.LogError($"クリアタイム{time}");
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