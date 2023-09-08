// --------------------------------------------------------- 
// ReactionDriver.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ReactionDriver : MonoBehaviour
{
    bool b = true;
    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            if (!b)
            {
                return;
            }
            InterfaceReaction.IReaction<Transform,Vector3> a = GetComponent<InterfaceReaction.IReaction<Transform,Vector3>>();

            a.Reaction(transform, transform.position);

            b = false;
            return;
        }
        b = true;
    }
}