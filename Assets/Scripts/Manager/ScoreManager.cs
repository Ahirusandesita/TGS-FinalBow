// --------------------------------------------------------- 
// ScoreManager.cs 
// 
// CreateDay: 2023/06/18
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;

public interface IFScoreManager_NomalEnemy
{
    void NomalScore_NomalEnemyScore();
}
public interface IFScoreManager_NomalEnemyGetScore
{
    int NomalScore_NomalEnemyGetScore();
}

public interface IFScoreManager_BossEnemy
{
    void NomalScore_BossEnemyScore();
}
public interface IFScoreManager_BossEnemyGetScore
{
    int NomalScore_BossEnemyGetScore();
}


public interface IFScoreManager_AllEnemy : IFScoreManager_NomalEnemy, IFScoreManager_BossEnemy { }

interface IFScoreManager_AllEnemyGetScore : IFScoreManager_NomalEnemyGetScore, IFScoreManager_BossEnemyGetScore { }


public interface IFScoreManager_Coin
{
    void NomalScore_CoinScore();
}

public interface IFScoreManager_CoinGetScore
{
    int NomalScore_CoinGetScore();
}

public interface IFScoreManager_Enchant
{
    void NomalScore_EnchantScore();
}

public interface IFScoreManager_EnchantGetScore
{
    int NomalScore_EnchantGetScore();
}

public interface IFScoreManager_Combo
{
    void NomalScore_ComboScore();
}

public interface IFScoreManager_ComboGetScore
{
    int NomalScore_ComboGetScore();
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
    public int _ScoreEnemy_NomalEnemy = 100;
    public int _ScoreEnemy_BossEnemy = 1000;
    public int _ScoreEnchant = 100;
    public int _ScoreCoin = 100;
    public int _ScoreHpBonus = 200;
    public int _ScoreAttractBonus = 100;
    public int _ScoreTimeBonus = 1000;
    public int _ScoreComboBonus = 100;

    private int _scoreNomalEnemy = 0;

    private int _scoreBossEnemy = 0;

    private int _scoreEnchant = 0;

    private int _scoreCoin = 0;

    private int _scoreComboBonus = 0;



    //仮
    private int _scoreHpBonus = 0;
    private int _valueHpBonus = 0;

    private int _scoreAttractBonus = 0;
    private int _valueAttractBonus = 0;

    private int _scoreTimeBonus = 0;
    private int _valueTimeBonus = 0;

    public struct Score
    {
        public int scoreNormalEnemy;
        public int scoreBossEnemy;
        public int scoreEnchant;
        public int scoreCoin;
        public int scoreComboBonus;

        public int scoreHpBonus;
        public int valueHpBonus;

        public int scoreAttractBonus;
        public int valueAttractBonus;

        public int scoreTimeBonus;
        public int valueTimeBonus;
    }

    public Score ScorePoint;

    IGameManagerScore _gameManager;

    //private int _scoreSum = 0;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        //_scoreHpBonus = プレイヤーのHpの数 * _ScoreHpBonus;
        _scoreTimeBonus = 4000;
        _gameManager = GameObject.FindGameObjectWithTag(InhallLibTags.GameController).GetComponent<GameManager>();

        ScoreManager scoreAll = _gameManager.ScoreManager;
       

