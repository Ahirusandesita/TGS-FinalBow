// --------------------------------------------------------- 
// EnchantUIPosition.cs 
// 
// CreateDay: 2023/06/29
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
public class EnchantUIPosition : MonoBehaviour
{
    #region variable 
    Transform _myTransform = default;
    Quaternion _myQuaternion;
    Transform _main;
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        _myTransform = this.transform;
        _main = Camera.main.transform;
    }

    private void Update()
    {
        _myQuaternion = Quaternion.LookRotation(_main.position,Vector3.up);
        _myQuaternion.z = 0f;
        _myTransform.rotation = _myQuaternion;
    }
    #endregion
}