// --------------------------------------------------------- 
// DropItem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

public static class DropFinalPositon
{
    public const float DROP_Y_FINALPOSITION = 0f;
}


public class DropItem : MonoBehaviour
{
    #region variable 
    private float angle = default;

    private float vector = default;

    [SerializeField]
    private float downForce = default;

    private Vector3 dropLotation = default;

    private Transform myTransform = default;

    [SerializeField]
    private float y_FinalPosition_Local = 0f;

    [SerializeField]
    private float speedMin = 0.1f;
    [SerializeField]
    private float speedMax = 0.1f;

    private float speed = 0.1f;

    public DropItemData DropItemData;

    #endregion
    #region property
    #endregion
    #region method
    //public void SetAngle(float angle) => Angle = angle;
    //public void SetAngle(DropItemData dropItemData) => Angle = dropItemData.DropAngle;

    private void Start()
    {
        myTransform = this.transform;
        y_FinalPosition_Local = myTransform.position.y - y_FinalPosition_Local;
        angle = -Random.Range(0f, DropItemData.DropAngle);
        vector = Random.Range(DropItemData.DropVectorMin, DropItemData.DropVectorMax);
        dropLotation.x = angle;
        dropLotation.y = vector;
        myTransform.rotation = Quaternion.Euler(dropLotation);
        speed = Random.Range(speedMin, speedMax);
    }

    private void Update()
    {
        if (myTransform.position.y < DropFinalPositon.DROP_Y_FINALPOSITION)
        {
            Destroy(this);
            DropItem dropItem = this;
            dropItem = null;
        }
        myTransform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (dropLotation.x < 90f)
        {
            dropLotation.x += downForce * Time.deltaTime;
            myTransform.rotation = Quaternion.Euler(dropLotation);
        }



        //if (myTransform.position.y < y_FinalPosition_Local) return;
    }



    #endregion
}