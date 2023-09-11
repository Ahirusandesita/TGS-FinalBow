// --------------------------------------------------------- 
// DropItem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

public static class DropFinalPositon
{
    public const float DROP_Y_FINALPOSITION = -100f;
}

public interface IFItemMove
{
    bool CanMove { get; set; }

    void SetParentNull();
}

[RequireComponent(typeof(CashObjectInformation))]
public class DropItem : MonoBehaviour, IFItemMove
{
    #region variable 
    private float angle = default;

    private float vector = default;

    private float _dropSpeedMin = default;

    private float _dropSpeedMax = default;

    private Vector3 dropLotation = default;

    private Transform myTransform = default;

    private enum ItemType { Fall = 0, Fly = 1 }

    //[SerializeField]
    //private float y_FinalPosition_Local = 0f;

    private float _moveSpeedMin = default;

    private float _moveSpeedMax = default;

    private float speed = default;

    private float downSpeed = default;

    public DropItemData DropItemData;

    [SerializeField] bool playAwake = true;

    bool _canMove = false;
    #endregion
    #region property
    public bool CanMove
    {
        get => _canMove; 

        set
        {
            _canMove = value;
            if (_canMove is true) { myTransform.rotation = Quaternion.Euler(dropLotation); }
        }
    }
    #endregion
    #region method
    //public void SetAngle(float angle) => Angle = angle;
    //public void SetAngle(DropItemData dropItemData) => Angle = dropItemData.DropAngle;

    private void Start()
    {
        //y_FinalPosition_Local = myTransform.position.y - y_FinalPosition_Local;
        _canMove = playAwake;
    }

    private void Update()
    {
        switch (DropItemData.dropItemStruct.itemType)
        {
            case DropItemStruct.ItemType.Fall:
                FallMove();
                break;

            case DropItemStruct.ItemType.Fly:
                FlyMove();
                break;
        }



        //if (myTransform.position.y < y_FinalPosition_Local) return;
    }

    private void OnEnable()
    {
        _dropSpeedMin = DropItemData.dropItemStruct.DropSpeedMin;
        _dropSpeedMax = DropItemData.dropItemStruct.DropSpeedMax;
        _moveSpeedMin = DropItemData.dropItemStruct.MoveSpeedMin;
        _moveSpeedMax = DropItemData.dropItemStruct.MoveSpeedMax;

        myTransform = this.transform;

        angle = -Random.Range(0f, DropItemData.dropItemStruct.DropAngle);
        vector = Random.Range(DropItemData.dropItemStruct.DropVectorMin, DropItemData.dropItemStruct.DropVectorMax);
        dropLotation.x = angle;
        dropLotation.y = vector;
        if (playAwake)
        {
            myTransform.rotation = Quaternion.Euler(dropLotation);

        }
        downSpeed = Random.Range(_dropSpeedMin, _dropSpeedMax);
        speed = Random.Range(_moveSpeedMin, _moveSpeedMax);
    }

    private void OnDisable()
    {
        CanMove = true;
    }
    public void SetDropItemData(DropItemStruct dropItemStruct) => DropItemData.dropItemStruct = dropItemStruct;

    private void FallMove()
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
    }

    private void FlyMove()
    {
        if (speed > 0)
        {
            speed -= Time.deltaTime * DropItemData.dropItemStruct.DownSpeedValue;
            myTransform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            return;
        }
    }

    public void SetParentNull()
    {
        transform.parent = null;
    }

    #endregion
}