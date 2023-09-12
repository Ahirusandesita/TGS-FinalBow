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

    [Tooltip("このワームが死んだときに実行 / 「敵の残存数」のデクリメント処理を登録")]
    public OnDeathEnemy _onDeathEnemy;


    public override void Death()
    {
        _drop.DropStart(_dropData, this.transform.position);
        _onDeathEnemy();

        base.Death();
    }

    public override void Despawn()
    {
        _onDeathEnemy();

        base.Despawn();
    }

    protected override void OnDeathReactions(Transform arrowTransform, Vector3 arrowVector)
    {
        _reaction.ReactionSetting(_takeEnchantment);

        _reaction.ReactionEventStart(arrowTransform, arrowVector);
    }
}