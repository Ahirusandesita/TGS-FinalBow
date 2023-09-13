// --------------------------------------------------------- 
// EnemyGimmickStats.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System;
[RequireComponent(typeof(Drop), typeof(GatReactiveEvent), typeof(Reaction))]
public class EnemyGimmickStats : EnemyStats, IFGetReactiveEvent
{

    bool used = false;
    public override int HP => throw new System.NotImplementedException();

    protected override void Start()
    {
        base.Start();

        _reaction.SubscribeReactionFinish(Death);
    }

    private void OnEnable()
    {
        _hp = 1;
        used = false;
    }

    public void CallEvent()
    {
        TakeDamage(_hp);
    }



    public override void Death()
    {
        this.gameObject.SetActive(false);
    }

    protected override void OnDeathReactions(Transform arrowTransform, Vector3 arrowVector)
    {
        _reaction.ReactionSetting(_takeEnchantment);
        _reaction.ReactionEventStart(arrowTransform, arrowVector);


    }

    public override void TakeBomb(int damage, Transform arrowTransform, Vector3 arrowVector)
    {
        base.TakeBomb(damage, arrowTransform, arrowVector);

        TakeDamage(damage, arrowTransform, arrowVector);
    }
}