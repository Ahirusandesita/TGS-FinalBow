// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

public class ClickInput
{
    /// <summary>
    /// マウスクリック　左クリック弱　右クリック強
    /// </summary>
    /// <returns></returns>
    public float Input_Click_Mouse()
    {
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            return 1f;
        }
        else if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
        {
            return 5f;
        }
        else if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            return 3f;
        }
        return 0f;
    }



    public bool Input_Space()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return true;
        }
        return false;
    }
    public bool Input_SpaceDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        return false;
    }
    public bool Input_SpaceUp()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            return true;
        }
        return false;
    }

    public bool Input_Enter()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            return true;
        }
        return false;
    }
    public bool Input_EnterDown()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            return true;
        }
        return false;
    }
    public bool Input_EnterUp()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            return true;
        }
        return false;
    }



}
