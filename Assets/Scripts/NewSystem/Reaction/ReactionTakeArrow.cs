// --------------------------------------------------------- 
// ReactionTakeArrow.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
/// <summary>
/// ëÊàÍTransformÇÕenemyÇ≈ëÊìÒTransformÇÕÅ®
/// </summary>
public class ReactionTakeArrow : MonoBehaviour, InterfaceReaction.IReaction<Transform, Transform>
{
    ObjectPoolSystem pool = default;
    private void Start()
    {
        pool = FindObjectOfType<ObjectPoolSystem>();
    }
    public bool ReactionEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void AfterReaction(Transform t1, Transform t2)
    {
        
    }

    public bool IsComplete()
    {
        return true;
    }

    public void OverReaction(Transform t1, Transform t2)
    {
        
    }

    public void Reaction(Transform enemy, Transform arrow)
    {
        CreateUsedArrow create = new(pool);

        create.SpawnArrow(enemy, arrow.position, arrow.rotation);
    }
}