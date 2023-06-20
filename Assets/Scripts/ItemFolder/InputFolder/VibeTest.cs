// --------------------------------------------------------- 
// VibeTest.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class VibeTest : MonoBehaviour
{
 #region variable 
 #endregion
 #region property
 #endregion
 #region method

 private void Update ()
 {
        OVRInput.Update();

        if (OVRInput.Get(OVRInput.RawButton.A))
        {
            OVRInput.SetControllerVibration(1f, 1f, OVRInput.Controller.LTouch);
        }

        if (OVRInput.Get(OVRInput.RawButton.B))
        {
            OVRInput.SetControllerVibration(1f, 1f, OVRInput.Controller.LHand);
        }

        
 }
 #endregion
}