// --------------------------------------------------------- 
// TargeterSetParent.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TargeterSetParent : MonoBehaviour
{
    #region �ϐ�
    private bool _isStart = false;
    private bool _isTargetArrival = false;
    private float _distance = default;
    private float _moveRadius = default;
    private float _timeCount = default;
    private float _moveValue_x = default;
    private float _moveValue_y = default;
    private float _attractPower = default;
    private const float PERIOD_VALUE = Mathf.PI * 2f;
    private const float COEFFICIENT_DISTANCExRADIUS = 10f;
    private Transform ParentObject = default;

    // ObjectPoolSystem�̑���p�ϐ�
    private ObjectPoolSystem PoolManager = default;

    // CashObjectInformation�̑���p�ϐ�
    private CashObjectInformation Cash = default;
    #endregion
    #region �v���p�e�B

    /// <summary>
    /// �ڕW�n�_�ɓ��B���Ă��邩�ǂ���
    /// </summary>
    public bool IsTargeterArrivel
    {
        get
        {
            return _isTargetArrival;
        }
    }

    /// <summary>
    /// �����񂹂鑬�x�̐ݒ�
    /// </summary>
    public float SetAttractPower
    {
        set
        {
            _attractPower = value;
        }
    }

    #endregion
    #region ���\�b�h

    /// <summary>
    /// �����ݒ�
    /// </summary>
    private void Start()
    {
        // ParentObject�̑��
        ParentObject = GameObject.FindGameObjectWithTag("PlayerController").transform;

        // PoolManager�̑��
        PoolManager = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();

        // CashObjectInformation�̑��
        Cash = this.GetComponent<CashObjectInformation>();
    }

    /// <summary>
    /// �ړ������̎��s
    /// </summary>
    private void Update()
    {
        // �J�n�t���O�������Ă�����
        if (_isStart)
        {
            // �ړ����\�b�h�̌Ăяo��
            TargeterMovement();
        }
    }

    /// <summary>
    /// �Ăяo�����̏����ݒ�
    /// </summary>
    private void OnEnable()
    {
        //�X�^�[�g�̎擾�O�ɑ������̂�h������
        if(ParentObject != null)
        {
            // �e�I�u�W�F�N�g�̐ݒ�
            this.transform.parent = ParentObject.transform;
        }

        // �ڕW�n�_�Ƃ̃��[�J���|�W�V�����y����
        _distance = this.transform.localPosition.z;

        // �J�n�t���O�𗧂Ă�
        _isStart = true;
    }

    /// <summary>
    /// �폜���̏������ݒ�
    /// </summary>
    private void OnDisable()
    {
        // �e�q�֌W�̉���
        this.transform.parent = null;

        // �J�n�t���O�̏�����
        _isStart = false;

        // ���B�t���O�̏�����
        _isTargetArrival = false;

        // ���Ԃ̏�����
        _timeCount = default;
    }

    /// <summary>
    /// �^�[�Q�b�g�̈ړ�����
    /// </summary>
    public void TargeterMovement()
    {
        // ���B���Ă��Ȃ�������@���B���Ă��Ȃ��Ԉړ��@���B������t���O�ύX
        if (_distance > 0)
        {
            // ����O���̔��a�����߂�
            _moveRadius = _distance * COEFFICIENT_DISTANCExRADIUS;

            // �w�x�����ꂼ��̈ʒu�����߂�
            _moveValue_x = Mathf.Sin(_timeCount * PERIOD_VALUE) * _moveRadius;
            _moveValue_y = Mathf.Cos(_timeCount * PERIOD_VALUE) * _moveRadius;

            // �e�����Ƃ̈ʒu����
            this.transform.localPosition = new Vector3( _moveValue_x,   // �w��
                                                        _moveValue_y,   // �x��
                                                        _distance   );  // �y��

            // �������k�߂�
            _distance -= _attractPower * Time.deltaTime;

            // ���Ԍo��
            _timeCount += Time.deltaTime;
        }
        else
        {
            print("���[�J���@" + this.transform.localPosition) ;
            print("���[���h�@" + this.transform.position);
            print("�e�@" + this.transform.parent.position);
            // �J�n�t���O��؂�
            _isStart = false;

            // ���B�t���O�𗧂Ă�
            _isTargetArrival = true;
        }
    }

    public void ReSetTargeter()
    {
        PoolManager.ReturnObject(Cash);
    }

    #endregion
}