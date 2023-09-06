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

    /// <summary>
    /// �������ꂽ�������񂾂Ƃ��ɌĂяo��
    /// </summary>
    public delegate void OnDeathBird();
    [Tooltip("���̒������񂾂Ƃ��Ɏ��s / �u�G�̎c�����v�̃f�N�������g������o�^")]
    public OnDeathBird _onDeathBird;

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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        _reaction.ReactionStart(_transform.position);
    }

    public override void TakeBomb(int damage)
    {
        TakeDamage(damage);
    }

    public override void Death()
    {
        _reaction.ReactionSetting(_takeEnchantment);
        // ---------------------------------------------------------------
        _reaction.ReactionEventStart(_transform, Vector3.zero); //���Ƃœ��������ꏊ�擾���Đݒ�
        //----------------------------------------------------------------


        //if (_reaction.IsReactionEnd)
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
