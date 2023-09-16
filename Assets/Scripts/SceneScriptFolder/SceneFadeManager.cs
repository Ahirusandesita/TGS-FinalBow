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
    private SceneFadeIn[] _sceneFadeIn = new SceneFadeIn[2];
    private SceneFadeOut[] _sceneFadeOut = new SceneFadeOut[2];
    public bool _isSceneFadeInEnd = false;
    public bool _isSceneFadeOutEnd = false;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        _sceneFadeIn[0] = transform.GetChild(0).GetComponent<SceneFadeIn>();
        _sceneFadeOut[0] = transform.GetChild(1).GetComponent<SceneFadeOut>();
        _sceneFadeIn[1] = transform.GetChild(2).GetComponent<SceneFadeIn>();
        _sceneFadeOut[1] = transform.GetChild(3).GetComponent<SceneFadeOut>();
    }

    public void SceneFadeOutStart()
    {
        StartCoroutine(SceneMoveFadeOut());
    }
    public void SceneFadeInStart()
    {
        StartCoroutine(SceneMoveFadeIn());
    }

    private IEnumerator SceneMoveFadeIn()
    {
        _sceneFadeIn[0].SceneFadeStart();
        _sceneFadeIn[1].SceneFadeStart();
        yield return new WaitUntil(() => _sceneFadeIn[0]._isFadeEnd);
        _isSceneFadeInEnd = true;
    }
    private IEnumerator SceneMoveFadeOut()
    {
        _sceneFadeOut[0] = transform.GetChild(1).GetComponent<SceneFadeOut>();
        
        _sceneFadeOut[1] = transform.GetChild(3).GetComponent<SceneFadeOut>();
        _sceneFadeOut[0].SceneFadeStart();
        _sceneFadeOut[1].SceneFadeStart();
        yield return new WaitUntil(() => _sceneFadeOut[0]._isFadeEnd);
        _isSceneFadeOutEnd = true;
    }


    #endregion
}