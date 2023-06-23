// --------------------------------------------------------- 
// ArrowMove.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nishigaki
// --------------------------------------------------------- 
using UnityEngine;
using Nekoslibrary;

interface IArrowMoveSetting
{
    float SetArrowSpeed { set; }
}
interface IArrowMoveReset
{
    void ResetsStart();
}
interface IArrowMoveSettingReset : IArrowMoveSetting, IArrowMoveReset
{

}


/// <summary>
/// 矢の動きのクラス　通常とホーミングの挙動を行う
/// </summary>
public class ArrowMove : MonoBehaviour, IArrowMoveSettingReset
{

    #region 変数 グロ注意

    #region 共用変数

    // 初期の角度　代入フラグが立っていたら代入する
    private Vector3 _firstAngle = default;

    // 代入フラグ　一回代入したらtrueにする
    private bool _isSetAngle = false;

    // 矢の初速　前方に進んでいく力の大きさ
    // SetArrowSpeedで速度を指定できる
    private float _arrowSpeed = 10f;


    // 定数

    // ゼロ　ただのゼロ　普通にゼロ　なおfloat ニュース番組ではない
    private const float ZERO = 0f;

    #endregion

    #region ノーマルで使用している変数

    // 矢の速度の計算上必要な仮想速度　
    // 横軸の速度のみを想定的に計算するのに使う
    // 計算式：　imageSpeed - (時間毎のAIR_RESISTによる減衰値)
    private float _imageSpeed = default;

    // 慣性降下率　速度に応じて変化する下方向にかかる力の倍率
    // 計算式：　FALL_MAX_VALUE －（現在速度÷FALLSPEEDLIMIT）　　　
    // （ FALL_MAX_VALUE ≧ fallValue ≧ ０ ）
    private float _fallValue = default;

    // 真下を向くために必要な補間値
    // fallValueとかけ合わせることで時間ごとの角度変化を求められる
    // firstAngleとFALL_MAX_VALUEの差
    private float _fallAngle = default;

    // 真下を向く角度　Ｘが正数か負数かによって変化させる
    private float _underVecter = default;

    // 降下上限速度　降下し始める上限値
    // この速度を超えている間は降下しないでまっすぐ飛ぶ
    // 仮想速度が０になったら真下方向に進む
    // 簡易法線計算によく使われる計算方法
    private float _fallStartSpeed = default;

    // 現在角度　ショボ撃ちの微妙感を減らすために使用
    private float _nowAngle = default;

    // ショボ撃ち補正速度の加算値　移動処理の_arrowSpeedに加算する                                                                           いも食べたい
    private float _addFallSpeed = default;



    // 定数

    // 慣性降下率の最大値　１　いち　one　正直いらない
    private const float FALL_MAX_VALUE = 1f;

    // 空気抵抗率　速度減衰の大小を決める値　０で無抵抗 １で毎秒１f減衰
    private const float AIR_RESIST = 5f;

    // 真下を向く角度　横移動しない Ｘが正数の時に使用
    private const float UNDER_VECTOR_POS = 90f;

    // 真下を向く角度　横移動しない Ｘが負数の時に使用
    private const float UNDER_VECTOR_NEG = 450f;

    // ショボ撃ち補正速度　この速度より遅い場合は加速させる
    private const float ADD_FALL_SPEED_MAX = 100f;

    // ショボ撃ち補正開始の角度
    private const float ADD_START_ANGLE = 90f;

    // ショボ撃ち補正終了の角度　Lerpの上限にも使う
    private const float ADD_FINISH_ANGLE = 180f;

    #endregion

    #region ホーミングで使用している変数

    // ホーミングするターゲット
    private GameObject _target = default;

    // ターゲットへ向くためのベクトル
    private Vector3 _lookVect = default;

    // 最終的に向く方向
    private Quaternion _lookRot = default;

    // ターゲットの方向を見る速度
    private float _lookSpeed = default;

    // もし判定内に敵がいなかった時の直進用ターゲット
    private GameObject _tmpTarget = default;

    // 定数

    // 向きの速度係数　色々試してみてね
    private const float SPEED_COEF = 0.00074f;

    // ターゲットを探す円錐の中心角　現在は３６０度すべてサーチする　値は中心角の半分を入れる
    private const float SEARCH_ANGLE = 180f;

    // Vector.forward　処理軽減のためにあらかじめ代入しておく
    private Vector3 _forward = Vector3.forward;

    #endregion

    #endregion

