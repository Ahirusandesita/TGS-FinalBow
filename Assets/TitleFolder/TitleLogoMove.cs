// --------------------------------------------------------- 
// TitleLogoMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TitleLogoMove : MonoBehaviour
{ 
    private RectTransform _myTransform = default;
    private Vector3 _nowPosition = default;
    private float _position_y = default;
    private float _positionValue = default;
    private float _startPos = default;

    [SerializeField]
    private float _translateSpeed_Serialized = 1;

    private float _translateSpeed = default;

    [SerializeField]
    private float _translateWidth = 2;

    private void Update()
    {

        _positionValue += Time.deltaTime * _translateSpeed;
        _position_y = Mathf.Sin(_positionValue) * _translateWidth;

        _nowPosition.y = _startPos + _position_y;

        _myTransform.localPosition = _nowPosition;
    }

    private void Awake()
    {
        _translateSpeed = _translateSpeed_Serialized * Mathf.PI;
        _myTransform = this.GetComponent<RectTransform>();
        _startPos = _myTransform.localPosition.y;
    }

    private void OnEnable()
    {
        _position_y = 0f;
    }

}