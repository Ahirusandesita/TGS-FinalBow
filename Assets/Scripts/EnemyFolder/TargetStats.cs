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
        TakeDamage(damage);
    }

    public override void TakeRapidShots()
    {
       // throw new System.NotImplementedException();
    }

    public override void TakeThunder(int a)
    {
        print("�܂Ё[�[");
        Transform ab = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;

        ab.position = transform.position;
        ab.localScale = ab.localScale * 5;


    }

    public override void Death()
    {
        // ��������
        //_onDeathTarget();
        //_objectPoolSystem.ReturnObject(_cashObjectInformation);
        this.gameObject.SetActive(false);
    }

    public override int HP
    {
        get
        {
            return _hp;
        }
    }
}