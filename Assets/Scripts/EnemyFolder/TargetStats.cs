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
    /// 的が死んだときに呼び出す
    /// </summary>
    public delegate void OnDeathTarget();
    [Tooltip("この的が死んだときに実行 / 「敵の残存数」のデクリメント処理を登録")]
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
        print("まひーー");
        Transform ab = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;

        ab.position = transform.position;
        ab.localScale = ab.localScale * 5;


    }

    public override void Death()
    {
        // 消す処理
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