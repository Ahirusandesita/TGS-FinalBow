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
    public SceneObject sceneObject;
    private SceneManagement sceneManagement;

    private void Start()
    {
        _transform = this.transform;
        sceneManagement = GameObject.FindGameObjectWithTag(InhallLibTags.SceneController).GetComponent<SceneManagement>();
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
                sceneManagement.SceneLoadSpecifyMove(sceneObject);
                //Instantiate(_bossPrefab, new Vector3(0f, 7f, 390f), new Quaternion(0,180,0,0));
                _isCompleted = true;
            }
        }
    }
}