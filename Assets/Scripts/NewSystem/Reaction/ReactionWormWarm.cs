// --------------------------------------------------------- 
// ReactionWormWarm.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ReactionWormWarm : MonoBehaviour,InterfaceReaction.IBombReaction
{
    Animator anim = default;
    bool end = false;
    [SerializeField] GameObject particle;
    public bool ReactionEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public bool IsComplete()
    {
        return end;
    }

    public void Reaction(Transform t1, Vector3 t2)
    {
        particle.SetActive(true);
        anim.speed = 1;
        anim.SetTrigger("Death");


    }

    public void EndAnim()
    {
        particle.SetActive(false);
        anim.speed = 1;
        end = true;

    }

    private void OnDisable()
    {
        end = false;
        
    }

}