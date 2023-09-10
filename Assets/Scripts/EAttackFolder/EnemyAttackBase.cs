// --------------------------------------------------------- 
// EnemyAttackBase.cs 
// 
// CreateDay: 2023/06/23
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;


public abstract class EnemyAttackBase : MonoBehaviour,IFItemMove
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
    protected Transform _transform = default;

    [Tooltip("�ړ��\�t���O")]
    protected bool _canMove = default;

    [Tooltip("��������")]
    protected float _lifeTime = default;

    [SerializeField, Tooltip("�U���̑���")]
    protected float _attackMoveSpeed = 20f;

    [Tooltip("�U���̔��˃f�B���C")]
    protected float _attackStartDelay = 0f;

    [Tooltip("�U���������܂ł̑ҋ@����")]
    protected WaitForSeconds _attackDelayWait = default;
    #endregion

    public bool CanMove { get; set; }

    #region method

    protected virtual void Awake()
    {
        SetAttackStartDelay();
        _attackDelayWait = new WaitForSeconds(_attackStartDelay);

        _transform = this.transform;
    }

    protected virtual void OnEnable()
    {
        // �U���̐������Ԃ�ݒ�
        _lifeTime = 12f;

        // �t���O��������
        _canMove = false;

        StartCoroutine(AttackStartWait());
    }

    protected virtual void Start()
    {
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
        if (!CanMove)
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
    protected abstract void AttackMove();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackStartWait()
    {
        yield return _attackDelayWait;
        _canMove = true;
        CanMove = true;
    }

    /// <summary>
    /// ���b��ɃX�^�[�g���邩
    /// </summary>
    protected virtual void SetAttackStartDelay()
    {
        _attackStartDelay = 0f;
    }

    public void SetParentNull()
    {
        
    }
    #endregion
}
