// --------------------------------------------------------- 
// FinalResult.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using TMPro;

public class FinalResult : MonoBehaviour
{
    #region variable 
    public SceneObject titleScene;

    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI killScoreText;
    public TextMeshProUGUI numberOfCombosText;
    public TextMeshProUGUI clearTimeText;
    public TextMeshProUGUI sumScoreText;

    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        ScoreDisPlay(ScoreNumber.ScorePoint);
        GameObject.FindObjectOfType<ScoreManager>().ScoreReset();
        this.transform.GetChild(7).gameObject.GetComponent<ResultString>().Result();
    }

    public void ScoreDisPlay(ScoreNumber.Score score)
    {

        killCountText.text = score.valueNormalEnemy.ToString();
        killScoreText.text = score.scoreNormalEnemy.ToString();
        numberOfCombosText.text = score.scoreComboBonus.ToString();
        clearTimeText.text = score.valueTimeBonus.ToString();
        sumScoreText.text= score.SumScore.ToString();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.FindObjectOfType<SceneManagement>().SceneLoadSpecifyMove(titleScene);
        }
    }

    #endregion
}