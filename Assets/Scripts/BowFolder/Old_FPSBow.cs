// --------------------------------------------------------- 
// FPSBow.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class Old_FPSBow : Old_BowManager
{
    #region variable 
    [Header("ここからしたが使うやつ/このプレハブにはメインカメラ入っているんで")]
    [SerializeField] float useDistance = 0.1f;

    [SerializeField] GameObject inhall = default;

    [SerializeField] ObjectPoolSystem _poolSystemC = default;

    [SerializeField] PlayerManager _playerManagerC = default;

    [SerializeField] float addC = 100000f;

    [SerializeField] float moveSpeed = 10f;

    [SerializeField] Transform arrowPos = default;

    [SerializeField] CamaraRoteNotMainCamera _rote = default;

    AttractZone _attractC;

    CashObjectInformation _arrowC;

    Transform _myTrans;

    


    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {

        _attractC = GetComponent<AttractZone>();

        _myTrans = transform;
    }
    private void Update()
    {

        InputingMouse();
        
        WASDMove();
    }

    private void InputingMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Shot();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            _arrowC = _poolSystemC.CallObject(PoolEnum.PoolObjectType.arrow, arrowPos.position);

            _arrowC.transform.rotation = arrowPos.rotation;

            _arrowC.transform.parent = arrowPos;

            

        }
        else if (Input.GetMouseButton(0))
        {
            Charge();
        }

    }

    private void Charge()
    {
       
        inhall.SetActive(true);

        _attractC.SetAngle(useDistance);
    }

    private void Shot()
    {
        inhall.SetActive(false);

        _playerManagerC.SetArrowMoveSpeed(useDistance * addC);

        _playerManagerC.ShotArrow(arrowPos.forward - arrowPos.localPosition);

        _attractC.SetAngle(0f);
    }

    /// <summary>
    /// 雑に作ったからバグるよ:)
    /// </summary>
    private void WASDMove()
    {
        Vector3 foward = (arrowPos.position - _myTrans.position).normalized;

        


        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(moveSpeed * Time.deltaTime * foward,Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(moveSpeed * Time.deltaTime * - (Quaternion.Euler(0f,90f,0f) * foward),Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime * (Quaternion.Euler(0f,90f,0f) * foward), Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(moveSpeed * Time.deltaTime * -foward , Space.World);
        }

        _rote.CameraRote();
    }


    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position,transform.forward);
        Gizmos.DrawRay(ray);
    }
    #endregion
}