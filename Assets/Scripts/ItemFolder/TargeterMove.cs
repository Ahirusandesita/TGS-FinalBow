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
    // 追跡してくるオブジェクト
    GameObject _object = default;

    // 目標地点のプレイヤー
    GameObject _player = default;

    // 経過時間
    float _time = default;

    // 現在の回転半径係数
    float _radius = default;

    // 現在の目標地点までの距離
    float _distance = default;

    [SerializeField ,Tooltip("目標地点へ向かうスピード")]
    float _speed = default;

    // 開始時の中心軸から見た角度
    float _startRotation = default;

    // 次の移動先
    Vector3 _nextPosition = default;

    // Targeterの動きはじめを知らせるbool
    bool _doMove = false;

    [SerializeField ,Tooltip("最大の半径係数")]
    private float MAX_RADIUS = 1f;

    [SerializeField ,Tooltip("１秒間に加算される半径係数の値")]
    private float _addRadius = 0.1f;




    private void OnEnable()
    {
        // 開始前の初期設定
        _object = this.gameObject;

        //--------------あとで修正する------------------------//
        _player = GameObject.FindObjectOfType<PlayerManager>().gameObject;
        //----------------------------------------------------//
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

        if(_radius < MAX_RADIUS)
        {
            _radius += Time.deltaTime * _addRadius;
        }

        _distance -= Time.deltaTime * _speed;
        if (_distance < 0f)
        {
            _doMove = false;
        }
    }

    public void StartSetting()
    {
        if(_object != null)
        {
            _object.transform.parent = _player.transform;
            _startRotation = Mathf.Atan2(_object.transform.position.y, _object.transform.position.x);

            _distance = _object.transform.localPosition.z;

            _radius = new Vector2(_object.transform.localPosition.x, _object.transform.localPosition.y).magnitude / _distance;

            _doMove = true;
        }
    }

    public void TargeterReSet()
    {
        if(_object != null)
        {
            _object.transform.parent = null;
            _time = default;
        }
    }
}