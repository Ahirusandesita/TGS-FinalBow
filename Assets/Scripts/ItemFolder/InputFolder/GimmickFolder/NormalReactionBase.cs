// --------------------------------------------------------- 
// ReactionTestTest.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class NormalReactionBase : MonoBehaviour, InterfaceReaction.IBombReaction
{

    #region variable 
    #endregion
    #region property
    #endregion
    #region method

    #endregion
    public bool ReactionEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void AfterReaction(Transform t1, Vector3 t2)
    {
        throw new System.NotImplementedException();
    }

    public void OverReaction(Transform t1, Vector3 t2)
    {
        throw new System.NotImplementedException();
    }

    public void Reaction(Transform t1, Vector3 t2)
    {
        throw new System.NotImplementedException();
    }

    public bool IsComplete() => true;
}
