// --------------------------------------------------------- 
// ScoreData.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public interface IScoreData
{
    ScoreState ScoreDataState { get; set; }
}
public enum ScoreState { normal, boss, enchant, coin, hpBonus, attractBonus, timeBonus, comboBonus };

[CreateAssetMenu(fileName = "ScoreData", menuName = "Scriptables/CreateScoreTable")]
public class ScoreData : ScriptableObject
{

    public ScoreState scoreState;

    public int score;
}
