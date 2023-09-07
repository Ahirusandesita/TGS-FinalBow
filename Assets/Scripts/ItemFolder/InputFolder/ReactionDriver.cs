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

    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            ReactionNormals a = GetComponent<ReactionNormals>();

            a.Reaction(transform, transform.forward);
        }
    }
}