// --------------------------------------------------------- 
// ScoreManager.cs 
// 
// CreateDay: 2023/06/18
// Creator  : Nomura
// --------------------------------------------------------- 
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IFScoreManager_NomalEnemy
{
    void NormalScore_NormalEnemyScore();
}
public interface IFScoreManager_NomalEnemyGetScore
{
    int NormalScore_NomalEnemyGetScore();
}

public interface IFScoreManager_BossEnemy
{
    void NormalScore_BossEnemyScore();
}
public interface IFScoreManager_BossEnemyGetScore
{
    int NormalScore_BossEnemyGetScore();
}


public interface IFScoreManager_AllEnemy : IFScoreManager_NomalEnemy, IFScoreManager_BossEnemy { }

interface IFScoreManager_AllEnemyGetScore : IFScoreManager_NomalEnemyGetScore, IFScoreManager_BossEnemyGetScore { }


public interface IFScoreManager_Coin
{
    void NormalScore_CoinScore();
}

public interface IFScoreManager_CoinGetScore
{
    int NormalScore_CoinGetScore();
}

public interface IFScoreManager_Enchant
{
    void NormalScore_EnchantScore();
}

public interface IFScoreManager_EnchantGetScore
{
    int NormalScore_EnchantGetScore();
}

public interface IFScoreManager_Combo
{
    void NormalScore_ComboScore();
}

public interface IFScoreManager_ComboGetScore
{
    int NormalScore_ComboGetScore();
}

public interface IFScoreManager_Hp
{
    void BonusScore_HpScore();
    void BonusScore_HpValueSetting(int hp);
}

public interface IFScoreManager_HpGetScore
{
    int BonusScore_HpGetScore();
}

public interface IFScoreManager_Attract
{
    void BonusScore_AttractBonus();
}

public interface IFScoreManager_AttractGetScore
{
    int BonusScore_AttractGetBonus();
}

public interface IFScoreManager_AllAttract : IFScoreManager_Attract, IFScoreManager_Enchant { }

public interface IFScoreManager_AllAttractGetScore : IFScoreManager_AttractGetScore, IFScoreManager_EnchantGetScore { }

public interface IFScoreManager_Time
{
    void BonusScore_TimeScore();
    void BonusValue_Time(int time);
}

public interface IFScoreManager_TimeGetScore
{
    int BonusScore_GetTime();
}

public interface IFScoreManager_ValueGetHp
{
    int BonusValue_GetHp();
}
public interface IFSccoreManager_ValueGetAttract
{
    int BonusValue_GetAttract();
}
public interface IFScoreManager_ValueGetTime
{
    int BonusValue_TimeGetScore();
}

public interface IFScoreManager_AllGetScore :
    IFScoreManager_NomalEnemyGetScore,
    IFScoreManager_BossEnemyGetScore,
    IFScoreManager_EnchantGetScore,
    IFScoreManager_AttractGetScore,
    IFScoreManager_ComboGetScore,
    IFScoreManager_CoinGetScore,
    IFScoreManager_HpGetScore,
     IFScoreManager_TimeGetScore,
    IFScoreManager_ValueGetHp,
    IFSccoreManager_ValueGetAttract,
    IFScoreManager_ValueGetTime
{
    void ScoreReset();
}

