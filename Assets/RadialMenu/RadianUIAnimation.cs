using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadianUIAnimation : EnchantsChenger
{
    private enum E_MyState 
    { 
        Center       = 0,
        inner        = 1,
        Checked      = 2,
        Outer        = 3,  
        Back_Center  = 4,
        Back_Outer   = 5,
        Emiter       = 6
    };

    [SerializeField]
    E_MyState _myState;

    [SerializeField]
    public RectTransform _myTransform = default;

    [SerializeField]
    public Image _myImage = default;


    private delegate void AnimationEvent(RadianUIAnimation _this);

    private AnimationEvent myEvent;

    [HideInInspector]
    public float _defaultWidth, _defaultHeight;

    [SerializeField]
    public Color _defaultColor;

    [HideInInspector]
    public Color _differenceColor = default;

    public readonly float _defaultAlpha = 255;

    public const float MAX_SCALE = 1f;

    public const float MIN_SCALE = 0.1f;

    public const float MAX_COLOR = 1f;

    public const float MIN_COLOR = 0f;

    [HideInInspector]
    public Color WHITE = Color.white;

    public const float MAX_ALPHA = 1f;

    public const float MIN_ALPHA = 0f;

    [SerializeField]
    public float _speedforScale = 1f;

    [SerializeField]
    public float _speed2Color = 1;

    [SerializeField]
    public float _speed2Alpha = 1;

    [HideInInspector]
    public float _mySizeValue = default;

    [HideInInspector]
    public float _myColorValue = default;

    [HideInInspector]
    public float _myAlphaValue = default;

    [HideInInspector]
    public bool _isSetCansel = false;
    [HideInInspector]
    public bool _isSetSelect = false;
    [HideInInspector]
    public bool _isSetNotselect = false;
    [HideInInspector]
    public bool _isSetDecision = false;
    [HideInInspector]
    public bool _isSetReSet = false;

    public E_Event nowEvent;

    private void Start()
    {
        _myTransform = this.GetComponent<RectTransform>();
        _myImage = this.GetComponent<Image>();
        _defaultWidth = _myTransform.sizeDelta.x;
        _defaultHeight = _myTransform.sizeDelta.y;


        SetCancelEvent(this);

        SetSelectEvent(this);

        SetNotselectEvent(this);

        SetDecisionEvent(this);

        if (_myState == E_MyState.Outer)
        {
            _differenceColor = WHITE - _defaultColor;
        }
        SetReSetState(this);
    }

    private void Update()
    {
        switch (nowEvent)
        {
            case E_Event.Cancel:
                SetCancelState(this);

                break;

            case E_Event.Select:
                SetSelectState(this);

                break;


            case E_Event.Notselect:
                SetNotselectState(this);

                break;

            case E_Event.Decision:
                SetDecisionState(this);

                break;

            case E_Event.ReSet:
                SetReSetState(this);

                break;
        }
        myEvent(this);
    }



    protected override void SetCancelState(RadianUIAnimation _this)
    {
        if (!_isSetCansel)
        {
            myEvent = cancelEvent;
            base.SetCancelState(_this);
        }
    }

    protected override void SetSelectState(RadianUIAnimation _this)
    {
        if (!_isSetSelect)
        {
            myEvent = selectEvent;
            base.SetSelectState(_this);
        }
    }

    protected override void SetNotselectState(RadianUIAnimation _this)
    {
        if (!_isSetNotselect)
        {
            myEvent = notselectEvent;
            base.SetNotselectState(_this);
        }
    }


    protected override void SetDecisionState(RadianUIAnimation _this)
    {
        if (!_isSetDecision)
        {
            myEvent = decisionEvent;
            base.SetDecisionState(_this);
        }
    }

    protected override void SetReSetState(RadianUIAnimation _this)
    {
        if (!_isSetReSet)
        {
            this._mySizeValue = 0f;
            this._myColorValue = 0f;
            this._myAlphaValue = 0f;
            this.nowEvent = E_Event.Cancel;
            base.SetReSetState(_this);
        }
    }

    AnimationEvent cancelEvent;
    AnimationEvent selectEvent;
    AnimationEvent notselectEvent;
    AnimationEvent decisionEvent;


    private void SetCancelEvent(RadianUIAnimation _this)
    {
        switch (_myState)
        {
            case E_MyState.Center:
                cancelEvent = SizeDown;
                cancelEvent += AlphaRe_Change;
                break;

            case E_MyState.inner:
                cancelEvent = SizeDown;
                cancelEvent += AlphaRe_Change;
                break;

            case E_MyState.Checked:
                cancelEvent = SizeDown;
                cancelEvent += AlphaRe_Change;
                break;

            case E_MyState.Outer:
                cancelEvent = SizeDown;
                cancelEvent += ColorRe_Change;
                cancelEvent += AlphaRe_Change;
                break;

            case E_MyState.Back_Center:
                cancelEvent = SizeDown;
                cancelEvent += AlphaRe_Change;
                break;

            case E_MyState.Back_Outer:
                cancelEvent = SizeDown;
                cancelEvent += AlphaRe_Change;
                break;

            case E_MyState.Emiter:
                cancelEvent = SizeDown;
                cancelEvent += AlphaRe_Change;
                break;
        }
    }

    private void SetSelectEvent(RadianUIAnimation _this)
    {
        switch (_myState)
        {
            case E_MyState.Center:
                selectEvent = SizeUp;
                selectEvent += AlphaChange;
                break;

            case E_MyState.inner:
                selectEvent = SizeUp;
                selectEvent += AlphaChange;
                break;

            case E_MyState.Checked:
                selectEvent = SizeUp;
                selectEvent += AlphaChange;
                break;

            case E_MyState.Outer:
                selectEvent = SizeUp;
                selectEvent += AlphaChange;
                selectEvent += ColorRe_Change;
                break;

            case E_MyState.Back_Center:
                selectEvent = SizeUp;
                selectEvent += AlphaChange;
                break;

            case E_MyState.Back_Outer:
                selectEvent = SizeUp;
                selectEvent += AlphaChange;
                break;

            case E_MyState.Emiter:
                selectEvent = SizeUp;
                selectEvent += AlphaChange;
                break;
        }
    }

    private void SetNotselectEvent(RadianUIAnimation _this)
    {
        switch (_myState)
        {
            case E_MyState.Center:
                notselectEvent = SizeUp;
                notselectEvent += AlphaChange;
                break;

            case E_MyState.inner:
                notselectEvent = SizeUp;
                notselectEvent += AlphaChange;
                break;

            case E_MyState.Checked:
                notselectEvent = SizeUp;
                notselectEvent += AlphaRe_Change;
                break;

            case E_MyState.Outer:
                notselectEvent = SizeUp;
                notselectEvent += AlphaChange;
                notselectEvent += ColorRe_Change;
                break;

            case E_MyState.Back_Center:
                notselectEvent = SizeUp;
                notselectEvent += AlphaChange;
                break;

            case E_MyState.Back_Outer:
                notselectEvent = SizeUp;
                notselectEvent += AlphaChange;
                break;

            case E_MyState.Emiter:
                notselectEvent = SizeUp;
                notselectEvent += AlphaChange;
                break;
        }
    }

    private void SetDecisionEvent(RadianUIAnimation _this)
    {
        switch (_myState)
        {
            case E_MyState.Center:
                decisionEvent = SizeUp;
                decisionEvent += AlphaChange;
                break;

            case E_MyState.inner:
                decisionEvent = SizeUp;
                decisionEvent += AlphaChange;
                break;

            case E_MyState.Checked:
                decisionEvent = SizeUp;
                decisionEvent += AlphaChange;
                break;

            case E_MyState.Outer:
                decisionEvent = SizeUp;
                decisionEvent += ColorChange;
                decisionEvent += AlphaChange;
                break;
        }
    }
}
