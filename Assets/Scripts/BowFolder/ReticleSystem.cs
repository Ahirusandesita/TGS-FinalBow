// --------------------------------------------------------- 
// ReticleSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System;

public class ReticleSystem : MonoBehaviour
{
    private ArrowEnchantment _enchant = default;

    [SerializeField]
    private float _trackingTime = 0.5f;

    [SerializeField]
    private int _passValue = 10;

    [SerializeField]
    private LineRenderer _reticleRenderer = default;

    [SerializeField]
    private Transform _startPosition = default;

    [SerializeField]
    private Transform _shadowTransform = default;

    private bool _nowCreate = false;

    private float _timeToRangeCoefficient = default;

    private float _timeByCount;



    private Vector3 _shadowVector;
    private float _arrowSpeed_Horizontal;
    private float _shadowSpeed_X;
    private float _shadowSpeed_Y;
    private float _shadowSpeed_Z;
    private float _maxRange;
    private float _maxGravity;
    private float _nowRange;
    private float _addGravity;
    private bool _endSetting;
    private Vector3 _forward = Vector3.forward;
    private float _nowSpeedValue;
    private float _arrowSpeed;
    private Vector3 _moveValue;
    private readonly float TERMINAL_VELOCITY;
    private readonly float INFINITY;
    private readonly float ZERO;
    private readonly float STANDARD_SPEED_VALUE = 1f;

