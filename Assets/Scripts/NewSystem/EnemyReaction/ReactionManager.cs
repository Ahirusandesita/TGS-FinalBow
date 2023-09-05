// --------------------------------------------------------- 
// ReactionManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReactionManager
{
    #region variable
    public List<IReaction<Transform, Vector3>> normalReactions = new List<IReaction<Transform, Vector3>>();
    public List<IReaction<Transform, Vector3>> bombReactions = new List<IReaction<Transform, Vector3>>();
    public List<IReaction<Transform, Vector3>> thunderReactions = new List<IReaction<Transform, Vector3>>();
    public List<IReaction<Transform, Vector3>> knockBackReactions = new List<IReaction<Transform, Vector3>>();
    public List<IReaction<Transform, Vector3>> penetrateReactions = new List<IReaction<Transform, Vector3>>();
    public List<IReaction<Transform, Vector3>> homingReactions = new List<IReaction<Transform, Vector3>>();
    #endregion
    #region property
    #endregion
    #region method

    public void ReactionSetting(EnchantmentEnum.EnchantmentState enchantmentState,Reaction reaction)
    {
        switch (enchantmentState)
        {
            case EnchantmentEnum.EnchantmentState.normal: reaction.AddReactionEvent(normalReactions);break;
            case EnchantmentEnum.EnchantmentState.bomb:reaction.AddReactionEvent(bombReactions);break;
            case EnchantmentEnum.EnchantmentState.thunder:reaction.AddReactionEvent(thunderReactions);break;
            case EnchantmentEnum.EnchantmentState.knockBack:reaction.AddReactionEvent(knockBackReactions);break;
            case EnchantmentEnum.EnchantmentState.penetrate:reaction.AddReactionEvent(penetrateReactions);break;
            case EnchantmentEnum.EnchantmentState.homing:reaction.AddReactionEvent(homingReactions);break;
        }
    }
    #endregion
}