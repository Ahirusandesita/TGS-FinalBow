// --------------------------------------------------------- 
// DropItem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

public static class DropFinalPositon
{
    public const float DROP_Y_FINALPOSITION = -1000f;
}

public interface IFItemMove
{
    bool CanMove { get; set; }
}

[RequireComponent(typeof(CashObjectInformation))]
public class DropItem : MonoBehaviour,IFItemMove
{
    #region variable 
    private float angle = default;

    private float vector = default;

    private float _dropSpeedMin = default;

    private float _dropSpeedMax = default;

    private Vector3 dropLotation = default;

    private Transform myTransform = default;

    //[SerializeField]
    //private float y_FinalPosition_Local = 0f;

    private float _moveSpeedMin = default;

    private float _moveSpeedMax = default;

    private float speed = default;

    private float downSpeed = default;

    public DropItemData DropItemData;

    #endregion
    #region property
    public bool CanMove { get; set; }
    #endregion
    #region method
    //public void SetAngle(float angle) => Angle = angle;
    //public void SetAngle(DropItemData dropItemData) => Angle = dropItemData.DropAngle;

    private void Start()
    {
        _dropSpeedMin = DropItemData.DropSpeedMin;
        _dropSpeedMax = DropItemData.DropSpeedMax;
        _moveSpeedMin = DropItemData.MoveSpeedMin;
        _moveSpeedMax = DropItemData.MoveSpeedMax;

        myTransform = this.transform;
        //y_FinalPosition_Local = myTransform.position.y - y_FinalPosition_Local;
        angle = -Random.Range(0f, DropItemData.DropAngle);
        vector = Random.Range(DropItemData.DropVectorMin, DropItemData.DropVectorMax);
        dropLotation.x = angle;
        dropLotation.y = vector;
        myTransform.rotation = Quaternion.Euler(dropLotation);
        downSpeed = Random.Range(_dropSpeedMin, _dropSpeedMax);
        speed = Random.Range(_moveSpeedMin, _moveSpeedMax);
    }

    private void Update()
    {
        if (myTransform.position.y < DropFinalPositon.DROP_Y_FINALPOSITION || !CanMove)
        {
            //Destroy(this);
            //DropItem dropItem = this;
            //dropItem = null;
            return;
        }
        myTransform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (dropLotation.x < 90f)
        {
            dropLotation.x += downSpeed * Time.deltaTime;
            myTransform.rotation = Quaternion.Euler(dropLotation);
        }



        //if (myTransform.position.y < y_FinalPosition_Local) return;
    }
    private void OnDisable()
    {
        CanMove = true;
    }



    #endregion
}