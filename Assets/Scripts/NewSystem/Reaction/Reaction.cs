// --------------------------------------------------------- 
// Reaction.cs 
// 
// CreateDay: 
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Reaction : MonoBehaviour
{
    #region variable 
    private IReaction<Transform, Vector3> reaction;

    private List<IReaction<Transform, Vector3>> hitOnlyReactions;

    private List<IReaction<Transform, Vector3>> reactions = new List<IReaction<Transform, Vector3>>();

    private Transform myTransform;
    private delegate void ReactionFinishDelefate();
    private ReactionFinishDelefate ReactionFinish;

    private delegate void ReactionDelegate(Transform targetTransform, Vector3 hitPosition);
    private event ReactionDelegate ReactionEvent;

    private event ReactionDelegate AfterReactionEvent;
    private event ReactionDelegate OverReactionEvent;

    private delegate bool ReactionEndDelegate();
    private event ReactionEndDelegate ReactionEndEvent;
    private event ReactionEndDelegate AfterReactionEndEvent;
    private event ReactionEndDelegate OverReactionEndEvent;

    private Transform targetTransform;
    private Vector3 hitPosition;
    private bool isStart = false;

    private ReactionManager reactionManager = new ReactionManager();
    #endregion
    #region property
    #endregion
    #region method
    private void OnEnable()
    {
        ReactionEvent = null;
        AfterReactionEvent = null;
        OverReactionEvent = null;
        ReactionEndEvent = null;
        AfterReactionEndEvent = null;
        OverReactionEndEvent = null;
    }
    private void Start()
    {
        myTransform = this.transform;
        reactionManager.AddReaction(this.GetComponents<INormalReaction>());
        reactionManager.AddReaction(this.GetComponents<IBombReaction>());
        reactionManager.AddReaction(this.GetComponents<IThunderReaction>());
        reactionManager.AddReaction(this.GetComponents<IKnockBackReaction>());
        reactionManager.AddReaction(this.GetComponents<IPenetrateReaction>());
        reactionManager.AddReaction(this.GetComponents<IHomingReaction>());

        IReaction<Transform,Vector3>[] hitReactions = this.GetComponents<IReaction<Transform, Vector3>>();
        List<IReaction<Transform, Vector3>> reactions = reactionManager.GetEnchantReaction();
        List<IReaction<Transform, Vector3>> workReactions = new List<IReaction<Transform, Vector3>>();
        for (int i = 0; i < hitReactions.Length; i++)
        {
            workReactions.Add(hitReactions[i]);
        }
        for(int i = 0; i < reactions.Count; i++)
        {
            for(int k = 0; k < workReactions.Count; k++)
            {
                if (reactions[i] != workReactions[k]) hitOnlyReactions.Add(workReactions[k]);
            }
        }

    }

    public void ReactionFactory(IReaction<Transform, Vector3> reaction) => this.reaction = reaction;

    public void AddReactionFactory(IReaction<Transform, Vector3> reaction) => reactions.Add(reaction);

    public void ReactionStart(Vector3 hitPosition)
    {
        if (reaction.ReactionEnd)

        foreach (IReaction<Transform, Vector3> reaction in hitOnlyReactions)
        {
            reaction.Reaction(myTransform, hitPosition);
        }
    }


    Action<bool> ReactionSelect;
    Func<bool> ReactionEnd;

    public void ReactionEventStart(Transform targetTransform, Vector3 hitPosition)
    {
        this.targetTransform = targetTransform;
        this.hitPosition = hitPosition;


        if (ReactionEvent.GetLength() == 0)
        {
            if (ReactionFinish.GetLength() != 0)
                ReactionFinish();
            return;
        }

        ReactionEvent(targetTransform, hitPosition);
        ReactionEvent = null;
        OverReactionEvent = null;
        isStart = true;

        ReactionEnd = () =>
        {
            foreach (ReactionEndDelegate handler in ReactionEndEvent.GetInvocationList())
            {
                if (!handler.Invoke()) return false;
            }
            return true;
        };

        ReactionSelect = isStart =>
        {
            if (!isStart) return;

            if (!ReactionEnd()) return;

            if (AfterReactionEvent.GetLength() == 0) return;

            AfterReactionEvent(this.targetTransform, this.hitPosition);
            AfterReactionEvent = null;

            ReactionSelect = null;
            ReactionFinish();
            //action = isStart =>
            //{
            //    if (!isStart) return;

            //    foreach (ReactionEndDelegate handler in AfterReactionEndEvent.GetInvocationList())
            //    {
            //        if (!handler.Invoke()) return;
            //    }

            //    action = null;
            //};
        };
    }

    public void OverReactionEventStart(Transform targetTransform, Vector3 hitPosition)
    {
        this.targetTransform = targetTransform;
        this.hitPosition = hitPosition;

        if (OverReactionEvent.GetLength() == 0)
        {
            if (OverReactionEndEvent.GetLength() != 0)
                ReactionFinish();
            return;
        }


        OverReactionEvent(targetTransform, hitPosition);
        ReactionEvent = null;
        OverReactionEvent = null;
        isStart = true;

        ReactionEnd = () =>
        {
            foreach (ReactionEndDelegate handler in OverReactionEndEvent.GetInvocationList())
            {
                if (!handler.Invoke()) return false;
            }
            return true;
        };

        ReactionSelect = isStart =>
        {
            if (!isStart) return;

            if (!ReactionEnd()) return;

            if (AfterReactionEvent.GetLength() == 0) return;

            AfterReactionEvent(this.targetTransform, this.hitPosition);
            AfterReactionEvent = null;

            ReactionSelect = null;
            ReactionFinish();
        };
    }

    public void AddReactionEvent(List<IReaction<Transform, Vector3>> reactions)
    {
        foreach (IReaction<Transform, Vector3> reaction in reactions)
        {
            ReactionEvent += new ReactionDelegate(reaction.Reaction);
            ReactionEndEvent += new ReactionEndDelegate(reaction.IsComplete);
            AfterReactionEvent += new ReactionDelegate(reaction.AfterReaction);
            OverReactionEvent += new ReactionDelegate(reaction.OverReaction);
        }
        this.reactions = reactions;
    }


    public void ReactionSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        reactionManager.ReactionSetting(enchantmentState, this);
    }

    /// <summary>
    /// ここまだ正しく実装できてない
    /// </summary>
    /// <returns></returns>
    public bool IsReactionEnd()
    {
        if (ReactionEnd.GetLength() == 0) return false;
        return ReactionEnd();
    }

    public void SubscribeReactionFinish(Action reactionFinishAction) => ReactionFinish = new ReactionFinishDelefate(reactionFinishAction);

    private void Update()
    {



        if (Input.GetKeyDown(KeyCode.U))
        {
            ReactionSetting(EnchantmentEnum.EnchantmentState.bomb);
            ReactionEventStart(this.transform, Vector3.zero);
        }

        if (ReactionSelect.GetLength() == 0) return;
        ReactionSelect(isStart);
    }


    #endregion
}
