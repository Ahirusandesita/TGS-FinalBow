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

	//　非同期動作で使用するAsyncOperation
	private AsyncOperation async;
	private IGameManagerSceneMoveNameGet _gameManager;
	private SceneFadeOut _sceneFadeOut = default;
	public void Start()
	{
		_gameManager = GameObject.FindGameObjectWithTag(_GameControllerTagData.TagName).GetComponent<GameManager>();
		_sceneFadeOut = GameObject.FindGameObjectWithTag("SceneFade").GetComponent<SceneFadeOut>();
		//　コルーチンを開始
		StartCoroutine(SceneLoadFadeIn());
	}

	private IEnumerator LoadData(string sceneName)
	{
		// シーンの読み込みをする
		async = SceneManager.LoadSceneAsync(sceneName);

		//　読み込みが終わるまで進捗状況をスライダーの値に反映させる
		while (!async.isDone)
		{
			//float progressVal = Mathf.Clamp01(async.progress / 0.9f);
			yield return null;
		}
	}
	private IEnumerator SceneLoadFadeIn()
    {
		_sceneFadeOut.SceneFadeStart();
		yield return new WaitUntil(() => _sceneFadeOut._isFadeEnd);
		LoadData(_gameManager.SceneManagement.SceneName._sceneName);
	}

	#endregion
}