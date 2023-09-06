// --------------------------------------------------------- 
// Select.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectEnchant : MonoBehaviour
{
    /*
    各パーツは７２°で計算
    始点は上方０°で考える
    */


    [SerializeField]
    private EnchantsChenger Chenger;

    private enum NowSelect { None = 0, Explosion = 1, Thunder = 2, Penetration = 3, Homing = 4, Rapid = 5 };
    private NowSelect _state;
    private Vector2 _inputVecter = default;
    private readonly Vector2 ZERO = new Vector2(0f, 0f);
    private float _inputAngle = default;

    private bool _useCoroutine = false;

    private const int around = 360;
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
        public EnchantmentEnum.EnchantmentState state;
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

        _inputVecter = SetInput();
        if (_inputVecter == ZERO)
        {
            switch (_state)
            {
                case NowSelect.None:

                    return;

                case NowSelect.Explosion:
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Decision);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
                    StartCoroutine(DecisionCancelCoroutine(0.5f,EnchantsChenger.E_Enchant.Explosion));
                    enchantSetter.EnchantSetting(EnchantmentEnum.EnchantmentState.bomb);
                    _useCoroutine = true;
                    break;

                case NowSelect.Thunder:
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Decision);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
                    StartCoroutine(DecisionCancelCoroutine(0.5f, EnchantsChenger.E_Enchant.Thunder));
                    enchantSetter.EnchantSetting(EnchantmentEnum.EnchantmentState.thunder);
                    _useCoroutine = true;
                    break;

                case NowSelect.Penetration:
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Decision);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
                    StartCoroutine(DecisionCancelCoroutine(0.5f, EnchantsChenger.E_Enchant.Penetration));
                    enchantSetter.EnchantSetting(EnchantmentEnum.EnchantmentState.penetrate);
                    _useCoroutine = true;
                    break;

                case NowSelect.Homing:
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Decision);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
                    StartCoroutine(DecisionCancelCoroutine(0.5f, EnchantsChenger.E_Enchant.Homing));
                    enchantSetter.EnchantSetting(EnchantmentEnum.EnchantmentState.homing);
                    _useCoroutine = true;
                    break;

                case NowSelect.Rapid:
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Decision);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
                    Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);
                    StartCoroutine(DecisionCancelCoroutine(0.5f, EnchantsChenger.E_Enchant.Rapid));
                    enchantSetter.EnchantSetting(EnchantmentEnum.EnchantmentState.knockBack);
                    _useCoroutine = true;
                    break;
            }
        }
        else
        {
            if (_useCoroutine)
            {
                StopAllCoroutines();
            }
            _inputAngle = Mathf.Atan2(_inputVecter.x, _inputVecter.y) * Mathf.Rad2Deg;

            if(_inputAngle >= 0f && _inputAngle < 144f)
            {
                if(_inputAngle < 72f)
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
                        _state = NowSelect.Explosion;
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
                        _state = NowSelect.Thunder;
                    }
                }
            }
            else if (_inputAngle >= -144)
            {
                if(_inputAngle <= -72f)
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
                        _state = NowSelect.Homing;
                    }
                }
                else if(_inputAngle < 0f)
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
                        _state = NowSelect.Rapid;
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
                    _state = NowSelect.Penetration;
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

        Vector2 SetInput()
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
    }

    private IEnumerator DecisionCancelCoroutine(float delay, EnchantsChenger.E_Enchant Enchant)
    {
        yield return new WaitForSeconds(delay);

        Chenger.SetCircleEnum(Enchant, EnchantsChenger.E_Event.Cancel);
        Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Cancel);
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



    #endregion
}