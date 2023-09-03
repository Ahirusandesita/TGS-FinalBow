// --------------------------------------------------------- 
// TestBossActionBase.cs 
// 
// CreateDay: 
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;

public interface IBossAction
{
    /// <summary>
    /// 移動
    /// </summary>
    void Move();
    /// <summary>
    /// 攻撃
    /// </summary>
    void Attack();
    /// <summary>
    /// 移動中か
    /// </summary>
    bool IsMove { get; }
    /// <summary>
    /// 攻撃中か
    /// </summary>
    bool IsAttack { get; }
}

/// <summary>
/// ボスのアクションの挙動を作成する
/// </summary>
public abstract class TestBossActionBase : MonoBehaviour, IBossAction
{
    #region variable 
    protected bool isMove;
    protected bool isAttack;

    private Animator animator;
    private ReActiveProperty<bool> isMoveAnimation = new ReActiveProperty<bool>();
    private ReActiveProperty<bool> isAttackAnimation = new ReActiveProperty<bool>();
    #endregion
    #region property
    public bool IsMove => isMove;
    public bool IsAttack => isAttack;
    #endregion
    #region method

    public void Move()
    {
        isMove = true;
        isMoveAnimation.Value = true;
    }
    public void Attack()
    {
        isAttack = true;
        isMoveAnimation.Value = true;
    }
    /// <summary>
    /// 移動の挙動
    /// </summary>
    protected abstract void MoveProcess();
    /// <summary>
    /// 攻撃の挙動
    /// </summary>
    protected abstract void AttackProcess();

    /// <summary>
    /// 移動のアニメーション
    /// </summary>
    protected abstract void MoveAnimation();

    /// <summary>
    /// 攻撃のアニメーション
    /// </summary>
    protected abstract void AttackAnimation();

    private void Awake()
    {
        //アニメーションフラグがTrueならアニメーションを再生する関数を実行する

        isMoveAnimation.Subject.Subscribe(
            isAction =>
            {
                if (isAction)
                {
                    MoveAnimation();
                    isMoveAnimation.Value = false;
                }
            }
            );
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

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        //移動してよいなら移動する
        if (IsMove) MoveProcess();

        //攻撃してよいなら攻撃する
        if (IsAttack) AttackProcess();
    }
    #endregion
}
