// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.SceneManagement;

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
interface IGameManagerScore
{
    ScoreManager ScoreManager { get; set; }
}
interface IGameManagerScoreGetOnly
{
    ScoreManager ScoreManager { get; }
}

interface ISubtitlesGetOnly
{
    SubtitlesType SubtitlesType { get; }
}

public enum SubtitlesType { Japanese, English }

public class GameManager : MonoBehaviour, IGame, IGameManagerSceneMoveNameSet, IGameManagerSceneMoveNameGet, IGameManagerScoreGetOnly, IGameManagerScore, ISubtitlesGetOnly
{

    public ScoreManager ScoreManager { set; get; }
    public SceneManagement SceneManagement { set; get; }
    public TimeManager TimeManager { set; get; }
    public SubtitlesType SubtitlesType { get; private set; }

    public static GameManager Instance = null;
    IStageSpawn stageSpawn;
    private string _sceneMoveName;

    void Awake()
    {
        SubtitlesType = SubtitlesType.Japanese;

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
        //Debug.LogError(ScoreManager.ScorePoint.scoreNormalEnemy);
        if (Input.GetKeyDown(KeyCode.Keypad1) && SceneManager.GetActiveScene().name == "TitleScene")
        {
            if (SubtitlesType == SubtitlesType.Japanese)
            {
                SubtitlesType = SubtitlesType.English;
                X_Debug.Log("‰pŒê");
            }
            else
            {
                SubtitlesType = SubtitlesType.Japanese;
                X_Debug.Log("“ú–{Œê");
            }
        }
    }
}
