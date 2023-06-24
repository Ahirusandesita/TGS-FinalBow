// --------------------------------------------------------- 
// EnemyStats.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
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
    protected Animator _animator = default;

    protected ObjectPoolSystem _objectPoolSystem;

    protected Transform _myTransform = default;

    [Tooltip("各敵のHP")]
    protected int _hp = default;
    #endregion

    protected virtual void Start()
    {
        _animator = this.GetComponent<Animator>();
        _objectPoolSystem = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
        _myTransform = this.transform;
    }

    //IFScoreManager_Combo _combo = default;
    /// <summary>
    /// 敵がダメージを受ける
    /// </summary>
    /// <param name="damage">ダメージ</param>
    public void TakeDamage(int damage)
    {
        _hp -= damage;
    }

    public abstract void TakeBomb(int damage);

    public abstract void TakeThunder();

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