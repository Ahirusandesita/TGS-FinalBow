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
    private ObjectPoolSystem objectPoolSystem;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        objectPoolSystem = GameObject.FindWithTag(InhallLibTags.PoolSystem).GetComponent<ObjectPoolSystem>();
    }
    /// <summary>
    /// Drop
    /// </summary>
    public void DropStart(DropData dropData,Vector3 spawnPosition)
    {
        for(int i = 0; i < dropData.DropValue; i++)
        {
            // objectPoolSystem.CallObject(PoolEnum.PoolObjectType.dropItem_1, spawnPosition);
            Instantiate(dropData.DropItem,spawnPosition,Quaternion.identity);
        }
    }
    #endregion
}