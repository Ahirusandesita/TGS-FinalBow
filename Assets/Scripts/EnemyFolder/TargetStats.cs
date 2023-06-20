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

    private ObjectPoolSystem _objectPoolSystem = default;

    private void OnEnable()
    {
        _hp = 1;
    }

    private void Start()
    {
        _cashObjectInformation = this.GetComponent<CashObjectInformation>();
        _objectPoolSystem = GameObject.FindWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
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
        // 消す処理
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