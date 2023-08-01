// --------------------------------------------------------- 
// Select.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class Select : MonoBehaviour
{
    #region variable 
    InputManagement mng;
    FakePlayerManager player;
    BowManager bow;
    [SerializeField] Vector2 selectDirection;
    [SerializeField] LineRenderer line;


    const float up = 2 / 3;
    const float down = -up;
    [OVRNamedArray(new string[] { "LeftUp", "Left", "LeftDown", "RightUp", "Right", "RightDown" }), SerializeField]
    EnchantmentEnum.ItemAttributeState[] enchantments;
    [OVRNamedArray(new string[] { "LeftUp", "Left", "LeftDown", "RightUp", "Right", "RightDown" }), SerializeField]
    SpriteRenderer[] sprites;
    [SerializeField] SpriteRenderer main;
    [SerializeField] int mode = -1;
    float linez = 0;
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        mng = GameObject.FindWithTag(InhallLibTags.InputController).GetComponent<InputManagement>();
        player = GameObject.FindWithTag(InhallLibTags.PlayerController).GetComponent<FakePlayerManager>();
        bow = GameObject.FindWithTag(InhallLibTags.BowController).GetComponent<BowManager>();

        linez = line.GetPosition(0).z;
    }

    private void Update()
    {
        SetInput();

        if (selectDirection.x < 0)
        {
            if (selectDirection.y > up)
            {
                mode = 0;
            }
            else if (selectDirection.y < down)
            {
                mode = 2;
            }
            else
            {
                mode = 1;
            }
        }
        else if (selectDirection.x > 0)
        {
            if (selectDirection.y > up)
            {
                mode = 3;
            }
            else if (selectDirection.y < down)
            {
                mode = 5;
            }
            else
            {
                mode = 4;
            }
        }
        else
        {
            return;
        }

        if (mode > 0 && bow.IsHolding)
        {
            player.SetEnchant(enchantments[mode]);

            Graphics(selectDirection);

        }

        void SetInput()
        {
            //try
            {
                if (mng.P_EmptyHand == InputManagement.EmptyHand.Left)
                {
                    selectDirection = mng.Axis2LeftStick();

                }
                else
                {
                    selectDirection = mng.Axis2RightStick();
                }
            }
            //finally
            //{
            //    X_Debug.Log("InputManagementnのアクセスに異常があります");
            //}

        }
    }

    private void Graphics(Vector3 input)
    {
        LineMove(input);

        ColorChange();

        void LineMove(Vector3 vec)
        {
            vec = vec.normalized * 0.5f + (Vector3.forward * linez);

            line.SetPosition(1, vec);
        }

        void ColorChange()
        {
            main.color = sprites[mode].color;
        }
    }

    void CircleDivide(int count)
    {
        float angle = 360 / count;


    }


    #endregion
}