public class ScoreManager : MonoBehaviour,
IFScoreManager_NomalEnemy, IFScoreManager_NomalEnemyGetScore,
IFScoreManager_BossEnemy, IFScoreManager_BossEnemyGetScore,
IFScoreManager_AllEnemy, IFScoreManager_AllEnemyGetScore,
IFScoreManager_Enchant, IFScoreManager_EnchantGetScore,
IFScoreManager_Attract, IFScoreManager_AttractGetScore,
IFScoreManager_AllAttract, IFScoreManager_AllAttractGetScore,
IFScoreManager_Combo, IFScoreManager_ComboGetScore,
IFScoreManager_Coin, IFScoreManager_CoinGetScore,
IFScoreManager_Hp, IFScoreManager_HpGetScore,
IFScoreManager_Time, IFScoreManager_TimeGetScore,
    IFScoreManager_AllGetScore
{
    #region variable 
    [System.Serializable]
    public struct ScoreStructure
    {
        public string name;
        public ScoreData scoreDatas;
    }
    public ScoreStructure[] _scoreStructures;



    public struct DefaultScoreStructure
    {
        public DefaultScoreStructure(int score)
        {
            DEFAULT_SCORE = score;
        }
        public readonly int DEFAULT_SCORE;
    }

    public DefaultScoreStructure[] defaultScoreStructures;

    private const int DEFAULT_NORMAL_ENEMY = 0;
    private const int DEFAULT_BOSS_ENEMY = 1;
    private const int DEFAULT_ENCHANT = 2;
    private const int DEFAULT_COIN = 3;
    private const int DEFAULT_HP_BONUS = 4;
    private const int DEFALUT_ATTRACT_BONUS = 5;
    private const int DEFAULT_TIME_BONUS = 6;
    private const int DEFALUT_COMBO_BONUS = 7;

    public ScoreNumber.Score ScorePoint;
    private List<ScoreNumber.Score> scorePoints = new List<ScoreNumber.Score>();
    private ScoreNumber.Score SumScorePoint;

    //private int _scoreSum = 0;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        //仮取得
        PlayerStats playerStats = GameObject.FindObjectOfType<PlayerStats>();
        playerStats.readOnlyPlayerHp.Subject.FirstSubscribe(hp => { BonusScore_HpValueSetting(hp); });
        playerStats.readOnlyPlayerHp.Subject.SecondOnwardsObservers(_ => { BonusScore_HpScore(); });

        ResultStage resultStage = GameObject.FindObjectOfType<ResultStage>();
        GameProgress gameProgress = GameObject.FindObjectOfType<GameProgress>();
        if (resultStage == null) return;
        gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if(progressType == GameProgressType.result)
                {
                    resultStage.ResultScreenScore(ScorePoint);
                    SumScorePoint = SumScorePoint + ScorePoint;
                    ScorePoint.Reset();
                }
            }
            );

    }

    private void Start()
    {
        ScorePoint.scoreTimeBonus = 4000;
        SumScorePoint = ScoreNumber.ScorePoint;
        if (ScorePoint.scoreHpBonus == 0)
        {
            //ScorePoint.scoreHpBonus = _ScoreHpBonus;
        }
        if (ScorePoint.scoreTimeBonus == 0)
        {
            ScorePoint.scoreTimeBonus = 4000;
        }
        ScoreNumber.ScorePoint = SumScorePoint;


        defaultScoreStructures = new DefaultScoreStructure[_scoreStructures.Length];
        for (int i = 0; i < _scoreStructures.Length; i++)
        {
            Action<int, int> action = (structerNumber, stateNumber) =>
             {
                 defaultScoreStructures[structerNumber] = new DefaultScoreStructure(_scoreStructures[stateNumber].scoreDatas.score);
             };

            for (int j = 0; j < Enum.GetNames(typeof(ScoreState)).Length; j++)
            {
                if ((int)_scoreStructures[i].scoreDatas.scoreState == j)
                {
                    action(j, i);
                }
            }
        }




    }
    /// <summary>
    /// 雑魚敵を倒すごとに呼ぶメソッド　加点する　デフォルトのスコア
    /// </summary>
    public void NormalScore_NormalEnemyScore()
    {
        ScorePoint.scoreNormalEnemy += _scoreStructures[DEFAULT_NORMAL_ENEMY].scoreDatas.score;
        ScorePoint.valueNormalEnemy++;
    }

    /// <summary>
    /// 雑魚的のスコアがデフォルトのスコアでないときに使用する
    /// </summary>
    /// <param name="score"></param>
    public void NormalScore_NormalEnemyScore(ScoreData score)
    {
        ScorePoint.scoreNormalEnemy += score.score;
    }

    /// <summary>
    /// 雑魚敵の合計スコアを取得する
    /// </summary>
    /// <returns></returns>
    public int NormalScore_NomalEnemyGetScore()
    {
        return ScorePoint.scoreNormalEnemy;
    }




    /// <summary>
    /// ボスを倒すごとに呼ぶメソッド　加点する  デフォルトスコア
    /// </summary>
    public void NormalScore_BossEnemyScore()
    {
        ScorePoint.scoreBossEnemy += _scoreStructures[DEFAULT_BOSS_ENEMY].scoreDatas.score;
    }

    /// <summary>
    /// ボスのスコアがデフォルトではないときに使用する
    /// </summary>
    /// <param name="scoreData"></param>
    public void NormalScore_BossEnemyScore(ScoreData scoreData)
    {
        ScorePoint.scoreBossEnemy += scoreData.score;
    }

    /// <summary>
    /// ボスの合計スコアを取得する
    /// </summary>
    /// <returns></returns>
    public int NormalScore_BossEnemyGetScore()
    {
        return ScorePoint.scoreBossEnemy;
    }


    /// <summary>
    /// コインを取るたびに呼ぶメソッド　加点する  デフォルトスコア
    /// </summary>
    public void NormalScore_CoinScore()
    {
        ScorePoint.scoreCoin += _scoreStructures[DEFAULT_COIN].scoreDatas.score;
    }

    /// <summary>
    /// コインのスコアがデフォルトではないとき
    /// </summary>
    /// <param name="scoreData"></param>
    public void NormalScore_CoinScore(ScoreData scoreData)
    {
        ScorePoint.scoreCoin += scoreData.score;
    }

    /// <summary>
    /// コインの合計スコアを取得する
    /// </summary>
    /// <returns></returns>
    public int NormalScore_CoinGetScore()
    {
        return ScorePoint.scoreCoin;
    }

    /// <summary>
    /// エンチャントする度に呼ぶメソッド　加点する
    /// </summary>
    public void NormalScore_EnchantScore()
    {
        ScorePoint.scoreEnchant += _scoreStructures[DEFAULT_ENCHANT].scoreDatas.score;
    }

    /// <summary>
    /// エンチャントのスコアがデフォルトではないとき
    /// </summary>
    /// <param name="scoreData"></param>
    public void NormalScore_EnchantScore(ScoreData scoreData)
    {
        ScorePoint.scoreEnchant += scoreData.score;
    }

    /// <summary>
    /// エンチャントの合計スコアを取得する
    /// </summary>
    /// <returns></returns>
    public int NormalScore_EnchantGetScore()
    {
        return ScorePoint.scoreEnchant;
    }

    /// <summary>
    /// コンボで倒すたびに呼ぶメソッド　加点する
    /// </summary>
    public void NormalScore_ComboScore()
    {
        ScorePoint.scoreComboBonus += _scoreStructures[DEFALUT_COMBO_BONUS].scoreDatas.score;
    }

    /// <summary>
    /// コンボスコアがデフォルトではないとき
    /// </summary>
    /// <param name="scoreData"></param>
    public void NormalScore_ComboScore(ScoreData scoreData)
    {
        ScorePoint.valueComboBonus++;
        ScorePoint.scoreComboBonus += scoreData.score;
    }

    public int NormalScore_ComboGetScore()
    {
        return ScorePoint.scoreComboBonus;
    }

    /// <summary>
    /// Hpが経るごとに呼ぶメソッド　減点する
    /// </summary>
    public void BonusScore_HpScore()
    {
        ScorePoint.scoreHpBonus -= _scoreStructures[DEFAULT_HP_BONUS].scoreDatas.score;
        if (ScorePoint.scoreHpBonus < 0)
        {
            ScorePoint.scoreHpBonus = 0;
        }
        ScorePoint.valueHpBonus++;
    }

    public void BonusScore_HpValueSetting(int hp)
    {
        ScorePoint.scoreHpBonus = hp * 200;
    }
    public int BonusScore_HpGetScore()
    {
        return ScorePoint.scoreHpBonus;
        //return _scoreHpBonus;
    }

    /// <summary>
    /// 吸い込むごとに呼ぶメソッド　加点する
    /// </summary>
    public void BonusScore_AttractBonus()
    {
        ScorePoint.scoreAttractBonus += _scoreStructures[DEFALUT_ATTRACT_BONUS].scoreDatas.score;
        ScorePoint.valueAttractBonus++;
    }

    public int BonusScore_AttractGetBonus()
    {
        return ScorePoint.scoreAttractBonus;
        //return _scoreAttractBonus;
    }

    /// <summary>
    /// 一分立つ毎に呼ぶメソッド　減点する
    /// </summary>
    public void BonusScore_TimeScore()
    {
        ScorePoint.scoreTimeBonus -= _scoreStructures[DEFAULT_TIME_BONUS].scoreDatas.score;
        if (ScorePoint.scoreTimeBonus < 0)
        {
            ScorePoint.scoreTimeBonus = 0;
        }
    }

    public void BonusValue_Time(int time)
    {
        ScorePoint.valueTimeBonus += time;
    }

    public int BonusScore_GetTime()
    {
        return ScorePoint.scoreTimeBonus;
        //return _scoreTimeBonus;
    }

    public int BonusValue_TimeGetScore()
    {
        return ScorePoint.valueTimeBonus;
        //return _valueTimeBonus;
    }


    public int BonusValue_GetHp()
    {
        return ScorePoint.valueHpBonus;
        //return _valueHpBonus;
    }
    public int BonusValue_GetAttract()
    {
        return ScorePoint.valueAttractBonus;
        //return _valueAttractBonus;
    }

    public void ScoreReset()
    {
        ScorePoint = new ScoreNumber.Score();
        ScoreNumber.ScorePoint = new ScoreNumber.Score();
        ScorePoint.scoreTimeBonus = 4000;
    }

    public void ScoreSave()
    {
        ScoreNumber.ScorePoint = SumScorePoint;
    }
    #endregion
}