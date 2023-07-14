// --------------------------------------------------------- 
// TestLinkPropera.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TestLinkPropera : MonoBehaviour, IFProperaLinkObject
{
    #region variable 
    ProperaGimmick propera;

    [SerializeField] float speed = 10f;
    [SerializeField] float end_z = 100f;

    bool needPower = true;
    #endregion
    #region property
    #endregion
    #region method


    #endregion

    public bool GetNeedPower => needPower;

    public ProperaGimmick SetLinkObject { set => propera = value; }

    public void UsePowerAction(float power)
    {
        if(needPower)
        transform.Translate(0f, 0f, speed * power * Time.deltaTime);

        if(transform.position.z > end_z)
        {
            needPower = false;
            propera.SetElementIsNeedPowerLinkObjects(this);
        }
    }
}