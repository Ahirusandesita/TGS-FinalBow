// --------------------------------------------------------- 
// Demomomomomomomomomomomo.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class Demomomomomomomomomomomo : MonoBehaviour
{
    #region variable 
    [SerializeField]
    // これにエネミーのトランスフォームを入れてね
    private Transform _enemyTransform = default;

    private Transform _myTransform = default;

    private Vector3 _rotateValue = default;

    [SerializeField, Tooltip("移動速度")]
    private float _moveSpeed = 5f;

    [SerializeField]
    public float angle;

    [SerializeField]
    public float value;

    [SerializeField]
    public float shift;
    #endregion
    #region property
    #endregion
    #region method

    private void Update()
    {
        MoveEvent();
    }

    private void Awake()
    {
        _myTransform = this.transform;
    }

    private delegate void EventHandle();

    private event EventHandle MoveEvent;

    private void Movement()
    {
        _myTransform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        _myTransform.Rotate(Vector3.left * value * Time.deltaTime);
    }

    private void StartSetting()
    {
        _myTransform.position = _enemyTransform.position;
        _myTransform.rotation = _enemyTransform.rotation;
        //_rotateValue.x = shift;
        //_rotateValue.z = angle;
        //_myTransform.localEulerAngles += _rotateValue ;
        _myTransform.localEulerAngles += _myTransform.forward * angle;
        _myTransform.localEulerAngles += _myTransform.right * shift;
        MoveEvent += Movement;
        MoveEvent -= StartSetting;
    }

    private void OnEnable()
    {
        MoveEvent += StartSetting;
    }
    private void OnDisable()
    {
        MoveEvent = null;
    }

    #endregion
}