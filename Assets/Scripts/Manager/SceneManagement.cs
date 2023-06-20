// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : ?
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���͎g��Ȃ�
/// </summary>
interface IScene
{
    /// <summary>
    /// �V�[���ړ�
    /// </summary>
    /// <param name="sceneName"></param>
    void SceneMove();
}
public class SceneManagement : MonoBehaviour,IScene
{
    private IGameManagerSceneMoveNameSet _gameManager;

    private string[] sceneName;
    private int sceneIndex;

    public TagObject _GameControllerTagData;
    public SceneObject _sceneName;

    public void SceneMove()
    {
        sceneIndex++;
    }

    private void SceneSpecifyMove(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// �V�[���ړ��p�֐�
    /// </summary>
    /// <param name="sceneName"></param>
    public void SceneLoadSpecifyMove(SceneObject sceneObject)
    {
        //_gameManager.SetSceneMoveName(sceneObject._sceneName);
        _sceneName = sceneObject;
        _gameManager.SceneManagement = this;
        SceneSpecifyMove("LoadScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        //�^�O�̖��O�X�N���v�^�u���ɂ���
        _gameManager = GameObject.FindGameObjectWithTag(_GameControllerTagData.TagName).GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
