// --------------------------------------------------------- 
// EnemyDeathEffect.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class EnemyDeathEffect : MonoBehaviour
{
    #region variable 
    private ObjectPoolSystem _objectPoolSystem = default;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _objectPoolSystem = GameObject.FindWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
    }
    private void OnParticleSystemStopped()
    {
        _objectPoolSystem.ReturnObject(EffectPoolEnum.EffectPoolState.enemyDeath,this.gameObject);
    }
    #endregion
}