// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using System;
using UnityEngine;

interface IFBowManagerGetDistance
{
    /// <summary>
    /// 現在の弓を引いた距離が返される
    /// </summary>
    public float GetDrawDistance();
}



[RequireComponent(typeof(BowDraw), typeof(BowHold), typeof(BowShotAim))]
[RequireComponent(typeof(BowVibe), typeof(AttractEffectCustom), typeof(AttractZone))]
[RequireComponent(typeof(Inhall), typeof(BowSE))]
public class Old_BowManager : MonoBehaviour, IFBowManagerGetDistance, IFBowManagerQue,IFBowManager_GetStats
{

    #region かつてpublicだった変数
    [SerializeField] TagObject _InputTagName = default;

    [SerializeField] TagObject _poolTagName = default;

    [SerializeField] TagObject _playerManagerTagName = default;

    [SerializeField] Transform _handLeftPosition = default;

    [SerializeField] Transform _handRightPosition = default;
    /// <summary>
    /// 引くオブジェクト
    /// </summary>
    [SerializeField] GameObject _drawObject = default;

    [SerializeField] Transform _changeHandObjectTransform = default;

    /// <summary>
    /// Debugモード
    /// </summary>
    public bool _mouseMode = false;

    /// <summary>
    /// VRで値追うモード
    /// </summary>
    public bool _traceValue = false;

    #endregion

    #region クラス、構造体

    IFBowDraw _draw = default;

    IFBowHold _hold = default;

    IFBowShotAim _aim = default;

    IFBowVibe _vibe = default;

    IFAttractEffectCustom _inhallCustom = default;

    AttractZone _attract = default;

    private InputManagement _mng = default;

    private ObjectPoolSystem _poolSystem = default;

    private BowSE _bowSE = default;
    //デバック用
    private PlayerManager _playerManager = default;
    //デバッグ
    private VR_Trace_Value _trace = default;

    delegate bool HandUseDelegate();

    delegate Vector3 HandPositionDelegate();

    delegate void VibeEndDelegate();



    /// <summary>
    /// 手の状態管理型
    /// </summary>
    enum HandStats
    {
        None,
        Hold,
    }

    HandUseDelegate _handTriggerDownDelegate = default;

    HandUseDelegate _handTriggerUpDelegate = default;

    HandPositionDelegate _handPositionDelegate = default;

    CashObjectInformation _arrow = default;

    HandStats _stats = HandStats.None;

    Vector3 _firstDrawObjectPositon = default;

    Vector3 _firstDrawObjectWorldPositon = default;

    Vector3 _directionMainCameraLookToBow = default;

    Quaternion _myQuaternion = default;

    #endregion

    #region パラメータ
    /// <summary>
    /// 弦掴める弦からの距離
    /// </summary>
    [SerializeField] float grapLimitDistance = 0.5f;

    /// <summary>
    /// 弦引ける限界角度
    /// </summary>
    [SerializeField] float drawLimitAngle = 90f;

    /// <summary>
    /// 弦引ける限界距離
    /// </summary>
    [SerializeField] float drawLimitDistance = 5f;

    /// <summary>
    /// 弦引いたパワーに掛ける力の強さ
    /// </summary>
    [SerializeField] float add = 1f;

    /// <summary>
    /// 連射防止
    /// </summary>
    [SerializeField] float cantShotDistance = 0.01f;

    /// <summary>
    /// 引いた時にマックスパワーになる距離の最高距離の割合
    /// </summary>
    [SerializeField] float drawDistancePercentMaxPower = 0.9f;

    #endregion

    float _distanceCameraToDrawObject = default;

    private void Start()
    {
        #region 初期化たち

        _draw = GetComponent<BowDraw>();

        _hold = GetComponent<BowHold>();

        _aim = GetComponent<BowShotAim>();

        _attract = GetComponent<AttractZone>();

        _vibe = GetComponent<BowVibe>();

        _inhallCustom = GetComponent<AttractEffectCustom>();

        _bowSE = GetComponent<BowSE>();

        _mng = GameObject.FindGameObjectWithTag(_InputTagName.TagName).GetComponent<InputManagement>();

        _playerManager = GameObject.FindGameObjectWithTag(_playerManagerTagName.TagName).GetComponent<PlayerManager>();

        _poolSystem = GameObject.FindGameObjectWithTag(_poolTagName.TagName).GetComponent<ObjectPoolSystem>();

        _firstDrawObjectPositon = _drawObject.transform.localPosition;

        _firstDrawObjectWorldPositon = _drawObject.transform.position;

        _distanceCameraToDrawObject = Vector3.Distance(_drawObject.transform.position, Camera.main.transform.position);

        _directionMainCameraLookToBow = (_drawObject.transform.position - Camera.main.transform.position).normalized;

        _myQuaternion = transform.localRotation;

        #endregion

        // インプットの設定
        SetUsingHand();

        // デバッグ用
        if (_traceValue)
        {
            _trace = this.gameObject.AddComponent<VR_Trace_Value>();
        }
    }

