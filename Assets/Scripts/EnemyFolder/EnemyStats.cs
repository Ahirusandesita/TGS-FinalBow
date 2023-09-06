// --------------------------------------------------------- 
// EnemyStats.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 

using System;
using UnityEngine;

/// <summary>
/// 敵のステータスの基底クラス
/// </summary>
public abstract class EnemyStats : MonoBehaviour
{
    #region protected変数
    private HpGage hpGage;

    protected Animator _animator = default;

    protected ObjectPoolSystem _objectPoolSystem;

    protected Transform _transform = default;

    protected Reaction _reaction = default;


    [SerializeField, Tooltip("各敵のHP")]
    protected int _maxHp = default;

    /// <summary>
    /// 初期HP
    /// </summary>
    protected int _hp;

    [Tooltip("直前に食らった矢のエンチャント")]
    protected EnchantmentEnum.EnchantmentState _takeEnchantment = EnchantmentEnum.EnchantmentState.nothing;
    #endregion

    protected virtual void Awake()
    {
        _reaction = this.GetComponent<Reaction>();
    }

    protected virtual void Start()
    {
        _animator = this.GetComponent<Animator>();
        _objectPoolSystem = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
        _transform = this.transform;
        hpGage = this.transform.GetComponentInChildren<HpGage>();
    }

    //IFScoreManager_Combo _combo = default;
    /// <summary>
    /// 敵がダメージを受ける
    /// </summary>
    /// <param name="damage">ダメージ</param>
    public virtual void TakeDamage(int damage)
    {
        _hp -= damage;

        float hp = (float)_hp;
        float maxHp = (float)_maxHp;
        if (hpGage == null) return;
        hpGage.Hp(hp / maxHp);

        if (_hp <= 0)
            OnDeathReactions();
    }

    public virtual void TakeBomb(int damage) { _takeEnchantment = EnchantmentEnum.EnchantmentState.bomb; }

    public virtual void TakeThunder(int power) { _takeEnchantment = EnchantmentEnum.EnchantmentState.thunder; }

    public virtual void TakeRapidShots() { _takeEnchantment = EnchantmentEnum.EnchantmentState.rapidShots; }

    public virtual void TakePenetrate() { _takeEnchantment = EnchantmentEnum.EnchantmentState.penetrate; }

    public virtual void TakeHoming() { _takeEnchantment = EnchantmentEnum.EnchantmentState.homing; }

    /// <summary>
    /// 敵が死ぬ
    /// </summary>
    public abstract void Death();

    /// <summary>
    /// 敵が死んだときのリアクション
    /// </summary>
    protected virtual void OnDeathReactions()
    {
        _reaction.ReactionSetting(_takeEnchantment);
        // ---------------------------------------------------------------
        _reaction.ReactionEventStart(_transform, Vector3.zero); //あとで当たった場所取得して設定
        //----------------------------------------------------------------
    }

    public abstract int HP
    {
        get;
    }
}
