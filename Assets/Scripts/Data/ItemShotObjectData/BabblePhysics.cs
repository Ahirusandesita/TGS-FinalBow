// --------------------------------------------------------- 
// BabblePhysics.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

public class BabblePhysics : MonoBehaviour, IFItemShoterObjectPhysics
{
    List<MoveItem> items = new List<MoveItem>();
    List<MoveItem> deleteObjs = new List<MoveItem>();

    /// <summary>
    /// �I�u�W�F�N�g���i�ރX�s�[�h
    /// </summary>
    [SerializeField] float upSpeed = 0.5f;
    /// <summary>
    /// ���������ς�鎞��
    /// </summary>
    [SerializeField] float reverceTime = 0.5f;
    /// <summary>
    /// ���ړ����x
    /// </summary>
    [SerializeField] float sideSpeed = 0.1f;
    protected struct MoveItem
    {
        public ItemMove shot;
        public Vector3 moveVector;
        public float time;
    }
    public void ItemMove()
    {
      
        if (items.Count > 0)
        {
            deleteObjs = new List<MoveItem>();
            for (int i = 0; i < items.Count; i++)
            {
                MoveItem obj = items[i];
                
                if (items[i].shot is not null && (items[i].shot._isStart is false && items[i].shot.gameObject.activeSelf))
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
                mi.moveVector = GenerateRandomVerticalVector(transform.forward);
                mi.shot = obj.GetComponent<ItemMove>();
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
        print(obj.shot.transform.position + "aaaaa");
        obj.shot.transform.Translate((obj.moveVector * sideSpeed + transform.forward * upSpeed) * Time.deltaTime);
        return obj;
    }

    private Vector3 GenerateRandomVerticalVector(Vector3 baseVector)
    {

        // �����_���Ȋp�x�𐶐�
        float randomAngle = Random.Range(0, 360);

        // ��x�N�g���𐳋K��
        Vector3 normalizedBaseVector = baseVector.normalized;

        Vector3 cross = Vector3.up;

        if (normalizedBaseVector == Vector3.up || normalizedBaseVector == Vector3.down)
        {
            cross = Vector3.right;
        }
        // �����_���Ȋp�x�Ɋ�Â��ĉ�]�s��𐶐�
        Quaternion rotation = Quaternion.AngleAxis(randomAngle, normalizedBaseVector);

        // ��]�s����x�N�g���ɓK�p���Đ����x�N�g���𓾂�
        Vector3 randomVerticalVector = rotation * Vector3.Cross(normalizedBaseVector, cross);

        return randomVerticalVector;

    }
}