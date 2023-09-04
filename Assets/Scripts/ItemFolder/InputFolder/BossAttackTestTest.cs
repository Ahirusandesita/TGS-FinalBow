// --------------------------------------------------------- 
// BossAttackTestTest.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BossAttackTestTest : EnemyAttack
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method

    public void SpawnAttack(PoolEnum.PoolObjectType spawnObjectType, Transform spawnPlace, int numberOfSpawn)
    {
        SpawnEAttackFanForm(spawnObjectType, spawnPlace, numberOfSpawn);
    }
    protected override void SpawnEAttackFanForm(PoolEnum.PoolObjectType spawnObjectType, Transform spawnPlace, int numberOfSpawn)
    {
        base.SpawnEAttackFanForm(spawnObjectType, spawnPlace, numberOfSpawn);
    }

    #endregion
}