// --------------------------------------------------------- 
// TestBossManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestBossManager : MonoBehaviour
{
    #region variable 
    private IBossAction[] bossActions;

    private IBossAction nowBossAction;

    [SerializeField]
    private List<TestBossActionBase> bossActionTypesLoop = new List<TestBossActionBase>();

    private int bossActionLoopIndex = 0;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        bossActions = this.GetComponents<TestBossActionBase>();
    }

    private void Update()
    {


        if (bossActionTypesLoop.Count != 0)
            for (int i = 0; i < bossActions.Length; i++)
            {
                if (bossActions[i] == (IBossAction)bossActionTypesLoop[bossActionLoopIndex]) nowBossAction = bossActions[i];
            }

        else
            nowBossAction = bossActions[Random.Range(0, bossActions.Length - 1)];


        if (!nowBossAction.IsMove)
        {
            nowBossAction.Move();
        }

        if (!nowBossAction.IsAttack)
        {
            nowBossAction.Attack();
        }

    }


    #endregion
}