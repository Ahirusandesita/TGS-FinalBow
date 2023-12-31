
using UnityEngine;

public class BugEnemyStats : CommonEnemyStats
{

    protected override void Start()
    {
        base.Start();

        _reaction.SubscribeReactionFinish(Death);
    }

    [Tooltip("このワームが死んだときに実行 / 「敵の残存数」のデクリメント処理を登録")]
    public OnDeathEnemy _onDeathEnemy;


    public override void Death()
    {
        _drop.DropStart(_dropData, this.transform.position);
        _onDeathEnemy();

        base.Death();
    }

    protected override void OnDeathReactions(Transform arrowTransform, Vector3 arrowVector)
    {
        _reaction.ReactionSetting(_takeEnchantment);

        _reaction.ReactionEventStart(arrowTransform, arrowVector);
    }
}