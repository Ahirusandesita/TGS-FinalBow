// --------------------------------------------------------- 
// SceneLoad.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SceneLoad : MonoBehaviour
{
	#region variable 
	public TagObject _GameControllerTagData;
	#endregion
	#region property
	#endregion
	#region method

	//�@�񓯊�����Ŏg�p����AsyncOperation
	private AsyncOperation async;
	private IGameManagerSceneMoveNameGet _gameManager;
	private SceneFadeOut _sceneFadeOut = default;
	public void Start()
	{
		_gameManager = GameObject.FindGameObjectWithTag(_GameControllerTagData.TagName).GetComponent<GameManager>();
		//�@�R���[�`�����J�n
		StartCoroutine(LoadData(_gameManager.SceneManagement.SceneName._sceneName));
	}

	private IEnumerator LoadData(string sceneName)
	{
		// �V�[���̓ǂݍ��݂�����
		async = SceneManager.LoadSceneAsync(sceneName);

		//�@�ǂݍ��݂��I���܂Ői���󋵂��X���C�_�[�̒l�ɔ��f������
		while (!async.isDone)
		{
			//float progressVal = Mathf.Clamp01(async.progress / 0.9f);
			yield return null;
		}
	}

	#endregion
}