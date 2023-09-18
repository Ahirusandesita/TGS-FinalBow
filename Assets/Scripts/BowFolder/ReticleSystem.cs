// --------------------------------------------------------- 
// ReticleSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

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

    [SerializeField]
    private bool _UseReticle = true;


    private Vector3 _shadowVector;
    private float _shadowSpeed_X;
    private float _shadowSpeed_Y;
    private float _shadowSpeed_Z;
    private float _addGravity;
    private Vector3 _forward = Vector3.forward;
    private float _nowSpeedValue;
    private Vector3 _moveValue;
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
     private void Start ()
     {
        _enchant = GameObject.FindGameObjectWithTag(InhallLibTags.ArrowEnchantmentController).GetComponent<ArrowEnchantment>();
        _timeByCount = _trackingTime / _passValue;
        _reticleRenderer.positionCount = _passValue;
    }

    private void Calculation(float arrowSpeed ,float SPEED_TO_RANGE_COEFFICIENT, float GRAVITY)
    {
        if (_nowCreate)
        {
            _timeToRangeCoefficient = SPEED_TO_RANGE_COEFFICIENT;

            Vector3 _shadowPosition = default ;
            _shadowPosition = _startPosition.position;
            _shadowTransform.position = _shadowPosition;

            _shadowVector = _startPosition.TransformVector(_forward).normalized;

            // ��̊e�������ւ̈ړ����x���Z�o
            _shadowSpeed_X = _shadowVector.x * arrowSpeed;    // �w��
            _shadowSpeed_Y = _shadowVector.y * arrowSpeed;    // �x��
            _shadowSpeed_Z = _shadowVector.z * arrowSpeed;    // �y��

            for (int Counter = 0; Counter < _passValue; Counter++)
            {
                // ���������ւ̈ړ����x�̌��������Z�o
                _nowSpeedValue = STANDARD_SPEED_VALUE - (_timeToRangeCoefficient * _timeByCount * Counter);
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
        }
    }

    #region �Ăяo����
    public void CreateReticleSystem(float arrowSpeed)
    {
        if (_UseReticle)
        {
            if (!_nowCreate && _enchant.EnchantmentNowState != EnchantmentEnum.EnchantmentState.homing)
            {
                _reticleRenderer.enabled = true;
                _nowCreate = true;
            }
            else
            {
                switch (_enchant.EnchantmentNowState)
                {
                    case EnchantmentEnum.EnchantmentState.bomb:
                        Calculation(arrowSpeed, SPEED_TO_RANGE_COEFFICIENT_NORMAL, GRAVITY_NORMAL);
                        break;


                    case EnchantmentEnum.EnchantmentState.thunder:
                        Calculation(arrowSpeed, SPEED_TO_RANGE_COEFFICIENT_NORMAL, GRAVITY_NORMAL);
                        break;


                    case EnchantmentEnum.EnchantmentState.penetrate:
                        Calculation(arrowSpeed, SPEED_TO_RANGE_COEFFICIENT_PENETRATE, GRAVITY_PENETRATE);
                        break;


                    case EnchantmentEnum.EnchantmentState.homing:
                        EndCreate();
                        // �z�[�~���O�͈͕`�悵����
                        break;


                    case EnchantmentEnum.EnchantmentState.rapidShots:
                        Calculation(arrowSpeed, SPEED_TO_RANGE_COEFFICIENT_NORMAL, GRAVITY_NORMAL);
                        break;


                    case EnchantmentEnum.EnchantmentState.normal:
                        Calculation(arrowSpeed, SPEED_TO_RANGE_COEFFICIENT_NORMAL, GRAVITY_NORMAL);
                        break;
                }
            }
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