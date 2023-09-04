// --------------------------------------------------------- 
// PlayerNearVani.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class PlayerNearVani : MonoBehaviour
{
    #region variable 

    [SerializeField]Renderer[] components;
    [SerializeField] Collider col;
    [SerializeField]Transform player;
    
 #endregion
 #region property
 #endregion
 #region method
 


 private void Update ()
 {
        if (Vector3.Distance(player.position, transform.position) < 5)
        {
            foreach(Renderer c in components)
            {
                c.enabled = false;
            }
            col.enabled = false;

        }
        else if(col.enabled == false)
        {
            foreach (Renderer c in components)
            {
                c.enabled = true;
            }
            col.enabled = true;
        }
 }


 #endregion
}