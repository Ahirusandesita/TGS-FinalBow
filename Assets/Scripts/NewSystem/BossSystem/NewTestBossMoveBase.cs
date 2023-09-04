// --------------------------------------------------------- 
// NewTestBossAction.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public interface IBossMove
{
    /// <summary>
    /// 移動
    /// </summary>
    void Move();

    /// <summary>
    /// 移動中か
    /// </summary>
    bool IsMove { get; }

}

public abstract class NewTestBossMoveBase : MonoBehaviour,IBossMove
{
    #region variable 
    protected bool isMove;

    private Animator animator;
    private ReActiveProperty<bool> isMoveAnimation = new ReActiveProperty<bool>();
    #endregion
    #region property
    public bool IsMove => isMove;
    #endregion
    #region method

    public void Move()
    {
        isMove = true;
        isMoveAnimation.Value = true;
    }
    /// <summary>
    /// 移動の挙動
    /// </summary>
    protected abstract void MoveProcess();

    /// <summary>
    /// 移動のアニメーション
    /// </summary>
    protected abstract void MoveAnimation();


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
    }

    protected virtual void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        //移動してよいなら移動する
        if (IsMove) MoveProcess();
    }
    #endregion
}