        if (scoreAll != null)
        {

            ScorePoint = scoreAll.ScorePoint;

            if(ScorePoint.scoreHpBonus == 0)
            {
                ScorePoint.scoreHpBonus = _ScoreHpBonus;
            }
            if(ScorePoint.scoreTimeBonus == 0)
            {
                ScorePoint.scoreTimeBonus = 4000;
            }


            _scoreNomalEnemy = scoreAll.NomalScore_NomalEnemyGetScore();
            _scoreBossEnemy = scoreAll.NomalScore_BossEnemyGetScore();
            _scoreEnchant = scoreAll.NomalScore_EnchantGetScore();
            _scoreCoin = scoreAll.NomalScore_CoinGetScore();
            _scoreHpBonus = scoreAll.BonusScore_HpGetScore();
            _scoreAttractBonus = scoreAll.BonusScore_AttractGetBonus();
            _scoreTimeBonus = scoreAll.BonusScore_GetTime();
            _scoreComboBonus = scoreAll.NomalScore_ComboGetScore();

            _valueAttractBonus = scoreAll.BonusValue_GetAttract();
            _valueHpBonus = scoreAll.BonusValue_GetHp();
            _valueTimeBonus = scoreAll.BonusValue_TimeGetScore();

            if (_scoreHpBonus == 0)
            {
                //_scoreHpBonus = プレイヤーのHpの数 * _ScoreHpBonus;
                _scoreHpBonus = _ScoreHpBonus;
            }
            if (_scoreTimeBonus == 0)
            {
                _scoreTimeBonus = 4000;
            }
        }
        _gameManager.ScoreManager = this;
    }

    /// <summary>
    /// 雑魚敵を倒すごとに呼ぶメソッド　加点する
    /// </summary>
    public void NomalScore_NomalEnemyScore()
    {
        _scoreNomalEnemy += _ScoreEnemy_NomalEnemy;

        ScorePoint.scoreNormalEnemy += _ScoreEnemy_NomalEnemy;
    }

    public int NomalScore_NomalEnemyGetScore()
    {
        return ScorePoint.scoreNormalEnemy;
    }




    /// <summary>
    /// ボスを倒すごとに呼ぶメソッド　加点する
    /// </summary>
    public void NomalScore_BossEnemyScore()
    {
        _scoreBossEnemy += _ScoreEnemy_BossEnemy;

        ScorePoint.scoreBossEnemy += _ScoreEnemy_BossEnemy;
    }

    public int NomalScore_BossEnemyGetScore()
    {
        return ScorePoint.scoreBossEnemy;
    }

    /// <summary>
    /// コインを取るたびに呼ぶメソッド　加点する
    /// </summary>
    public void NomalScore_CoinScore()
    {
        _scoreCoin += _ScoreCoin;

        ScorePoint.scoreCoin += _ScoreCoin;
    }

    public int NomalScore_CoinGetScore()
    {
        return ScorePoint.scoreCoin;
    }

    /// <summary>
    /// エンチャントする度に呼ぶメソッド　加点する
    /// </summary>
    public void NomalScore_EnchantScore()
    {
        _scoreEnchant += _ScoreEnchant;

        ScorePoint.scoreEnchant += _ScoreEnchant;
    }

    public int NomalScore_EnchantGetScore()
    {
        return ScorePoint.scoreEnchant;
    }

    /// <summary>
    /// コンボで倒すたびに呼ぶメソッド　加点する
    /// </summary>
    public void NomalScore_ComboScore()
    {
        _scoreComboBonus += _ScoreComboBonus;

        ScorePoint.scoreComboBonus += _ScoreComboBonus;
    }

    public int NomalScore_ComboGetScore()
    {
        return ScorePoint.scoreComboBonus;
    }

    /// <summary>
    /// Hpが経るごとに呼ぶメソッド　減点する
    /// </summary>
    public void BonusScore_HpScore()
    {

        _scoreHpBonus -= _ScoreHpBonus;
        ScorePoint.scoreHpBonus -= _ScoreHpBonus;
        if (ScorePoint.scoreHpBonus < 0)
        {
            ScorePoint.scoreHpBonus = 0;
        }

        _valueHpBonus++;
        ScorePoint.valueHpBonus++;
    }
    public void BonusScore_HpValueSetting(int hp)
    {
        _scoreHpBonus = hp * 200;
        ScorePoint.scoreHpBonus = hp * 200;
    }
    public int BonusScore_HpGetScore()
    {
        return ScorePoint.scoreHpBonus;
    }

    /// <summary>
    /// 吸い込むごとに呼ぶメソッド　加点する
    /// </summary>
    public void BonusScore_AttractBonus()
    {
        _scoreAttractBonus += _ScoreAttractBonus;
        _valueAttractBonus++;

        ScorePoint.scoreAttractBonus += _ScoreAttractBonus;
        ScorePoint.valueAttractBonus++;
    }

    public int BonusScore_AttractGetBonus()
    {
        return ScorePoint.scoreAttractBonus;
    }

    /// <summary>
    /// 一分立つ毎に呼ぶメソッド　減点する
    /// </summary>
    public void BonusScore_TimeScore()
    {
        _scoreTimeBonus -= _ScoreTimeBonus;
        ScorePoint.scoreTimeBonus -= _ScoreTimeBonus;
        if (ScorePoint.scoreTimeBonus < 0)
        {
            ScorePoint.scoreTimeBonus = 0;
        }
    }

    public void BonusValue_Time(int time)
    {
        _valueTimeBonus += time;
        ScorePoint.valueTimeBonus += time;
    }

    public int BonusScore_GetTime()
    {
        return ScorePoint.scoreTimeBonus;
    }

    public int BonusValue_TimeGetScore()
    {
        return ScorePoint.valueTimeBonus;
    }


    public int BonusValue_GetHp()
    {
        return ScorePoint.valueHpBonus;
    }
    public int BonusValue_GetAttract()
    {
        return ScorePoint.valueAttractBonus;
    }

    public void ScoreReset()
    {
        _scoreNomalEnemy = 0;

        _scoreBossEnemy = 0;

        _scoreEnchant = 0;

        _scoreCoin = 0;

        _scoreComboBonus = 0;
        _scoreHpBonus = _ScoreHpBonus;
        _valueHpBonus = 0;

        _scoreAttractBonus = 0;
        _valueAttractBonus = 0;

        _scoreTimeBonus = 4000;
        _valueTimeBonus = 0;

        ScorePoint = new Score();
        ScorePoint.scoreTimeBonus = 4000;

    }

    public void ScoreSave()
    {
        _gameManager.ScoreManager = this;
    }
    #endregion
}