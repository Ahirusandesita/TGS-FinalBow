// --------------------------------------------------------- 
// ArrowMove.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nishigaki
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;



#region インターフェース

/// <summary>
/// 矢の挙動の設定のために必要なインターフェース
/// </summary>
interface IArrowMoveSetting
{
    /// <summary>
    /// 矢の速度を指定するプロパティ　setしかないため注意
    /// </summary>
    float SetArrowSpeed { set; }
}
/// <summary>
/// 矢の挙動のリセットのために必要なインターフェース
/// </summary>
interface IArrowMoveReset
{
    /// <summary>
    /// 矢の挙動のリセット処理メソッド
    /// </summary>
    void ResetsStart();
}

/// <summary>
/// ArrowMoveに必要なインターフェース
/// </summary>
interface IArrowMoveSettingReset : IArrowMoveSetting, IArrowMoveReset
{

}

#endregion


/// <summary>
/// 矢の動きのクラス　通常とホーミングの挙動を行う
/// </summary>
public class ArrowMove : MonoBehaviour, IArrowMoveSettingReset,IArrowEnchantable<Transform>
{
    #region 変数一覧 グロ注意

    #region 共用変数

    // 代入フラグ　一回代入したらtrueにする
    private bool _endSetting = false;

    // 矢の初速　前方に進んでいく力の大きさ
    private float _arrowSpeed = 10f;

    // Vector.forward　処理軽減のためにあらかじめ代入しておく
    private Vector3 _forward = Vector3.forward;


    /***  ここから下　定数  ***/

    // ゼロ　ただのゼロ　普通にゼロ　なおfloat ニュース番組ではない
    private const float ZERO = 0f;

    // 貫通フラグ　貫通の時は true
    private const bool PENETRATE = true;

    // 貫通フラグ　貫通以外の時は false
    private const bool NOT_PENETRATE = false;

    // 無限　上限なしのクランプ等に使用
    private const float INFINITY = Mathf.Infinity;
    #endregion

    #region ノーマルで使用している変数

    // 矢が向いている方向ベクトル
    private Vector3 _arrowVector = default;

    // 矢の移動量　各軸方向ごとの移動量を代入する
    private Vector3 _moveValue = default;

    // 矢のＸ軸方向への移動速度
    private float _arrowSpeed_X = default;

    // 矢のＹ軸方向への移動速度
    private float _arrowSpeed_Y = default;

    // 矢のＺ軸方向への移動速度
    private float _arrowSpeed_Z = default;

    // 現在の水平方向への移動速度の割合　どのぐらい速度減衰しているか
    private float _nowSpeedValue = default;

    // 矢の種類による重力量
    private float _gravity = default;

    // 矢の落下速度の加算値　_arrowSpeed_Y に加算する
    private float _addGravity = default;

    // 矢の落下速度の加算値の上限
    private float _maxGravity = default;

    // 矢の種類による速度減衰量
    private float _attenuation = default;

    // 矢の飛行時間
    private float _flightTime = default;



    /***  ここから下　定数  ***/

    // 通常時の重力加速度　絶対値が大きいほど降下するのが早くなる
    [Tooltip("重力加速度　大きいほど降下するのが早くなる　調整が終わったらシリアライズ消す"), SerializeField] //デバッグ用
    private float GRAVITY_NORMAL = -100f;

    // サンダーの時の重力加速度　絶対値が大きいほど降下するのが早くなる
    [Tooltip("重力加速度　大きいほど降下するのが早くなる　調整が終わったらシリアライズ消す"), SerializeField] //デバッグ用
    private float GRAVITY_THUNDER = -10f;

    // 通常時の射程倍率　射程が長いほど速度減衰が少ない
    [Tooltip("矢の射程を決める値　射程が長いほど速度減衰が少ない　調整が終わったらシリアライズ消す"), SerializeField] //デバッグ用
    private float NORMAL_ATTENUATION = 0.14286f;

