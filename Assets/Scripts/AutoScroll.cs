// --------------------------------------------------------- 
// StageMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;

public class AutoScroll : MonoBehaviour
{
    private Transform _transform = default;
    private Vector3 _movedirection = Vector3.forward;
    private float _currentTime = 0f;
    public GameObject _bossPrefab;
    private bool _isCompleted = false;

    private void Start()
    {
        _transform = this.transform;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime <= 60f)
        {
            _transform.Translate(_movedirection * 5.8f * Time.deltaTime);
        }
        else
        {
            if (this.gameObject.name == "OVRCameraRig Variant" && !_isCompleted)
            {
                Instantiate(_bossPrefab, new Vector3(0f, 7f, 390f), Quaternion.identity);
                _isCompleted = true;
            }
        }
    }
}