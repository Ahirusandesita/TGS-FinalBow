// 81-C# NomalScript-NewScript.cs
//
//CreateDay:2023/06/13
//Creator  :�ɂ�[�[�[�[
//
using UnityEngine;

interface ITargeterMove
{

}
public class TargeterMove : MonoBehaviour
{
    // �ǐՂ��Ă���I�u�W�F�N�g
    GameObject _object = default;

    // �ڕW�n�_�̃v���C���[
    GameObject _player = default;

    // �o�ߎ���
    float _time = default;

    // ���݂̉�]���a�W��
    float _radius = default;

    // ���݂̖ڕW�n�_�܂ł̋���
    float _distance = default;

    [SerializeField ,Tooltip("�ڕW�n�_�֌������X�s�[�h")]
    float _speed = default;

    // �J�n���̒��S�����猩���p�x
    float _startRotation = default;

    // ���̈ړ���
    Vector3 _nextPosition = default;

    // Targeter�̓����͂��߂�m�点��bool
    bool _doMove = false;

    [SerializeField ,Tooltip("�ő�̔��a�W��")]
    private float MAX_RADIUS = 1f;

    [SerializeField ,Tooltip("�P�b�Ԃɉ��Z����锼�a�W���̒l")]
    private float _addRadius = 0.1f;




    private void OnEnable()
    {
        // �J�n�O�̏����ݒ�
        _object = this.gameObject;

        //--------------���ƂŏC������------------------------//
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