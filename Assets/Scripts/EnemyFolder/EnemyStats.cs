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
/// �G�̃X�e�[�^�X�̊��N���X
/// </summary>
public abstract class EnemyStats : MonoBehaviour, IFTake
{
    #region protected�ϐ�
    protected Animator _animator = default;

    protected ObjectPoolSystem _objectPoolSystem;

    protected Transform _myTransform = default;

    [Tooltip("�e�G��HP")]
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
    /// �G���_���[�W���󂯂�
    /// </summary>
    /// <param name="damage">�_���[�W</param>
    public void TakeDamage(int damage)
    {
        _hp -= damage;
    }

    public abstract void TakeBomb(int damage);

    public abstract void TakeThunder();

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