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
    public CreateUsedArrow(ObjectPoolSystem pool)
    {
        SetPool = pool;
    }
    public ObjectPoolSystem SetPool
    {
        set { SetPool = value; }
        private get { return SetPool; }
    }

    public void SpawnArrow(Transform parent,Vector3 worldPosition,Quaternion arrowRotation)
    {
        Transform arrow = SetPool.CallObject
            (PoolEnum.PoolObjectType.usedArrow, worldPosition, arrowRotation).transform;

    }
    
}