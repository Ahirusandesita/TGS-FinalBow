// --------------------------------------------------------- 
// Drop.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using YouYouLibrary.LoopSystem;

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
             objectPoolSystem.CallObject(dropData.PoolObjectType, spawnPosition);
        }
    }

    public void MiniDropStart(DropData dropData,Vector3 spawnPosition,float endTime,int spawnValue)
    {
        StartCoroutine(WaitDrop(dropData.PoolObjectType, spawnPosition, new WaitForSeconds(endTime / spawnValue), spawnValue));
       
    }

    public void MiniDropStart(DropData dropData, Transform spawnPosition, float endTime, int spawnValue)
    {
        StartCoroutine(WaitDrop(dropData.PoolObjectType, spawnPosition, new WaitForSeconds(endTime / spawnValue), spawnValue));

    }

    IEnumerator WaitDrop(PoolEnum.PoolObjectType objectType,Vector3 spawnPosition,WaitForSeconds wait,int value)
    {
        for(int i = 0; i < value; i++)
        {
            objectPoolSystem.CallObject(objectType, spawnPosition);
            yield return wait;
        }
    }

    IEnumerator WaitDrop(PoolEnum.PoolObjectType objectType, Transform spawnPosition, WaitForSeconds wait, int value)
    {
        for (int i = 0; i < value; i++)
        {
            objectPoolSystem.CallObject(objectType, spawnPosition.position);
            yield return wait;
        }
    }
    #endregion
}