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
    [SerializeField]
    private DropData bodyChip = default;
    private Drop drop;


    public bool ReactionEnd { get; set; }

    public void Reaction(Transform t1, Vector3 t2)
    {
        drop.DropStart(bodyChip, this.transform.position);
    }
    void Start()
    {
        drop = this.GetComponent<Drop>();
    }
   
}