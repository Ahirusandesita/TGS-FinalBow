// --------------------------------------------------------- 
// TestScore.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TestScore : MonoBehaviour
{
    #region variable 
    ScoreManager scoreManager;
 #endregion
 #region property
 #endregion
 #region method
 
 private void Awake()
 {

 }
 
 private void Start ()
 {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreManager>();
        scoreManager.NormalScore_NormalEnemyScore();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();
        scoreManager.BonusScore_AttractBonus();

        scoreManager.BonusScore_HpScore();
        scoreManager.BonusScore_TimeScore();
        scoreManager.BonusValue_Time(120);
 }

 private void Update ()
 {

 }
 #endregion
}