    // サンダーの時の射程倍率　射程が長いほど速度減衰が少ない
    [Tooltip("矢の射程を決める値　射程が長いほど速度減衰が少ない　調整が終わったらシリアライズ消す"), SerializeField] //デバッグ用
    private float PENETRATE_ATTENUATION = 0.01f;

    // 速度減衰の元値　現在速度 = STANDARD_SPEED_VALUE - 減衰率
    private const float STANDARD_SPEED_VALUE = 1f;

    // 速度減衰の最大値
    private const float MAXIMUM_SPEED_VALUE = 1f;

    // 最大落下速度　落下速度が加速する頂点　これより速くは落下しない
    [Tooltip("最大落下速度　落下速度が加速する頂点　これより速くは落下しない　調整が終わったらシリアライズ消す"), SerializeField] //デバッグ用
    private float TERMINAL_VELOCITY = -1500f;

    // ワープ後の矢の初期速度
    [SerializeField, Tooltip("ワープ後の矢の初期速度")]
    private float AFTER_WARP_SPEED = 100f;

    #endregion

    #region ホーミングで使用している変数

    // ホーミングするターゲット
    private Transform _target = default;

    // ターゲットへ向くためのベクトル
    private Vector3 _lookVect = default;

    // 最終的に向く方向
    private Quaternion _lookRot = default;

    // ターゲットの方向を見る速度
    private float _lookSpeed = default;

    // 時間経過によって増加する方向の調整速度の係数
    private float _lookSpeedCoefficient = LOOKSPEED_DEFAULT;

    // LockOnに使うクラス
    private LockOnSystem _lockOnSystem = default;



    /***  ここから下　定数  ***/

    // 向きの速度係数　色々試してみてね
    private const float SPEED_COEF = 0.00074f;

    // ターゲットを探す円錐の中心角　現在は３６０度すべてサーチする　値は中心角の半分を入れる
    private const int SEARCH_ANGLE = 1;

    // _lookSpeedCoefficientの初期値
    private const float LOOKSPEED_DEFAULT = 1f;

    // _lookSpeedCoefficientの増加係数
    private const float LOOKSPEED_ADDVALUE = 2f;

    // _lookSpeedCoefficientの最大値
    private const float LOOKSPEED_MAX = 4f;

    #endregion

    #endregion

    #region プロパティ

    /// <summary>
    /// 空気抵抗の設定プロパティ　通常か貫通で変化
    /// </summary>
    /// <param name="isPenetrate">貫通フラグ</param>
    /// <returns></returns>
    private float Attenuation(bool isPenetrate)
    {
        // 貫通かどうか判定
        if (isPenetrate)
        {
            // 貫通の空気抵抗を返す
            return PENETRATE_ATTENUATION;
        }
        else
        {
            // 通常の空気抵抗を返す
            return NORMAL_ATTENUATION;
        }
    }

    /// <summary>
    /// 重力加速度の設定プロパティ　通常か貫通で変化
    /// </summary>
    /// <param name="isPenetrate">貫通フラグ</param>
    /// <returns></returns>
    private float GravityValue(bool isPenetrate)
    {
        // 貫通かどうか判定
        if (isPenetrate)
        {
            // 貫通の重力加速度を返す
            return GRAVITY_THUNDER;
        }
        else
        {
            // 通常の重力加速度を返す
            return GRAVITY_NORMAL;
        }
    }

    /// <summary>
    /// 矢の速度を指定するプロパティ　setしかないため注意
    /// </summary>
    public float SetArrowSpeed
    {
        set
        {
            // 矢の速度を設定
            _arrowSpeed = value;
        }
    }

    #endregion

    #region メソッド

    #region イベント設定用メソッド
    // イベントの設定を行っているかどうか判定
    private bool _isSet = false;

    // 矢の挙動を管理するデリゲート
    private delegate void MovementDelegate(Transform arrowTransform, bool isThunder);

    // 矢の挙動を登録するイベント
    MovementDelegate movement;

    /// <summary>
    /// 通常の矢の挙動イベントを登録するメソッド
    /// </summary>
    private void SetNormal()
    {
        movement = NormalMove;
        _isSet = true;
    }

