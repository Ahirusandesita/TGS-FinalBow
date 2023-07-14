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
    /// 地上雑魚：投擲攻撃
    /// </summary>
    /// <param name="spawnObjectType">呼び出すオブジェクトの種類</param>
    /// <param name="spawnPlace">スポーンさせるオブジェクトのTransform情報</param>
    public void ThrowingAttack(Transform spawnPlace)
    {
        SpawnEAttackOne(PoolEnum.PoolObjectType.groundBullet, spawnPlace);
    }
    #endregion
}