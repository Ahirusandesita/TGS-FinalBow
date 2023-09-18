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

    [Tooltip("重力加速度　大きいほど降下するのが早くなる　調整が終わったらシリアライズ消す"), SerializeField] //デバッグ用
    private float GRAVITY_NORMAL = -100f;

    [Tooltip("重力加速度　大きいほど降下するのが早くなる　調整が終わったらシリアライズ消す"), SerializeField] //デバッグ用
    private float GRAVITY_PENETRATE = -10f;


    [Tooltip("矢の射程を決める値　射程が長いほど速度減衰が少ない　調整が終わったらシリアライズ消す")] 
    private float SPEED_TO_RANGE_COEFFICIENT_NORMAL = 1/7f;

    [Tooltip("矢の射程を決める値　射程が長いほど速度減衰が少ない　調整が終わったらシリアライズ消す")] 
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

            // 矢の各軸方向への移動速度を算出
            _shadowSpeed_X = _shadowVector.x * arrowSpeed;    // Ｘ軸
            _shadowSpeed_Y = _shadowVector.y * arrowSpeed;    // Ｙ軸
            _shadowSpeed_Z = _shadowVector.z * arrowSpeed;    // Ｚ軸

            for (int Counter = 0; Counter < _passValue; Counter++)
            {
                // 水平方向への移動速度の減衰率を算出
                _nowSpeedValue = STANDARD_SPEED_VALUE - (_timeToRangeCoefficient * _timeByCount * Counter);
                // 各軸方向への移動量を算出
                _moveValue.x = (_shadowSpeed_X * _nowSpeedValue);    // Ｘ軸
                _moveValue.y = (_shadowSpeed_Y + _addGravity);       // Ｙ軸
                _moveValue.z = (_shadowSpeed_Z * _nowSpeedValue);    // Ｚ軸

                // 各軸方向へ移動量の分だけ移動
                _shadowTransform.position += _moveValue * Counter;

                // 重力による下方向への移動量を算出
                _addGravity = GRAVITY * Counter * _timeByCount;

                _reticleRenderer.SetPosition(Counter, _shadowTransform.position);
            }
        }
    }

    #region 呼び出し部
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
                        // ホーミング範囲描画したい
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