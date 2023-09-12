// --------------------------------------------------------- 
// FinalResult.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class FinalResult : MonoBehaviour
{
    #region variable 
    public SceneObject titleScene;

    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI killScoreText;
    public TextMeshProUGUI numberOfCombosText;
    public TextMeshProUGUI clearTimeText;
    public TextMeshProUGUI sumScoreText;

    public List<TMP_FontAsset> fontAssets = new List<TMP_FontAsset>();
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
        sumScoreText.text = score.SumScore.ToString();

    }

    private void Update()
    {
        int nowint = default;
        int lastInt = default;
        while (nowint == lastInt)
            nowint = Random.Range(0, fontAssets.Count - 1);

        lastInt = nowint;

        killCountText.font = fontAssets[nowint];
        killScoreText.font = fontAssets[nowint];
        numberOfCombosText.font = fontAssets[nowint];
        clearTimeText.font = fontAssets[nowint];
        sumScoreText.font = fontAssets[nowint];


        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.FindObjectOfType<SceneManagement>().SceneLoadSpecifyMove(titleScene);
        }
    }

    #endregion
}