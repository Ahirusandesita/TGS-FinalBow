// --------------------------------------------------------- 
// TargetStats.cs 
// 
// CreateDay: 2023/06/16
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Reaction))]
public class TargetStats : EnemyStats
{
    /// <summary>
    /// �I�����񂾂Ƃ��ɌĂяo��
    /// </summary>
    public delegate void OnDeathTarget();
    [Tooltip("���̓I�����񂾂Ƃ��Ɏ��s / �u�G�̎c�����v�̃f�N�������g������o�^")]
    public OnDeathTarget _onDeathTarget;

    private CashObjectInformation _cashObjectInformation = default;

    private void OnEnable()
    {
        _hp = 1;

        
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
        // ��������
        //_onDeathTarget();
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