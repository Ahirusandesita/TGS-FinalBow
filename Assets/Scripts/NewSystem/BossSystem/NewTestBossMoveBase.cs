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
    /// �ړ�
    /// </summary>
    void Move();

    /// <summary>
    /// �ړ�����
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
    /// �ړ��̋���
    /// </summary>
    protected abstract void MoveProcess();

    /// <summary>
    /// �ړ��̃A�j���[�V����
    /// </summary>
    protected abstract void MoveAnimation();


    private void Awake()
    {
        //�A�j���[�V�����t���O��True�Ȃ�A�j���[�V�������Đ�����֐������s����

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
        //�ړ����Ă悢�Ȃ�ړ�����
        if (IsMove) MoveProcess();
    }
    #endregion
}