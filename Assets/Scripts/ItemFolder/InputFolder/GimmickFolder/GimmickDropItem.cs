// --------------------------------------------------------- 
// GimmickDropItem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public class GimmickDropItem : MonoBehaviour, IFGimmickCaller, IFUseEnchantGimmickTakeBomb, IFGetReactiveEvent
{
    [SerializeField] GameObject breakObject;

    [SerializeField] Drop drop;

    [SerializeField] DropData data;

    [SerializeField] GameObject particle;
    public bool IsFinish => used;

    public bool Moving => used;

    bool used = false;

    public void GimmickAction()
    {
        BreakGimmick();
    }


    public void TakeBomb()
    {
        BreakGimmick();
    }

    public void CallEvent()
    {
        BreakGimmick();
    }

    private void BreakGimmick()
    {
        drop.DropStart(data, breakObject.transform.position);

        breakObject.SetActive(false);

        particle.SetActive(true);

        used = true;
        
    }

}