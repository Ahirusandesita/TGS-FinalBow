// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

interface ISceneManager
{
    SceneObject SceneName { get; }
}

public class SceneManagement : MonoBehaviour,ISceneManager
{
    private IGameManagerSceneMoveNameSet _gameManager;

    public TagObject _GameControllerTagData;

    public SceneObject SceneName { private set; get; }

    private SceneFadeManager _sceneFadeManager = default;

    private void SceneSpecifyMove(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// ÉVÅ[Éìà⁄ìÆópä÷êî
    /// </summary>
    /// <param name="sceneName"></param>
    public void SceneLoadSpecifyMove(SceneObject sceneObject)
    {
        SceneName = sceneObject;
        _gameManager.SceneManagement = this;
        GameObject.FindWithTag(InhallLibTags.ScoreController).GetComponent<ScoreManager>().ScoreSave();
        StartCoroutine(SceneMoveFadeIn());
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag(_GameControllerTagData.TagName).GetComponent<GameManager>();

        _sceneFadeManager = GameObject.FindGameObjectWithTag("SceneFade").GetComponent<SceneFadeManager>();
        if (_sceneFadeManager == null) return;
        _sceneFadeManager.SceneFadeOutStart();
    }

    private IEnumerator SceneMoveFadeIn()
    {
        _sceneFadeManager.SceneFadeInStart();
        yield return new WaitUntil(() => _sceneFadeManager._isSceneFadeInEnd);
        SceneSpecifyMove("LoadScene");
    }
}
