// --------------------------------------------------------- 
// ProperaGimmickCallAction.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ProperaGimmickCallActionObject : MonoBehaviour, IFProperaLinkObject
{
    ProperaGimmick propera = default;

    bool needPower = true;

    [SerializeField]IFGimmickCallerUsePower actionClassUsePower = default;

    [SerializeField]IFGimmickCaller actionClass = default;

    [SerializeField] bool usePower = false;

    public bool GetNeedPower => needPower;

    public ProperaGimmick SetLinkObject { set => propera = value; }

    private void Start()
    {
        if (!usePower &&( actionClassUsePower is not null && actionClass is null))
        {
            actionClass = actionClassUsePower;
        }
    }
    public void UsePowerAction(float power)
    {
        if (usePower)
        {
            actionClassUsePower.GimmickAction(power);
            needPower = !actionClassUsePower.IsFinish;
        }
        else
        {
            actionClass.GimmickAction();
            needPower = !actionClass.IsFinish;
        }
    }
}