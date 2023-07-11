// --------------------------------------------------------- 
// OnGroundThrowMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using Nekoslibrary;
using System.Collections;
public class OnGroundThrowMove : EnemyAttackBase
{
    #region variable 
    [SerializeField, Tooltip("")]
    private Transform _playerTransform = default;

    [SerializeField, Tooltip("")]
    private Transform _objectTransform = default;

    [SerializeField, Tooltip("")]
    private float _imaginaryDistance = default;

    [SerializeField, Tooltip("")]
    private float _moveSpeed = 10f;

    [SerializeField, Tooltip("")]
    private float _peak = 10f;


    private float _coefficient = default;

    private float _distance = default;

    private float _standardHigh = default;

    private float _trajectory = default;

    private float _sign = default;

    private float _checkHigh = default;

    private float _moveValue = default;


    private bool _isOver = default;

    private const float QUADRUPLE = 4f;

    private float[] _counter = { 1f, 0.1f, 0.01f, 0.001f, 0.0001f };

    private int _counterNumber = default;

    private bool _endSetting = true;

    private Vector3 _targetVecter = default;

    private Vector3 _newPosition = default;

    private Vector3 _playerPosition = default;

    private Vector3 _objectPosition = default;


    #endregion
    #region property

    #endregion
    #region method

    protected override void AttackMove()
    {
        _moveValue += _moveSpeed * Time.deltaTime;


        _trajectory = _coefficient * MathN.Pow(_distance - _imaginaryDistance / 2f - _moveValue) + _peak;

        _newPosition = _objectPosition + (_targetVecter * _moveValue);
        _newPosition.y = _standardHigh + _trajectory;


        this.transform.position = _newPosition;
    }

    private void GetTrajectory()
    {

        _playerPosition = _playerTransform.position;
        _objectPosition = _objectTransform.position;

        _targetVecter = MathN.Vector3To2_XZ(_playerPosition - _objectPosition).normalized;
        print(_targetVecter);


        _distance = Mathf.Sqrt(MathN.Pow(MathN.Abs(_playerPosition.x + _objectPosition.x)) + MathN.Pow(MathN.Abs(_playerPosition.z + _objectPosition.z)));
        _imaginaryDistance = _distance;
        _standardHigh = _playerPosition.y;
        if(_playerPosition.y == _objectPosition.y)
        {
            _coefficient = -(QUADRUPLE * _peak) / MathN.Pow(_distance);
        }
        else if(_playerPosition.y < _objectPosition.y)
        {
            _sign = 1f; //プラス
            _isOver = true;
            _endSetting = false;
            _counterNumber = 0;
        }
        else
        {
            _sign = -1f; //マイナス
            _isOver = false;
            _endSetting = false;
            _counterNumber = 0;
        }

        while (!_endSetting)
        {
            _imaginaryDistance += _counter[_counterNumber] * _sign;
            _coefficient = -(QUADRUPLE * _peak) / MathN.Pow(_imaginaryDistance);
            _checkHigh = _coefficient * MathN.Pow(_distance - _imaginaryDistance / 2f) + _peak;
            if (_isOver && _objectPosition.y - _playerPosition.y < _checkHigh || !_isOver && _objectPosition.y - _playerPosition.y > _checkHigh)
            {
                // 超えた
                if (_counterNumber < _counter.Length - 1)
                {
                    _imaginaryDistance -= _counter[_counterNumber] * _sign;
                    _counterNumber++;
                }
                else
                {
                    _endSetting = true;
                    _counterNumber = 0;
                }

            }

        }
    }

    private void OnEnable()
    {
        GetTrajectory();
    }

    private void OnDisable()
    {
        
    }

    #endregion
}