    /// <summary>
    /// ホーミングの矢の挙動イベントを登録するメソッド
    /// </summary>
    private void SetHoming()
    {
        movement = HomingMove;
        _isSet = true;
    }

    #endregion

    #region ノーマルの挙動
    /// <summary>
    /// ノーマルの挙動をさせるメソッド
    /// </summary>
    /// <param name="arrowTransform">矢のトランスフォーム</param>
    /// <param name="isPenetrate">貫通かそれ以外か</param>
    private void NormalMove(Transform arrowTransform, bool isPenetrate)
    {
        // 設定が終わっていなければ
        if (!_endSetting)
        {
            // 設定用メソッドを実行
            NormalSetting(arrowTransform, _arrowSpeed, isPenetrate);
        }

        // 設定が終わっていたら
        else
        {
            // 水平方向への移動速度の減衰率を算出
            _nowSpeedValue = Mathf.Clamp(STANDARD_SPEED_VALUE - (_flightTime * _attenuation), ZERO , MAXIMUM_SPEED_VALUE);

            // 各軸方向への移動量を算出
            _moveValue.x = (_arrowSpeed_X * _nowSpeedValue);    // Ｘ軸
            _moveValue.y = (_arrowSpeed_Y + _addGravity);       // Ｙ軸
            _moveValue.z = (_arrowSpeed_Z * _nowSpeedValue);    // Ｚ軸

            // 各軸方向へ移動量の分だけ移動
            arrowTransform.position += _moveValue * Time.deltaTime;

            // 移動している方向に向かせる
            arrowTransform.rotation = Quaternion.LookRotation(_moveValue.normalized, _forward);

            // 現在の移動距離を加算
            _flightTime += Time.deltaTime;

            // 重力による下方向への移動量を算出
            _addGravity = Mathf.Clamp(_addGravity + _gravity * Time.deltaTime, _maxGravity , INFINITY);
        }
    }

    /// <summary>
    /// ノーマルの挙動のための初期設定を行うメソッド
    /// </summary>
    /// <param name="arrowTransform">矢のトランスフォーム</param>
    /// <param name="arrowSpeed">矢が飛んでいくスピード</param>
    /// <param name="isPenetrate">貫通かそれ以外か</param>
    private void NormalSetting(Transform arrowTransform, float arrowSpeed, bool isPenetrate)
    {
        // 矢の角度を取得
        _arrowVector = arrowTransform.TransformVector(_forward).normalized;

        // 矢の各軸方向への移動速度を算出
        _arrowSpeed_X = _arrowVector.x * arrowSpeed;    // Ｘ軸
        _arrowSpeed_Y = _arrowVector.y * arrowSpeed;    // Ｙ軸
        _arrowSpeed_Z = _arrowVector.z * arrowSpeed;    // Ｚ軸

        // 矢の速度減衰量を設定
        _attenuation = Attenuation(isPenetrate);

        // 矢の重力量を設定
        _gravity = GravityValue(isPenetrate);

        // Ｙ軸の速度から降下量の上限値を算出
        _maxGravity = Mathf.Clamp(TERMINAL_VELOCITY - _arrowSpeed_Y, -INFINITY ,ZERO);

        // 矢の飛行時間の初期化
        _flightTime = default;

        // 重力により移動量を初期化
        _addGravity = default;

        // 設定完了
        _endSetting = true;
    }

    /// <summary>
    /// 通常の矢の挙動の設定を初期化・再設定するメソッド
    /// </summary>
    public void ReSetNormalSetting()
    {
        NormalSetting(this.transform, AFTER_WARP_SPEED, false);
    }
    #endregion

    #region ホーミングの挙動

