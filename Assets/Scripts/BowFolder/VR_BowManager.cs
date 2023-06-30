// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using System;
using UnityEngine;

[RequireComponent(typeof(BowVibe), typeof(AttractEffectCustom), typeof(AttractZone))]
[RequireComponent(typeof(Inhall))]
public class VR_BowManager : CanDraw_BowManager
{

    #region かつてpublicだった変数

    [SerializeField] Transform _changeHandObjectTransform = default;

    /// <summary>
    /// VRで値追うモード、デバッグ用
    /// </summary>
    public bool _traceValue = false;

    #endregion

    #region クラス、構造体

    private InputManagement _vrInput = default;

    private  new IFBowTransformControl_Bow _transformControl = default;

    private VR_Trace_Value _trace = default;

    private IFBowVibe _vibe = default;

    #endregion


    protected override void Start()
    {

        _vibe = GetComponent<BowVibe>();

        _vrInput = GameObject.FindGameObjectWithTag(_InputTagName.TagName).GetComponent<InputManagement>();

        base.Start();

        _transformControl = base._transformControl as IFBowTransformControl_Bow;

        // デバッグ用
        if (_traceValue)
        {
            _trace = this.gameObject.AddComponent<VR_Trace_Value>();
        }
    }

    private void Update()
    {
        BowUpdateCallProcess();
    }

    protected override void ProcessOfGrapObject()
    {
        X_Debug.Log("aaa" + Vector3.Distance(_drawObject.position, _transformControl.GetHandPosition) + (grapLimitDistance > Vector3.Distance(_drawObject.position, _transformControl.GetHandPosition)));
        // 手が弓の弦の近くにある場合引く
        if (grapLimitDistance > Vector3.Distance(_drawObject.position, _transformControl.GetHandPosition))
        {
            BowStringGrap();
        }
        // 手が、弓の持つ手の切り替えるポジションの近くにある場合
        else if (grapLimitDistance > Vector3.Distance(_changeHandObjectTransform.position, _transformControl.GetHandPosition))
        {
            ChangeHand();
        }
    }

    protected override void ProcessOfHoldObject()
    {
        BowStringHold();
    }

    protected override void ProcessOfReleaseObjcect()
    {
        BowShotStart();
    }

    protected override void SetInputDelegate()
    {
        SetHandUseDelegate();
    }

    protected override float GetShotPercentPower()
    {
        return _transformControl.GetPercentDrawDistance(drawLimitDistance, drawDistancePercentMaxPower);
    }

    protected override void BowStringGrap()
    {
        base.BowStringGrap();
    }

    /// <summary>
    /// 弓の弦掴み中
    /// </summary>
    protected override void BowStringHold()
    {
        _vibe.StartDrawVibe(_percentDrawPower);

        base.BowStringHold();
    }

    /// <summary>
    /// 弓を撃つ処理
    /// </summary>
    protected override void  BowShotStart()
    {
        _vibe.EndDrawVibe();

        base.BowShotStart();

    }


    protected override void AddBowShotProcess()
    {
        _vibe.StartShotVibe(_percentDrawPower);
    }

    protected override void CastInterface()
    {
        _transformControl = base._transformControl as IFBowTransformControl_Bow;
    }


    /// <summary>
    /// 手の切り替え
    /// </summary>
    private void ChangeHand()
    {
        // 左手の場合
        if (_vrInput.P_EmptyHand == InputManagement.EmptyHand.Left)
        {
            _vrInput.P_EmptyHand = InputManagement.EmptyHand.Right;
        }
        // 右手の場合
        else
        {
            _vrInput.P_EmptyHand = InputManagement.EmptyHand.Left;

        }

        _transformControl.SetBowTransformInHand(_vrInput.P_EmptyHand);

        SetHandUseDelegate();
    }

    /// <summary>
    /// 弓の持っている手によるインプットの設定
    /// </summary>
    private void SetHandUseDelegate()
    {

        // 左手空き
        if (_vrInput.P_EmptyHand == InputManagement.EmptyHand.Left)
        {
            _grapTriggerInput = () => _vrInput.ButtonLeftUpTrigger();

            _releaseTriggerInput = () => !_vrInput.ButtonLeftUpTrigger();

            _vibe.SetRightShotAction();

        }
        // 右手空き
        else
        {
            _grapTriggerInput = () => _vrInput.ButtonRightUpTrigger();

            _releaseTriggerInput = () => !_vrInput.ButtonRightUpTrigger();

            _vibe.SetLeftShotAction();
        }

        _transformControl.SetEmptyHandPositionDelegete(_vrInput.P_EmptyHand);
    }

    /// <summary>
    /// デバッグ用
    /// </summary>
    /// <param name="text"></param>
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


}
