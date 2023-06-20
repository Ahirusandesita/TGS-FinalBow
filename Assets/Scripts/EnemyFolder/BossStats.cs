// --------------------------------------------------------- 
// BossStats.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class BossStats : EnemyStats
{
    [Tooltip("ボスの最大HP")]
    public const int MAX_BOSS_HP = 100;

    private void OnEnable()
    {
        // HPの設定
        _hp = MAX_BOSS_HP;
    }

    public override void TakeBomb(int damage)
    {
        TakeDamage(damage);
    }

    public override void TakeThunder()
    {
        
    }

    public override void TakeKnockBack()
    {

    }

    public override void Death()
    {
        print("ボスを倒しました");
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
