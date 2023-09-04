// --------------------------------------------------------- 
// TestBossMoveActionEventTransform.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System;
using System.Collections;
public class TestBossMoveActionEventTransform
{

    Transform transform;

    Vector2 _moveDirection = Vector2.one;

    float _cacheMoveTimeLine = 0;

    float _sideCache = 0;

    float _virticalCache = 0;

    float idleSideTime = 0.5f;
    float idleSideSpeed = 1f;
    float idleUpTime = 1f;
    float idleUpSpeed = 0.3f;

    public Transform SetTransform
    {
        set
        {
            transform = value;
            _sideCache = 0;
            _virticalCache = 0;
        }

    }

    public void Move()
    {

        _cacheMoveTimeLine += Time.deltaTime;
        
        if (_sideCache + idleSideTime < _cacheMoveTimeLine)
        {
            _sideCache = _cacheMoveTimeLine; 
            _moveDirection.x *= -1;
        }

        if (_virticalCache + idleUpTime < _cacheMoveTimeLine)
        {
            _virticalCache = _cacheMoveTimeLine;
            _moveDirection.y *= -1;
        }

        Vector2 _moveSpeed = new Vector2(_moveDirection.x * idleSideSpeed, _moveDirection.y * idleUpSpeed);

        transform.Translate(_moveSpeed * Time.deltaTime);

        


      
    }
}


