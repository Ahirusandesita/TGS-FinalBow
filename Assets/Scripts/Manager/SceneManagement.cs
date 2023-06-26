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

    private SceneFadeIn _sceneFadeIn = default;

    private SceneFadeIn _sceneFadeOut = default;

    private void SceneSpecifyMove(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// シーン移動用関数
    /// </summary>
    /// <param name="sceneName"></param>
    public void SceneLoadSpecifyMove(SceneObject sceneObject)
    {
        SceneName = sceneObject;
        _gameManager.SceneManagement = this;
        StartCoroutine(SceneMoveFadeIn());
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag(_GameControllerTagData.TagName).GetComponent<GameManager>();

        _sceneFadeIn = GameObject.FindGameObjectWithTag("SceneFade").GetComponent<SceneFadeManager>().GetSceneFadeIn();

        _sceneFadeOut = GameObject.FindGameObjectWithTag("SceneFade").GetComponent<SceneFadeManager>().GetSceneFadeOut();

        StartCoroutine(SceneMoveFadeOut());
    }

    private IEnumerator SceneMoveFadeIn()
    {
        _sceneFadeIn.SceneFadeStart();
        yield return new WaitUntil(() => _sceneFadeIn._isFadeEnd);
        SceneSpecifyMove("LoadScene");
    }


    private IEnumerator SceneMoveFadeOut()
    {
        _sceneFadeOut.SceneFadeStart();
        yield return new WaitUntil(() => _sceneFadeOut._isFadeEnd);

    }

}
