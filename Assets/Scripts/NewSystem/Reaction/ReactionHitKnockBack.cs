// --------------------------------------------------------- 
// ReactionHitKnockBack.cs 
// 
// CreateDay: 
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public interface IReaction
{
    void Reaction();
}
public interface IReaction<T1, T2>
{
    void Reaction(T1 t1, T2 t2);

    void AfterReaction(T1 t1, T2 t2);

    void OverReaction(T1 t1, T2 t2);

    bool ReactionEnd { get; set; }

    bool IsComplete();

}
public interface INormalReaction : IReaction<Transform, Vector3> { }
public interface IBombReaction : IReaction<Transform, Vector3> { }

public interface IThunderReaction : IReaction<Transform, Vector3> { }

public interface IKnockBackReaction: IReaction<Transform, Vector3> { }

public interface IPenetrateReaction : IReaction<Transform, Vector3> { }

public interface IHomingReaction : IReaction<Transform, Vector3> { }


public interface IReactionEnd
{
    void End();
}


[RequireComponent(typeof(Reaction))]
public class ReactionHitKnockBack : MonoBehaviour, IReaction<Transform, Vector3>, IReactionEnd
{

    public bool ReactionEnd { get; set; }
    [SerializeField]
    private Animator animator;

    public void Start()
    {
        ReactionEnd = true;
        this.GetComponent<Reaction>().ReactionFactory(this);
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