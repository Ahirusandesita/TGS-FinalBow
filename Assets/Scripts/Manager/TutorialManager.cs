// --------------------------------------------------------- 
// TutorialManager.cs 
// 
// CreateDay: 2023/09/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    #region variable 
    [SerializeField]
    private StageDataTable _stageDataTable = default;

    private ObjectPoolSystem _poolSystem = default;

    [Tooltip("チュートリアルの進行度")]
    private int _currentTutorialIndex = 0;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        try
        {
            _poolSystem = GameObject.FindWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
        }
        catch (Exception)
        {
            X_Debug.LogError("ObjectPoolSystemがアタッチされていません。");
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnTarget(0));
    }

    private void Update()
    {

    }

    private IEnumerator SpawnTarget(int listIndex)
    {
        // チュートリアル用のデータのパスを取得（チュートリアルなので
        TargetDataTable dataPath = _stageDataTable._waveInformation[_currentTutorialIndex]._targetData[listIndex];

        // 時間差スポーン制御
        yield return new WaitForSeconds(dataPath._spawnDelay_s);

        TargetMove target = _poolSystem.CallObject(PoolEnum.PoolObjectType.targetObject, dataPath._spawnPlace.position).GetComponent<TargetMove>();
        target.TargetData = dataPath;
    }

    /// <summary>
    /// 吸い込み完了
    /// </summary>
    public void CompleteAttract()
    {

    }

    /// <summary>
    /// 矢を発射した
    /// </summary>
    public void OnShot()
    {

    }

    /// <summary>
    /// ボムが選ばれた
    /// </summary>
    public void OnSelectedBomb()
    {

    }

    /// <summary>
    /// ラジアルメニューが表示されたとき
    /// </summary>
    public void OnRadialMenuDisplayed()
    {

    }
    #endregion
}