// --------------------------------------------------------- 
// GroundEnemyStats.cs 
// 
// CreateDay: 2023/07/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 


using UnityEngine;

public class GroundEnemyStats : CommonEnemyStats
{
    protected override void Start()
    {
        base.Start();

        _reaction.SubscribeReactionFinish(Death);
    }
    public override void Death()
    {
        _drop.DropStart(_dropData, this.transform.position);

        base.Death();
    }

    protected override void OnDeathReactions(Transform arrowTransform, Vector3 arrowVector)
    {
        _reaction.ReactionSetting(_takeEnchantment);

        _reaction.ReactionEventStart(arrowTransform, arrowVector);
    }
}