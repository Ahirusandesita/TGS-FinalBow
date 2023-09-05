// --------------------------------------------------------- 
// EnemyStats.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 

using System;
using UnityEngine;

public interface IFTake
{
    void TakeDamage(int damage);
    void TakeBomb(int damage);
}

/// <summary>
/// 敵のステータスの基底クラス
/// </summary>
public abstract class EnemyStats : MonoBehaviour, IFTake
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
    #endregion

    protected virtual void Start()
    {
        _animator = this.GetComponent<Animator>();
        _objectPoolSystem = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
        _transform = this.transform;
        hpGage = this.transform.GetComponentInChildren<HpGage>();
        try
        {
            _reaction = this.GetComponent<Reaction>();
        }
        catch (Exception)
        {
            _reaction = null;
            X_Debug.LogError("Reactionクラスがついていません");
        }
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
        hpGage.Hp(hp/maxHp);
    }

    public abstract void TakeBomb(int damage);

    public abstract void TakeThunder(int power);

    public abstract void TakeKnockBack();

    /// <summary>
    /// 敵が死ぬ
    /// </summary>
    public abstract void Death();

    public abstract int HP
    {
        get;
    }
}