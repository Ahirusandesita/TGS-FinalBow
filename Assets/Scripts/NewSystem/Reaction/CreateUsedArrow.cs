// --------------------------------------------------------- 
// CreateUsedArrow.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class CreateUsedArrow
{
    private ObjectPoolSystem pool;
    public CreateUsedArrow(ObjectPoolSystem pool)
    {
        SetPool = pool;
    }
    public ObjectPoolSystem SetPool
    {
        set { pool = value; }
        private get { return pool; }
    }

    public void SpawnArrow(Transform parent,Vector3 worldPosition,Quaternion arrowRotation)
    {
        Transform arrow = SetPool.CallObject
            (PoolEnum.PoolObjectType.usedArrow, worldPosition, arrowRotation).transform;

        arrow.parent = parent;

        Debug.Log("aaa" + arrow.position);
    }
    
}