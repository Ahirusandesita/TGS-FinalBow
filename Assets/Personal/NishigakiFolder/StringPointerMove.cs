// --------------------------------------------------------- 
// StringPointerMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class StringPointerMove : MonoBehaviour
{
    #region variable 
    private Transform Player;
    #endregion
    #region property
    #endregion
    #region method
    private void Awake()
    {
        Player = GameObject.Find("RightEyeAnchor").transform;
    }

    private void Update()
    {
        this.transform.LookAt(Player);
    }

    #endregion
}