// --------------------------------------------------------- 
// ReactionWormWarm.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ReactionWormWarm : MonoBehaviour, InterfaceReaction.IBombReaction
{

    [SerializeField] DropData dropData = default;
    IFNeedMoveRotineEnd needEnd = default;
    Animator anim = default;
    Drop drop = default;
    bool end = false;
    bool actioning = false;
    [SerializeField] GameObject particle;
    [SerializeField] Transform dropSpawnTransform;
    int dropValue = 4;
    public bool ReactionEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        drop = GetComponent<Drop>();
        needEnd = GetComponent<IFNeedMoveRotineEnd>();
    }
    public bool IsComplete()
    {
        return end;
    }

    public void Reaction(Transform t1, Vector3 t2)
    {
        if (actioning)
        {
            return;
        }
        actioning = true;
        needEnd.MoveEnd();
        particle.SetActive(true);
        anim.speed = 1;
        anim.SetTrigger("Death");
        anim.Update(0f);
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        drop.MiniDropStart(dropData, dropSpawnTransform, state.length, dropValue);
    }

    public void EndAnim()
    {
        particle.SetActive(false);
        anim.speed = 1;
        actioning = false;
        end = true;

    }

    private void OnDisable()
    {
        end = false;

    }

}