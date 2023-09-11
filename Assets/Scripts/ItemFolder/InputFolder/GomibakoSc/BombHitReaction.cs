// -----------------
// ---------------------------------------- 
// BombHitReaction.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BombHitReaction : MonoBehaviour, InterfaceReaction.IBombReaction
{
    Animator animator = default;
    //[SerializeField]
    //private DropData bodyChip = default;
    //[SerializeField]
    //private DropData bodyChipFire = default;
    //[SerializeField]
    //private DropData bodyChipBig = default;
    //[SerializeField]
    //private DropData bodyChipBigFire = default;

    //private Drop drop;
    [SerializeField] GameObject particle;
    [SerializeField] GameObject body;
    [SerializeField] Collider[] colliders;

    bool end = false;

    public bool ReactionEnd { get => end; set => end = value; }


    public void Reaction(Transform t1, Vector3 t2)
    {
        //drop.DropStart(bodyChip, this.transform.position);
        //drop.DropStart(bodyChipFire, this.transform.position);
        //drop.DropStart(bodyChipBig, this.transform.position);
        //drop.DropStart(bodyChipBigFire, this.transform.position);
        particle.SetActive(true);
        body.SetActive(false);
        foreach(Collider col in colliders)
        {
            col.enabled = false;
        }
        StartCoroutine(Wait());
    }

    public bool IsComplete() => end;
    void Start()
    {
        //drop = this.GetComponent<Drop>();
    }
   
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        end = true;
    }

    private void OnDisable()
    {
        particle.SetActive(false);
        body.SetActive(true);
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
    }
}