// --------------------------------------------------------- 
// TestShotPhysics.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestShotPhysics : MonoBehaviour, IFItemShoterObjectPhysics
{
    List<GameObject> shots = default;
    List<GameObject> deleteObjs = default;
    public void ItemMove()
    {
        foreach (GameObject obj in shots)
        {
            if (obj is not null)
            {
                obj.transform.Translate(Vector3.forward);

            }
            else
            {
                deleteObjs.Add(obj);
            }
        }

        //if(deleteObjs)
    }

    public void ItemMoveStart(GameObject[] Items)
    {
        foreach (GameObject obj in Items)
        {
            if (obj is not null)
                shots.Add(obj);
        }
    }

}