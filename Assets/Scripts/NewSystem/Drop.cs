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


    ObjectPoolSystem ObjectPoolSystem;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        ObjectPoolSystem = GameObject.FindObjectOfType<ObjectPoolSystem>();
    }
    /// <summary>
    /// Drop
    /// </summary>
    public void DropStart(Vector3 spawnPosition)
    {
        for(int i = 0; i < _dropData.DropValue; i++)
        {
            ObjectPoolSystem.CallObject(PoolEnum.PoolObjectType.dropItem_1, spawnPosition);
        }
    }
    #endregion
}