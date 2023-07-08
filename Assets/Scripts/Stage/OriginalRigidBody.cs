// --------------------------------------------------------- 
// OriginalRigidBody.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

/// <summary>
/// �I���W�i�����W�b�h�{�e�B�N���X
/// </summary>
public class OriginalRigidBody : MonoBehaviour
{
    #region variable 
    private HitZone _hitZone;

    private HitZone.HitDistanceScale _hitDistanceScale;

    private Transform _myTransform;

    private ContainObject _containObject;

    private Vector3 _myPosition;

    public GameObject c;
    #endregion
    #region property
    #endregion
    #region method



    private void Start()
    {
        _myTransform = this.transform;

        _hitDistanceScale._hitDistanceX = 3f;
        _hitDistanceScale._hitDistanceY = 3f;
        _hitDistanceScale._hitDistanceZ = 3f;

        _containObject = new ContainObject();

        Bounds bounds = GetComponent<Renderer>().bounds;
        Vector3 size = bounds.size;
        //_hitDistanceScale._hitDistanceX = 95.35f;
        //_hitDistanceScale._hitDistanceY = 23.3f;
        //_hitDistanceScale._hitDistanceZ = 96.41f;

        _hitDistanceScale._hitDistanceX = size.x / 2f;
        _hitDistanceScale._hitDistanceY = size.y / 2f;
        _hitDistanceScale._hitDistanceZ = size.z / 2f;


        _hitZone = new HitZone(_hitDistanceScale, _myTransform.position);

        Vector3[] vecs = _hitZone.GetHitZoneVertexPositions();

        for (int i = 0; i < vecs.Length; i++)
        {
            Instantiate(c, vecs[i], Quaternion.identity);
        }
        for (int i = 0; i < ContainObject.floors.Count; i++)
        {
            Debug.Log(ContainObject.floors[i]);
        }
    }

    private void Update()
    {
        _hitZone.SetPosition(_myTransform.position);

       

        for (int i = 0; i < _hitZone.Y_DownPosition().Length; i++)
        {
            if (_containObject.IsContainObjectFloor(_hitZone.Y_DownPosition()[i]))
            {
                _myPosition = _myTransform.position;
                _myPosition.y = _containObject.GetAdjustmentPosition_Floor().onePoint + _hitDistanceScale._hitDistanceY;
                _myTransform.position = _myPosition;
            }
        }

        //for(int i = 0; i < _hitZone.X_LeftPosition().Length; i++)
        //{
        //    if (_containObject.IsContainObjectWall(_hitZone.X_RightPositon()[i]))
        //    {
        //        _myPosition = _myTransform.position;
        //        _myPosition.x = _containObject.GetAdjustmentPosition_Wall().twoPoint - _hitDistanceScale._hitDistanceX;
        //        _myTransform.position = _myPosition;
        //    }
        //}
        //for (int i = 0; i < _hitZone.X_RightPositon().Length; i++)
        //{
        //    if (_containObject.IsContainObjectWall(_hitZone.X_LeftPosition()[i]))
        //    {
        //        _myPosition = _myTransform.position;
        //        _myPosition.x = _containObject.GetAdjustmentPosition_Wall().onePoint + _hitDistanceScale._hitDistanceX;
        //        _myTransform.position = _myPosition;
        //    }
        //}

    }
    #endregion
}