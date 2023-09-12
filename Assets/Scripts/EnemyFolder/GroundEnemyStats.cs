// --------------------------------------------------------- 
// GroundEnemyStats.cs 
// 
// CreateDay: 2023/07/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 


using UnityEngine;

public class GroundEnemyStats : CommonEnemyStats
{
    [SerializeField] Transform dropSpawn = default;
    [SerializeField] float spawnOffsetY = 3f;
    protected override void Start()
    {
        base.Start();

        _reaction.SubscribeReactionFinish(Death);
    }

    [Tooltip("このワームが死んだときに実行 / 「敵の残存数」のデクリメント処理を登録")]
    public OnDeathEnemy _onDeathEnemy;


    public override void Death()
    {
        Vector3 spawn = dropSpawn.position + Vector3.up * spawnOffsetY;
        _drop.DropStart(_dropData, spawn);

        if(_onDeathEnemy is not null)
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