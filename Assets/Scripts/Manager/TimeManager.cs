// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
interface ITime
{
    /// <summary>
    /// 残り時間
    /// </summary>
    /// <returns></returns>
    float TimeCounter();
}
public class TimeManager :MonoBehaviour , ITime
{

    private int time = 0;
    private const int START_TIME = 0;
    private const int ONE_MINUTES = 60;
    private const int MINUTES_COUNTTIME = 0;

    private IFScoreManager_Time _scoreManager;
    private GameManager _gameManager;
    private ResultStage resultStage;

    private bool canTimeCount = false;
    private GameProgress gameProgress;
    private void Awake()
    {
        gameProgress = GameObject.FindObjectOfType<GameProgress>();

        gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if (progressType == GameProgressType.tutorial) canTimeCount = false;
                if (progressType == GameProgressType.inGame) canTimeCount = true;
                if (progressType == GameProgressType.inGameLastStageEnd) canTimeCount = false;
                if (progressType == GameProgressType.extra) canTimeCount = true;
            }
            );
    }

    private void Start()
    {
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _scoreManager = GameObject.FindWithTag("ScoreController").GetComponent<ScoreManager>();

        if(_gameManager.TimeManager != null)
        {
            time = _gameManager.TimeManager.GetTime();
        }

        StartCoroutine(TimeCount());

        resultStage = GameObject.FindObjectOfType<ResultStage>();
        GameProgress gameProgress = GameObject.FindObjectOfType<GameProgress>();
        gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if(progressType == GameProgressType.result)
                {
                    resultStage.ResultScreenTime(time);
                }
            }
            );
    }

    public float TimeCounter()
    {
        //キャッチのときとかに呼ぶ
        return 1;
    }

    public int GetTime()
    {
        return time;
    }

    private IEnumerator TimeCount()
    {

        while (true)
        {

            yield return new WaitForSeconds(1f);

            if (canTimeCount)
            {
                time++;
                _gameManager.TimeManager = this;
                _scoreManager.BonusValue_Time(1);

                if (time % ONE_MINUTES == MINUTES_COUNTTIME && time != START_TIME)
                {
                    _scoreManager.BonusScore_TimeScore();
                }
            }


        }

    }


}
