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



public class ReactionHitKnockBack : MonoBehaviour, IReaction<Transform, Vector3>
{

    public bool ReactionEnd { get; set; }

    public void Start()
    {
        this.GetComponent<Reaction>().ReactionFactory(this);
    }


    public void Reaction(Transform transform, Vector3 hitPosition)
    {
        //ノックバック処理
    }

}