    private void Update()
    {
        BowManagement();
    }

    /// <summary>
    /// これを呼んでくれないと働いてくれない基本行動
    /// </summary>
    public void BowManagement()
    {
        // 上トリガー押して、手が空いている状態なら
        if (_handTriggerDownDelegate() && _stats == HandStats.None)
        {

            // 弦を掴める範囲内なら掴む
            if (grapLimitDistance > Vector3.Distance(_drawObject.transform.position, _handPositionDelegate()))
            {
                Grap();
            }

            // 弓持ちかえる処理
            if (grapLimitDistance > Vector3.Distance(_changeHandObjectTransform.position, _handPositionDelegate()) && !_mouseMode)
            {
                ChangeHand();
            }

        }
        // 弓の弦を掴んでいる状態なら
        else if (_stats == HandStats.Hold)
        {
            Holding();

        }
        else
        {
            //引くのをやめたらプロペラを止める、メーターを０にする
        }

    }


    /// <summary>
    /// 現在の弓を引いた距離が返される
    /// </summary>
    public float GetDrawDistance()
    {
        return Vector3.Distance(_firstDrawObjectPositon, _drawObject.transform.localPosition);
    }

    /// <summary>
    /// 現在の弓の引いた距離(%)
    /// </summary>
    public float GetPercentDrawDistance()
    {
        return Vector3.Magnitude(_drawObject.transform.position - transform.position) / (drawLimitDistance * drawDistancePercentMaxPower);
    }

    /// <summary>
    /// 矢をキューする
    /// </summary>
    /// <param name="arrow">キューするオブジェクト</param>
    public void ArrowQue(CashObjectInformation arrow)
    {
        _poolSystem.ReturnObject(arrow);
    }

    public bool IsHolding
    {
        get
        { return _stats == HandStats.Hold; }
    }
    /// <summary>
    /// 掴み中
    /// </summary>
    private void Holding()
    {

        // ドローオブジェクト引いている手に移動
        _draw.BowDrawing(_handPositionDelegate(), _drawObject, _firstDrawObjectPositon);

        _vibe.StartDrawVibe(GetPercentDrawDistance());
        SetText(GetPercentDrawDistance().ToString());
        _inhallCustom.SetActive(true);

        _inhallCustom.SetEffectSize(GetPercentDrawDistance());

        _attract.SetAngle(GetPercentDrawDistance());

        _bowSE.CallDrawingSE(GetPercentDrawDistance());

        _bowSE.CallAttractSE(GetPercentDrawDistance());

        // 弓のローテーション変更
        TurnBow();
        // 弓を引きすぎると手が弦から離れてうつ
        if (!ConeDecision.ConeInObject(transform, _drawObject.transform, drawLimitAngle, drawLimitDistance, -1))
        {
            ShotProcess();
        }

        // 上トリガー離して弓の弦を掴んでいる状態ならうつ
        if (!_handTriggerUpDelegate())
        {
            ShotProcess();
        }
    }

    /// <summary>
    /// 手の切り替え
    /// </summary>
    private void ChangeHand()
    {
        // 左手の場合
        if (_mng.P_EmptyHand == InputManagement.EmptyHand.Left)
        {
            _mng.P_EmptyHand = InputManagement.EmptyHand.Right;

            transform.parent = _handLeftPosition.parent;

            transform.localPosition = Vector3.zero;

            transform.localRotation = _myQuaternion;
        }
        // 右手の場合
        else
        {
            _mng.P_EmptyHand = InputManagement.EmptyHand.Left;

            transform.parent = _handRightPosition.parent;

            transform.localPosition = Vector3.zero;

            transform.localRotation = _myQuaternion;
        }

        SetUsingHand();
    }

