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
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        _myTransform = this.transform;
    }

    private void Update()
    {
        _myTransform.rotation = Quaternion.identity;
    }
    #endregion
}