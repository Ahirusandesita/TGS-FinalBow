// --------------------------------------------------------- 
// EnemyStatsCaller.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class EnemyStatsCaller : EnemyStats
{
    [SerializeField]
    EnemyStats rootStats = default;

    public override int HP => rootStats.HP;

    public override void Death()
    {
        
    }

    protected override void Awake()
    {
        if(rootStats is null)
        rootStats = transform.root.GetComponent<EnemyStats>();   
    }

    protected override void Start()
    {
        
    }

    protected override void OnDeathReactions(Transform arrowTransform, Vector3 arrowVector)
    {
        
    }

    public override void TakeBomb(int damage)
    {
        rootStats.TakeBomb(damage);
    }

    public override void TakeBomb(int damage, Transform arrowTransform, Vector3 arrowVector)
    {
        rootStats.TakeBomb(damage, arrowTransform, arrowVector);
    }

    public override void TakeDamage(int damage)
    {
        rootStats.TakeDamage(damage);
    }

    public override void TakeDamage(int damage, Transform arrowTransform, Vector3 arrowVector)
    {
        rootStats.TakeDamage(damage, arrowTransform, arrowVector);
    }

    public override void TakeHoming()
    {
        rootStats.TakeHoming();
    }

    public override void TakeNormal()
    {
        rootStats.TakeNormal();
    }

    public override void TakePenetrate()
    {
        rootStats.TakePenetrate();
    }

    public override void TakeRapidShots()
    {
        rootStats.TakeRapidShots();
    }

    public override void TakeThunder(int power)
    {
        rootStats.TakeThunder(power);
    }
}