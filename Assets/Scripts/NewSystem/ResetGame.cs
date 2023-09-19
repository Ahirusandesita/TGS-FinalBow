// --------------------------------------------------------- 
// ResetGame.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ResetGame : MonoBehaviour
{
    #region variable 
    public SceneObject sceneObject;
    private float _buttonTime = 0f;
    private bool _isFirst = true;
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        _isFirst = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject.FindObjectOfType<SceneManagement>().SceneLoadSpecifyMove(sceneObject);
        }
    }
    #endregion
}