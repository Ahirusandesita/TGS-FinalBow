// --------------------------------------------------------- 
// LinkWall.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class LinkWall : MonoBehaviour, IFProperaLinkObject
{
    ProperaGimmick propera;

    [SerializeField] float speed = 10f;
    [SerializeField] float end_z = 100f;
    private Vector3 startPos;
    public bool aaa = true;


    bool needPower = true;

    #region property
    public bool GetNeedPower => needPower;

    public ProperaGimmick SetLinkObject { set => propera = value; }
    #endregion
    #region method


    #endregion

    private void Start()
    {
        end_z = transform.position.x + end_z;
        startPos = this.transform.position;
    }

    public void UsePowerAction(float power)
    {
        if (needPower && aaa)
            transform.Translate(speed * power * Time.deltaTime, 0f, 0f);
        else if (needPower && !aaa)
            transform.Translate(-speed * power * Time.deltaTime, 0f, 0f);

        if (transform.position.x < startPos.x && aaa)
            transform.position = startPos;
        else if (transform.position.x > startPos.x && !aaa)
            transform.position = startPos;


        if (aaa)
        {
            if (transform.position.x > end_z)
            {
                needPower = false;
                propera.SetElementIsNeedPowerLinkObjects(this);
            }
        }
        else
        {
            if (transform.position.x < end_z)
            {
                needPower = false;
                propera.SetElementIsNeedPowerLinkObjects(this);
            }
        }
    }
}