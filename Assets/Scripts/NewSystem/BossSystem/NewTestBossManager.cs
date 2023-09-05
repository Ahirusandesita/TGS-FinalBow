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

    private IBossMove LastBossMoveProcess => bossMoveBasesLoop[LastIndexProcess(bossMoveLoopIndex, bossMoveBasesLoop.Count)];
    private IBossAttack LastBossAttackProcess => bossAttackBasesLoop[LastIndexProcess(bossAttackLoopIndex, bossAttackBasesLoop.Count)];

    private int lastRandomMoveIndex = 0;
    private int lastRandomAttackIndex = 0;
    #endregion
    #region method

    private void Start()
    {
        bossMoves = this.GetComponents<NewTestBossMoveBase>();
        bossAttacks = this.GetComponents<NewTestBossAttackBase>();


        if (bossMoveBasesLoop.Count == 0)
        {
            if (bossMoves.Length == 0)
            {

            }
            else
            {
                MoveDelegate = () =>
                {
                    int randomIndex = Random.Range(0, bossMoves.Length - 1);
                    if (!bossMoves[lastRandomMoveIndex].IsMove)
                    {
                        bossMoves[Random.Range(0, bossMoves.Length - 1)].Move();
                        lastRandomMoveIndex = randomIndex;
                    }
                };
            }
        }
        else
        {
            MoveDelegate = () =>
            {
                if (!LastBossMoveProcess.IsMove)
                {
                    NowBossMoveProcess.Move();
                    IndexProcess(ref bossMoveLoopIndex, bossMoveBasesLoop.Count);
                }
            };
        }

        if (bossAttackBasesLoop.Count == 0)
        {
            if (bossAttacks.Length == 0)
            {

            }
            else
            {
                AttackDelegate = () =>
                {
                    int randomIndex = Random.Range(0, bossAttacks.Length - 1);
                    if (!bossAttacks[lastRandomAttackIndex].IsAttack)
                    {
                        bossAttacks[Random.Range(0, bossAttacks.Length - 1)].Attack();
                        lastRandomAttackIndex = randomIndex;
                    }
                };
            }
        }
        else
        {
            AttackDelegate = () =>
            {
                if (!LastBossAttackProcess.IsAttack)
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
        MoveDelegate();

        //攻撃開始したい時
        AttackDelegate();
    }


    private void IndexProcess(ref int index, int maxIndex) =>
        index = index + 1 == maxIndex ? index = 0 : index + 1;

    private int LastIndexProcess(int index, int maxIndex) =>
        index == -1 ? index = maxIndex - 1 : index -= 1;


    #endregion
}