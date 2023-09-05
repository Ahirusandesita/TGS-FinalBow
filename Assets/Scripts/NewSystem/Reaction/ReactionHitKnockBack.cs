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

    bool ReactionEnd { get; set; }

}
public interface INormaReaction<T1,T2> : IReaction<T1, T2> { }
public interface IBombReaction<T1,T2> : IReaction<T1, T2> { }

public interface IThunderReaction<T1, T2> : IReaction<T1, T2> { }

public interface IKnockBackReaction<T1, T2> : IReaction<T1, T2> { }

public interface IPenetrateReaction<T1, T2> : IReaction<T1, T2> { }

public interface IHomingReaction<T1, T2> : IReaction<T1, T2> { }


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

    public void End() => ReactionEnd = true;


}