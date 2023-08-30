// --------------------------------------------------------- 
// nake.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class nake : MonoBehaviour
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method

    [SerializeField]
    GameObject nage = default;
 
 private void Awake()
 {

 }
 
 private void Start ()
 {

 }

 private void Update ()
 {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(nage, this.gameObject.transform.position, Quaternion.identity, null);
        }
 }
 #endregion
}