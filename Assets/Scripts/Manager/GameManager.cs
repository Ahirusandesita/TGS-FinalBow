// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
interface IGame
{
    //Time0‚ÅSceneMove‚æ‚Ô
}
interface IGameManagerSceneMoveName
{
    string SceneMoveName { set; get; }
}
interface IGameManagerSceneMoveNameSet
{
    //void SetSceneMoveName(string sceneName);
    SceneManagement SceneManagement { set; get; }
}
interface IGameManagerSceneMoveNameGet
{
    //string GetSceneMoveName();
    SceneManagement SceneManagement { set; get; }
}



public class GameManager : MonoBehaviour, IGame, IGameManagerSceneMoveNameSet, IGameManagerSceneMoveNameGet
{

    public ScoreManager ScoreManager { set; get; }
    public SceneManagement SceneManagement { set; get; }

    public static GameManager Instance = null;
    IScene scene;
    IStageSpawn stageSpawn;
    private string _sceneMoveName;

    //public void SetSceneMoveName(string sceneName)
    //{
    //    _sceneMoveName = sceneName;
    //}
    //public string GetSceneMoveName()
    //{
    //    return _sceneMoveName;
    //}


    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        stageSpawn = this.GetComponent<IStageSpawn>();
    }

    private void Update()
    {

    }
}
