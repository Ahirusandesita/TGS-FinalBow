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
    private InterfaceReaction.IReaction<Transform, Vector3> reaction;

    private List<InterfaceReaction.IReaction<Transform, Vector3>> hitOnlyReactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();

    private List<InterfaceReaction.IReaction<Transform, Vector3>> reactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();

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
        reactionManager.AddReaction(this.GetComponents<InterfaceReaction.INormalReaction>());
        reactionManager.AddReaction(this.GetComponents<InterfaceReaction.IBombReaction>());
        reactionManager.AddReaction(this.GetComponents<InterfaceReaction.IThunderReaction>());
        reactionManager.AddReaction(this.GetComponents<InterfaceReaction.IKnockBackReaction>());
        reactionManager.AddReaction(this.GetComponents<InterfaceReaction.IPenetrateReaction>());
        reactionManager.AddReaction(this.GetComponents<InterfaceReaction.IHomingReaction>());

        reactionManager.AddReaction(this.GetComponents<InterfaceReaction.IReactionFirst>());
        reactionManager.AddReaction(this.GetComponents<InterfaceReaction.IAfterReaction>());
        reactionManager.AddReaction(this.GetComponents<InterfaceReaction.IOverReaction>());

        InterfaceReaction.IReaction<Transform, Vector3>[] hitReactions = this.GetComponents<InterfaceReaction.IReaction<Transform, Vector3>>();
        List<InterfaceReaction.IReaction<Transform, Vector3>> reactions = reactionManager.GetEnchantReaction();
        List<InterfaceReaction.IReaction<Transform, Vector3>> workReactions = new List<InterfaceReaction.IReaction<Transform, Vector3>>();
        for (int i = 0; i < hitReactions.Length; i++)
        {
            workReactions.Add(hitReactions[i]);
        }

        for (int i = 0; i < workReactions.Count; i++)
        {
            bool isHitOnlyReaction = true;
            for (int k = 0; k < reactions.Count; k++)
            {
                if (workReactions[i] == reactions[k])
                {
                    isHitOnlyReaction = false;
                    continue;
                }
            }

            if (isHitOnlyReaction)
                hitOnlyReactions.Add(workReactions[i]);
        }

    }

    public void ReactionFactory(InterfaceReaction.IReaction<Transform, Vector3> reaction) => this.reaction = reaction;

    public void AddReactionFactory(InterfaceReaction.IReaction<Transform, Vector3> reaction) => reactions.Add(reaction);

    public void ReactionStart(Vector3 hitPosition)
    {
        //if (reaction.ReactionEnd)

        foreach (InterfaceReaction.IReaction<Transform, Vector3> reaction in hitOnlyReactions)
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

            if (AfterReactionEvent.GetLength() == 0)
            {
                ReactionFinish();
                ReactionSelect = null;
                ReactionEnd = null;
                return;
            }

            AfterReactionEvent(this.targetTransform, this.hitPosition);
            AfterReactionEvent = null;

            ReactionSelect = null;
            ReactionEnd = () =>
            {
                foreach (ReactionEndDelegate handler in AfterReactionEndEvent.GetInvocationList())
                {
                    if (!handler.Invoke()) return false;
                }
                return true;
            };


            ReactionSelect = isStart =>
            {
                if (!isStart) return;

                if (!ReactionEnd()) return;

                ReactionFinish();
                ReactionSelect = null;
                ReactionEnd = null;
            };
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

            if (AfterReactionEvent.GetLength() == 0)
            {
                ReactionFinish();
                ReactionSelect = null;
                return;
            }

            AfterReactionEvent(this.targetTransform, this.hitPosition);
            AfterReactionEvent = null;

            ReactionSelect = null;
        };


        ReactionEnd = () =>
        {
            foreach (ReactionEndDelegate handler in AfterReactionEndEvent.GetInvocationList())
            {
                if (!handler.Invoke()) return false;
            }
            return true;
        };


        ReactionSelect = isStart =>
        {
            if (!isStart) return;

            if (!ReactionEnd()) return;

            ReactionFinish();
            ReactionSelect = null;
            ReactionEnd = null;
        };
    }

    public void AddReactionEvent(List<InterfaceReaction.IReaction<Transform, Vector3>> reactions)
    {
        ReactionEvent = null;
        AfterReactionEvent = null;
        OverReactionEvent = null;
        ReactionEndEvent = null;
        AfterReactionEndEvent = null;
        OverReactionEndEvent = null;

        foreach (InterfaceReaction.IReaction<Transform, Vector3> reaction in reactions)
        {
            if (reaction == reactionManager.GetFirstReaction)
            {
                ReactionEvent += new ReactionDelegate(reaction.Reaction);
                ReactionEndEvent += new ReactionEndDelegate(reaction.IsComplete);
            }
            else if (reaction == reactionManager.GetAfterReaction)
            {
                AfterReactionEvent += new ReactionDelegate(reaction.Reaction);
                AfterReactionEndEvent += new ReactionEndDelegate(reaction.IsComplete);
            }
            else if (reaction == reactionManager.GetOverReaction)
            {
                OverReactionEvent += new ReactionDelegate(reaction.Reaction);
                OverReactionEndEvent += new ReactionEndDelegate(reaction.IsComplete);
            }
            else
            {
                ReactionEvent += new ReactionDelegate(reaction.Reaction);
                ReactionEndEvent += new ReactionEndDelegate(reaction.IsComplete);
            }
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
