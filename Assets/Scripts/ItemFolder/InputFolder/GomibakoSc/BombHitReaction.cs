// --------------------------------------------------------- 
// BombHitReaction.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BombHitReaction : MonoBehaviour, IBombReaction<Transform,Vector3>
{
    Animator animator = default;
    public bool ReactionEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Reaction(Transform t1, Vector3 t2)
    {
        print("‚ ‚ ‚ ‚ ‚ ‚ ‚ ");
        //animator.SetTrigger("Dead");
    }
   
}