// --------------------------------------------------------- 
// TestBossNormalAttack.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TestBossNormalAttack : NewTestBossAttackBase
{
    [SerializeField] float numberOfAttacks;
    BossAttackTestTest enemyAttack = default;

    const int NUMBER_OF_BULLETS = 3;

    float cacheTime = default;

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
        NormalAttack();
    }

    private void NormalAttack()
    {
        // ‚¤‚Â
        enemyAttack.SpawnAttack(PoolEnum.PoolObjectType.normalBullet, transform, NUMBER_OF_BULLETS);


    }
}