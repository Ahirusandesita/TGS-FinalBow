// --------------------------------------------------------- 
// TargetMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using System.Collections;
using UnityEngine;
public class TargetMove : MonoBehaviour
{
    #region variable 
    private TargetStats _targetStats = default;

    private TargetDataTable _targetData = default;

    private Transform _transform = default;

    private float _initialRotation_y = default;

    private Vector3 _initialScale = default;

    private float _rotateSpeed = 500f;

    private Vector3 _scalerSpeed = new(1500f, 1500f, 0f);

    private Vector3 _movingVector = default;

    private float _goalDistance = default;

    private float _movedDistance = 0f;

    private Transform _rotationPoint = default;

    private readonly Vector3 UP = Vector3.up;

    private readonly Vector3 RIGHT = Vector3.right;
    #endregion
    #region property
    public TargetDataTable TargetData { set => _targetData = value; }
    #endregion
    #region method

    private void Awake()
    {
        _transform = this.transform;
        _targetStats = this.GetComponent<TargetStats>();
        _rotationPoint = _transform.GetChild(0).transform;
    }


    /// <summary>
    /// スポーン時の初期化
    /// </summary>
    public void InitializeWhenEnable()
    {
        _transform.rotation = Quaternion.Euler(Vector3.zero);
        _initialRotation_y = _transform.rotation.eulerAngles.y;
        _initialScale = _transform.localScale;

        if (_targetData._needMove)
        {
            _movingVector = (_targetData._spawnPlace.position - _targetData._goalPlace.position).normalized;
            _goalDistance = (_targetData._spawnPlace.position - _targetData._goalPlace.position).magnitude;
        }

        StartCoroutine(LagerAtSpawn());
    }

    /// <summary>
    /// 回転しながらスポーンする処理
    /// </summary>
    private IEnumerator RotateAtSpawn()
    {
        while (_transform.rotation.eulerAngles.y <= _initialRotation_y + 180f)
        {
            _transform.Rotate(UP * Time.deltaTime * _rotateSpeed);
            yield return null;
        }

        if (_targetData._needMove)
            StartCoroutine(Move());
    }

    /// <summary>
    /// 大きくなりながらスポーンする処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator LagerAtSpawn()
    {
        _transform.localScale = new(0f, 0f, _transform.localScale.z);
        bool startRotate = false;

        while (_transform.localScale.x <= _initialScale.x)
        {
            _transform.localScale += Time.deltaTime * _scalerSpeed;
            yield return null;

            if (!startRotate && _transform.localScale.x > _initialScale.x * 0.1f)
            {
                StartCoroutine(RotateAtSpawn());
                startRotate = true;
            }
        }
    }

    private IEnumerator Move()
    {
        WaitForSeconds wait = new(_targetData._stayTime_s);

        while (true)
        {
            if (_movedDistance >= _goalDistance)
            {
                _movedDistance = 0f;
                _movingVector *= -1f;

                yield return wait;
            }

            _transform.Translate(_movingVector * Time.deltaTime * _targetData._speed);
            _movedDistance += (_movingVector * Time.deltaTime * _targetData._speed).magnitude;

            yield return null;
        }
    }

    public IEnumerator RotateAtDespawn()
    {
        float rotatedValue = 0f;

        while (rotatedValue < 90f)
        {
            _transform.RotateAround(_rotationPoint.position, RIGHT, Time.deltaTime * _rotateSpeed);
            rotatedValue += Time.deltaTime * _rotateSpeed;
            yield return null;
        }

        _targetStats.Despawn();
    }
    #endregion
}