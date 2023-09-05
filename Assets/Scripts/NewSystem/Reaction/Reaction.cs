// --------------------------------------------------------- 
// Reaction.cs 
// 
// CreateDay: 
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Reaction : MonoBehaviour
{
    #region variable 
    private IReaction<Transform, Vector3> reaction;

    private List<IReaction<Transform, Vector3>> reactions = new List<IReaction<Transform, Vector3>>();

    private Transform myTransform;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        myTransform = this.transform;
    }

    public void ReactionFactory(IReaction<Transform, Vector3> reaction) => this.reaction = reaction;

    public void AddReactionFactory(IReaction<Transform, Vector3> reaction) => reactions.Add(reaction);

    public void ReactionStart(Vector3 hitPosition)
    {
        if (reaction.ReactionEnd)
            reaction.Reaction(myTransform, hitPosition);
    }

    public void ReactionEvent(Vector3 hitPosition)
    {
        for(int i = 0; i < reactions.Count; i++)
        {
            reactions[i].Reaction(myTransform, hitPosition);
        }
    }

    #endregion
}