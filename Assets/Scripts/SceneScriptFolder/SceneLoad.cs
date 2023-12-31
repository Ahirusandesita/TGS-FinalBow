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
	public void Start()
	{
		_gameManager = GameObject.FindGameObjectWithTag(_GameControllerTagData.TagName).GetComponent<GameManager>();
		//　コルーチンを開始
		StartCoroutine(LoadData(_gameManager.SceneManagement.SceneName._sceneName));
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

	#endregion
}