// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using UnityEngine;

public class BirdStats : CommonEnemyStats
{
    [Tooltip("�������ꂽ�����ǂ���")]
    private bool _isSummmon = false;

    [Tooltip("���̒������񂾂Ƃ��Ɏ��s / �u�G�̎c�����v�̃f�N�������g������o�^")]
    public OnDeathEnemy _onDeathBird;

    protected override void OnEnable()
    {
        // HP�̍Đݒ���Ăяo��
        base.OnEnable();

        // �u��������Ă��Ȃ��v�ɏ�����
        _isSummmon = false;
    }

    /// <summary>
    /// �������ꂽ�����ǂ���
    /// </summary>
    public bool IsSummmon
    {
        get
        {
            return _isSummmon;
        }
        set
        {
            _isSummmon = value;
        }
    }

    protected override void Start()
    {
        base.Start();

        _reaction.SubscribeReactionFinish(Death);
    }

    public override void TakeDamage(int damage, Transform arrowTransform, Vector3 arrowVector)
    {
        _reaction.ReactionStart(_transform.position);

        base.TakeDamage(damage, arrowTransform, arrowVector);
    }

    public override void TakeDamage(int damage)
    {
        _reaction.ReactionStart(_transform.position);

        base.TakeDamage(damage);
    }

    public override void Death()
    {
        

        // �ϐ��̃f�N�������g
        _onDeathBird();

        _drop.DropStart(_dropData, this.transform.position);

        base.Death();
    }

    public override void Despawn()
    {
        // �ϐ��̃f�N�������g
        _onDeathBird();

        base.Despawn();
    }
}
