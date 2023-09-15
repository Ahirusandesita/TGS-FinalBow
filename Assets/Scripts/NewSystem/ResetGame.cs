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
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {

    }

    private void Start()
    {

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