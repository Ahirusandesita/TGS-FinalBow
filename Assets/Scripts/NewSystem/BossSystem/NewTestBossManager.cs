// --------------------------------------------------------- 
// NewTestBossManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewTestBossManager : MonoBehaviour
{
    #region variable 
    private delegate void ProcessDelegate();
    private ProcessDelegate MoveDelegate;
    private ProcessDelegate AttackDelegate;

    private IBossMove[] bossMoves;
    private IBossAttack[] bossAttacks;

    [SerializeField]
    private List<NewTestBossMoveBase> bossMoveBasesLoop = new List<NewTestBossMoveBase>();
    private int bossMoveLoopIndex = 0;

    [SerializeField]
    private List<NewTestBossAttackBase> bossAttackBasesLoop = new List<NewTestBossAttackBase>();
    private int bossAttackLoopIndex = 0;

    #endregion
    #region property
    private IBossMove NowBossMoveProcess => bossMoveBasesLoop[bossMoveLoopIndex];
    private IBossAttack NowBossAttackProcess => bossAttackBasesLoop[bossAttackLoopIndex];
    #endregion
    #region method

    private void Start()
    {
        bossMoves = this.GetComponents<NewTestBossMoveBase>();
        bossAttacks = this.GetComponents<NewTestBossAttackBase>();


        if (bossMoveBasesLoop.Count == 0)
        {
            if(bossMoves.Length == 0)
            {

            }
            else
            {
                MoveDelegate = () =>
                {
                    if (bossMoves[Random.Range(0, bossMoves.Length - 1)].IsMove) bossMoves[Random.Range(0, bossMoves.Length - 1)].Move();
                };
            }
        }
        else
        {
            MoveDelegate = () =>
            {
                if (!NowBossMoveProcess.IsMove)
                {
                    NowBossMoveProcess.Move();
                    IndexProcess(ref bossMoveLoopIndex, bossMoveBasesLoop.Count);
                }
            };
        }

        if (bossAttackBasesLoop.Count == 0)
        {
            if(bossAttacks.Length == 0)
            {

            }
            else
            {
                AttackDelegate = () =>
                {
                    if (bossAttacks[Random.Range(0, bossAttacks.Length - 1)].IsAttack) bossAttacks[Random.Range(0, bossAttacks.Length - 1)].Attack();
                };
            }
        }
        else
        {
            AttackDelegate = () =>
            {
                if (!NowBossAttackProcess.IsAttack)
                {
                    NowBossAttackProcess.Attack();
                    IndexProcess(ref bossAttackLoopIndex, bossAttackBasesLoop.Count);
                }
            };
        }

    }

    private void Update()
    {
        //移動開始したい時　
        //MoveDelegate();

        //攻撃開始したい時
        //AttackDelegate();
    }


    private void IndexProcess(ref int index, int maxIndex)
    {
        index++;
        if (index == maxIndex) index = 0;
    }
    #endregion
}