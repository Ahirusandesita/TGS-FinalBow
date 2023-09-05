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

    private delegate void ReactionDelegate(Transform targetTransform,Vector3 hitPosition);
    private event ReactionDelegate ReactionEvent;
    private ReactionManager reactionManager = new ReactionManager();
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        myTransform = this.transform;
        reactionManager.AddReaction(this.GetComponents<INormalReaction<Transform, Vector3>>());
        reactionManager.AddReaction(this.GetComponents<IBombReaction<Transform, Vector3>>());
        reactionManager.AddReaction(this.GetComponents<IThunderReaction<Transform, Vector3>>());
        reactionManager.AddReaction(this.GetComponents<IKnockBackReaction<Transform, Vector3>>());
        reactionManager.AddReaction(this.GetComponents<IPenetrateReaction<Transform, Vector3>>());
        reactionManager.AddReaction(this.GetComponents<IHomingReaction<Transform, Vector3>>());
    }

    public void ReactionFactory(IReaction<Transform, Vector3> reaction) => this.reaction = reaction;

    public void AddReactionFactory(IReaction<Transform, Vector3> reaction) => reactions.Add(reaction);

    public void ReactionStart(Vector3 hitPosition)
    {
        if (reaction.ReactionEnd)
            reaction.Reaction(myTransform, hitPosition);
    }

    public void ReactionEventStart(Transform targetTransform,Vector3 hitPosition)
    {
        ReactionEvent(targetTransform, hitPosition);
        ReactionEvent = default;
    }
    public void AddReactionEvent(List<IReaction<Transform, Vector3>> reactions)
    {
        foreach(IReaction<Transform,Vector3> reaction in reactions)
        {
            ReactionEvent += new ReactionDelegate(reaction.Reaction);
        }
    }
    
    public void ReactionSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        reactionManager.ReactionSetting(enchantmentState,this);
    }
    #endregion
}