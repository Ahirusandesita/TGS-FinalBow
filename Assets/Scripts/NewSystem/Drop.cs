// --------------------------------------------------------- 
// Drop.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

/// <summary>
/// Drop
/// </summary>
public class Drop : MonoBehaviour
{
    #region variable 
    public DropData _dropData;
    #endregion
    #region property
    #endregion
    #region method

    /// <summary>
    /// Drop
    /// </summary>
    public void DropStart()
    {
        for(int i = 0; i < _dropData.DropValue; i++)
        {
            GameObject dropItem = Instantiate(_dropData.DropItem.gameObject, this.transform.position, Quaternion.identity);
        }
    }
    #endregion
}