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
public class ReactionTakeArrow : MonoBehaviour, IReaction<Transform, Vector3>
{
    ObjectPoolSystem pool = default;

    CreateUsedArrow create;
    private void Start()
    {
        pool = FindObjectOfType<ObjectPoolSystem>();
        create = new(pool);
    }
    public bool ReactionEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void AfterReaction(Transform t1, Vector3 t2)
    {
        
    }

    public bool IsComplete()
    {
        return true;
    }

    public void OverReaction(Transform t1, Vector3 t2)
    {
        
    }

    public void Reaction(Transform enemy, Vector3 arrow)
    {
        
        Quaternion rote = Quaternion.LookRotation(enemy.position - arrow);
        if(create is null)
        {
            create = new(pool);
        }
        create.SpawnArrow(enemy, arrow, rote);
    }
}