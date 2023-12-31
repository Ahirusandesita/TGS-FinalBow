// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

interface IGame
{
    //Time0でSceneMoveよぶ
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
    private float _buttonTime = 0f;
    private bool _isButtonDown = false;
    private float _arrowDownTime = 0f;
    private bool _isArrowDown = false;
    private TextMeshProUGUI _text = default;
    private const string JAPANESE = "日本語";
    private const string ENGLISH = "English";

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
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            _text = GameObject.FindWithTag("SubtitleText").GetComponent<TextMeshProUGUI>();
            SubtitlesType = SubtitlesType.Japanese;
            _text.text = JAPANESE;
        }

        stageSpawn = this.GetComponent<IStageSpawn>();
    }

    private void Update()
    {
        //Debug.LogError(ScoreManager.ScorePoint.scoreNormalEnemy);
        if (Input.GetKeyDown(KeyCode.DownArrow) && SceneManager.GetActiveScene().name == "TitleScene")
        {
            if (SubtitlesType == SubtitlesType.Japanese)
            {
                SubtitlesType = SubtitlesType.English;

                if (_text == null)
                    _text = GameObject.FindWithTag("SubtitleText").GetComponent<TextMeshProUGUI>();

                _text.text = ENGLISH;
            }
            else
            {
                SubtitlesType = SubtitlesType.Japanese;
                if (_text == null)
                    _text = GameObject.FindWithTag("SubtitleText").GetComponent<TextMeshProUGUI>();

                _text.text = JAPANESE;
            }
        }
    }
}