    public void ArrowMove_Nomal(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, true); }





    public void ArrowMove_Bomb(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_Thunder(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_KnockBack(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_Homing(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_Penetrate(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_BombThunder(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_BombKnockBack(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_BombHoming(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_BombPenetrate(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_ThunderKnockBack(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_ThunderHoming(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_ThunderPenetrate(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_KnockBackHoming(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_KnockBackPenetrate(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_HomingPenetrate(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }


    /// <summary>
    /// 通常の矢の挙動をさせるメソッド
    /// </summary>
    /// <param name="arrowTransform">矢のトランスフォーム</param>
    /// <param name="arrowSpeed">矢が飛んでいく速度</param>
    private void NormalMove(Transform arrowTransform, float arrowSpeed)
    {
        #region 普通の飛び方
        // 初期角度の代入
        if (!_isSetAngle)
        {
            // 初動の角度をワールド座標の数値で代入　色々使う
            _firstAngle = new Vector3(arrowTransform.eulerAngles.x, arrowTransform.eulerAngles.y, arrowTransform.eulerAngles.z);

            // フラグの設定　一回代入したら今後代入しないように変更
            _isSetAngle = !_isSetAngle;

            // 角度補正の補間値計算
            // 初期角度が９０を超えているかどうか判定　それによって_underVecterが変化する
            if (_firstAngle.x > 90)
            {
                // 初期角度が９０を超えている場合の値を代入
                _underVecter = UNDER_VECTOR_NEG;
            }
            else
            {
                // 初期角度が９０を超えていない場合の値を代入
                _underVecter = UNDER_VECTOR_POS;
            }

            // 傾ける必要のある角度そのものの代入
            _fallAngle = _underVecter - _firstAngle.x;

            // 矢の速度を設定する
            _imageSpeed = arrowSpeed;
            _fallStartSpeed = _imageSpeed * 0.8f;
        }

        // 矢の移動
        arrowTransform.Translate(ZERO,                                           // Ｘ軸
                                    ZERO,                                           // Ｙ軸
                                    (arrowSpeed + _addFallSpeed) * Time.deltaTime,  // Ｚ軸
                                    Space.Self);                                    // ローカル座標で指定　矢先はＺ軸に向いている前提

        // 速度減衰
        _imageSpeed = MathN.Clamp_min(_imageSpeed - (AIR_RESIST * Time.deltaTime), ZERO);

        // 慣性降下率計算
        _fallValue = MathN.Clamp(FALL_MAX_VALUE - (_imageSpeed / _fallStartSpeed), ZERO, FALL_MAX_VALUE);

        // 角度補正
        // 現在角度設定
        _nowAngle = _firstAngle.x + (_fallAngle * _fallValue);
        // 角度代入
        arrowTransform.rotation = Quaternion.Euler(_nowAngle,
                                                   _firstAngle.y,
                                                   _firstAngle.z);
        // ショボ撃ち補正
        if (arrowSpeed < ADD_FALL_SPEED_MAX && ADD_START_ANGLE <= _nowAngle && _nowAngle <= ADD_FINISH_ANGLE)
        {
            _addFallSpeed = Mathf.Lerp(arrowSpeed, ADD_FALL_SPEED_MAX, _nowAngle / ADD_FINISH_ANGLE) - arrowSpeed;
        }


        #endregion
    }

    /// <summary>
    /// ホーミングの挙動をさせるメソッド
    /// </summary>
    /// <param name="arrowTransform">矢のトランスフォーム</param>
    /// <param name="arrowSpeed">矢が飛んでいくスピード</param>
    private void HomingMove(Transform arrowTransform, float arrowSpeed)
    {
        #region ホーミングの飛び方
        // 初期角度の代入　ターゲットがなくなった場合は再度代入
        if (_target == null || _target == default)
        {
            // 初期設定とターゲットの選定
            SetHomingTarget(arrowTransform, arrowSpeed);
        }


        // ターゲットへのベクトルを取得
        _lookVect = _target.transform.position - arrowTransform.position;
        // 最終角度を設定
        _lookRot = Quaternion.LookRotation(_lookVect);
        // オブジェクトのrotationを変更
        arrowTransform.rotation = Quaternion.Slerp(arrowTransform.rotation, _lookRot, _lookSpeed);

        // 矢の移動
        arrowTransform.Translate(ZERO,                               // Ｘ軸
                                    ZERO,                               // Ｙ軸
                                    arrowSpeed * Time.deltaTime,        // Ｚ軸
                                    Space.Self);                        // ローカル座標で指定　矢先はZ軸に向いている前提
        #endregion
    }

    /// <summary>
    /// ホーミングの初期設定とターゲットの選定を行うメソッド
    /// </summary>
    /// <param name="arrowTransform"></param>
    /// <param name="arrowSpeed"></param>
    private void SetHomingTarget(Transform arrowTransform, float arrowSpeed)
    {
        // もろもろの初期化処理とターゲット再選定
        // 初動の角度をワールド座標の数値で代入　色々使う
        _firstAngle = new Vector3(arrowTransform.eulerAngles.x, arrowTransform.eulerAngles.y, arrowTransform.eulerAngles.z);

        // フラグの設定　一回代入したら今後代入しないように変更
        _isSetAngle = !_isSetAngle;

        // 追尾性能を速度によって差ができないように設定
        _lookSpeed = SPEED_COEF * arrowSpeed;

        // ターゲットを探索して代入
        _target = ConeDecision.ConeSearchNearest(arrowTransform, AttractObjectList.GetAttractObject(), SEARCH_ANGLE);


        // 例外処理　もし判定内にオブジェクトが一つもなかった場合にnullRefを回避するための処理
        if (_target == null || _target == default)
        {
            // まっすぐ飛んで行くようにターゲットを代入
            _target = _tmpTarget;
        }
    }

    #region NormalSetting,NormalMove2で使用している変数
    private Vector3 _arrowVector = default;
    private float _arrowSpeed_Horizontal = default;
    private float _arrowSpeed_X = default;
    private float _arrowSpeed_Y = default;
    private float _arrowSpeed_Z = default;
    private float _maxRange = default;
    private bool _endSetting = false;

    [Tooltip("矢の射程を決める値　射程が長いほど速度減衰が少ない"),SerializeField] //デバッグ用
    private float SPEED_TO_RANGE_COEFFICIENT_NORMAL = 15f;

    [Tooltip("矢の射程を決める値　射程が長いほど速度減衰が少ない"), SerializeField] //デバッグ用
    private float SPEED_TO_RANGE_COEFFICIENT_THUNDER = 30f;

    private float _nowRange = default;
    private Vector3 _moveValue = default;
    private float _nowSpeedValue = default;
    private float _addGravity = default;

    [Tooltip("重力加速度　大きいほど降下するのが早くなる"), SerializeField] //デバッグ用
    private float GRAVITY = -50f;

    private const float STANDARD_SPEED_VALUE = 1f;

    /// <summary>
    /// 
    /// </summary>
    [Tooltip("最大落下速度　落下速度が加速する頂点　これより速くは落下しない"), SerializeField] //デバッグ用
    private float TERMINAL_VELOCITY = 100f;
    #endregion


    /// <summary>
    /// ノーマルアロー開始時の設定メソッド
    /// </summary>
    private void NormalSetting(Transform arrowTransform, float arrowSpeed, bool isThunder)
    {
        _arrowVector = arrowTransform.TransformVector(_forward).normalized;
        _arrowSpeed_Horizontal = Mathf.Sqrt(MathN.Pow(_arrowVector.x) + MathN.Pow(_arrowVector.z)) * arrowSpeed;
        _arrowSpeed_X = _arrowVector.x * arrowSpeed;
        _arrowSpeed_Y = _arrowVector.y * arrowSpeed;
        _arrowSpeed_Z = _arrowVector.z * arrowSpeed;
        _maxRange = _arrowSpeed_Horizontal * SPEED_TO_RANGE_COEFFICIENT_NORMAL;

        _nowRange = default;
        _addGravity = default;

        _endSetting = true;
    }

    private void NormalMove2(Transform arrowTransform, float arrowSpeed, bool isThunder)
    {
        if (!_endSetting)
        {
            NormalSetting(arrowTransform, arrowSpeed, isThunder);
        }

        _nowSpeedValue = MathN.Clamp_min( STANDARD_SPEED_VALUE - (_nowRange / _maxRange), ZERO );

        _moveValue.x = (_arrowSpeed_X * _nowSpeedValue);
        _moveValue.y = (_arrowSpeed_Y + _addGravity);
        _moveValue.z = (_arrowSpeed_Z * _nowSpeedValue);

        arrowTransform.position +=  _moveValue * Time.deltaTime ;
        arrowTransform.rotation = Quaternion.LookRotation(_moveValue.normalized, _forward);

        _nowRange += _arrowSpeed_Horizontal * Time.deltaTime;
        _addGravity = MathN.Clamp_max( _addGravity + GRAVITY * Time.deltaTime, TERMINAL_VELOCITY + _arrowSpeed_Y);
    }

    /// <summary>
    /// 矢の速度を指定するプロパティ　setしかないため注意
    /// </summary>
    public float SetArrowSpeed
    {
        set
        {
            _arrowSpeed = value / 10;
        }
    }

    /// <summary>
    /// リスタート設定用プロパティ
    /// </summary>
    public void ResetsStart()
    {
        _isSetAngle = false;
        _endSetting = false;
    }

}
