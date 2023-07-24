// 81-C# NomalScript-NewScript.cs
//
//CreateDay:2023/06/13
//Creator  :にゃーーーー
//
using UnityEngine;

interface ITargeterMove
{

}
public class TargeterMove : MonoBehaviour
{
    private ItemMove _itemMove = default;

    GameObject _object = default;

    GameObject _player = default;

    float _time = default;

    [SerializeField]
    float _radius = default;

    float _distance = default;

    [SerializeField]
    float _speed = default;


    float _startRotation = default;
    Vector3 _nextPosition = default;

    bool _doMove = false;



    private void Start()
    {
        _object = this.gameObject;
        _player = GameObject.FindWithTag("PlayerController");
        _itemMove = this.GetComponent<ItemMove>();
    }


    private void Update()
    {
        if (_doMove)
        {
            TargeterMovement();
        }
    }

    private void TargeterMovement()
    {
        _time += Time.deltaTime * Mathf.PI;
        _nextPosition.x = Mathf.Cos(_startRotation + _time) * (_radius * _distance);
        _nextPosition.y = Mathf.Sin(_startRotation + _time) * (_radius * _distance);
        _nextPosition.z = _distance;
        _object.transform.localPosition = _nextPosition;

        _distance -= Time.deltaTime * _speed;
        if (_distance < 0f)
        {
            _doMove = false;
        }
    }

    private void OnEnable()
    {
        _object.transform.parent = _player.transform;
        _startRotation = Mathf.Atan2(_object.transform.position.y, _object.transform.position.x);

        _distance = _object.transform.localPosition.z;

        _doMove = true;
    }

    private void OnDisable()
    {
        _object.transform.parent = null;
        _time = default;
    }
}