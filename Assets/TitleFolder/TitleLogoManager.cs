// --------------------------------------------------------- 
// TitleLogoManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TitleLogoManager : MonoBehaviour
{
    public SceneObject sceneObject;

    private float _buttonTime = 0f;

    private bool _isFirst = true;
    #region variable 
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {

    }

    private void Start()
    {
        _isFirst = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _buttonTime += Time.deltaTime;

            if (_buttonTime >= 1.5f && _isFirst)
            {
                _isFirst = false;
                GameObject.FindObjectOfType<SceneManagement>().SceneLoadSpecifyMove(sceneObject);
            }
        }
        else
        {
            _buttonTime = 0f;
        }
    }
    #endregion
}