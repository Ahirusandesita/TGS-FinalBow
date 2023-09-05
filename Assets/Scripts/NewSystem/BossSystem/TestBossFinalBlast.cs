// --------------------------------------------------------- 
// TestBossFinalBlast.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
[RequireComponent((typeof(BossAttackTestTest)))]
public class TestBossFinalBlast : NewTestBossAttackBase
{
    #region variable 
    [SerializeField] float waitTime = 3f;

    BossAttackTestTest enemyAttack = default;
    WaitForSeconds wait = default;
    #endregion
    #region property
    #endregion
    #region method

    protected override void Start()
    {
        base.Start();
        enemyAttack = GetComponent<BossAttackTestTest>();
        wait = new WaitForSeconds(waitTime);
    }
    protected override void AttackAnimation()
    {

    }

    protected override void AttackProcess()
    {
        FinalBlast();
    }

    private void FinalBlast()
    {

        // ‚¤‚Â
    }

    IEnumerable WaitShot()
    {

        yield return wait;
        FinalBlast();
    }

    #endregion

}