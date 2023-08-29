// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nishigaki
// --------------------------------------------------------- 
using UnityEngine;
using Nekoslibrary;

public class ItemMove : MonoBehaviour
{
    #region �ϐ�

    #region�@bool�ꗗ

    // �ړ����J�n����t���O�@true�Ȃ�ړ��@false�Ȃ��~�@�g���Ƃ���true�ɂ��Ă�
    public bool _isStart = false;

    [SerializeField, Tooltip("�Վ��ł̎g�p�̉�")]
    public bool _UseTemporary = true;

    public bool _endSetting = false;

    public ItemVibration itemVibration;

    #endregion

    #region�@Unity�ϐ��ꗗ

    // �A�C�e�����g��transform
    private Transform _itemTransform = default;

    // �^�[�Q�b�g��transform
    private Transform _targeterTransform = default;

    // �v���C���[��transform
    private Transform _playerTransform = default;

    // �ڕW�n�_�������Ă������
    private Vector3 _goalVector = default;

    // �ڕW�n�_���猩���A�C�e���ւ̌���
    private Vector3 _betweenVector = default;

    // �A�C�e�����猩���^�[�Q�b�g�̌���
    private Vector3 _targetVector = default;

    // �ǐՂ���^�[�Q�b�g�̃I�u�W�F�N�g
    private GameObject _targeterObject = null;

    #endregion

    #region�@float�ϐ��ꗗ�@�O������

    // �����񂹂鑬�x�̌W���@���x�͋����ɉ����Ĕ��I�ɏ㏸���邽�߂��̌W��
    private float _attractSpeed = 50f;

    // �����p�ϐ��@�����񂹂�͂̑傫���@������͒萔��������
    private float _attract_Power = 1f;

    // �J�n���̖ڕW�n�_�Ƃ̒�������
    private float _startDistance = default;

    // �ǐՂ���^�[�Q�b�g�Ƃ̋���
    private float _targetDistance = default;

    // �ǐՂ��鑬�x�̉��Z�l�@�����Η����قǑ�������
    private float _addAttractSpeed = default;

    // ���[�J�����W�ɂ�����ڕW�n�_�Ƃ̋����̂y��
    private float _goalLocal_z = default;

    // �ڕW�n�_�̌����ƃA�C�e���ւ̌����̍��@�ʓx�@�ő��
    private float _differenceAngle = default;

    // �v���C���[�ƃA�C�e���̋���
    private float _playerDistance = default;
    #endregion

    #region�@float�萔�ꗗ

    // �����ɂ�鑬�x���Z�l�̏���l
    private const float SPEED_UP_MAXVARUE = 5f;

    // �����ɂ�鑬�x���Z�l�̏㏸�W��
    private const float SPEED_UP_COEFFICIENT = 1f;

    // ���B���苗���@�v���C���[�Ƃ̋����Ŕ���
    private const float CHECK_ALLIVE_DISTANCE = 10f;
    #endregion

    #region �N���X�̑���p�ϐ�

    // ObjectPoolSystem�̑���p�ϐ�
    private ObjectPoolSystem _poolManager = default;

    // CashObjectInformation�̑���p�ϐ�
    private CashObjectInformation _itemCash = default;

    // CashObjectInformation�̑���p�ϐ�
    private CashObjectInformation _targeterCash = default;

    // TargeterMove�̑���p�ϐ�
    private TargeterMove targeterMove = default;

    // IFPlayerManagerEnchantParameter�̑���p�ϐ�
    private IFPlayerManagerEnchantParameter _playerManager = default;

    // ItemStatus�̑���p�ϐ�
    private ItemStatus _itemStatus = default;

    // TargeterSetParent�̑���p�ϐ�
    private TargeterSetParent targeterclass = default;

    // BowManager
    private IFBowManager_GetStats _bowManager = default;

    // ScoreManager�̑���p�ϐ�
    private IFScoreManager_AllAttract _scoreManager = default;
    #endregion

    #endregion

    private AttractSE _attractSE = default;

