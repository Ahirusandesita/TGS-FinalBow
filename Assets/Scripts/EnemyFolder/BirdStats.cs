// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using UnityEngine;

public class BirdStats : CommonEnemyStats
{
    [Tooltip("召喚された鳥かどうか")]
    private bool _isSummmon = false;

    [Tooltip("この鳥が死んだときに実行 / 「敵の残存数」のデクリメント処理を登録")]
    public OnDeathEnemy _onDeathBird;

    protected override void OnEnable()
    {
        // HPの再設定を呼び出し
        base.OnEnable();

        // 「召喚されていない」に初期化
        _isSummmon = false;
    }

    /// <summary>
    /// 召喚された鳥かどうか
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
        

        // 変数のデクリメント
        _onDeathBird();

        _drop.DropStart(_dropData, this.transform.position);

        base.Death();
    }

    public override void Despawn()
    {
        // 変数のデクリメント
        _onDeathBird();

        base.Despawn();
    }
}
