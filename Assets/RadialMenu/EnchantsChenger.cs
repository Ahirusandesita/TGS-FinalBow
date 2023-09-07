using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnchantsChenger : MonoBehaviour
{
    [SerializeField, Tooltip("Explosion = 0, Thunder = 1, Penetration = 2, Homing = 3, Rapid = 4")]
    private RadianUIAnimation[] _centerCircles = new RadianUIAnimation[5];

    [SerializeField, Tooltip("Explosion = 0, Thunder = 1, Penetration = 2, Homing = 3, Rapid = 4")]
    private RadianUIAnimation[] _innerCircles = new RadianUIAnimation[5];

    [SerializeField, Tooltip("Explosion = 0, Thunder = 1, Penetration = 2, Homing = 3, Rapid = 4")]
    private RadianUIAnimation[] _selectCircles = new RadianUIAnimation[5];

    [SerializeField, Tooltip("Explosion = 0, Thunder = 1, Penetration = 2, Homing = 3, Rapid = 4")]
    private RadianUIAnimation[] _outerCircles = new RadianUIAnimation[5];

    [SerializeField, Tooltip("_backGround_CenterCircle")]
    private RadianUIAnimation _backGround_CenterCircle;

    [SerializeField, Tooltip("_backGround_OuterCircle")]
    private RadianUIAnimation _backGround_OuterCircle;

    [SerializeField, Tooltip("_backGround_Emiter")]
    private RadianUIAnimation _backGround_Emiter;

    public enum E_Enchant { Explosion = 0, Thunder = 1, Penetration = 2, Homing = 3, Rapid = 4 };

    public enum E_BackGround { CenterCircle = 0, OuterCircle = 1, Emiter = 2 }

    public enum E_Event { Cancel = 0, Select = 1, Notselect = 2, Decision  = 3, ReSet = 4 }




    // Cancel→　全部消えてるor消えていく　　Select→　Alphaで出現、真ん中の文字出す　　Noselect→　Alphaで消失、真ん中の文字消す（文字は一瞬で） 　　Decision→　選んでないやつはすべてCancelに、選んでる奴は少し残して消える、漢字の色を変えて光らせ、少し経ったら薄めて消す
    // Reset→　例外処理　全部消す　　



    void Start()
    {

    }

    void Update()
    {

    }

    protected virtual void SetCancelState(RadianUIAnimation _this)
    {
        _this._isSetCansel = true;
        _this._isSetSelect = false;
        _this._isSetNotselect = false;
        _this._isSetDecision = false;
        _this._isSetReSet = false;
    }

    protected virtual void SetSelectState(RadianUIAnimation _this)
    {
        _this._isSetCansel = false;
        _this._isSetSelect = true;
        _this._isSetNotselect = false;
        _this._isSetDecision = false;
        _this._isSetReSet = false;
    }
    protected virtual void SetNotselectState(RadianUIAnimation _this)
    {
        _this._isSetCansel = false;
        _this._isSetSelect = false;
        _this._isSetNotselect = true;
        _this._isSetDecision = false;
        _this._isSetReSet = false;
    }
    protected virtual void SetDecisionState(RadianUIAnimation _this)
    {
        _this._isSetCansel = false;
        _this._isSetSelect = false;
        _this._isSetNotselect = false;
        _this._isSetDecision = true;
        _this._isSetReSet = false;
    }
    protected virtual void SetReSetState(RadianUIAnimation _this)
    {
        _this._isSetCansel = false;
        _this._isSetSelect = false;
        _this._isSetNotselect = false;
        _this._isSetDecision = false;
        _this._isSetReSet = true;
    }

    protected void SizeUp(RadianUIAnimation _this)
    {
        _this._mySizeValue += Time.deltaTime * _this._speedforScale;

        if (_this._mySizeValue > RadianUIAnimation.MAX_SCALE)
        {
            _this._mySizeValue = RadianUIAnimation.MAX_SCALE;
        }
        SizeChange(_this);
    }

    protected void SizeDown(RadianUIAnimation _this)
    {
        _this._mySizeValue -= Time.deltaTime * _this._speedforScale;

        if (_this._mySizeValue < RadianUIAnimation.MIN_SCALE)
        {
            _this._mySizeValue = RadianUIAnimation.MIN_SCALE;
        }
        SizeChange(_this);
    }

    private void SizeChange(RadianUIAnimation _this)
    {
        _this._myTransform.sizeDelta = new Vector2(_this._mySizeValue * _this._defaultWidth , _this._mySizeValue * _this._defaultHeight);
    }

    protected void ColorChange(RadianUIAnimation _this)
    {
        _this._myColorValue += Time.deltaTime * _this._speed2Color;
        if(_this._myColorValue > RadianUIAnimation.MAX_COLOR)
        {
            _this._myColorValue = RadianUIAnimation.MAX_COLOR;
        }
        ColorPaint(_this);
    }

    protected void ColorRe_Change(RadianUIAnimation _this)
    {
        _this._myColorValue -= Time.deltaTime * _this._speed2Color;

        if (_this._myColorValue < RadianUIAnimation.MIN_COLOR)
        {
            _this._myColorValue = RadianUIAnimation.MIN_COLOR;
        }
        ColorPaint(_this);
    }

    protected void ColorPaint(RadianUIAnimation _this)
    {
        _this._myImage.color = Color.Lerp(_this.WHITE,_this._defaultColor,_this._myColorValue);
    }

    protected void AlphaChange(RadianUIAnimation _this)
    {
        _this._myAlphaValue += Time.deltaTime * _this._speed2Alpha;

        if (_this._myAlphaValue > RadianUIAnimation.MAX_ALPHA)
        {
            _this._myAlphaValue = RadianUIAnimation.MAX_ALPHA;
        }
        AlphaPaint(_this);
    }

    protected void AlphaRe_Change(RadianUIAnimation _this)
    {
        _this._myAlphaValue -= Time.deltaTime * _this._speed2Alpha;

        if (_this._myAlphaValue < RadianUIAnimation.MIN_ALPHA)
        {
            _this._myAlphaValue = RadianUIAnimation.MIN_ALPHA;
        }
        AlphaPaint(_this);
    }

    protected void AlphaPaint(RadianUIAnimation _this)
    {
        _this._myImage.color = new Color(_this._myImage.color.r, _this._myImage.color.g, _this._myImage.color.b, _this._myAlphaValue);
    }

    protected void AllClear(RadianUIAnimation _this)
    {
        SizeChange(_this);
        ColorChange(_this);
        AlphaChange(_this);
    }


    public void SetCircleEnum(E_Enchant _enchant , E_Event _event)
    {
        _centerCircles[(int)_enchant].nowEvent = _event;
        _innerCircles[(int)_enchant].nowEvent = _event;
        _selectCircles[(int)_enchant].nowEvent = _event;
        _outerCircles[(int)_enchant].nowEvent = _event;
    }

    public void SetBackGroundEnum(E_Event _event)
    {
        _backGround_CenterCircle.nowEvent = _event;
        _backGround_OuterCircle.nowEvent = _event;
        _backGround_Emiter.nowEvent = _event;
    }

    public void SetOnlyBackGround(E_BackGround _backGround , E_Event _event)
    {
        switch (_backGround)
        {
            case E_BackGround.CenterCircle:
                _backGround_CenterCircle.nowEvent = _event;
                break;

            case E_BackGround.OuterCircle:
                _backGround_OuterCircle.nowEvent = _event;
                break;

            case E_BackGround.Emiter:
                _backGround_Emiter.nowEvent = _event;
                break;
        }
    }

    public void RotateEmiter(float rotation)
    {
        _backGround_Emiter._myTransform.localRotation = Quaternion.Euler(_backGround_Emiter._myTransform.rotation.x, _backGround_Emiter._myTransform.rotation.y, -rotation);
    }

}
