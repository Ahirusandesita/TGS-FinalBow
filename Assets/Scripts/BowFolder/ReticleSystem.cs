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
        /*  ----------------------------いらない部分---------------------------------------------------
            // 矢の角度を取得
            _arrowVector = arrowTransform.TransformVector(_forward).normalized;

            // 矢の各軸方向への移動速度を算出
            //_arrowSpeed_X = 0;    // Ｘ軸
            //_arrowSpeed_Y = 0;    // Ｙ軸
            _arrowSpeed_Z = arrowSpeed;    // Ｚ軸

            // 現在の移動速度を初期化
            _nowRange = default;

            // 重力により移動量を初期化
            _addGravity = default;

            // Ｙ軸の速度から降下量の上限値を算出
            _maxGravity = TERMINAL_VELOCITY;

            // 設定完了
            _endSetting = true;
         
            // 速度から最大の移動距離を算出
            _maxRange = arrowSpeed * SPEED_TO_RANGE_COEFFICIENT;

            // 現在の移動距離を加算
            _nowRange += _arrowSpeed * Time.deltaTime;

            // 移動している方向に向かせる
            _shadowTransform.rotation = Quaternion.LookRotation(_moveValue.normalized, _forward);

            // 設定が終わっていなければ
            if (!_endSetting)
            {
                // 設定用メソッドを実行
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

            // 矢の各軸方向への移動速度を算出
            _shadowSpeed_X = _shadowVector.x * arrowSpeed;    // Ｘ軸
            _shadowSpeed_Y = _shadowVector.y * arrowSpeed;    // Ｙ軸
            _shadowSpeed_Z = _shadowVector.z * arrowSpeed;    // Ｚ軸

            print("X:" +_shadowSpeed_X + ",Y:" + _shadowSpeed_Y+ "Z,:" + _shadowSpeed_Z);

            for (int Counter = 0; Counter < _passValue; Counter++)
            {
                // 水平方向への移動速度の減衰率を算出
                _nowSpeedValue = STANDARD_SPEED_VALUE - (_timeToRangeCoefficient * _timeByCount * Counter);
                print("SpeedValue  "+_nowSpeedValue);
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
            print("レティクルの位置　：　" + _reticleRenderer.GetPosition(9) + "    影の位置　：　" + _shadowTransform.position + "　　速度　:　" + arrowSpeed);
        }
    }


    #region 呼び出し部
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
                    // ホーミング範囲描画したい
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