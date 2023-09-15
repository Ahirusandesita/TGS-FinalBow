// --------------------------------------------------------- 
// ReactionNormalsAnimationVer.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ReactionNormalsAnimationVer : MonoBehaviour, InterfaceReaction.INormalReaction, InterfaceReaction.IPenetrateReaction, InterfaceReaction.IKnockBackReaction
{
    Animator anim = default;

    bool _end = false;
    public bool ReactionEnd { get => _end; set => _end = value; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public bool IsComplete()
    {
        return ReactionEnd;
    }

    private void OnEnable()
    {
        _end = false;
    }

    public void Reaction(Transform t1, Vector3 t2)
    {
        anim.SetTrigger("Death");
        anim.Update(0f);
        AnimatorStateInfo info = anim.GetNextAnimatorStateInfo(0);
        StartCoroutine(ActionEnd(info.length));

    }

    private IEnumerator ActionEnd(float end)
    {
        WaitForSeconds wait = new WaitForSeconds(end);
        yield return wait;
        _end = true;
    }
}