    /// <summary>
    /// ホーミングの挙動をさせるメソッド
    /// </summary>
    /// <param name="arrowTransform">矢のトランスフォーム</param>
    /// <param name="isPenetrate">貫通かそれ以外か</param>
    private void HomingMove(Transform arrowTransform, bool isPenetrate)
    {
        // 初期設定
        if (!_endSetting)
        {
            HomingSetting(arrowTransform , _arrowSpeed);
        }

        // 補正を時間とともに増加
        if(_lookSpeedCoefficient < LOOKSPEED_MAX)
        {
            _lookSpeedCoefficient += LOOKSPEED_ADDVALUE * Time.deltaTime;
        }

        // ターゲットへのベクトルを取得
        _lookVect = _target.position - arrowTransform.position;

        // 最終角度を設定
        _lookRot = Quaternion.LookRotation(_lookVect);

        // オブジェクトのrotationを変更
        arrowTransform.rotation = Quaternion.Slerp(arrowTransform.rotation, _lookRot, _lookSpeed * _lookSpeedCoefficient);

        // 矢の移動
        arrowTransform.Translate( ZERO,                               // Ｘ軸
                                  ZERO,                               // Ｙ軸
                                  _arrowSpeed * Time.deltaTime,        // Ｚ軸
                                  Space.Self);                        // ローカルで指定　矢先はＺ軸

        // ターゲットが壊れたら挙動変更
        if (_target.gameObject.activeSelf == false)
        {
            _endSetting = false;
            SetNormal();
        }
    }

    /// <summary>
    /// ホーミングの初期設定とターゲットの選定を行うメソッド
    /// </summary>
    /// <param name="arrowTransform">矢のトランスフォーム</param>
    /// <param name="arrowSpeed">矢が飛んでいくスピード</param>
    private void HomingSetting(Transform arrowTransform, float arrowSpeed)
    {
        // フラグの設定　一回代入したら今後代入しないように変更
        _endSetting = true;

        // 追尾性能を速度によって差ができないように設定
        _lookSpeed = SPEED_COEF * arrowSpeed;

        // 例外処理　もし判定内にオブジェクトが一つもなかった場合にnullRefを回避するための処理
        if (_target == null || _target == default)
        {
            // まっすぐ飛んで行くように変更
            SetNormal();
            _endSetting = false;
        }
    }

    #endregion

    #region　共用メソッド
    /// <summary>
    /// ロックオン用のクラスを登録
    /// </summary>
    private void Start()
    {
        // LockOnSystemの代入
        _lockOnSystem = GameObject.FindGameObjectWithTag(InhallLibTags.BowController).GetComponent<LockOnSystem>();
    }

    /// <summary>
    /// 矢の挙動のリセット処理メソッド
    /// </summary>
    public void ResetsStart()
    {
        // 矢の初期設定完了のフラグをリセット
        _endSetting = false;
        _isSet = false;

        // 一部変数の初期化
        _lookSpeedCoefficient = LOOKSPEED_DEFAULT;
    }

    public void Normal(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void Bomb(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void Thunder(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void KnockBack(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void Penetrate(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void Homing(Transform t)
    {
        if (!_isSet)
        {
            if(_lockOnSystem.LockOnTarget == null)
            {
                SetNormal();
            }
            else
            {
                _target = _lockOnSystem.TargetSet(this);
                SetHoming();
            }

        }
        movement(t, NOT_PENETRATE);
    }

    public void BombThunder(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, PENETRATE);
    }

    public void BombKnockBack(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void BombPenetrate(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void BombHoming(Transform t)
    {
        if (!_isSet)
        {
            SetHoming();
        }
        movement(t, NOT_PENETRATE);
    }

    public void ThunderKnockBack(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, PENETRATE);
    }

    public void ThunderPenetrate(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, PENETRATE);
    }

    public void ThunderHoming(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, PENETRATE);
    }

    public void KnockBackPenetrate(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void KnockBackHoming(Transform t)
    {
        if (!_isSet)
        {
            SetHoming();
        }
        movement(t, NOT_PENETRATE);
    }

    public void PenetrateHoming(Transform t)
    {
        if (!_isSet)
        {
            SetHoming();
        }
        movement(t, NOT_PENETRATE);
    }
    #endregion

    #endregion
}
