// --------------------------------------------------------- 
// SceneFadeController.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class SceneFadeManager : MonoBehaviour
{
    #region variable 
    private SceneFadeIn _sceneFadeIn;
    private SceneFadeOut _sceneFadeOut;

    #endregion
    #region property
    #endregion
    #region method
    public SceneFadeIn GetSceneFadeIn()
    {
        _sceneFadeIn = transform.GetChild(0).GetComponent<SceneFadeIn>();
        return _sceneFadeIn;
    }
    public SceneFadeOut GetSceneFadeOut()
    {
        _sceneFadeOut = transform.GetChild(1).GetComponent<SceneFadeOut>();
        return _sceneFadeOut;
    }

    #endregion
}