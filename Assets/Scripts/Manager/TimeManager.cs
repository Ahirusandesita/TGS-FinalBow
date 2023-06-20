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

    private void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        _scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager>();

        if(_gameManager.TimeManager != null)
        {
            time = _gameManager.TimeManager.GetTime();
        }

        StartCoroutine(TimeCount());
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
            time++;
            _gameManager.TimeManager = this;
            _scoreManager.BonusValue_Time(time);

            if(time % ONE_MINUTES == MINUTES_COUNTTIME && time != START_TIME)
            {
                _scoreManager.BonusScore_TimeScore();
            }


        }

    }


}
