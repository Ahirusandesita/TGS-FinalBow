// --------------------------------------------------------- 
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
    private Drop drop;


    public bool ReactionEnd { get; set; }


    public void Reaction(Transform t1, Vector3 t2)
    {
        drop.DropStart(bodyChip, this.transform.position);
    }
    public void AfterReaction(Transform t1, Vector3 t2)
    {
        Debug.LogError("After");
    }

    public void OverReaction(Transform t1, Vector3 t2)
    {
        throw new System.NotImplementedException();
    }

    public bool IsComplete() => true;
    void Start()
    {
        drop = this.GetComponent<Drop>();
    }
   
}