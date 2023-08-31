// --------------------------------------------------------- 
// MovieCamera.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class MovieCamera : MonoBehaviour
{
    //[SerializeField]
    private float _moveSpeed = 10f;
    //[SerializeField]
    private float _rotationSpeed = 50f;

    private bool _forward;
    private bool _back;

    private float _upDown;
    private float _vertical;
    private float _horizontal;

    private bool _cameraLeft;
    private bool _cameraRight;
    private bool _cameraUp;
    private bool _cameraDown;
    private bool _cameraReset;

    private float _rotationHorizontal;
    private float _rotationVertical;

    private bool _moveSpeedUp;
    private bool _moveSpeedDown;
    private bool _rotationSpeedup;
    private bool _rotationSpeedDown;


    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _upDown = Input.GetAxis("Vertical");
        _forward = Input.GetKey(KeyCode.LeftShift);
        _back = Input.GetKey(KeyCode.Space);

        _cameraLeft = Input.GetKey(KeyCode.K);
        _cameraRight = Input.GetKey(KeyCode.Semicolon);
        _cameraUp = Input.GetKey(KeyCode.O);
        _cameraDown = Input.GetKey(KeyCode.L);
        _cameraReset = Input.GetKeyDown(KeyCode.Backspace);

        _moveSpeedUp = Input.GetKeyDown(KeyCode.Alpha1);
        _moveSpeedDown = Input.GetKeyDown(KeyCode.Alpha2);
        _rotationSpeedup = Input.GetKeyDown(KeyCode.Alpha3);
        _rotationSpeedDown = Input.GetKeyDown(KeyCode.Alpha4);

        if (_forward)
        {
            _vertical = 1f;
        }
        else if (_back)
        {
            _vertical = -1f;
        }
        else
        {
            _vertical = 0f;
        }

        if (_cameraLeft)
        {
            _rotationHorizontal = -1f;
        }
        else if (_cameraRight)
        {
            _rotationHorizontal = 1f;
        }
        else
        {
            _rotationHorizontal = 0f;
        }

        if (_cameraUp)
        {
            _rotationVertical = -1f;
        }
        else if (_cameraDown)
        {
            _rotationVertical = 1f;
        }
        else
        {
            _rotationVertical = 0f;
        }

        if (_cameraReset)
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
        }

        if (_moveSpeedUp)
        {
            _moveSpeed += 3f;
        }
        else if (_moveSpeedDown)
        {
            _moveSpeed -= 3f;
        }

        if (_rotationSpeedup)
        {
            _rotationSpeed += 5f;
        }
        else if (_rotationSpeedDown)
        {
            _rotationSpeed -= 5f;
        }

        if (_moveSpeed < 0f)
        {
            _moveSpeed = 0f;
        }

        if (_rotationSpeed < 0f)
        {
            _rotationSpeed = 0f;
        }

        transform.Translate(Vector3.right * _horizontal * Time.deltaTime * _moveSpeed);
        transform.Translate(Vector3.up * _upDown * Time.deltaTime * _moveSpeed);
        transform.Translate(Vector3.forward * _vertical * Time.deltaTime * _moveSpeed);
        transform.Rotate(Vector3.up * _rotationHorizontal * Time.deltaTime * _rotationSpeed);
        transform.Rotate(Vector3.right * _rotationVertical * Time.deltaTime * _rotationSpeed);
    }
}