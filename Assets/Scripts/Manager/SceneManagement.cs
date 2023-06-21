// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.SceneManagement;

interface ISceneManager
{
    SceneObject SceneName { get; }
}

public class SceneManagement : MonoBehaviour,ISceneManager
{
    private IGameManagerSceneMoveNameSet _gameManager;

    public TagObject _GameControllerTagData;

    public SceneObject SceneName { private set; get; }

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
        SceneSpecifyMove("LoadScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag(_GameControllerTagData.TagName).GetComponent<GameManager>();
    }
}
