// --------------------------------------------------------- 
// Select.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectEnchant : MonoBehaviour
{
    /*
    各パーツは７２°で計算
    始点は上方０°で考える
    */

    [SerializeField]
    private AudioSource _speaker = default;

    [SerializeField]
    private AudioClip _selectSound = default;

    [SerializeField]
    private AudioClip _enchantSound = default;

    [SerializeField]
    private EnchantsChenger Chenger;

    private TutorialManager _tutorialManager;

    private float _waitTime = 0.25f;

    private enum NowSelect { None = 0, Explosion = 1, Thunder = 2, Penetration = 3, Homing = 4, Rapid = 5 };
    private NowSelect _state;
    private Vector2 _inputVecter = default;
    private readonly Vector2 ZERO = new Vector2(0f, 0f);
    private float _inputAngle = default;

    private bool _useCoroutine = false;

    private EnchantmentEnum.EnchantmentState _enchantState = EnchantmentEnum.EnchantmentState.normal;

    [SerializeField]
    private Image _dontSelect = default;

    #region variable 
    InputManagement mng;
    IArrowEnchantSet enchantSetter;
    [SerializeField] Vector2 selectDirection;

    [SerializeField]
    EnchantSetting[] setting;
    [SerializeField] SpriteRenderer main;
    [SerializeField] int mode = -1;
    [SerializeField] Transform barParent;
    [SerializeField] GameObject image;
    float linez = 0;
    float minAngle = default;
    float nextAngle = default;
    float[] circleLinesAngle = default;
    [System.Serializable]
    struct EnchantSetting
    {
        [SerializeField]
        public EnchantmentEnum.EnchantmentState _enchantState;
        [SerializeField]
        public Color color;
    }
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        mng = GameObject.FindWithTag(InhallLibTags.InputController).GetComponent<InputManagement>();
        enchantSetter = GameObject.FindWithTag(InhallLibTags.ArrowEnchantmentController).GetComponent<IArrowEnchantSet>();
        try
        {
            _tutorialManager = GameObject.FindGameObjectWithTag("TutorialController").GetComponent<TutorialManager>();
        }
        catch
        {
            _tutorialManager = null;
        }

        if (_tutorialManager != null)
        {
            TutorialEventSetting();
        }
        else
        {
            NormalEventSetting();
        }


        //circleLinesAngle = CircleDivide(setting.Length);
        //image.SetActive(false);
        //foreach (float a in circleLinesAngle)
        //{
        //    print(a);
        //}

        //for(int i = 0; i < circleLinesAngle.Length; i++)
        //{
        //    Instantiate(bar,)
        //}

    }

    


    private void Update()
    {
        enchantSetter.EnchantSetting(_enchantState);

        _inputVecter = SetInput();
        if (_inputVecter == ZERO)
        {
            switch (_state)
            {
                case NowSelect.None:

                    return;

                case NowSelect.Explosion:
                    DecisionExplosion();
                    break;

                case NowSelect.Thunder:
                    DecisionThunder();
                    break;

                case NowSelect.Penetration:
                    DecisionPenetrate();
                    break;

                case NowSelect.Homing:
                    DecisionHoming();
                    break;

                case NowSelect.Rapid:
                    DecisionRapid();
                    break;
            }
        }
        else
        {
            if (_useCoroutine)
            {
                StopAllCoroutines();
                DecisionCancel(ExchengeState(_enchantState));
            }
            _inputAngle = Mathf.Atan2(_inputVecter.x, _inputVecter.y) * Mathf.Rad2Deg;

            print(_inputAngle);

            if (_inputAngle < 144f)
            {
                if (_inputAngle >= 0f)
                {
                    if (_inputAngle < 72f)
                    {
                        //Explosion
                        if (_state != NowSelect.Explosion)
                        {
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Select);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Notselect);
                            TutorialOpenMenu();
                            _state = NowSelect.Explosion;
                            _speaker.PlayOneShot(_selectSound);
                        }
                    }
                    else
                    {
                        //Thunder
                        if (_state != NowSelect.Thunder)
                        {
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Select);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Notselect);
                            TutorialOpenMenu();
                            _state = NowSelect.Thunder;
                            _speaker.PlayOneShot(_selectSound);
                        }
                    }
                }
                else if(_inputAngle > -144f)
                {
                    if(_inputAngle >= -72)
                    {
                        //Rapid
                        if (_state != NowSelect.Rapid)
                        {
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Select);
                            Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Notselect);
                            TutorialOpenMenu();
                            _state = NowSelect.Rapid;
                            _speaker.PlayOneShot(_selectSound);
                        }
                    }
                    else
                    {
                        //Homing
                        if (_state != NowSelect.Homing)
                        {
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Select);
                            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Notselect);
                            Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Notselect);
                            TutorialOpenMenu();
                            _state = NowSelect.Homing;
                            _speaker.PlayOneShot(_selectSound);
                        }
                    }

                }
                else
                {
                    //Penetra
                    if (_state != NowSelect.Penetration)
                    {
                        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Notselect);
                        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Notselect);
                        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Select);
                        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Notselect);
                        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Notselect);
                        Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Notselect);
                        TutorialOpenMenu();
                        _state = NowSelect.Penetration;
                        _speaker.PlayOneShot(_selectSound);
                    }
                }
            }
            else
            {
                //Penetra
                if (_state != NowSelect.Penetration)
                {
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Notselect);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Notselect);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Select);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Notselect);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Notselect);
                    Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Notselect);
                    TutorialOpenMenu();
                    _state = NowSelect.Penetration;
                    _speaker.PlayOneShot(_selectSound);
                }
            }
            Chenger.RotateEmiter(_inputAngle);

            #region 使わない
            //if (_inputAngle != 0)
            //{

            //    if (_inputAngle < 0)
            //    {
            //            _inputAngle = around + _inputAngle;
            //    }

            //    for (int i = 0; i < circleLinesAngle.Length; i++)
            //    {


            //        if (i == circleLinesAngle.Length - 1)
            //        {
            //            minAngle = circleLinesAngle[i];
            //            nextAngle = circleLinesAngle[0];
            //        }
            //        else
            //        {
            //            minAngle = circleLinesAngle[i];
            //            nextAngle = circleLinesAngle[i + 1];
            //        }

            //        if (minAngle > nextAngle)
            //        {
            //            nextAngle += around;
            //        }

            //        if (minAngle <= _inputAngle && _inputAngle < nextAngle)
            //        {
            //            mode = i;
            //            break;
            //        }
            //    }
            //}
            #endregion
        }



        //if (_state != NowSelect.None)
        //{
        //    enchantSetter.EnchantSetting(setting[mode].state);
        //    Graphics(_inputAngle);

        //}

    }
    private Vector2 SetInput()
    {

        if (mng.P_EmptyHand == InputManagement.EmptyHand.Left)
        {
            selectDirection = mng.Axis2RightStick();
        }
        else
        {
            selectDirection = mng.Axis2LeftStick();
        }
        return selectDirection;
        //print("x:" + Mathf.Cos(Time.time)+"y:"+ Mathf.Sin(Time.time));
        //return new Vector2(Mathf.Cos(Time.time), Mathf.Sin(Time.time));
    }

    private IEnumerator DecisionCancelCoroutine(float delay, EnchantsChenger.E_Enchant Enchant)
    {
        yield return new WaitForSeconds(delay);

        DecisionCancel(Enchant);
    }

    private void DecisionCancel(EnchantsChenger.E_Enchant Enchant)
    {
        Chenger.SetCircleEnum(Enchant, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Cancel);
        _useCoroutine = false;
    }

    private EnchantsChenger.E_Enchant ExchengeState(EnchantmentEnum.EnchantmentState enchantState)
    {
        EnchantsChenger.E_Enchant value = EnchantsChenger.E_Enchant.Explosion;
        switch (enchantState)
        {
            case EnchantmentEnum.EnchantmentState.bomb:
                value = EnchantsChenger.E_Enchant.Explosion;
                break;

            case EnchantmentEnum.EnchantmentState.thunder:
                value = EnchantsChenger.E_Enchant.Thunder;
                break;

            case EnchantmentEnum.EnchantmentState.penetrate:
                value = EnchantsChenger.E_Enchant.Penetration;
                break;

            case EnchantmentEnum.EnchantmentState.homing:
                value = EnchantsChenger.E_Enchant.Homing;
                break;

            case EnchantmentEnum.EnchantmentState.rapidShots:
                value = EnchantsChenger.E_Enchant.Rapid;
                break;
        }
        return value;
    }



    //private void Graphics(float input)
    //{
    //    LineMove(input);

    //    ColorChange();

    //    void LineMove(float vec)
    //    {
    //        Quaternion trans;
    //        if (input != 0f)
    //        {
    //            trans = Quaternion.Euler(0, 0, vec);
    //            image.SetActive(true);
    //        }
    //        else
    //        {
    //            trans = Quaternion.Euler(0, 0, (minAngle + nextAngle) / 2);
    //            image.SetActive(false);

    //        }
    //        barParent.localRotation = trans;
    //    }

    //    void ColorChange()
    //    {
    //        //main.color = setting[mode].color;
    //    }
    //}

    //float[] CircleDivide(int count)
    //{
    //    float angle = around / count;

    //    const float UP = 90;

    //    //float firstDivide = (angle / 2);

    //    List<float> angleLines = new List<float>();

    //    angleLines.Add(UP);

    //    for (int i = 0; i < count - 1; i++)
    //    {
    //        float setAngle = angleLines[i] + angle;
    //        if (setAngle >= around)
    //        {
    //            setAngle -= around;
    //        }
    //        angleLines.Add(setAngle);
    //    }

    //    return angleLines.ToArray();
    //}

    public delegate void DecisionEvent();

    DecisionEvent DecisionExplosion;
    DecisionEvent DecisionThunder;
    DecisionEvent DecisionPenetrate;
    DecisionEvent DecisionHoming;
    DecisionEvent DecisionRapid;

    private void NormalDecisionExplosion()
    {
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Decision);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
        StartCoroutine(DecisionCancelCoroutine(_waitTime, EnchantsChenger.E_Enchant.Explosion));
        _enchantState = EnchantmentEnum.EnchantmentState.bomb;
        _state = NowSelect.None;
        _useCoroutine = true;
        _speaker.PlayOneShot(_enchantSound);
    }

    private void NormalDecisionThunder()
    {
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Decision);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
        StartCoroutine(DecisionCancelCoroutine(_waitTime, EnchantsChenger.E_Enchant.Thunder));
        _enchantState = EnchantmentEnum.EnchantmentState.thunder;
        _state = NowSelect.None;
        _useCoroutine = true;
        _speaker.PlayOneShot(_enchantSound);
    }

    private void NormalDecisionPenetrate()
    {
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Decision);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
        StartCoroutine(DecisionCancelCoroutine(_waitTime, EnchantsChenger.E_Enchant.Penetration));
        _enchantState = EnchantmentEnum.EnchantmentState.penetrate;
        _state = NowSelect.None;
        _useCoroutine = true;
        _speaker.PlayOneShot(_enchantSound);
    }

    private void NormalDecisionHoming()
    {
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Decision);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
        StartCoroutine(DecisionCancelCoroutine(_waitTime, EnchantsChenger.E_Enchant.Homing));
        _enchantState = EnchantmentEnum.EnchantmentState.homing;
        _state = NowSelect.None;
        _useCoroutine = true;
        _speaker.PlayOneShot(_enchantSound);
    }

    private void NormalDecisionRapid()
    {
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Decision);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
        StartCoroutine(DecisionCancelCoroutine(_waitTime, EnchantsChenger.E_Enchant.Rapid));
        _enchantState = EnchantmentEnum.EnchantmentState.rapidShots;
        _state = NowSelect.None;
        _useCoroutine = true;
        _speaker.PlayOneShot(_enchantSound);
    }




    private void TutorialDecisionExplosion()
    {
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Decision);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
        StartCoroutine(DecisionCancelCoroutine(_waitTime, EnchantsChenger.E_Enchant.Explosion));
        _enchantState = EnchantmentEnum.EnchantmentState.bomb;
        _state = NowSelect.None;
        _useCoroutine = true;
        _speaker.PlayOneShot(_enchantSound);
        _tutorialManager.OnSelectedBomb();
    }

    private void TutorialDecisionThunder()
    {
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
        //StartCoroutine(DecisionCancelCoroutine(_waitTime, EnchantsChenger.E_Enchant.Thunder));
        _state = NowSelect.None;
        //_useCoroutine = true;
        //_speaker.PlayOneShot(_enchantSound);
        TutorialUI();
    }

    private void TutorialDecisionPenetrate()
    {
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
        //StartCoroutine(DecisionCancelCoroutine(_waitTime, EnchantsChenger.E_Enchant.Thunder));
        _state = NowSelect.None;
        //_useCoroutine = true;
        //_speaker.PlayOneShot(_enchantSound);
        TutorialUI();
    }

    private void TutorialDecisionHoming()
    {
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
        //StartCoroutine(DecisionCancelCoroutine(_waitTime, EnchantsChenger.E_Enchant.Thunder));
        _state = NowSelect.None;
        //_useCoroutine = true;
        //_speaker.PlayOneShot(_enchantSound);
        TutorialUI();
    }

    private void TutorialDecisionRapid()
    {
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
        Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
        //StartCoroutine(DecisionCancelCoroutine(_waitTime, EnchantsChenger.E_Enchant.Thunder));
        _state = NowSelect.None;
        //_useCoroutine = true;
        //_speaker.PlayOneShot(_enchantSound);
        TutorialUI();
    }

    private void NormalEventSetting()
    {
        DecisionExplosion = NormalDecisionExplosion;
        DecisionThunder = NormalDecisionThunder;
        DecisionPenetrate = NormalDecisionPenetrate;
        DecisionHoming = NormalDecisionHoming;
        DecisionRapid = NormalDecisionRapid;
    }

    private void TutorialEventSetting()
    {
        DecisionExplosion = TutorialDecisionExplosion;
        DecisionThunder = TutorialDecisionThunder;
        DecisionPenetrate = TutorialDecisionPenetrate;
        DecisionHoming = TutorialDecisionHoming;
        DecisionRapid = TutorialDecisionRapid;
    }

    private void TutorialUI()
    {
        return;
    }

    private IEnumerator DontSelectCoroutine()
    {
        yield return new WaitForEndOfFrame();
    }

    private void TutorialOpenMenu()
    {
        if (_tutorialManager != null && _state == NowSelect.None)
        {
            _tutorialManager.OnRadialMenuDisplayed();
        }
    }

    #endregion
}