// --------------------------------------------------------- 
// DropItemChild.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class DropItemChild : MonoBehaviour
{
    #region variable 
    private Transform _myTransform = default;
    #endregion
    #region property
    #endregion
    #region method
    private void Update()
    {
        _myTransform.eulerAngles = Vector3.zero;
    }

    private void Awake()
    {
        _myTransform = this.transform;
    }
    #endregion
}