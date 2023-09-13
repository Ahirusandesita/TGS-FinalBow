// --------------------------------------------------------- 
// BreakCrystalsReaction.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Drop))]
public class BreakCrystalsReaction : MonoBehaviour,InterfaceReaction.IBombReaction, InterfaceReaction.IHomingReaction, InterfaceReaction.IKnockBackReaction, InterfaceReaction.INormalReaction, InterfaceReaction.IPenetrateReaction,InterfaceReaction.IThunderReaction
{
    bool used = false;

    [SerializeField] GameObject breakObject;

    [SerializeField] Drop drop;

    [SerializeField] DropData data;

    [SerializeField] GameObject particle;

    [SerializeField] Collider col;

    [SerializeField]
    private AudioClip audioClip;

    private AudioSource audioSource;
    public bool ReactionEnd { get => used; set => used = value; }

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public bool IsComplete()
    {
        return used;
    }

    public void Reaction(Transform t1, Vector3 t2)
    {
        BreakEvent();
    }

    private void BreakEvent()
    {
        if (used)
        {
            return;
        }
        drop.DropStart(data, breakObject.transform.position);

        particle.SetActive(true);
        audioSource.PlayOneShot(audioClip);
        StartCoroutine(BreakSound());
        breakObject.SetActive(false);
        col.enabled = false;
    }

    private IEnumerator BreakSound()
    {
        yield return new WaitForSeconds(0.5f);
        used = true;
    }

}