// --------------------------------------------------------- 
// EAttackMoveBase.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class EAttackMoveBase : MonoBehaviour
{
    public TagObject _PoolSystemTagData = default;

    public TagObject _PlayerControllerTagData = default;


    [Tooltip("�擾����ObjectPoolSystem�N���X")]
    protected ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("�擾����CashObjectInformation�N���X")]
    protected CashObjectInformation _cashObjectInformation = default;

    [Tooltip("�擾����PlayerHitZone�N���X")]
    protected PlayerHitZone _playerHitZone = default;

    [Tooltip("�ړ��\�t���O")]
    protected bool _canMove = default;

    [Tooltip("��������")]
    protected float _lifeTime = default;

    [Tooltip("���g��Transform�̃L���b�V��")]
    protected Transform _transform = default;

    [SerializeField, Tooltip("�U���̑���")]
    protected float _attackMoveSpeed = 20f;


    /// <summary>
    /// �܂�������ԋ���
    /// </summary>
    public virtual void Straight()
    {
        // Z�������֔�΂�
        _transform.Translate(Vector3.forward * _attackMoveSpeed * Time.deltaTime);
    }

    public void CanMove()
    {
        _canMove = false;
    }
}
