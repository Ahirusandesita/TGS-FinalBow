// --------------------------------------------------------- 
// NewBehaviourScript.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BreakMato
{
    #region variable 
    IFItemMove[] moves;

    Transform transform;
    #endregion
    #region property
    #endregion
    #region method

    public BreakMato(Transform myTransform)
    {
        transform = myTransform;
        moves = transform.parent.GetComponents<IFItemMove>();
    }


    public void BreakStart()
    {
        foreach(IFItemMove i in moves)
        {
            i.SetParentNull();
            i.CanMove = true;
            
        }
    }
    #endregion
}