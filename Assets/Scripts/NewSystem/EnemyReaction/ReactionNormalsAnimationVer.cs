// --------------------------------------------------------- 
// ReactionNormalsAnimationVer.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ReactionNormalsAnimationVer : MonoBehaviour, InterfaceReaction.INormalReaction, InterfaceReaction.IPenetrateReaction, InterfaceReaction.IKnockBackReaction,InterfaceReaction.IHomingReaction
{
    Animator anim = default;
    IFNeedMoveRotineEnd need = default;
    [SerializeField] string actionTrigger = "Death";
    [SerializeField] float _endTime = 1.1f;
    [SerializeField] Transform effectPosition = default;
    ObjectPoolSystem pool = default;
    bool _end = false;
    public bool ReactionEnd { get => _end; set => _end = value; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        need = GetComponent<IFNeedMoveRotineEnd>();
        pool = FindObjectOfType<ObjectPoolSystem>();
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
        anim.SetTrigger("Test Death");

        if(need is not null)
        need.MoveEnd();
        //float time = 0f;
        anim.Update(0f);
        AnimatorStateInfo info = anim.GetNextAnimatorStateInfo(0);
        _endTime = info.length;

        StartCoroutine(ActionEnd(_endTime));

    }

    private IEnumerator ActionEnd(float end)
    {
        WaitForSeconds wait = new WaitForSeconds(end);
        yield return wait;
        GameObject ef = pool.CallObject(EffectPoolEnum.EffectPoolState.enemyDeath, effectPosition.position);
        pool.ResetActive(ef);
        _end = true;
    }
}