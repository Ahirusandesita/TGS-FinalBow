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

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.FindObjectOfType<SceneManagement>().SceneLoadSpecifyMove(sceneObject);
        }
    }
    #endregion
}