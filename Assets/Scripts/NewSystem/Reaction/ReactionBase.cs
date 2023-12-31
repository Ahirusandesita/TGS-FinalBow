// --------------------------------------------------------- 
// ReactionBase.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Reaction))]
public class ReactionBase : MonoBehaviour, InterfaceReaction.IReaction<Transform, Vector3>, InterfaceReaction.IReactionEnd
{
    #region variable 
    #endregion
    #region property
    public bool ReactionEnd { get; set; }
    #endregion
    #region method

    private void Start()
    {
        this.GetComponent<Reaction>().AddReactionFactory(this);
    }

    public void Reaction(Transform t1, Vector3 t2)
    {

    }

    public void End() => ReactionEnd = true;

    public void AfterReaction(Transform t1, Vector3 t2)
    {
        throw new System.NotImplementedException();
    }

    public void OverReaction(Transform t1, Vector3 t2)
    {
        throw new System.NotImplementedException();
    }

    public bool IsComplete() => true;
    #endregion
}