// --------------------------------------------------------- 
// ReactionManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ReactionManager
{
    #region variable
    private List<IReaction<Transform, Vector3>> normalReactions = new List<IReaction<Transform, Vector3>>();
    private List<IReaction<Transform, Vector3>> bombReactions = new List<IReaction<Transform, Vector3>>();
    private List<IReaction<Transform, Vector3>> thunderReactions = new List<IReaction<Transform, Vector3>>();
    private List<IReaction<Transform, Vector3>> knockBackReactions = new List<IReaction<Transform, Vector3>>();
    private List<IReaction<Transform, Vector3>> penetrateReactions = new List<IReaction<Transform, Vector3>>();
    private List<IReaction<Transform, Vector3>> homingReactions = new List<IReaction<Transform, Vector3>>();
    #endregion
    #region property
    #endregion
    #region method

    public void ReactionSetting(EnchantmentEnum.EnchantmentState enchantmentState, Reaction reaction)
    {
        switch (enchantmentState)
        {
            case EnchantmentEnum.EnchantmentState.normal: reaction.AddReactionEvent(normalReactions); break;
            case EnchantmentEnum.EnchantmentState.bomb: reaction.AddReactionEvent(bombReactions); break;
            case EnchantmentEnum.EnchantmentState.thunder: reaction.AddReactionEvent(thunderReactions); break;
            case EnchantmentEnum.EnchantmentState.knockBack: reaction.AddReactionEvent(knockBackReactions); break;
            case EnchantmentEnum.EnchantmentState.penetrate: reaction.AddReactionEvent(penetrateReactions); break;
            case EnchantmentEnum.EnchantmentState.homing: reaction.AddReactionEvent(homingReactions); break;
        }
    }

    private Action<IReaction<Transform, Vector3>[], List<IReaction<Transform, Vector3>>> SetReaction = (reactions, addReactions) =>
   {
       for (int i = 0; i < reactions.Length; i++)
       {
           addReactions.Add(reactions[i]);
       }
   };
    public void AddReaction(INormalReaction<Transform, Vector3>[] reactions) => SetReaction(reactions, normalReactions);
    public void AddReaction(IBombReaction<Transform, Vector3>[] reactions) => SetReaction(reactions, bombReactions);

    public void AddReaction(IThunderReaction<Transform, Vector3>[] reactions) => SetReaction(reactions, thunderReactions);
    public void AddReaction(IKnockBackReaction<Transform, Vector3>[] reactions) => SetReaction(reactions, knockBackReactions);
    public void AddReaction(IPenetrateReaction<Transform, Vector3>[] reactions) => SetReaction(reactions, penetrateReactions);
    public void AddReaction(IHomingReaction<Transform, Vector3>[] reactions) => SetReaction(reactions, homingReactions);
    #endregion
}