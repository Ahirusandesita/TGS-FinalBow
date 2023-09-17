// --------------------------------------------------------- 
// TargetStats.cs 
// 
// CreateDay: 2023/06/16
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Reaction))]
public class TargetStats : EnemyStats
{
    public Action _decrementTargetAmount { set; private get; }

    private CashObjectInformation _cashObjectInformation = default;

    private bool _isFirst = true;

    public bool _canDeath { private get; set; }

    private void OnEnable()
    {
        _hp = 1;
        _isFirst = true;
        _canDeath = true;
    }

    protected override void Start()
    {
        base.Start();

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        _reaction.SubscribeReactionFinish(Death);

        _cashObjectInformation = transform.root.GetComponent<CashObjectInformation>();

    }

    public override void TakeBomb(int damage, Transform arrowTransform, Vector3 arrowVector)
    {
        base.TakeBomb(damage, arrowTransform, arrowVector);
        TakeDamage(damage, arrowTransform, arrowVector);
    }

    public override void Death()
    {
        if (_isFirst && _canDeath)
        {
            _isFirst = false;

            // è¡Ç∑èàóù
            _decrementTargetAmount();
            _objectPoolSystem.ReturnObject(_cashObjectInformation);
        }
    }

    public void Despawn()
    {
        // è¡Ç∑èàóù
        _decrementTargetAmount();
        _objectPoolSystem.ReturnObject(_cashObjectInformation);
    }

    public override int HP
    {
        get
        {
            return _hp;
        }
    }

    protected override void OnDeathReactions(Transform arrowTransform, Vector3 arrowVector)
    {
        base.OnDeathReactions(arrowTransform, arrowVector);

    }
}