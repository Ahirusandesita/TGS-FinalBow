// --------------------------------------------------------- 
// EnemyScriptTriggerDriver.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class EnemyScriptTriggerDriver : MonoBehaviour
{
    #region variable 
    public EnemyStats enemyStats;
 #endregion
 #region property
 #endregion
 #region method
 
 private void Awake()
 {

 }
 
 private void Start ()
 {

 }

 private void Update ()
 {
        if (Input.GetKeyDown(KeyCode.A))
        {
            enemyStats.TakeKnockBack();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            enemyStats.TakeThunder();
        }
 }
 #endregion
}