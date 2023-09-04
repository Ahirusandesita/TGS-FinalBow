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
/// �G�̃X�e�[�^�X�̊��N���X
/// </summary>
public abstract class EnemyStats : MonoBehaviour, IFTake
{
    #region protected�ϐ�
    protected Animator _animator = default;

    protected ObjectPoolSystem _objectPoolSystem;

    protected Transform _transform = default;

    protected Reaction _reaction = default;


    [SerializeField, Tooltip("�e�G��HP")]
    protected int _maxHp = default;

    /// <summary>
    /// ����HP
    /// </summary>
    protected int _hp;
    #endregion

    protected virtual void Start()
    {
        _animator = this.GetComponent<Animator>();
        _objectPoolSystem = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
        _transform = this.transform;

        try
        {
            _reaction = this.GetComponent<Reaction>();
        }
        catch (Exception)
        {
            _reaction = null;
            X_Debug.LogError("Reaction�N���X�����Ă��܂���");
        }
    }

    //IFScoreManager_Combo _combo = default;
    /// <summary>
    /// �G���_���[�W���󂯂�
    /// </summary>
    /// <param name="damage">�_���[�W</param>
    public virtual void TakeDamage(int damage)
    {
        _hp -= damage;
        HpGage g = this.transform.GetComponentInChildren<HpGage>();
        float hp = (float)_hp;
        float maxHp = (float)_maxHp;
        g.Hp(hp/maxHp);
    }

    public abstract void TakeBomb(int damage);

    public abstract void TakeThunder(int power);

    public abstract void TakeKnockBack();

    /// <summary>
    /// �G������
    /// </summary>
    public abstract void Death();

    public abstract int HP
    {
        get;
    }
}