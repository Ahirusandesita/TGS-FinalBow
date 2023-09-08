// --------------------------------------------------------- 
// ReactionHitKnockBack.cs 
// 
// CreateDay: 
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

namespace InterfaceReaction
{
    public interface IReaction
    {
        void Reaction();
    }
    public interface IReaction<T1, T2>
    {
        void Reaction(T1 t1, T2 t2);

        bool ReactionEnd { get; set; }

        bool IsComplete();

    }
    public interface INormalReaction : IReaction<Transform, Vector3> { }
    public interface IBombReaction : IReaction<Transform, Vector3> { }

    public interface IThunderReaction : IReaction<Transform, Vector3> { }

    public interface IKnockBackReaction : IReaction<Transform, Vector3> { }

    public interface IPenetrateReaction : IReaction<Transform, Vector3> { }

    public interface IHomingReaction : IReaction<Transform, Vector3> { }


    public interface IReactionEnd
    {
        void End();
    }

    public interface IReactionFirst : IReaction<Transform, Vector3> { }

    public interface IAfterReaction : IReaction<Transform, Vector3> { }

    public interface IOverReaction : IReaction<Transform, Vector3> { }
}

[RequireComponent(typeof(Reaction))]
public class ReactionHitKnockBack : MonoBehaviour, InterfaceReaction.IReaction<Transform, Vector3>, InterfaceReaction.IReactionEnd
{

    public bool ReactionEnd { get; set; }
    [SerializeField]
    private Animator animator;

    public void Start()
    {
        ReactionEnd = true;
        animator = this.GetComponent<Animator>();
    }


    public void Reaction(Transform transform, Vector3 hitPosition)
    {
        ReactionEnd = false;
        animator.SetTrigger("HitKnockBack");
        //ノックバック処理
    }
    public void AfterReaction(Transform t1, Vector3 t2)
    {
        throw new System.NotImplementedException();
    }

    public void OverReaction(Transform t1, Vector3 t2)
    {
        throw new System.NotImplementedException();
    }

    public bool IsComplete() => true;
    public void End() => ReactionEnd = true;

}