// --------------------------------------------------------- 
// ReactionMoveRay.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ReactionMoveRay
{
    #region variable 
    int mask = 1 << 13;
    float myColiderSize = default;
    Transform rayRoot = default;
    #endregion
    #region property
    #endregion
    #region method

    public ReactionMoveRay(Transform root,float coliderSize)
    {
        myColiderSize = coliderSize;

        rayRoot = root;
    }

    public bool HitCollision(Vector3 moveDistance)
    {
        Ray ray = new Ray(rayRoot.position, moveDistance);
        if (Physics.SphereCast(ray, myColiderSize, moveDistance.magnitude, mask))
        {
            return true;
        }
        return false;
    }

    #endregion
}