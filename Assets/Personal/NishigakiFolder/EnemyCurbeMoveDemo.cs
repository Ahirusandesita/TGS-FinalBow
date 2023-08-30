// --------------------------------------------------------- 
// EnemyCabeMoveDemo.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class EnemyCurveMoveDemo : EnemyAttackBase
{
    #region variable 
    // これにエネミーのトランスフォームを入れてね
    private Transform _enemyTransform = default;

    private Transform _myTransform = default;

    private float angle { get; set; }

    private float value { get; set; }

    private float shift { get; set; }
    #endregion
    #region property
    #endregion
    #region method

    protected override void AttackMove()
    {
        MoveEvent();
        throw new System.NotImplementedException();
    }

    private delegate void EventHandle();

    private event EventHandle MoveEvent;

    private void Movement()
    {
        _myTransform.Translate(Vector3.forward * _attackMoveSpeed * Time.deltaTime);
        _myTransform.Rotate(Vector3.left * value * Time.deltaTime);
    }

    private void StartSetting()
    {
        _myTransform.position = _enemyTransform.position;
        _myTransform.rotation = _enemyTransform.rotation;
        _myTransform.localEulerAngles += _myTransform.forward * angle;
        _myTransform.localEulerAngles += _myTransform.right * shift;
        MoveEvent += Movement;
        MoveEvent -= StartSetting;
    }

    protected override void OnEnable()
    {
        MoveEvent += StartSetting;
        base.OnEnable();
    }

    private void OnDisable()
    {
        MoveEvent = null;
    }

    protected override void Awake()
    {
        _myTransform = this.transform;
        base.Awake();
    }

    #endregion
}