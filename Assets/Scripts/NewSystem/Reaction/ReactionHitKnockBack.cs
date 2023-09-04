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
public interface IReactionEnd
{
    void End();
}


[RequireComponent(typeof(Reaction))]
public class ReactionHitKnockBack : MonoBehaviour, IReaction<Transform, Vector3>,IReactionEnd
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
        X_Debug.Log("BBBBBBBBBBBBBBBB");
    }

    public void End() => ReactionEnd = true;




}