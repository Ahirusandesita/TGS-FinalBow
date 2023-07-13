// --------------------------------------------------------- 
// GroundEnemyAttack.cs 
// 
// CreateDay: 2023/07/13
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
public class GroundEnemyAttack : EnemyAttack
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method

    /// <summary>
    /// 地上雑魚
    /// </summary>
    /// <param name="spawnObjectType">呼び出すオブジェクトの種類</param>
    /// <param name="spawnPlace">スポーンさせるオブジェクトのTransform情報</param>
    public void Attack(PoolEnum.PoolObjectType spawnObjectType, Transform spawnPlace)
    {
        SpawnEAttackOne(spawnObjectType, spawnPlace);
    }
    #endregion
}