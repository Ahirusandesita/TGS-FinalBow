// --------------------------------------------------------- 
// TutorialManager.cs 
// 
// CreateDay: 2023/09/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum TutorialIventType
{
    opening,
    shot1,
    shot2,
    enchant1,
    enchant2,
    attract1,
    attract2
}


public partial class TutorialManager : MonoBehaviour, ITextLikeSpeaking
{
    #region variable 
    [SerializeField]
    private StageDataTable _stageDataTable = default;

    private ObjectPoolSystem _poolSystem = default;

    [SerializeField]
    private List<TutorialManagementData> _tutorialTextsData = new();

    [SerializeField]
    private TextSystem _textSystem = default;

    [SerializeField]
    private GameObject _kakashi = default;

    [SerializeField]
    private GameObject _textFrame = default;

    [SerializeField]
    private GameObject _crystal = default;


    [Tooltip("チュートリアルの進行度")]
    private TutorialIventType _currentTutorialType = 0;    // opening

    [Tooltip("現在の的の出現回数")]
    private int _targetSpawnCount = 0;

    [Tooltip("存在する的の数")]
    private int _spawndTargetAmount = default;

    [Tooltip("最初に的に当たった")]
    private bool _isHitFirst = true;
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
        _textFrame.SetActive(false);
        _crystal.SetActive(false);
        Tutorial();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _textSystem.NextText();
        }
    }


    private void Tutorial()
    {
        StartCoroutine(CallText(2f));
    }

    /// <summary>
    /// チュートリアル進行処理
    /// </summary>
    private void ProgressingTheTutorial()
    {
        _currentTutorialType++;
        _isHitFirst = true;
        StartCoroutine(CallText(2f));
    }

    /// <summary>
    /// テキストを表示する
    /// </summary>
    /// <param name="waitTime">待機時間</param>
    /// <param name="currentTime">呼び出した時間</param>
    private IEnumerator CallText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _textFrame.SetActive(true);
        _textSystem.TextLikeSpeaking(_tutorialTextsData[(int)_currentTutorialType], this);
    }

    /// <summary>
    /// 的の出現用のコルーチンを呼び出す処理
    /// </summary>
    private void CallSpawn()
    {
        for (int i = 0; i < _stageDataTable._waveInformation[_targetSpawnCount]._targetData.Count; i++)
            StartCoroutine(SpawnTarget(i));

        _targetSpawnCount++;
    }

    /// <summary>
    /// 的のスポーン
    /// </summary>
    /// <param name="targetIndex">スポーンさせる的の数</param>
    /// <returns></returns>
    private IEnumerator SpawnTarget(int targetIndex)
    {
        // チュートリアル用のデータのパスを取得
        TargetDataTable dataPath = _stageDataTable._waveInformation[_targetSpawnCount]._targetData[targetIndex];

        // 時間差スポーン制御
        yield return new WaitForSeconds(dataPath._spawnDelay_s);

        GameObject target = _poolSystem.CallObject(PoolEnum.PoolObjectType.targetObject, dataPath._spawnPlace.position).gameObject;
        TargetMove targetMove = target.GetComponent<TargetMove>();
        targetMove.TargetData = dataPath;
        targetMove.InitializeWhenEnable();

        target.GetComponent<TargetStats>()._decrementTargetAmount = DecrementTargetAmount;

        _spawndTargetAmount++;
    }

    /// <summary>
    /// 的をデクリメントする処理
    /// </summary>
    private void DecrementTargetAmount()
    {
        _spawndTargetAmount--;

        if (_isHitFirst && _currentTutorialType == TutorialIventType.enchant2)
            StartCoroutine(RemoveTarget());

        if (_spawndTargetAmount <= 0)
            ProgressingTheTutorial();
    }

    /// <summary>
    /// 的の消去処理
    /// </summary>
    private IEnumerator RemoveTarget()
    {
        _isHitFirst = false;
        yield return new WaitForSeconds(0.5f);

        TargetMove[] targets = FindObjectsOfType<TargetMove>();

        for (int i = 0; i < targets.Length; i++)
            StartCoroutine(targets[i].RotateAtDespawn());
    }

    /// <summary>
    /// 弦を掴んだ
    /// </summary>
    public void OnGrabTheString()
    {

    }

    /// <summary>
    /// 吸い込み完了した
    /// </summary>
    public void OnAttractCompleted()
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
    /// ラジアルメニューが表示された
    /// </summary>
    public void OnRadialMenuDisplayed()
    {

    }

    /// <summary>
    /// テキスト表示の終了感知（共通）
    /// </summary>
    public void IsComplete()
    {
        _textFrame.SetActive(false);

        switch (_currentTutorialType)
        {
            // VRが見えるか確認した後
            case TutorialIventType.opening:

                // かかしを消す
                _kakashi.SetActive(false);
                ProgressingTheTutorial();
                break;

            // 弦を掴んだ後
            case TutorialIventType.shot1:

                //-----------------------------------------------
                // ここで弦を掴んだかどうか検知
                //-----------------------------------------------
                ProgressingTheTutorial();
                break;

            // 的に当てた後
            case TutorialIventType.shot2:

                // 的を出現させる
                CallSpawn();

                break;

            // ラジアルメニュー展開を指示した後
            case TutorialIventType.enchant1:

                //-----------------------------------------------
                // ここでラジアルメニューを検知
                //-----------------------------------------------
                ProgressingTheTutorial();
                break;

            // 爆発エンチャントの選択を指示した後
            case TutorialIventType.enchant2:

                //-----------------------------------------------
                // ここで爆発の選択を検知
                //-----------------------------------------------
                CallSpawn();
                // ここでは、矢が一発でもいずれかの的に当たったら次のチュートリアルへ進行する

                break;

            // 吸い込みの紹介をした後
            case TutorialIventType.attract1:

                _crystal.SetActive(true);
                StartCoroutine(_crystal.GetComponent<TutorialCrystalBreak>().Break());

                break;

            case TutorialIventType.attract2:

                break;

            default:
                break;
        }
    }
    #endregion
}
