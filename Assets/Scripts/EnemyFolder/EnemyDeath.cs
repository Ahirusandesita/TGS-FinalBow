// --------------------------------------------------------- 
// EnemyDeth.cs 
// 
// CreateDay: 2023/06/22
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class EnemyDeath : MonoBehaviour
{
    #region variable 
    Animator _animator = default;
    IFScoreManager_NomalEnemy _scoreManager;
    public GameObject deathEffectTest;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
        _scoreManager = GameObject.FindWithTag("ScoreController").GetComponent<ScoreManager>();
    }

    public void Death()
    {
        //_animator.SetTrigger("Death");
        DeathEnd();
    }

    private void DeathEnd()
    {
        Instantiate(deathEffectTest, this.transform.position, Quaternion.identity);
        _scoreManager.NomalScore_NomalEnemyScore();
    }


    #endregion
}