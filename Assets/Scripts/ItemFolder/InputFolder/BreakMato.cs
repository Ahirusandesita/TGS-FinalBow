// --------------------------------------------------------- 
// NewBehaviourScript.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Drop))]
public class BreakMato:MonoBehaviour, InterfaceReaction.IBombReaction, InterfaceReaction.IHomingReaction, InterfaceReaction.IKnockBackReaction, InterfaceReaction.INormalReaction, InterfaceReaction.IPenetrateReaction, InterfaceReaction.IThunderReaction
{
    #region variable 
    IFItemMove[] moves;

    Transform _transrom;
    [SerializeField]
    private DropData dropData;
    [SerializeField]
    private GameObject particle;
    private Drop drop;
    #endregion
    #region property
    #endregion
    #region method
   
    bool used = false;
    private void Start()
    {
        moves = transform.parent.GetComponentsInChildren<IFItemMove>();
        drop = GetComponent<Drop>();
    }

    public bool ReactionEnd { get => used; set => used = value; }

    public void BreakStart()
    {
        particle.SetActive(true);
        foreach(IFItemMove i in moves)
        {
            
            i.CanMove = true;
            
        }
    }

    public bool IsComplete()
    {
        return used;
    }

    public void Reaction(Transform t1, Vector3 t2)
    {
        drop.DropStart(dropData, this.transform.position);
        BreakStart();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        used = true;
    }
    #endregion
}