    /// <summary>
    /// 掴む
    /// </summary>
    private void Grap()
    {
        _hold.BowHoldStart(_handPositionDelegate(), _drawObject);

        _bowSE.CallDrawStart();

        _bowSE.CallAttractStartSE();

        _arrow = _poolSystem.CallObject(PoolEnum.PoolObjectType.arrow, _drawObject.transform.position);

        _arrow.transform.rotation = this.transform.rotation;

        _arrow.transform.parent = _drawObject.transform;

        _arrow.transform.position -= _arrow.transform.GetChild(0).position - _arrow.transform.position;

        _stats = HandStats.Hold;
    }

    /// <summary>
    /// うつ時のしょりまとめただけ
    /// </summary>
    private void ShotProcess()
    {
        _bowSE.StopAttractSE();

        _vibe.EndDrawVibe();

        _vibe.StartShotVibe(GetPercentDrawDistance());

        _inhallCustom.SetActive(false);

        _playerManager.SetArrowMoveSpeed(GetPercentDrawDistance() * add);

        StartShotArrow(_aim.GetAim());

        // ちゃんと弓引いてなければすぐにデキュー
        if (GetDrawDistance() < cantShotDistance)
        {
            //Destroy(_arrow);
            _poolSystem.ReturnObject(_arrow);

        }
        else
        {
            // 撃つのでSE鳴らす
            _bowSE.CallShotSE();

        }

        _drawObject.transform.localPosition = _firstDrawObjectPositon;

        transform.localRotation = _myQuaternion;

        _attract.SetAngle(0f);

        _stats = HandStats.None;
    }

    /// <summary>
    /// 弓と弦の方向ベクトルで弓の角度決める、Zは可変
    /// </summary>
    private void TurnBow()
    {
        Vector3 bowPosition = transform.position;

        Vector3 drawPosition = _drawObject.transform.position;

        Vector3 directionDrawObjectToBow = bowPosition - drawPosition;

        float angleBowZ = transform.rotation.eulerAngles.z;

        transform.rotation = Quaternion.LookRotation(directionDrawObjectToBow);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y, angleBowZ);

    }

    /// <summary>
    /// 弓の持っている手によるインプットの設定
    /// </summary>
    private void SetUsingHand()
    {
        // マウスモード
        if (_mouseMode)
        {
            _handTriggerDownDelegate = new HandUseDelegate(GetMouseDownButton);

            _handTriggerUpDelegate = new HandUseDelegate(GetMouseDownButton);

            _handPositionDelegate = new HandPositionDelegate(GetMousePos);

            return;
        }

        // 左手空き
        if (_mng.P_EmptyHand == InputManagement.EmptyHand.Left)
        {
            _handTriggerDownDelegate = new HandUseDelegate(_mng.ButtonLeftUpTrigger);

            _handTriggerUpDelegate = new HandUseDelegate(_mng.ButtonLeftUpTrigger);

            _handPositionDelegate = new HandPositionDelegate(GetHandLeftPosition);

            _vibe.SetRightShotAction();




        }
        // 右手空き
        else
        {
            _handTriggerDownDelegate = new HandUseDelegate(_mng.ButtonRightUpTrigger);

            _handTriggerUpDelegate = new HandUseDelegate(_mng.ButtonRightUpTrigger);

            _handPositionDelegate = new HandPositionDelegate(GetHandRightPosition);

            _vibe.SetLeftShotAction();
        }

    }

    /// <summary>
    /// 左手のポジション返す
    /// </summary>
    private Vector3 GetHandLeftPosition()
    {
        return _handLeftPosition.position;
    }

    /// <summary>
    /// 右手のポジション返す
    /// </summary>
    private Vector3 GetHandRightPosition()
    {
        return _handRightPosition.position;
    }

    /// <summary>
    /// 射出開始
    /// </summary>
    /// <param name="aim">射出される方向ベクトルだけどLookRotation使うならtransform.position足して</param>
    private void StartShotArrow(Vector3 aim)
    {
        // うつしょりなんかかいて
        //GameObject obj=  Instantiate(debug, transform.position, Quaternion.LookRotation(aim + transform.position));
        _playerManager.ShotArrow(aim);
    }

    private void SetText(string text)
    {
        if (_traceValue)
        {
            _trace.SetText(text);
        }
        else
        {
            X_Debug.Log("値追いモードつけて");
        }

    }

    #region マウス関係
    private Vector3 GetMousePos()
    {
        // マウスの二次元上の座標
        Vector3 pos = Input.mousePosition;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        Vector3 finalPos = _directionMainCameraLookToBow * _distanceCameraToDrawObject + worldPos;

        return finalPos;

    }

    bool GetMouseDownButton() => Input.GetMouseButton(0);

    bool GetMouseUpButton() => Input.GetMouseButtonUp(0);

    #endregion
}
