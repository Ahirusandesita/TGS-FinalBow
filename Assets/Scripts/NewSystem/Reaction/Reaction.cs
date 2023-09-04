// --------------------------------------------------------- 
// Reaction.cs 
// 
// CreateDay: 
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;


public class Reaction : MonoBehaviour
{
    #region variable 
    private IReaction<Transform, Vector3> reaction;
    #endregion
    #region property
    #endregion
    #region method

    public void ReactionFactory(IReaction<Transform, Vector3> reaction) => this.reaction = reaction;

    public void ReactionStart(Vector3 hitPosition)
    {
        if (reaction.ReactionEnd)
            reaction.Reaction(this.transform, hitPosition);
    }

    #endregion
}