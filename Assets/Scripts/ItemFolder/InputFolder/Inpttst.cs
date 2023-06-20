// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

public class Inpttst : MonoBehaviour
{
    public InputManagement mng;
    string str = "none";
    
    // Update is called once per frame
    void Update()
    {
        if (mng.Axis2LeftStick() != Vector2.zero)
        {
            str = mng.Axis2LeftStick().ToString();
        }

        if (mng.ButtonB())
        {
            str = "b";
        }

        if (mng.ButtonY())
        {
            str = "y";
        }

        if (mng.ButtonX())
        {
            str = "x";
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 1000, 100), str);
    }
}
