// --------------------------------------------------------- 
// EnemyAttackBase.cs 
// 
// CreateDay: 2023/06/23
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public abstract class EnemyAttackBase : MonoBehaviour
{
    #region variable 
    public TagObject _PoolSystemTagData = default;

    public TagObject _PlayerControllerTagData = default;


    [Tooltip("�擾����ObjectPoolSystem�N���X")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("�擾����CashObjectInformation�N���X")]
    private CashObjectInformation _cashObjectInformation = default;

    [Tooltip("�擾����PlayerHitZone�N���X")]
    private PlayerHitZone _playerHitZone = default;


    [Tooltip("���g��Transform�̃L���b�V��")]
    private Transform _transform = default;


    [Tooltip("�ړ��\�t���O")]
    protected bool _canMove = default;

    [Tooltip("��������")]
    protected float _lifeTime = default;

    [SerializeField, Tooltip("�U���̑���")]
    protected float _attackMoveSpeed = 20f;

    protected float _attackStartTime = 0f;

    #endregion
    #region property
    #endregion
    #region method

    private void OnEnable()
    {
        // �U���̐������Ԃ�ݒ�
        _lifeTime = 5f;

        // �t���O��������
        _canMove = false;

        SetAttackStartTime();

        StartCoroutine(AttackStartTime());
    }

    protected void Start()
    {
        _transform = this.transform;

        // �{�X�̍U�����v�[������I�u�W�F�N�g����v�[�����擾
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();

        // Return�p�Ɏ������g�̏����擾
        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        // �v���C���[���瓖���蔻��̃N���X���擾
        _playerHitZone = GameObject.FindWithTag(_PlayerControllerTagData.TagName).GetComponent<PlayerHitZone>();
    }

    protected void Update()
    {

        // �����Ȃ���ΕԂ�
        if (!_canMove)
        {
            return;
        }

        AttackMove();


        // �v���C���[�Ƀq�b�g���Ă��邩�ǂ����𔻒�
        _playerHitZone.HitZone(_transform.position);

        // ���g�̎c�������Ԃ��f�N�������g
        _lifeTime -= Time.deltaTime;

        // �������Ԃ�0�ɂȂ��������
        if (_lifeTime <= 0f)
        {
            _objectPoolSystem.ReturnObject(_cashObjectInformation);
        }
    }

    /// <summary>
    /// �U���̋����������~
    /// </summary>
    public void CanNotMove()
    {
        _canMove = false;
    }


    /// <summary>
    /// �U���̋���
    /// </summary>
    public abstract void AttackMove();

    private IEnumerator AttackStartTime()
    {
        yield return new WaitForSeconds(_attackStartTime);
        _canMove = true;
    }

    /// <summary>
    /// ���b��ɃX�^�[�g���邩
    /// </summary>
    protected virtual void SetAttackStartTime()
    {
        _attackStartTime = 0f;
    }
    #endregion
}
