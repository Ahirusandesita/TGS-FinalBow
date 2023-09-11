// --------------------------------------------------------- 
// GroundEnemyStats.cs 
// 
// CreateDay: 2023/07/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 


using UnityEngine;

public class GroundEnemyStats : CommonEnemyStats
{
    public override void Death()
    {
        _drop.DropStart(_dropData, this.transform.position);

        base.Death();
    }

    protected override void OnDeathReactions(Transform arrowTransform, Vector3 arrowVector)
    {

    }
}