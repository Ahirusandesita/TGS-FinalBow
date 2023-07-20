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

    #endregion

    #region�@Unity�ϐ��ꗗ

    // �ڕW�n�_�������Ă������
    private Vector3 _goalVector = default;

    // �ڕW�n�_���猩���A�C�e���ւ̌���
    private Vector3 _betweenVector = default;

    // �A�C�e�����猩���^�[�Q�b�g�̌���
    private Vector3 _targetVector = default;

    // 
    private Vector3 _spawnPosition = default;

    // �ǐՂ���^�[�Q�b�g�̃I�u�W�F�N�g
    private GameObject _targeterObject = null;

    #endregion

    #region�@float�ϐ��ꗗ�@�O������

    // �����p�ϐ��@�����񂹂�͂̑傫���@������͒萔��������
    private float _attract_Power = 1f;

    // �ڕW�n�_�Ƃ̒�������
    private float _distance = default;

    // �ǐՂ���^�[�Q�b�g�Ƃ̋���
    private float _targetDistance = default;

    // �ǐՂ��鑬�x�̉��Z�l�@�����Η����قǑ�������
    private float _addAttractSpeed = default;

    // ���[�J�����W�ɂ�����ڕW�n�_�Ƃ̋����̂y��
    private float _goalLocal_z = default;

    // �ڕW�n�_�̌����ƃA�C�e���ւ̌����̍��@�ʓx�@�ő��
    private float _differenceAngle = default;

    #endregion

    #region�@float�萔�ꗗ

    // �����ɂ�鑬�x���Z�l�̏���l
    private const float SPEED_UP_MAXVARUE = 5f;

    // �����ɂ�鑬�x���Z�l�̏㏸�W��
    private const float SPEED_UP_COEFFICIENT = 1f;
    #endregion

    #region �N���X�̑���p�ϐ�

    // ObjectPoolSystem�̑���p�ϐ�
    private ObjectPoolSystem PoolManager = default;

    // CashObjectInformation�̑���p�ϐ�
    private CashObjectInformation Cash = default;

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
        // PoolManager�̑��
        PoolManager = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();

        // CashObjectInformation�̑��
        Cash = this.GetComponent<CashObjectInformation>();

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
        }

    }

    /// <summary>
    /// <para>�ړ��J�n���̐ݒ胁�\�b�h</para>
    /// <para>�^�[�Q�b�g�̐ݒ��C���X�^���X�Ƒ��x�̐ݒ�Ȃǂ��s��</para>
    /// </summary>
    /// <param name="goalTransform">�����񂹂�Ώۂ̃I�u�W�F�N�g</param>
    /// <param name="attractPower">�����񂹂�͂̑傫��</param>
    public void StartSetting(Transform goalTransform, float attractPower)
    {
        // �ڕW�n�_�Ƃ̒������������߂�
        _distance = MathN.Vector.Distance(goalTransform.position, this.transform.position);

        // �ڕW�n�_�̌�������
        _goalVector = goalTransform.TransformVector(Vector3.forward);

        // �ڕW�n�_���猩���A�C�e���ւ̌��������߂�
        _betweenVector = MathN.Vector.Normalize(goalTransform.position, this.transform.position);

        // �ڕW�n�_�̌����ƖڕW�n�_���猩���A�C�e���ւ̌����̍������߂Čʓx�@�ɕϊ�
        _differenceAngle = MathN.Mod.Chenge_DegToRad(Vector3.Angle(_goalVector, _betweenVector));

        // ���[�J�����W�y�� = ���� �~ Cos��
        _goalLocal_z = _distance * Mathf.Cos(_differenceAngle);

        // �ڕW�n�_�̌����Ƀ��[�J���y�������ꂽ�n�_�����߂�
        _spawnPosition = goalTransform.TransformVector(Vector3.forward) * _goalLocal_z;

        // �ڕW�n�_����ɗ��ꂽ�������Z���ăX�|�[���ʒu�����߂�
        _spawnPosition = goalTransform.localPosition + _spawnPosition;

        // ���߂��ʒu�ɒǐՂ���^�[�Q�b�g�̍쐬����
        CreateTargeter(_spawnPosition);

        //�^�[�Q�b�g�̃N���X����
        targeterclass = _targeterObject.GetComponent<TargeterSetParent>();

        // ���g�̈ړ����x��ݒ�
        _attract_Power = attractPower;

        // �^�[�Q�b�g�̈ړ����x��ݒ�
        targeterclass.SetAttractPower = attractPower;

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
            StartSetting(_goalTransform, _attract_Power);
        }

        // ���g���猩���ǐՂ���^�[�Q�b�g�ւ̌��������߂�
        _targetVector = MathN.Vector.Normalize(this.transform.position, _targeterObject.transform.position);

        // �ǐՂ���^�[�Q�b�g�Ƃ̋��������߂�
        _targetDistance = MathN.Vector.Distance(this.transform.position, _targeterObject.transform.position);

        // �ǐՂ���^�[�Q�b�g�Ƃ̋�������ɑ��x�̉��Z�l�����߂�
        _addAttractSpeed = MathN.Clamp.Max(SPEED_UP_COEFFICIENT * _targetDistance, SPEED_UP_MAXVARUE);

        // ���M���^�[�Q�b�g�Ɍ����Ĉړ�
        transform.position += _targetVector * (_attract_Power + _addAttractSpeed) * Time.deltaTime;

        // '�ǐՂ���^�[�Q�b�g���ڕW�n�_�ɓ��B���Ă���' ���� '���g���^�[�Q�b�g�ɒǂ����Ă���' ���̔���
        if (targeterclass.IsTargeterArrivel && _targetDistance < _destroyDistance || !_bowManager.IsHolding)
        {
            // �ǐՂ���^�[�Q�b�g�̍폜�y�у��Z�b�g
            ReSetAll();
        }
    }

    /// <summary>
    /// �ǐՂ���^�[�Q�b�g�̐������\�b�h
    /// </summary>
    /// <param name="distance_z">���[�J�����W�ɂ�����ڕW�n�_�Ƃ̋����̂y��</param>
    /// <param name="goalTransform">�ڕW�n�_��Transform</param>
    public void CreateTargeter(Vector3 spawnPosition)
    {
        SetTargeter = PoolManager.CallObject(PoolEnum.PoolObjectType.targeter, spawnPosition).gameObject;
    }

    /// <summary>
    /// �ǐՂ���^�[�Q�b�g�̏������\�b�h
    /// </summary>
    public void ReSetAll()
    {
        targeterclass.ReSetTargeter();
        this.transform.localScale = startsize;
        PoolManager.ReturnObject(Cash);
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

        // �����񂹂鑬�x�̏�����
        _attract_Power = default;

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
        _attract_Power = set;


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
        this.transform.Translate(_tmpDistance * _attract_Power * Time.deltaTime);
        _tmpdif = _tmpdif - _attract_Power * Time.deltaTime;

        _sizeValue = MathN.Clamp.Min(_tmpdif / _tmpStartDif, SIZE_MINIMUM);

        this.transform.localScale = startsize * _sizeValue;

        if (_tmpdif < _destroyDistance || !_bowManager.IsHolding)
        {
            this.transform.localScale = startsize;
            PoolManager.ReturnObject(Cash);
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