    /// <summary>
    /// �����̑������
    /// </summary>
    private void Start()
    {
        // �A�C�e����Transform�̑��
        _itemTransform = this.gameObject.transform;

        // �v���C���[��Transform�̑��
        _playerTransform = GameObject.FindGameObjectWithTag("PlayerController").transform;

        // PoolManager�̑��
        _poolManager = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();

        // Item��CashObjectInformation�̑��
        _itemCash = this.gameObject.GetComponent<CashObjectInformation>();

        // PlayerManager�̑��
        _playerManager = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerManager>();

        // ItemStatus�̑��
        _itemStatus = this.gameObject.GetComponent<ItemStatus>();

        // ScoreManager�̑��
        _scoreManager = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreManager>();

        // AttractSE�̑��
        _attractSE = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<AttractSE>();

        // IFBowManager_GetStats�̑��
        _bowManager = GameObject.FindGameObjectWithTag("BowController").GetComponent<BowManager>();


    }

    /// <summary>
    /// �ړ������̎��s
    /// </summary>
    private void Update()
    {

        // �J�n�̃t���O�Ǘ�
        if (_isStart)
        {
            // �{�����ƗՎ������̐؂�ւ�
            if (!_UseTemporary)
            {

                //�ړ��������s
                ItemMovement();
            }
            else
            {
                //�Վ�
                ItemAttractTemporary();
            }

            /*
            if (itemVibration is null) return;
            itemVibration.canVibration = true;
            float distance = Vector3.Distance(_playerTransform.position, this.transform.position);
            distance = 20 - distance;
            if (distance < 0f)
            {
                distance = 0f;
            }
            itemVibration.vibrateSpeed = distance;
            */

        }

    }

    /// <summary>
    /// <para>�ړ��J�n���̐ݒ胁�\�b�h</para>
    /// <para>�^�[�Q�b�g�̐ݒ��C���X�^���X�Ƒ��x�̐ݒ�Ȃǂ��s��</para>
    /// </summary>
    /// <param name="goalTransform">�����񂹂�Ώۂ̃I�u�W�F�N�g</param>
    /// <param name="attractPower">�����񂹂�͂̑傫��</param>
    public void StartSetting()
    {
        _targeterObject = _poolManager.CallObject(PoolEnum.PoolObjectType.targeter, this.gameObject.transform.position).gameObject;

        _targeterTransform = _targeterObject.transform;

        _targeterObject.GetComponent<TargeterMove>().StartSetting();

        _targeterCash = _targeterObject.GetComponent<CashObjectInformation>();

        _startDistance = Vector3.Distance(_playerTransform.position, _itemTransform.position);

        //�ݒ芮��
        _endSetting = true;
    }

    /// <summary>
    /// �^�[�Q�b�g��Ǐ]�����鋓�����\�b�h
    /// </summary>
    private void ItemMovement()
    {
        if (!_endSetting)
        {
            StartSetting();
        }

        // �^�[�Q�b�g�ւ̃x�N�g�����擾����
        _targetVector = _targeterTransform.position - _itemTransform.position;

        // ���g���^�[�Q�b�g�Ɍ����Ĉړ�
        _itemTransform.position += _targetVector.normalized * (_targetVector.magnitude * _attractSpeed * Time.deltaTime) ;

        // �v���C���[�ƃA�C�e���̋������擾����
        _playerDistance = Vector3.Distance(_playerTransform.position , _itemTransform.position);

        // �����ŃT�C�Y�ύX
        _itemTransform.localScale = startsize * (_playerDistance / _startDistance);

        // '�ǐՂ���^�[�Q�b�g���ڕW�n�_�ɓ��B���Ă���' ���� '���g���^�[�Q�b�g�ɒǂ����Ă���' ���̔���
        if (_targetVector.magnitude < CHECK_ALLIVE_DISTANCE && _playerDistance < CHECK_ALLIVE_DISTANCE)
        {
            // �ǐՂ���^�[�Q�b�g�̍폜�y�у��Z�b�g
            ReSetAll();
        }
    }

    /// <summary>
    /// �ǐՂ���^�[�Q�b�g�̏������\�b�h
    /// </summary>
    public void ReSetAll()
    {
        this.transform.localScale = startsize;
        _targeterObject.GetComponent<TargeterMove>().TargeterReSet();
        _poolManager.ReturnObject(_targeterCash);
        _poolManager.ReturnObject(_itemCash);
    }

