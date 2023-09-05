// --------------------------------------------------------- 
// ReactionBase.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Reaction))]
public class ReactionBase : MonoBehaviour, IReaction<Transform, Vector3>,IReactionEnd
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
    #endregion
}