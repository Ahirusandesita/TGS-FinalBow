// --------------------------------------------------------- 
// TestScri.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TestScri : MonoBehaviour
{
    #region variable 
    SceneObject sceneObject;
 #endregion
 #region property
 #endregion
 #region method
 
 private void Awake()
 {

 }
 
 private void Start ()
 {
        sceneObject = (SceneObject)ScriptableFind.ScriptableObjectFind(ScriptableEnum.scriptableName.SceneObject,ScriptableEnum.name.Result);
        Debug.Log(sceneObject._sceneName);
 }

 private void Update ()
 {

 }
 #endregion
}