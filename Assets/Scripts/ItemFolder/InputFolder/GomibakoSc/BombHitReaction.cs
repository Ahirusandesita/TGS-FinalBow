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


    public bool ReactionEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void AfterReaction(Transform t1, Vector3 t2)
    {
        throw new System.NotImplementedException();
    }

    public void OverReaction(Transform t1, Vector3 t2)
    {
        throw new System.NotImplementedException();
    }

    public void Reaction(Transform t1, Vector3 t2)
    {
        drop.DropStart(bodyChip, this.transform.position);
    }
    void Start()
    {
        drop = this.GetComponent<Drop>();
    }
   
}