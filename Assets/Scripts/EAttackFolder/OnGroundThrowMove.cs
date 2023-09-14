// --------------------------------------------------------- 
// OnGroundThrowMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class OnGroundThrowMove : EnemyAttackBase
{
    #region variable 
    [Tooltip("")]
    private Transform _playerTransform = default;

    [Tooltip("")]
    private Transform _objectTransform = default;

    [SerializeField, Tooltip("")]
    private float _imaginaryDistance = default;

    [SerializeField, Tooltip("")]
    private float _peak = 10f;


    private float _coefficient = default;

    private float _distance = default;

    private float _standardHigh = default;

    private float _trajectory = default;

    private float _sign = default;

    private float _checkHigh = default;

    private float _moveValue = default;

    private float _checkLoop_Under = -1f;

    private float _checkLoop_Over = default;

    private float _needHitTime = default;

    private float _playerSpeed = 5.8f;

    private float _playerMoveValue = default;


    private bool _isLoop = false;

    private bool _isOver = default;

    private const float QUADRUPLE = 4f;

    private float[] _counter = { 1f, 0.1f, 0.01f, 0.001f, 0.0001f };

    private int _counterNumber = default;

    private bool _endSetting = true;

    private Vector3 _targetVecter = default;

    private Vector3 _newPosition = default;

    private Vector3 _playerPosition = default;

    private Vector3 _objectPosition = default;

    [Tooltip("一度目（プールが生成されたとき）")]
    private bool _isFirstTime = true;
    #endregion
    #region property

    #endregion
    #region method

    protected override void OnEnable()
    {
        base.OnEnable();

        if (_isFirstTime)
        {
            _objectTransform = _transform;
            _isFirstTime = false;
            _playerTransform = GameObject.FindWithTag("PlayerController").transform;
        }
        else
        {
            GetTrajectory();
        }
    }

    private void OnDisable()
    {
        _moveValue = default;
    }


    protected override void AttackMove()
    {
        _moveValue += _attackMoveSpeed * Time.deltaTime;


        _trajectory = _coefficient * Mathf.Pow(_distance - _imaginaryDistance / 2f - _moveValue , 2) + _peak;

        _newPosition = _objectPosition + (_targetVecter * _moveValue);
        _newPosition.y = _standardHigh + _trajectory;


        _transform.position = _newPosition;
    }

    private void GetTrajectory()
    {
        _playerPosition = _playerTransform.position;
        _objectPosition = _objectTransform.position;

        _targetVecter = (_playerPosition - _objectPosition).normalized;

        _targetVecter.y = 0f;

        _distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(_playerPosition.x - _objectPosition.x) ,2) + Mathf.Pow(Mathf.Abs(_playerPosition.z - _objectPosition.z) ,2));

        //------あとで直す----------------------

        _needHitTime = _distance / _attackMoveSpeed;

        _playerMoveValue = _playerSpeed * _needHitTime;

        _distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(_playerPosition.x - _objectPosition.x) ,2) + Mathf.Pow(Mathf.Abs((_playerPosition.z + _playerMoveValue) - _objectPosition.z) , 2)) ;

        _needHitTime = _distance / _attackMoveSpeed;

        _playerMoveValue = _playerSpeed * _needHitTime;

        _distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(_playerPosition.x - _objectPosition.x),2) + Mathf.Pow(Mathf.Abs((_playerPosition.z + _playerMoveValue) - _objectPosition.z),2));

        ; _needHitTime = _distance / _attackMoveSpeed;

        _playerMoveValue = _playerSpeed * _needHitTime;

        _distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(_playerPosition.x - _objectPosition.x),2) + Mathf.Pow(Mathf.Abs((_playerPosition.z + _playerMoveValue) - _objectPosition.z),2));

        //------------------------------------

        _imaginaryDistance = _distance;
        _standardHigh = _playerPosition.y;
        if (_playerPosition.y == _objectPosition.y)
        {
            _coefficient = -(QUADRUPLE * _peak) / Mathf.Pow(_distance,2);
        }
        else if (_playerPosition.y < _objectPosition.y)
        {
            _sign = 1f; //プラス
            _isOver = true;
            _endSetting = false;
            _counterNumber = 0;
            _checkLoop_Over = _distance * 2 + 1f;
        }
        else
        {
            _sign = -1f; //マイナス
            _isOver = false;
            _endSetting = false;
            _counterNumber = 0;
            _checkLoop_Over = _distance * 2 + 1f;
        }

        while (!_endSetting)
        {
            _imaginaryDistance += _counter[_counterNumber] * _sign;
            _coefficient = -(QUADRUPLE * _peak) / Mathf.Pow(_imaginaryDistance,2);
            _checkHigh = _coefficient * Mathf.Pow(_distance - _imaginaryDistance / 2f,2) + _peak;
            if (_imaginaryDistance < _checkLoop_Under || _imaginaryDistance > _checkLoop_Over)
            {
                EscapeLoop(_playerPosition, _objectPosition);
                _counterNumber = _counter.Length;
                _isLoop = true;
            }
            if (_isOver && _objectPosition.y - _playerPosition.y < _checkHigh || !_isOver && _objectPosition.y - _playerPosition.y > _checkHigh || _isLoop)
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
                    _isLoop = false;
                    _counterNumber = 0;
                }
            }
        }
    }

    private void EscapeLoop(Vector3 playerPosition, Vector3 objectPosition)
    {
        _peak = objectPosition.y - playerPosition.y;
        _imaginaryDistance = _distance * 2f;
        _coefficient = -(QUADRUPLE * _peak) / Mathf.Pow(_imaginaryDistance,2);
    }
    #endregion
}