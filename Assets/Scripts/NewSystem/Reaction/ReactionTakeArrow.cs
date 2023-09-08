// --------------------------------------------------------- 
// ReactionTakeArrow.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
/// <summary>
/// ‘æˆêTransform‚Íenemy‚Å‘æ“ñTransform‚Í¨
/// </summary>

public class ReactionTakeArrow : MonoBehaviour, InterfaceReaction.IReaction<Transform,Vector3>
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

    public void Reaction(Transform arrow, Vector3 arrowPos)
    {

        Quaternion rote = arrow.rotation;
        if(create is null)
        {
            create = new(pool);
        }
        create.SpawnArrow(this.transform, arrowPos, rote);
    }
}