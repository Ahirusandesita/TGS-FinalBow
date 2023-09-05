// --------------------------------------------------------- 
// NewTestBossAttackBase.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public interface IBossAttack
{
    /// <summary>
    /// 攻撃
    /// </summary>
    void Attack();

    /// <summary>
    /// 攻撃中か
    /// </summary>
    bool IsAttack { get; }
}
public abstract class NewTestBossAttackBase : MonoBehaviour,IBossAttack
{
    #region variable 
    public IReActiveProperty<bool> readOnlyIsAttack => isAttack;
    protected ReActiveProperty<bool> isAttack = new ReActiveProperty<bool>();

    private Animator animator;
    private ReActiveProperty<bool> isAttackAnimation = new ReActiveProperty<bool>();
    #endregion
    #region property
    public bool IsAttack => isAttack.Value;
    #endregion
    #region method

    public void Attack()
    {
        //攻撃してよいなら攻撃する
        if (IsAttack) AttackProcess();
        isAttackAnimation.Value = true;
    }

    /// <summary>
    /// 攻撃の挙動
    /// </summary>
    protected abstract void AttackProcess();

    /// <summary>
    /// 攻撃のアニメーション
    /// </summary>
    protected abstract void AttackAnimation();

    private void Awake()
    {
        //アニメーションフラグがTrueならアニメーションを再生する関数を実行する
        isAttackAnimation.Subject.Subscribe(
            isAction =>
            {
                if (isAction)
                {
                    AttackAnimation();
                    isAttackAnimation.Value = false;
                }
            }
            );
    }

    protected virtual void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    #endregion
}