// -----------------
// ---------------------------------------- 
// BombHitReaction.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BombHitReaction : MonoBehaviour, IBombReaction
{
    Animator animator = default;
    [SerializeField]
    private DropData bodyChip = default;
    [SerializeField]
    private DropData bodyChipFire = default;
    [SerializeField]
    private DropData bodyChipBig = default;
    [SerializeField]
    private DropData bodyChipBigFire = default;

    private Drop drop;


    public bool ReactionEnd { get; set; }


    public void Reaction(Transform t1, Vector3 t2)
    {
        drop.DropStart(bodyChip, this.transform.position);
        drop.DropStart(bodyChipFire, this.transform.position);
        drop.DropStart(bodyChipBig, this.transform.position);
        drop.DropStart(bodyChipBigFire, this.transform.position);
    }
    public void AfterReaction(Transform t1, Vector3 t2)
    {

    }

    public void OverReaction(Transform t1, Vector3 t2)
    {

    }

    public bool IsComplete() => true;
    void Start()
    {
        drop = this.GetComponent<Drop>();
    }
   
}