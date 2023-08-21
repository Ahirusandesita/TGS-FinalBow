// --------------------------------------------------------- 
// TestShotPhysics.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestShotPhysics :MonoBehaviour, IFItemShoterObjectPhysics
{
    [SerializeReference] protected List<GameObject> shots = default;
    [SerializeReference] protected List<GameObject> deleteObjs = default;
    public void ItemMove()
    {
        foreach (GameObject obj in shots)
        {
            if (obj is not null)
            {

                Moving(obj);
            }
            else
            {
                deleteObjs.Add(obj);
            }
        }

        if(deleteObjs.Count > 0)
        {
            foreach(GameObject obj in deleteObjs)
            {
                shots.Remove(obj);
            }
        }
    }

    public void ItemMoveStart(GameObject[] Items)
    {
        foreach (GameObject obj in Items)
        {
            if (obj is not null)
                shots.Add(obj);
        }
    }

    protected virtual void Moving(GameObject obj)
    {
        obj.transform.Translate(Vector3.forward);
    }
}