// --------------------------------------------------------- 
// PhenixAnimationPlayerDriver.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class PhenixAnimationPlayerDriver : MonoBehaviour
{
    #region variable 
    public Animation anm;
 #endregion
 #region property
 #endregion
 #region method
 
 private void Awake()
 {

 }
 
 private void Start ()
 {
        anm.Play();
 }

 private void Update ()
 {

 }
 #endregion
}