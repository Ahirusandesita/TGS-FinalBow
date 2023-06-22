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
    private Animator _animator = default;
    private ObjectPoolSystem _objectPoolSystem;
    private IFScoreManager_NomalEnemy _scoreManager;
    private Transform _myTransform = default;

    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _myTransform = this.transform;
        _animator = this.GetComponent<Animator>();
        _scoreManager = GameObject.FindWithTag("ScoreController").GetComponent<ScoreManager>();
        _objectPoolSystem = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
    }

    public void Death()
    {
        //_animator.SetTrigger("Death");
        DeathEnd();
    }

    private void DeathEnd()
    {
        _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.enemyDeath, _myTransform.position, Quaternion.identity);
        _scoreManager.NomalScore_NomalEnemyScore();
    }


    #endregion
}