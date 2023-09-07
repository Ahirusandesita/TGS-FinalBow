// --------------------------------------------------------- 
// DropItemChild.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class DropItemChild : MonoBehaviour
{
    #region variable 
    private Transform _myTransform = default;
    private Vector3 _nowPosition = default;
    private Vector3 _nowRotation = default;
    private float _position_y = default;
    private float _positionValue = default;
    private float _rotation_y = default;

    [SerializeField]
    private float _rotationSpeed = 100f;

    [SerializeField]
    private float _translateSpeed_Serialized = 1;

    private float _translateSpeed = default;

    [SerializeField]
    private float _translateWidth = 2;
    #endregion
    #region property
    #endregion
    #region method
    private void Update()
    {
        _rotation_y += Time.deltaTime * _rotationSpeed;
        if (_rotation_y > 360f)
        {
            _rotation_y -= 360f;
        }

        _positionValue += Time.deltaTime * _translateSpeed;
        _position_y = Mathf.Sin(_positionValue) * _translateWidth;

        _nowRotation.y = _rotation_y;
        _nowPosition.y = _position_y;

        _myTransform.eulerAngles = _nowPosition;
        _myTransform.localPosition = _nowPosition;
    }

    private void Awake()
    {
        _translateSpeed = _translateSpeed_Serialized * Mathf.PI;
        _myTransform = this.transform;
    }

    private void OnEnable()
    {
        _rotation_y = 0f;
        _position_y = 0f;
    }
    #endregion
}