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

public class SceneManagement : MonoBehaviour,ISceneManager,ISceneFadeCallBack
{
    private IGameManagerSceneMoveNameSet _gameManager;

    public TagObject _GameControllerTagData;

    public SceneObject SceneName { private set; get; }

    private SceneFadeManager _sceneFadeManager = default;

    private bool isSceneFadeOutComplete = false;

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

    void Awake()
    {
        isSceneFadeOutComplete = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag(_GameControllerTagData.TagName).GetComponent<GameManager>();
        this.SceneFadeInStart();
    }

    private IEnumerator SceneMoveFadeIn()
    {
        this.SceneFadeOutStart();
        yield return new WaitUntil(() => isSceneFadeOutComplete);
        SceneSpecifyMove("LoadScene2");
    }

    public void SceneFadeInComplete()
    {
    }

    public void SceneFadeOutComplete()
    {
        isSceneFadeOutComplete = true;
    }
}