    /// <summary>
    /// �ǐՂ���I�u�W�F�N�g��ݒ肷��v���p�e�B
    /// </summary>
    public GameObject SetTargeter
    {
        set
        {
            // �܂��ǐՂ���^�[�Q�b�g���������Ă��Ȃ���Ύ��s
            if (_targeterObject == null)
            {
                // �ǐՂ���^�[�Q�b�g�̐ݒ�
                _targeterObject = value;

                // �ړ��̊J�n
                _isStart = true;
            }
        }
    }

    /// <summary>
    /// �������̏�����
    /// </summary>
    public void OnDisable()
    {
        // �ړ��������s���Ă����ꍇ
        if (_isStart && _bowManager.IsHolding)
        {
            GameObject.FindObjectOfType<BowVibe>().GetComponent<BowVibe>().InhallVibe();


            // �폜����I�u�W�F�N�g�̑������v���C���[�ɓn��
            _playerManager.SetEnchantParameter(_itemStatus.GetState());

            // �_���[�W�̉��Z�����ƃ��[�^�[�̉��Z�������\�b�h
            _playerManager.ArrowEnchantPlusDamage();

            // �X�R�A�̉��Z����
            _scoreManager.NormalScore_EnchantScore();
            _scoreManager.BonusScore_AttractBonus();

            // �����񂹂�����SE���Ă�
            _attractSE.PlayAttractSE();
        }


        // �X�^�[�g�t���O�̏�����
        _isStart = false;

        // �ǐՂ���^�[�Q�b�g�̏�����
        _targeterObject = null;

        //�ݒ�t���O�̏�����
        _endSetting = false;
    }



    /// <summary>
    /// �I�u�W�F�N�g�������񂹂鑬�x��ݒ肷��v���p�e�B
    /// �Ă�ۂ��[�ł����g��Ȃ��@���C���͈�����
    /// </summary>
    /// <param name="set"></param>
    public void SetAttractPower(float set)
    {
        // �����񂹑��x��ݒ�
        _attractSpeed = set;


        //���̑������ݒ� ��ŏ���
        MoveSetUp();
    }

    /// <summary>
    /// �ڕW�n�_�̃I�u�W�F�N�g��ݒ肷�郁�\�b�h
    /// �Ă�ۂ��[�ł����g��Ȃ��@���C���͈�����
    /// </summary>
    public Transform SetGoalPosition
    {
        get
        {
            return _goalTransform;
        }

        set
        {
            _goalTransform = value;
        }

    }


    #region ����Ł@�ϐ��ꗗ�@���͎g��Ȃ��@�����@�Ă�ۂ��[�Ŏg���Ă���ϐ��ꕔ����

    [SerializeField]
    //�T�C�Y�̍ŏ��l�@�����񂹎��ɏk�����鉺���l
    private const float SIZE_MINIMUM = 0.001f;

    [SerializeField]
    private float _destroyDistance = 1f;

    float _sizeValue = default;

    Vector3 _tmpDistance = default;

    Vector3 startsize = default;

    private Transform _goalTransform = default;

    private float _tmpdif = default;

    private float _tmpStartDif = default;

    #endregion

    #region �Ă�ۂ��[
    public void ItemAttractTemporary()
    {
        this.transform.Translate(_tmpDistance * _attractSpeed * Time.deltaTime);
        _tmpdif = _tmpdif - _attractSpeed * Time.deltaTime;

        _sizeValue = MathN.Clamp.Min(_tmpdif / _tmpStartDif, SIZE_MINIMUM);

        this.transform.localScale = startsize * _sizeValue;

        if (_tmpdif < _destroyDistance || !_bowManager.IsHolding)
        {

            this.transform.localScale = startsize;
            _poolManager.ReturnObject(_itemCash);
        }
    }
    #endregion

    #region ����Ł@�����ݒ�
    /// <summary>
    /// ����^���̏����ݒ�@�ŏ��Ɉ�񂾂��Ă�
    /// </summary>
    protected void MoveSetUp()
    {
        //�Վ��p
        _tmpDistance = (_goalTransform.position - this.transform.position).normalized;
        _tmpdif = MathN.Vector.Distance(this.transform.position, _goalTransform.position);
        _tmpStartDif = _tmpdif;
        startsize = this.transform.localScale;
    }
    #endregion
}
