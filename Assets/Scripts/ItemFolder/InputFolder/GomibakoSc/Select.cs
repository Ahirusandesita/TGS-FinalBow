// --------------------------------------------------------- 
// Select.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Select : MonoBehaviour
{
    #region variable 
    InputManagement mng;
    IArrowEnchantSet enchantSetter;
    [SerializeField] Vector2 selectDirection;

    [SerializeField]
    EnchantSetting[] setting;
    [SerializeField] SpriteRenderer main;
    [SerializeField] int mode = -1;
    [SerializeField] GameObject bar;
    [SerializeField] Transform barParent;
    float linez = 0;
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

        circleLinesAngle = CircleDivide(setting.Length);

        foreach(float a in circleLinesAngle)
        {
            print(a);
        }

        //for(int i = 0; i < circleLinesAngle.Length; i++)
        //{
        //    Instantiate(bar,)
        //}
    }

    private void Update()
    {

        Vector2 inputVR = SetInput();

        float input = Mathf.Atan2(inputVR.y, inputVR.x) * Mathf.Rad2Deg;
        if(input < 0)
        {
            input = 360 + input;
        }
        print("input:" + input);
        for (int i = 0; i < circleLinesAngle.Length; i++)
        {
            float minAngle = default;
            float nextAngle = default;

            if(i == circleLinesAngle.Length - 1)
            {
                minAngle = circleLinesAngle[i];
                nextAngle = circleLinesAngle[0];
            }
            else
            {
                minAngle = circleLinesAngle[i];
                nextAngle = circleLinesAngle[i + 1];
            }

            if(minAngle > nextAngle)
            {
                nextAngle += 360f;
            }

            if (circleLinesAngle[i] <= input && input < nextAngle)
            {
                mode = i;
                break;
            }
        }

        if (mode >= 0)
        {

            enchantSetter.EnchantSetting(setting[mode].state);
            Graphics(input);

        }

        Vector2 SetInput()
        {

            if (mng.P_EmptyHand == InputManagement.EmptyHand.Left)
            {
                selectDirection = mng.Axis2LeftStick();

            }
            else
            {
                selectDirection = mng.Axis2RightStick();
            }
            return selectDirection;
            //print("x:" + Mathf.Cos(Time.time)+"y:"+ Mathf.Sin(Time.time));
            //return new Vector2(Mathf.Cos(Time.time), Mathf.Sin(Time.time));
        }
    }

    private void Graphics(float input)
    {
        LineMove(input);

        ColorChange();

        void LineMove(float vec)
        {
            Quaternion trans;
            if(input != 0f)
            {
                trans = Quaternion.Euler(0, 0, vec);
            }
            else
            {
                trans = Quaternion.Euler(0, 0, (circleLinesAngle[mode] + circleLinesAngle[mode + 1]) / 2);
            }
            barParent.localRotation = trans;
        }

        void ColorChange()
        {
            //main.color = setting[mode].color;
        }
    }

    float[] CircleDivide(int count)
    {
        float angle = 360 / count;

        const float UP = 90;

        //float firstDivide = (angle / 2);

        List<float> angleLines = new List<float>();

        angleLines.Add(UP);

        for (int i = 0; i < count - 1; i++)
        {
            float setAngle = angleLines[i] + angle;
            if (setAngle >= 360)
            {
                setAngle -= 360;
            }
            angleLines.Add(setAngle);
        }

        return angleLines.ToArray();
    }



    #endregion
}