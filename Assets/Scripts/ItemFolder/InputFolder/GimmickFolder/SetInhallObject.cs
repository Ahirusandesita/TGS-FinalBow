// --------------------------------------------------------- 
// SetInhallObject.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class SetInhallObject : ObjectParent
{
    public override void ObjectAction()
    {
        GetComponent<ItemMove>()._isStart = true;
    }
}