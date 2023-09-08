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
using YouYouLibrary.LoopSystem;
public class ReactionManager
{
    #region variable

    private List<InterfaceReaction.IReaction<Transform, Vector3>> normalReactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();
    private List<InterfaceReaction.IReaction<Transform, Vector3>> bombReactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();
    private List<InterfaceReaction.IReaction<Transform, Vector3>> thunderReactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();
    private List<InterfaceReaction.IReaction<Transform, Vector3>> knockBackReactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();
    private List<InterfaceReaction.IReaction<Transform, Vector3>> penetrateReactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();
    private List<InterfaceReaction.IReaction<Transform, Vector3>> homingReactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();

    private List<InterfaceReaction.IReaction<Transform, Vector3>> firstReactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();
    private List<InterfaceReaction.IReaction<Transform, Vector3>> afterReactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();
    private List<InterfaceReaction.IReaction<Transform, Vector3>> overReactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();
    #endregion
    #region property
    #endregion
    #region method
    public void ReactionOnlySetting(EnchantmentEnum.EnchantmentState enchantmentState,Reaction reaction)
    {
        switch (enchantmentState)
        {
            case EnchantmentEnum.EnchantmentState.normal: reaction.SetReactionOnlyEvent(normalReactions); break;
            case EnchantmentEnum.EnchantmentState.bomb: reaction.SetReactionOnlyEvent(bombReactions); break;
            case EnchantmentEnum.EnchantmentState.thunder: reaction.SetReactionOnlyEvent(thunderReactions); break;
            case EnchantmentEnum.EnchantmentState.rapidShots: reaction.SetReactionOnlyEvent(knockBackReactions); break;
            case EnchantmentEnum.EnchantmentState.penetrate: reaction.SetReactionOnlyEvent(penetrateReactions); break;
            case EnchantmentEnum.EnchantmentState.homing: reaction.SetReactionOnlyEvent(homingReactions); break;
        }
    }
    public void ReactionSetting(EnchantmentEnum.EnchantmentState enchantmentState, Reaction reaction)
    {
        switch (enchantmentState)
        {
            case EnchantmentEnum.EnchantmentState.normal: reaction.SetReactionEvent(normalReactions); break;
            case EnchantmentEnum.EnchantmentState.bomb: reaction.SetReactionEvent(bombReactions); break;
            case EnchantmentEnum.EnchantmentState.thunder: reaction.SetReactionEvent(thunderReactions); break;
            case EnchantmentEnum.EnchantmentState.rapidShots: reaction.SetReactionEvent(knockBackReactions); break;
            case EnchantmentEnum.EnchantmentState.penetrate: reaction.SetReactionEvent(penetrateReactions); break;
            case EnchantmentEnum.EnchantmentState.homing: reaction.SetReactionEvent(homingReactions); break;
        }
    }
    public void AddReactionSetting(EnchantmentEnum.EnchantmentState enchantmentState,Reaction reaction)
    {
        switch (enchantmentState)
        {
            case EnchantmentEnum.EnchantmentState.normal: reaction.AddReactionEvent(normalReactions); break;
            case EnchantmentEnum.EnchantmentState.bomb: reaction.AddReactionEvent(bombReactions); break;
            case EnchantmentEnum.EnchantmentState.thunder: reaction.AddReactionEvent(thunderReactions); break;
            case EnchantmentEnum.EnchantmentState.rapidShots: reaction.AddReactionEvent(knockBackReactions); break;
            case EnchantmentEnum.EnchantmentState.penetrate: reaction.AddReactionEvent(penetrateReactions); break;
            case EnchantmentEnum.EnchantmentState.homing: reaction.AddReactionEvent(homingReactions); break;
        }
    }




    private Action<InterfaceReaction.IReaction<Transform,Vector3>[], List<InterfaceReaction.IReaction<Transform, Vector3>>> SetReaction = (reactions, addReactions) =>
   {
       for (int i = 0; i < reactions.Length; i++)
       {
           addReactions.Add(reactions[i]);
       }
   };
    public void AddReaction(InterfaceReaction.INormalReaction[] reactions) => SetReaction(reactions, normalReactions);
    public void AddReaction(InterfaceReaction.IBombReaction[] reactions) => SetReaction(reactions, bombReactions);

    public void AddReaction(InterfaceReaction.IThunderReaction[] reactions) => SetReaction(reactions, thunderReactions);
    public void AddReaction(InterfaceReaction.IKnockBackReaction[] reactions) => SetReaction(reactions, knockBackReactions);
    public void AddReaction(InterfaceReaction.IPenetrateReaction[] reactions) => SetReaction(reactions, penetrateReactions);
    public void AddReaction(InterfaceReaction.IHomingReaction[] reactions) => SetReaction(reactions, homingReactions);


    public void AddReaction(InterfaceReaction.IReactionFirst[] reactions) => SetReaction(reactions, firstReactions);

    public void AddReaction(InterfaceReaction.IAfterReaction[] reactions) => SetReaction(reactions, afterReactions);
    public void AddReaction(InterfaceReaction.IOverReaction[] reactions) => SetReaction(reactions, overReactions);

    public List<InterfaceReaction.IReaction<Transform,Vector3>> GetEnchantReaction()
    {
        List<InterfaceReaction.IReaction<Transform, Vector3>> reactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();
        foreach (InterfaceReaction.IReaction<Transform, Vector3> reaction in normalReactions) reactions.Add(reaction);
        foreach (InterfaceReaction.IReaction<Transform, Vector3> rection in bombReactions) reactions.Add(rection);
        foreach (InterfaceReaction.IReaction<Transform, Vector3> rection in thunderReactions) reactions.Add(rection);
        foreach (InterfaceReaction.IReaction<Transform, Vector3> rection in knockBackReactions) reactions.Add(rection);
        foreach (InterfaceReaction.IReaction<Transform, Vector3> rection in penetrateReactions) reactions.Add(rection);
        foreach (InterfaceReaction.IReaction<Transform, Vector3> rection in homingReactions) reactions.Add(rection);
        return reactions;
    }
    public List<InterfaceReaction.IReaction<Transform, Vector3>> GetFirstReaction => firstReactions;
    public List<InterfaceReaction.IReaction<Transform, Vector3>> GetAfterReaction => afterReactions;
    public List<InterfaceReaction.IReaction<Transform, Vector3>> GetOverReaction => overReactions;
    #endregion
}