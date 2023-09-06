// --------------------------------------------------------- 
// TanTest.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TanTest : MonoBehaviour
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {

    }

    private void Start()
    {
        print("XXX  x 0 : y 0 = " + Mathf.Atan2(0, 0) * Mathf.Rad2Deg);

        print("x 0 : y 1 = " + Mathf.Atan2(0f, 1f) * Mathf.Rad2Deg);
        print("x 1 : y 1 = " + Mathf.Atan2(1f, 1f) * Mathf.Rad2Deg);
        print("x 1 : y 0 = " + Mathf.Atan2(1f, 0f) * Mathf.Rad2Deg);
        print("x 1 : y -1 = " + Mathf.Atan2(1f, -1f) * Mathf.Rad2Deg);
        print("x 0 : y -1 = " + Mathf.Atan2(0f, -1f) * Mathf.Rad2Deg);
        print("x -1 : y -1 = " + Mathf.Atan2(-1f, -1f) * Mathf.Rad2Deg);
        print("x -1 : y 0 = " + Mathf.Atan2(-1f, 0f) * Mathf.Rad2Deg);
        print("x -1 : y 1 = " + Mathf.Atan2(-1f, 1f) * Mathf.Rad2Deg);
    }

    private void Update()
    {

    }
    #endregion
}