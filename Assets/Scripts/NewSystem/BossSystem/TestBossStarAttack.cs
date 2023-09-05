// --------------------------------------------------------- 
// TestBossStarAttack.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BossAttackTestTest))]
public class TestBossStarAttack : NewTestBossAttackBase 
{
    BossAttackTestTest enemyAttack = default;
    const int NUMBER_OF_BULLETS = 7;
    protected override void Start()
    {
        base.Start();
        enemyAttack = GetComponent<BossAttackTestTest>();
    }
    protected override void AttackAnimation()
    {
     
    }

    protected override void AttackProcess()
    {
        StarAttack();
    }
  
    
    private void StarAttack()
    {
        enemyAttack.SpawnAttack(PoolEnum.PoolObjectType.groundBullet, transform, NUMBER_OF_BULLETS);
    }
}