// --------------------------------------------------------- 
// TargetStats.cs 
// 
// CreateDay: 2023/06/16
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
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
    }

    public override void TakeBomb(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override void TakeKnockBack()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeThunder()
    {
        throw new System.NotImplementedException();
    }

    public override void Death()
    {
        // ��������
        _onDeathTarget();
        _objectPoolSystem.ReturnObject(_cashObjectInformation);
    }

    public override int HP
    {
        get
        {
            return _hp;
        }
    }
}