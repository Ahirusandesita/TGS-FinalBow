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
    /// �U��
    /// </summary>
    void Attack();

    /// <summary>
    /// �U������
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
        //�U�����Ă悢�Ȃ�U������
        if (IsAttack) AttackProcess();
        isAttackAnimation.Value = true;
    }

    /// <summary>
    /// �U���̋���
    /// </summary>
    protected abstract void AttackProcess();

    /// <summary>
    /// �U���̃A�j���[�V����
    /// </summary>
    protected abstract void AttackAnimation();

    private void Awake()
    {
        //�A�j���[�V�����t���O��True�Ȃ�A�j���[�V�������Đ�����֐������s����
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