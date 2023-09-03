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
    /// �ړ�
    /// </summary>
    void Move();
    /// <summary>
    /// �U��
    /// </summary>
    void Attack();
    /// <summary>
    /// �ړ�����
    /// </summary>
    bool IsMove { get; }
    /// <summary>
    /// �U������
    /// </summary>
    bool IsAttack { get; }
}

/// <summary>
/// �{�X�̃A�N�V�����̋������쐬����
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
    /// �ړ��̋���
    /// </summary>
    protected abstract void MoveProcess();
    /// <summary>
    /// �U���̋���
    /// </summary>
    protected abstract void AttackProcess();

    /// <summary>
    /// �ړ��̃A�j���[�V����
    /// </summary>
    protected abstract void MoveAnimation();

    /// <summary>
    /// �U���̃A�j���[�V����
    /// </summary>
    protected abstract void AttackAnimation();

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
        //�ړ����Ă悢�Ȃ�ړ�����
        if (IsMove) MoveProcess();

        //�U�����Ă悢�Ȃ�U������
        if (IsAttack) AttackProcess();
    }
    #endregion
}