    [Tooltip("�d�͉����x�@�傫���قǍ~������̂������Ȃ�@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float GRAVITY_NORMAL = -100f;

    [Tooltip("�d�͉����x�@�傫���قǍ~������̂������Ȃ�@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float GRAVITY_PENETRATE = -10f;


    [Tooltip("��̎˒������߂�l�@�˒��������قǑ��x���������Ȃ��@�������I�������V���A���C�Y����")] 
    private float SPEED_TO_RANGE_COEFFICIENT_NORMAL = 1/7f;

    [Tooltip("��̎˒������߂�l�@�˒��������قǑ��x���������Ȃ��@�������I�������V���A���C�Y����")] 
    private float SPEED_TO_RANGE_COEFFICIENT_PENETRATE = 1/100f;

    #region variable 
    #endregion
    #region property
    #endregion
    #region method

        private void Awake()
     {

     }
 
     private void Start ()
     {
        _enchant = GameObject.FindGameObjectWithTag(InhallLibTags.ArrowEnchantmentController).GetComponent<ArrowEnchantment>();
        _timeByCount = _trackingTime / _passValue;
        _reticleRenderer.positionCount = _passValue;
    }

     private void Update ()
     {

     }
        /*  ----------------------------����Ȃ�����---------------------------------------------------
            // ��̊p�x���擾
            _arrowVector = arrowTransform.TransformVector(_forward).normalized;

            // ��̊e�������ւ̈ړ����x���Z�o
            //_arrowSpeed_X = 0;    // �w��
            //_arrowSpeed_Y = 0;    // �x��
            _arrowSpeed_Z = arrowSpeed;    // �y��

            // ���݂̈ړ����x��������
            _nowRange = default;

            // �d�͂ɂ��ړ��ʂ�������
            _addGravity = default;

            // �x���̑��x����~���ʂ̏���l���Z�o
            _maxGravity = TERMINAL_VELOCITY;

            // �ݒ芮��
            _endSetting = true;
         
            // ���x����ő�̈ړ��������Z�o
            _maxRange = arrowSpeed * SPEED_TO_RANGE_COEFFICIENT;

            // ���݂̈ړ����������Z
            _nowRange += _arrowSpeed * Time.deltaTime;

            // �ړ����Ă�������Ɍ�������
            _shadowTransform.rotation = Quaternion.LookRotation(_moveValue.normalized, _forward);

            // �ݒ肪�I����Ă��Ȃ����
            if (!_endSetting)
            {
                // �ݒ�p���\�b�h�����s
                NormalSetting(arrowTransform, _arrowSpeed, isThunder);
            }

         *///------------------------------------------------------------------------------------------

    private void Calculation(float arrowSpeed ,float SPEED_TO_RANGE_COEFFICIENT, float GRAVITY)
    {
        if (_nowCreate)
        {
            _timeToRangeCoefficient = SPEED_TO_RANGE_COEFFICIENT;

            Vector3 _shadowPosition = default ;
            //Quaternion _shadowRotation = default;

            _shadowPosition = _startPosition.position;
            //_shadowRotation = _startPosition.rotation;

            _shadowTransform.position = _shadowPosition;
            //_shadowTransform.rotation = _shadowRotation;


            _shadowVector = _startPosition.TransformVector(_forward).normalized;

            // ��̊e�������ւ̈ړ����x���Z�o
            _shadowSpeed_X = _shadowVector.x * arrowSpeed;    // �w��
            _shadowSpeed_Y = _shadowVector.y * arrowSpeed;    // �x��
            _shadowSpeed_Z = _shadowVector.z * arrowSpeed;    // �y��

            print("X:" +_shadowSpeed_X + ",Y:" + _shadowSpeed_Y+ "Z,:" + _shadowSpeed_Z);

            for (int Counter = 0; Counter < _passValue; Counter++)
            {
                // ���������ւ̈ړ����x�̌��������Z�o
                _nowSpeedValue = STANDARD_SPEED_VALUE - (_timeToRangeCoefficient * _timeByCount * Counter);
                print("SpeedValue  "+_nowSpeedValue);
                // �e�������ւ̈ړ��ʂ��Z�o
                _moveValue.x = (_shadowSpeed_X * _nowSpeedValue);    // �w��
                _moveValue.y = (_shadowSpeed_Y + _addGravity);       // �x��
                _moveValue.z = (_shadowSpeed_Z * _nowSpeedValue);    // �y��

                // �e�������ֈړ��ʂ̕������ړ�
                _shadowTransform.position += _moveValue * Counter;

                // �d�͂ɂ�鉺�����ւ̈ړ��ʂ��Z�o
                _addGravity = GRAVITY * Counter * _timeByCount;


                _reticleRenderer.SetPosition(Counter, _shadowTransform.position);
            }
            print("���e�B�N���̈ʒu�@�F�@" + _reticleRenderer.GetPosition(9) + "    �e�̈ʒu�@�F�@" + _shadowTransform.position + "�@�@���x�@:�@" + arrowSpeed);
        }
    }


    #region �Ăяo����
    public void CreateReticleSystem(float arrowSpeed)
    {
        if (_nowCreate)
        {
            float SPEED_TO_RANGE_COEFFICIENT;
            float GRAVITY;
            switch (_enchant.EnchantmentNowState)
            {
                case EnchantmentEnum.EnchantmentState.bomb:
                    SPEED_TO_RANGE_COEFFICIENT = SPEED_TO_RANGE_COEFFICIENT_NORMAL;
                    GRAVITY = GRAVITY_NORMAL;
                    Calculation(arrowSpeed, SPEED_TO_RANGE_COEFFICIENT_NORMAL, GRAVITY_NORMAL);
                    break;


                case EnchantmentEnum.EnchantmentState.thunder:
                    SPEED_TO_RANGE_COEFFICIENT = SPEED_TO_RANGE_COEFFICIENT_NORMAL;
                    GRAVITY = GRAVITY_NORMAL;
                    Calculation(arrowSpeed, SPEED_TO_RANGE_COEFFICIENT_NORMAL, GRAVITY_NORMAL);
                    break;


                case EnchantmentEnum.EnchantmentState.penetrate:
                    SPEED_TO_RANGE_COEFFICIENT = SPEED_TO_RANGE_COEFFICIENT_PENETRATE;
                    GRAVITY = GRAVITY_PENETRATE;
                    Calculation(arrowSpeed, SPEED_TO_RANGE_COEFFICIENT_PENETRATE, GRAVITY_PENETRATE);
                    break;


                case EnchantmentEnum.EnchantmentState.homing:
                    // �z�[�~���O�͈͕`�悵����
                    break;


                case EnchantmentEnum.EnchantmentState.rapidShots:
                    SPEED_TO_RANGE_COEFFICIENT = SPEED_TO_RANGE_COEFFICIENT_NORMAL;
                    GRAVITY = GRAVITY_NORMAL;
                    Calculation(arrowSpeed, SPEED_TO_RANGE_COEFFICIENT_NORMAL, GRAVITY_NORMAL);
                    break;


                case EnchantmentEnum.EnchantmentState.normal:
                    SPEED_TO_RANGE_COEFFICIENT = SPEED_TO_RANGE_COEFFICIENT_NORMAL;
                    GRAVITY = GRAVITY_NORMAL;
                    Calculation(arrowSpeed, SPEED_TO_RANGE_COEFFICIENT_NORMAL, GRAVITY_NORMAL);
                    break;
            }
        }
    }

    public void StartCreate()
    {
        if (!_nowCreate)
        {
            _reticleRenderer.enabled = true;
            _nowCreate = true;
        }
    }

    public void EndCreate()
    {
        if (_nowCreate)
        {
            _reticleRenderer.enabled = false;
            _nowCreate = false;
        }
    }
    #endregion

    #endregion
}