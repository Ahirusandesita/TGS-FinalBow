// --------------------------------------------------------- 
// BabblePhysics.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BabblePhysics : MonoBehaviour, IFItemShoterObjectPhysics
{
    [SerializeReference] List<MoveItem> items = new List<MoveItem>();
    [SerializeReference] List<MoveItem> deleteObjs = new List<MoveItem>();
    [SerializeField] float upSpeed = 0.5f;
    [SerializeField] float reverceTime = 0.5f;
    protected struct MoveItem
    {
        public GameObject shot;
        public Vector3 moveVector;
        public float time;
    }
    public void ItemMove()
    {
        if (items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                MoveItem obj = items[i];
                if (obj.shot is not null)
                {

                    items[i] = Moving(ref obj);
                }
                else
                {
                    deleteObjs.Add(obj);
                }
            }

            if (deleteObjs.Count > 0)
            {
                foreach (MoveItem obj in deleteObjs)
                {
                    items.Remove(obj);
                }
            }

        }
    }

    public void ItemMoveStart(GameObject[] Items)
    {
        foreach (GameObject obj in Items)
        {
            if (obj is not null)
            {
                MoveItem mi = new MoveItem();
                mi.moveVector = Random.insideUnitCircle.normalized;
                mi.moveVector.z = mi.moveVector.y;
                mi.moveVector.y = 0f;
                mi.shot = obj;
                mi.time = Time.time;
                items.Add(mi);
            }

        }
    }

    protected virtual MoveItem Moving(ref MoveItem obj)
    {
        
        if (Time.time > obj.time + reverceTime)
        {
            obj.time = Time.time;

            obj.moveVector *= -1f;
        }
        
        obj.shot.transform.Translate((obj.moveVector + Vector3.up * upSpeed) * Time.deltaTime);
        return obj;
    }
}