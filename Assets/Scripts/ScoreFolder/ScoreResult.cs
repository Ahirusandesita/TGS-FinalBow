// --------------------------------------------------------- 
// Score.cs 
// 
// CreateDay: 2023/06/19
// Creator  : NomuraYuhei
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreResult : MonoBehaviour
{
    #region variable 
    private IFScoreManager_AllGetScore _scoerManager;
    
    private enum ScoreType { nomal, hp, attract, time ,all,hpValue,attractValue,timeValue};

    private enum DisplayType { nomal,bonus,value,valueHp,valueAttract,valueTime};

    private NumberObject _numberObject;
    private int nomalScore = 0;
    List<GameObject> _nomalScoreObjects = new List<GameObject>();
    List<GameObject> _bonusScoreObjects = new List<GameObject>();

    //スコアを出すポジション
    private Vector3 _scorePosition = new Vector3(121f, 140.6f, 44f);
    private Vector3 _scorePositionStart;
    //ボーナスを出すポジション
    private Vector3[] _bonusPositions = { new Vector3(114f, 91.7f, 44f), new Vector3(114f, 66f, 44f), new Vector3(114f, 43.3f, 44f) };
    private Vector3 _bonusPosition = new Vector3(125f, 160.4f, 44f);
    private Vector3[] _bonusPositionStarts;
    private Vector3 _bonusPositionStart;
    private int _bonusScoreIndex = 0;
    private float _scorePlusXPosition = 20f;
    private float _scorePlusXPosition_bonus = 10f;

    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        _scoerManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>().ScoreManager;
        _numberObject = transform.parent.GetComponent<ScoreUIManager>()._NumberObjectData;
        StartCoroutine(ScoreTimer());

        _scorePositionStart = _scorePosition;
        _bonusPositionStarts = _bonusPositions;

        _bonusPositionStart = _bonusPosition;

    }

    private void ScoreDisplay(ScoreType scoreType)
    {
        switch (scoreType)
        {
            //ノーマルスコア
            case ScoreType.nomal:
                nomalScore +=
                    _scoerManager.NomalScore_NomalEnemyGetScore() +
                    _scoerManager.NomalScore_BossEnemyGetScore() +
                    _scoerManager.NomalScore_EnchantGetScore() +
                    _scoerManager.NomalScore_CoinGetScore() +
                    _scoerManager.NomalScore_ComboGetScore();

                ScoreDigits(nomalScore,DisplayType.nomal);
                break;

            case ScoreType.hp:
                //Hpスコアを出す
                ScoreDigits(_scoerManager.BonusScore_HpGetScore(),DisplayType.bonus);
                nomalScore += _scoerManager.BonusScore_HpGetScore();
                //Hpスコアを足したボーナススコア
                ScoreDigits(nomalScore, DisplayType.nomal);
                break;

            case ScoreType.attract:
                ScoreDigits(_scoerManager.BonusScore_AttractGetBonus(), DisplayType.bonus);
                nomalScore += _scoerManager.BonusScore_AttractGetBonus();
                ScoreDigits(nomalScore, DisplayType.nomal);
                break;

            case ScoreType.time:
                ScoreDigits(_scoerManager.BonusScore_GetTime(), DisplayType.bonus);
                nomalScore += _scoerManager.BonusScore_GetTime();
                ScoreDigits(nomalScore, DisplayType.nomal);
                break;

            case ScoreType.all:
                
                break;

            case ScoreType.hpValue:
                ScoreDigits(_scoerManager.BonusValue_GetHp(),DisplayType.valueHp);
                break;
            case ScoreType.attractValue:
                ScoreDigits(_scoerManager.BonusValue_GetAttract(), DisplayType.valueAttract);
                break;
            case ScoreType.timeValue:
                ScoreDigits(_scoerManager.BonusValue_TimeGetScore(),DisplayType.valueTime);
                break;
        }
    }
    private IEnumerator ScoreTimer()
    {
        yield return new WaitForSeconds(2f);
        ScoreDisplay(ScoreType.nomal);
        yield return new WaitForSeconds(1f);
        ScoreDisplay(ScoreType.hp);
        ScoreDisplay(ScoreType.hpValue);
        ScoreDisplay(ScoreType.all);
        yield return new WaitForSeconds(1f);
        ScoreDisplay(ScoreType.attract);
        ScoreDisplay(ScoreType.attractValue);
        ScoreDisplay(ScoreType.all);
        yield return new WaitForSeconds(1f);
        ScoreDisplay(ScoreType.time);
        ScoreDisplay(ScoreType.timeValue);
        ScoreDisplay(ScoreType.all);
        yield return new WaitForSeconds(1f);
        ScoreDigits(-1, DisplayType.bonus);
    }

    private void ScoreDigits(int score,DisplayType displayType)
    {
        bool isValueTimeMinutesZero = false;
        if(displayType == DisplayType.valueTime)
        {
            isValueTimeMinutesZero = IsTimeValueMinutesZero(score);
            score = ChengeTimer(score);
        }

        //ノーマルスコアを更新するためにリセットする
        if (_nomalScoreObjects.Count != 0 && displayType == DisplayType.nomal)
        {
            for(int i = 0; i < _nomalScoreObjects.Count; i++)
            {
                Destroy(_nomalScoreObjects[i]);
            }
            _nomalScoreObjects.Clear();
        }
        if (_bonusScoreObjects.Count != 0 && displayType == DisplayType.bonus)
        {
            for(int i = 0; i < _bonusScoreObjects.Count; i++)
            {
                Destroy(_bonusScoreObjects[i]);
            }
            _bonusScoreObjects.Clear();
        }

        //マイナス1なら表示を終了
        if(score == -1)
        {
            return;
        }

        //ポジションリセット
        _scorePosition = _scorePositionStart;
        _bonusPosition = _bonusPositionStart;
        _bonusPositions = _bonusPositionStarts;

        //スコアの桁数を計算する
        int numberDigits = score;
        int countDigits = 1;

        while (numberDigits > 0)
        {
            numberDigits /= 10;
            countDigits++;
        }
        countDigits--;
        if(countDigits == 0)
        {
            countDigits++;
        }

        int digit = 1;

        List<int> digitsList = new List<int>();
        while (countDigits > 0)
        {
            //スコアの桁数を一つ減らす
            int digitsScore = score;
            for (int i = 0; i < digit; i++)
            {
                digitsScore = digitsScore / 10;
            }
            for (int i = 0; i < digit; i++)
            {
                digitsScore = digitsScore * 10;
            }
            int displayNumber = score - digitsScore;
            Debug.Log(displayNumber);
            digitsList.Add(displayNumber);

            score = score / 10;
            countDigits --;
        }


        for (int i = 0; i < digitsList.Count; i++)
        {
            if(isValueTimeMinutesZero && i == digitsList.Count - 1)
            {
                return;
            }

            if (displayType == DisplayType.valueTime && i == 2)
            {
                _bonusPositions[_bonusScoreIndex].x += 5;
                GameObject bonusScore = Instantiate(_numberObject.numberObject[12].numberObject, _bonusPositions[_bonusScoreIndex], Quaternion.Euler(0f, 180f, 0f));
                bonusScore.transform.localScale *= 70f;
                _bonusPositions[_bonusScoreIndex].x -= _scorePlusXPosition;
            }


            if (i == 0)//&& value
            {
                if (displayType == DisplayType.valueHp)
                {
                    GameObject bonusScore = Instantiate(_numberObject.numberObject[10].numberObject, _bonusPositions[_bonusScoreIndex], Quaternion.Euler(0f, 180f, 0f));
                    bonusScore.transform.localScale *= 70f;
                    _bonusPositions[_bonusScoreIndex].x -= _scorePlusXPosition;
                }
                if (displayType == DisplayType.valueAttract)
                {
                    GameObject bonusScore = Instantiate(_numberObject.numberObject[11].numberObject, _bonusPositions[_bonusScoreIndex], Quaternion.Euler(0f, 180f, 0f));
                    bonusScore.transform.localScale *= 70f;
                    _bonusPositions[_bonusScoreIndex].x -= _scorePlusXPosition;
                }
                //秒　個　などの感じを出す場合はこれを使用する
                // _bonusPositions[_bonusScoreIndex].x -=_scorePlusXPosition;
            }

            if (displayType == DisplayType.nomal)
            {
                GameObject nomalScore = Instantiate(_numberObject.numberObject[digitsList[i]].numberObject, _scorePosition, Quaternion.Euler(0f, 180f, 0f));
                nomalScore.transform.localScale *= 70f;
                _nomalScoreObjects.Add(nomalScore);
                _scorePosition.x -= _scorePlusXPosition;
            }
            else if (displayType == DisplayType.bonus)
            {
                GameObject bonusScore = Instantiate(_numberObject.numberObject[digitsList[i]].numberObject, _bonusPosition, Quaternion.Euler(0f, 180f, 0f));

                bonusScore.transform.localScale *= 35f;
                _bonusScoreObjects.Add(bonusScore);
                //_bonusPositions[_bonusScoreIndex].x += _scorePlusXPosition;
                _bonusPosition.x -= _scorePlusXPosition_bonus;
            }
            else
            {
                GameObject bonusScore = Instantiate(_numberObject.numberObject[digitsList[i]].numberObject, _bonusPositions[_bonusScoreIndex], Quaternion.Euler(0f, 180f, 0f));
                bonusScore.transform.localScale *= 70f;
                _bonusPositions[_bonusScoreIndex].x -= _scorePlusXPosition;
            }
            
        }


        //桁数になるまでループ
        //while (countDigits > 0)
        //{
        //    //スコアの桁数を一つ減らす
        //    int digitsScore = score;
        //    int digits = 1;
        //    while (digitsScore >= 10)
        //    {
        //        digitsScore = digitsScore / 10;
        //        digits = digits * 10;
        //    }


        //    if (displayType == DisplayType.nomal)
        //    {
        //        GameObject nomalScore = Instantiate(_numberObject.numberObject[digitsScore].numberObject, _scorePosition, Quaternion.Euler(0f, 180f, 0f));
        //        nomalScore.transform.localScale *= 70f;
        //        _nomalScoreObjects.Add(nomalScore);
        //        _scorePosition.x += _scorePlusXPosition;
        //    }
        //    else if (displayType == DisplayType.bonus)
        //    {
        //        GameObject bonusScore = Instantiate(_numberObject.numberObject[digitsScore].numberObject, _bonusPosition, Quaternion.Euler(0f, 180f, 0f));

        //        bonusScore.transform.localScale *= 70f;
        //        _bonusScoreObjects.Add(bonusScore);
        //        //_bonusPositions[_bonusScoreIndex].x += _scorePlusXPosition;
        //        _bonusPosition.x += _scorePlusXPosition;
        //    }
        //    else
        //    {
        //        GameObject bonusScore = Instantiate(_numberObject.numberObject[digitsScore].numberObject, _bonusPositions[_bonusScoreIndex], Quaternion.Euler(0f, 180f, 0f));
        //        bonusScore.transform.localScale *= 70f;
        //        _bonusPositions[_bonusScoreIndex].x += _scorePlusXPosition;
        //    }

        //    digitsScore = digitsScore * digits;
        //    score -= digitsScore;
        //    countDigits = countDigits /= 10;
        //}
        if (displayType == DisplayType.value || displayType == DisplayType.valueHp || displayType == DisplayType.valueAttract || displayType == DisplayType.valueTime)
        {
            _bonusScoreIndex++;
        }
    }
    private int ChengeTimer(int score)
    {
            int timeMinutes = score / 60;
            if (timeMinutes == 0)
            {
                timeMinutes = 100;
            }
            int timeSecond = score % 60;
            timeMinutes = timeMinutes * 100;
            score = timeMinutes + timeSecond;
        return score;
    }
    private bool IsTimeValueMinutesZero(int score)
    {
        if(score /60 == 0)
        {
            return true;
        }
        return false;
    }
    #endregion
}