// --------------------------------------------------------- 
// OriginalRigidBody.cs 
// 
// CreateDay: 2023/07/9
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

/// <summary>
/// オリジナルリジッドボティクラス
/// </summary>
public class OriginalRigidBody : MonoBehaviour
{
    #region variable 
    public GameObject c;

    private HitZone _hitZone;

    private HitZone.HitDistanceScale _hitDistanceScale;

    private Transform _myTransform;

    private ContainObject _containObject;

    private Vector3 _myPosition;

    private OriginalMonoBehaviour[] _originalMonoBehaviours = default;

    private bool _isCollision = false;
    private bool[,] _isHits =
    {
        {false,false },//Y軸　Up、Down
        {false,false },//X軸　Left、Right
        {false,false },//Z軸　Forward、Back
    };

    private bool[,] _needCollisions =
    {
        {false,false },//Y軸　Up、Down
        {false,false },//X軸　Left、Right
        {false,false },//Z軸　Forward、Back
    };

    private delegate void OriginalCollision();

    OriginalCollision _originalCollision = default;

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

        _originalMonoBehaviours = this.GetComponents<OriginalMonoBehaviour>();

        Vector3[] vecs = _hitZone.GetHitZoneVertexPositions();

        for (int i = 0; i < vecs.Length; i++)
        {
            Instantiate(c, vecs[i], Quaternion.identity).transform.parent = this.transform;
        }

    }

    private void LateUpdate()
    {
        _hitZone.SetPosition(_myTransform.position);

        //if (_containObject.IsContainObjectFloor3(_myPosition,new Vector3(_hitDistanceScale._hitDistanceX,_hitDistanceScale._hitDistanceY,_hitDistanceScale._hitDistanceZ)))
        //{
        //    _myPosition = _myTransform.position;
        //    if (_containObject.GetAdjustmentY(_myPosition) != 0f)
        //    {
        //        _myPosition.y = _containObject.GetAdjustmentY(_myPosition) + _hitDistanceScale._hitDistanceY;
        //        _myTransform.position = _myPosition;
        //        if (!_needCollisions[0, 1])
        //        {
        //            _isHits[0, 1] = true;
        //            _needCollisions[0, 1] = true;
        //        }
        //    }
        //}
        //else
        //{
        //    _needCollisions[0, 1] = false;
        //}


        //軸ごとに埋まりこみ防止制御を行う
        for (int i = 0; i < _hitZone.Y_DownPosition().Length; i++)
        {
            if (_containObject.IsContainObjectFloor(_hitZone.Y_DownPosition()[i]))
            {
                _myPosition = _myTransform.position;
                if (_containObject.GetAdjustmentY(_myPosition) != 0f)
                {
                    _myPosition.y = _containObject.GetAdjustmentY(_myPosition) + _hitDistanceScale._hitDistanceY;
                    _myTransform.position = _myPosition;
                    if (!_needCollisions[0, 1])
                    {
                        _isHits[0, 1] = true;
                        _needCollisions[0, 1] = true;
                    }
                }
            }
            else
            {
                _needCollisions[0, 1] = false;
            }
        }

        for (int i = 0; i < _hitZone.Y_UpPosition().Length; i++)
        {
            if (_containObject.IsContainObjectFloor(_hitZone.Y_UpPosition()[i]))
            {
                _myPosition = _myTransform.position;
                if (_containObject.GetAdjustmentY(_myPosition) != 0f)
                {
                    _myPosition.y = _containObject.GetAdjustmentY(_myPosition) - _hitDistanceScale._hitDistanceY;
                    _myTransform.position = _myPosition;
                    if (!_needCollisions[0, 0])
                    {
                        _isHits[0, 0] = true;
                        _needCollisions[0, 0] = true;
                    }
                }
            }
            else
            {
                _needCollisions[0, 0] = false;
            }
        }

        for (int i = 0; i < _hitZone.X_LeftPosition().Length; i++)
        {
            if (_containObject.IsContainObjectFloor(_hitZone.X_LeftPosition()[i]))
            {
                _myPosition = _myTransform.position;
                if (_containObject.GetAdjustmentX(_myPosition) != 0f)
                {
                    _myPosition.x = _containObject.GetAdjustmentX(_myPosition) + _hitDistanceScale._hitDistanceX;
                    _myTransform.position = _myPosition;
                    if (!_needCollisions[1, 0])
                    {
                        _isHits[1, 0] = true;
                        _needCollisions[1, 0] = true;
                    }
                }
            }
            else
            {
                _needCollisions[1, 0] = false;
            }
        }

        for (int i = 0; i < _hitZone.X_RightPosition().Length; i++)
        {
            if (_containObject.IsContainObjectFloor(_hitZone.X_RightPosition()[i]))
            {
                _myPosition = _myTransform.position;
                if (_containObject.GetAdjustmentX(_myPosition) != 0f)
                {
                    _myPosition.x = _containObject.GetAdjustmentX(_myPosition) - _hitDistanceScale._hitDistanceX;
                    _myTransform.position = _myPosition;
                    if (!_needCollisions[1, 1])
                    {
                        _isHits[1, 1] = true;
                        _needCollisions[1, 1] = true;
                    }
                }
            }
            else
            {
                _needCollisions[1, 1] = false;
            }
        }

        for (int i = 0; i < _hitZone.Z_BackPosition().Length; i++)
        {
            if (_containObject.IsContainObjectFloor(_hitZone.Z_BackPosition()[i]))
            {
                _myPosition = _myTransform.position;
                if (_containObject.GetAdjustmentZ(_myPosition) != 0f)
                {
                    _myPosition.z = _containObject.GetAdjustmentZ(_myPosition) + _hitDistanceScale._hitDistanceZ;
                    _myTransform.position = _myPosition;
                    if (!_needCollisions[2, 1])
                    {
                        _isHits[2, 1] = true;
                        _needCollisions[2, 1] = true;
                    }
                }
            }
            else
            {
                _needCollisions[2, 1] = false;
            }
        }

        for (int i = 0; i < _hitZone.Z_ForwardPosition().Length; i++)
        {
            if (_containObject.IsContainObjectFloor(_hitZone.Z_ForwardPosition()[i]))
            {
                _myPosition = _myTransform.position;
                if (_containObject.GetAdjustmentZ(_myPosition) != 0f)
                {
                    _myPosition.z = _containObject.GetAdjustmentZ(_myPosition) - _hitDistanceScale._hitDistanceZ;
                    _myTransform.position = _myPosition;
                    if (!_needCollisions[2, 0])
                    {
                        _isHits[2, 0] = true;
                        _needCollisions[2, 0] = true;
                    }
                }
            }
            else
            {
                _needCollisions[2, 0] = false;
            }
        }



        if (_isHits[0, 0] || _isHits[0, 1] || _isHits[1, 0] || _isHits[1, 1] || _isHits[2, 0] || _isHits[2, 1])
        {
            for (int i = 0; i < _originalMonoBehaviours.Length; i++)
            {
                _originalCollision += new OriginalCollision(_originalMonoBehaviours[i].OrignalOnCollisionEnter);
            }
        }


        if (_isHits[0, 0] || _isHits[0, 1])
        {
            for (int i = 0; i < _originalMonoBehaviours.Length; i++)
            {
                _originalCollision += new OriginalCollision(_originalMonoBehaviours[i].OrignalOnCollisionEnter_HitFloor);
                _originalCollision += new OriginalCollision(_originalMonoBehaviours[i].OrignalOnCollisionEnter_HitWall_AxisY);
            }
        }

        if (_isHits[1, 0] || _isHits[1, 1] || _isHits[2, 0] || _isHits[2, 1])
        {
            for (int i = 0; i < _originalMonoBehaviours.Length; i++)
            {
                _originalCollision += new OriginalCollision(_originalMonoBehaviours[i].OrignalOnCollisionEnter_HitWall);
                if (_isHits[1, 0] || _isHits[1, 1])
                {
                    _originalCollision += new OriginalCollision(_originalMonoBehaviours[i].OrignalOnCollisionEnter_HitWall_AxisX);
                }
                if (_isHits[2, 0] || _isHits[2, 1])
                {
                    _originalCollision += new OriginalCollision(_originalMonoBehaviours[i].OrignalOnCollisionEnter_HitWall_AxisZ);
                }
            }
        }

        if (_originalCollision != null)
        {
            _originalCollision();
        }

        _originalCollision = null;

        for (int i = 0; i < _isHits.GetLength(0); i++)
        {
            for (int j = 0; j < _isHits.GetLength(1); j++)
            {
                _isHits[i, j] = false;
            }
        }

        //if (_containObject.IsContainObjectAll(_hitZone.GetHitZone()))
        //{

        //    _myPosition = _myTransform.position;
        //    _myPosition.y = _containObject.GetAdjustmentAll(_myPosition) + _hitDistanceScale._hitDistanceY;
        //    _myTransform.position = _myPosition;

        //}

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