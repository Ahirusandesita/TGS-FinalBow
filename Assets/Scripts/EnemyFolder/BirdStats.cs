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

    /// <summary>
    /// 召喚された鳥が死んだときに呼び出す
    /// </summary>
    public delegate void OnDeathBird();
    [Tooltip("この鳥が死んだときに実行 / 「敵の残存数」のデクリメント処理を登録")]
    public OnDeathBird _onDeathBird;

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

    public override void TakeBomb(int damage)
    {
        TakeDamage(damage);
        X_Debug.Log("鳥が爆発うけた");
    }

    public override void Death()
    {
        // 変数のデクリメント
        _onDeathBird();

        // スコアを加算
        _score.NomalScore_NomalEnemyScore();
        _combo.NomalScore_ComboScore();

        // 死んだときのエフェクト呼び出し
        _objectPoolSystem.CallObject(EffectPoolEnum.EffectPoolState.enemyDeath, _myTransform.position, Quaternion.identity);

        // プールに返す
        _objectPoolSystem.ReturnObject(_cashObjectInformation);
    }

    public override void Despawn()
    {
        // 変数のデクリメント
        _onDeathBird();

        // 小さくなる演出

        // プールに返す
        _objectPoolSystem.ReturnObject(_cashObjectInformation);
    }
}
