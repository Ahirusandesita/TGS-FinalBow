// --------------------------------------------------------- 
// Score.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    #region variable 
    private IFScoreManager_AllGetScore _scoerManager;
    
    private enum ScoreType { nomal, hp, attract, time ,all,hpValue,attractValue,timeValue};

    private enum DisplayType { nomal,bonus,value};

    private NumberObject _numberObject;
    private int nomalScore = 0;
    List<GameObject> _nomalScoreObjects = new List<GameObject>();
    List<GameObject> _bonusScoreObjects = new List<GameObject>();

    //�X�R�A���o���|�W�V����
    private Vector3 _scorePosition = new Vector3(1.6f, 140.6f, 34f);
    private Vector3 _scorePositionStart;
    //�{�[�i�X���o���|�W�V����
    private Vector3[] _bonusPositions = { new Vector3(1.6f, 91.7f, 34f), new Vector3(20.7f, 66f, 34f), new Vector3(27.3f, 43.3f, 34f) };
    private Vector3 _bonusPosition = new Vector3(51.7f, 160.4f, 34f);
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
        StartCoroutine(A());

        _scorePositionStart = _scorePosition;
        _bonusPositionStarts = _bonusPositions;

        _bonusPositionStart = _bonusPosition;

    }

    private void ScoreDisplay(ScoreType scoreType)
    {
        switch (scoreType)
        {
            //�m�[�}���X�R�A
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
                //Hp�X�R�A���o��
                ScoreDigits(_scoerManager.BonusScore_HpGetScore(),DisplayType.bonus);
                nomalScore += _scoerManager.BonusScore_HpGetScore();
                //Hp�X�R�A�𑫂����{�[�i�X�X�R�A
                ScoreDigits(nomalScore, DisplayType.nomal);
                break;

            case ScoreType.attract:
                ScoreDigits(_scoerManager.BonusScore_AttractGetBonus(), DisplayType.bonus);
                nomalScore += _scoerManager.BonusScore_AttractGetBonus();
                ScoreDigits(nomalScore, DisplayType.nomal);
                break;

            case ScoreType.time:
                ScoreDigits(_scoerManager.BonusScore_TimeGetScore(), DisplayType.bonus);
                nomalScore += _scoerManager.BonusScore_TimeGetScore();
                ScoreDigits(nomalScore, DisplayType.nomal);
                break;

            case ScoreType.all:
                
                break;

            case ScoreType.hpValue:
                ScoreDigits(_scoerManager.BonusValue_GetHp(),DisplayType.value);
                break;
            case ScoreType.attractValue:
                ScoreDigits(_scoerManager.BonusValue_GetAttract(), DisplayType.value);
                break;
        }
    }
    IEnumerator A()
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
        ScoreDisplay(ScoreType.all);
        yield return new WaitForSeconds(1f);
        ScoreDigits(0, DisplayType.bonus);
    }

    private void ScoreDigits(int score,DisplayType displayType)
    {
        //�m�[�}���X�R�A���X�V���邽�߂Ƀ��Z�b�g����
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
        //�|�W�V�������Z�b�g
        _scorePosition = _scorePositionStart;
        _bonusPosition = _bonusPositionStart;
        _bonusPositions = _bonusPositionStarts;

        //�X�R�A�̌������v�Z����
        int numberDigits = score;
        int countDigits = 1;
        while (numberDigits > 0)
        {
            numberDigits /= 10;
            countDigits *= 10;
        }
        countDigits /= 10;


        //�����ɂȂ�܂Ń��[�v
        while (countDigits > 0)
        {
            //�X�R�A�̌���������炷
            int digitsScore = score;
            int digits = 1;
            while (digitsScore >= 10)
            {
                digitsScore = digitsScore / 10;
                digits = digits * 10;
            }


            if (displayType == DisplayType.nomal)
            {
                GameObject nomalScore = Instantiate(_numberObject.numberObject[digitsScore].numberObject, _scorePosition, Quaternion.Euler(0f, 180f, 0f));
                nomalScore.transform.localScale *= 70f;
                _nomalScoreObjects.Add(nomalScore);
                _scorePosition.x += _scorePlusXPosition;
            }
            else if(displayType == DisplayType.bonus)
            {
                GameObject bonusScore = Instantiate(_numberObject.numberObject[digitsScore].numberObject, _bonusPosition, Quaternion.Euler(0f, 180f, 0f));

                bonusScore.transform.localScale *= 70f;
                _bonusScoreObjects.Add(bonusScore);
                //_bonusPositions[_bonusScoreIndex].x += _scorePlusXPosition;
                _bonusPosition.x += _scorePlusXPosition;
            }
            else
            {
                GameObject bonusScore = Instantiate(_numberObject.numberObject[digitsScore].numberObject, _bonusPositions[_bonusScoreIndex], Quaternion.Euler(0f, 180f, 0f));
                bonusScore.transform.localScale *= 70f;
                _bonusPositions[_bonusScoreIndex].x += _scorePlusXPosition;
            }

            digitsScore = digitsScore * digits;
            score -= digitsScore;
            countDigits = countDigits /= 10;
        }
        if (displayType == DisplayType.value)
        {
            _bonusScoreIndex++;
        }
    }

    #endregion
}