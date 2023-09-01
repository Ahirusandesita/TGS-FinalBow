// --------------------------------------------------------- 
// GimmickDropItem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class GimmickDropItem : MonoBehaviour, IFGimmickCaller
{
    [SerializeField] GameObject breakObject;

    [SerializeField] Drop drop;

    [SerializeField] DropData data;
    public bool IsFinish => used;

    public bool Moving => used;

    bool used = false;

    public void GimmickAction()
    {
        drop.DropStart(data, breakObject.transform.position);

        breakObject.SetActive(false);

        used = true;
    }
}