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


    private float _coefficient = default;

    private float _distance = default;

    private float _standardHigh = default;


    private float _peakLow = default;

    private float _peakMiddle = default;

    private float _peakHigh = default;

    private float _trajectory = default;

    private float _sign = default;

    private float _checkHigh = default;

    private bool _isOver = default;

    private const float QUADRUPLE = 4f;

    private float[] _counter = { 1f, 0.1f, 0.01f, 0.001f, 0.0001f };

    private int _counterNumber = default;

    private const float COUNTER_LENGTH = 5;
    
    private bool _endSetting = true;


    #endregion
    #region property
    private float GetHigh(int number)
    {
        if (number == 0)
        {
            return _peakHigh;
        }
        else if (number == 1)
        {
            return _peakMiddle;
        }
        else
        {
            return _peakLow;
        }
    }

    #endregion
    #region method
    protected override void AttackMove()
    {

    }

    private void GetTrajectory()
    {
        _distance = Mathf.Sqrt(MathN.Pow(MathN.Abs(_playerTransform.position.x + _objectTransform.position.x)) + MathN.Pow(MathN.Abs(_playerTransform.position.z + _objectTransform.position.z)));
        _imaginaryDistance = _distance;
        _standardHigh = _playerTransform.position.y;
        if(_playerTransform.position.y == _objectTransform.position.y)
        {
            _coefficient = (QUADRUPLE * GetHigh(0)) / MathN.Pow(_distance);
        }
        else if(_playerTransform.position.y < _objectTransform.position.y)
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

        while (_endSetting)
        {
            _imaginaryDistance += _counter[_counterNumber] * _sign;
            _coefficient = (QUADRUPLE * GetHigh(0)) / MathN.Pow(_imaginaryDistance);
            _checkHigh = _coefficient * MathN.Pow(_distance - _imaginaryDistance / 2f) + GetHigh(0);
            

        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    #endregion
}