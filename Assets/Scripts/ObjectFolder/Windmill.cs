// --------------------------------------------------------- 
// Windmill.cs 
// 
// CreateDay: 2023/06/16
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
public class Windmill : ObjectParent
{
    #region variable 
    private float _rotateSpeed=0f;
    private float _addRotateSpeed = -600f;
    private float _remRotateSpeed = 300f;

    private bool isRotate = false;

    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {

    }

    public override void ObjectAction()
    {
        _rotateSpeed += _addRotateSpeed * Time.deltaTime;
        if(_rotateSpeed < -1000f)
        {
            _rotateSpeed = -1000f;
        }
    }
    private void Update()
    {
        _rotateSpeed += _remRotateSpeed * Time.deltaTime;
        if (_rotateSpeed > 0)
        {
            _rotateSpeed = 0f;
        }
        this.transform.Rotate(0f, _rotateSpeed * Time.deltaTime, 0f);

    }

    #endregion
}