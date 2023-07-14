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
    public SceneObject _sceneObject = default;

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
            if (this.gameObject.name == "OVRCameraRig Variant")
            {
                GameObject.FindWithTag("SceneController").GetComponent<SceneManagement>().SceneLoadSpecifyMove(_sceneObject);
            }
        }
    }
}