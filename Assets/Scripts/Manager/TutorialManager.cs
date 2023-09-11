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
}

public class TutorialManager : MonoBehaviour, ITextLikeSpeaking
{
    #region class
    /// <summary>
    /// TutorialOpeningテキスト
    /// </summary>
    private class TutorialOpening : ITextLikeSpeaking
    {
        private GameObject _kakashi;
        private Action _action;

        /// <param name="kakashi">かかし</param>
        /// <param name="action">ProgressingTheTutorial</param>
        public TutorialOpening(GameObject kakashi, Action action)
        {
            _kakashi = kakashi;
            _action = action;
        }

        public void IsComplete()
        {
            _kakashi.SetActive(false);
            _action();
        }
    }

    /// <summary>
    /// TutorialShot1テキスト
    /// </summary>
    private class TutorialShot1 : ITextLikeSpeaking
    {
        public void IsComplete()
        {
            // 弦を掴んだ後の処理
        }
    }

    private class TutorialShot2 : ITextLikeSpeaking
    {
        public delegate IEnumerator TargetSpawn(int a);

        private int _targetAmount;
        private TargetSpawn _targetSpawn;
        private MonoBehaviour _mono;
        private Action _action;

        /// <param name="targetAmount">スポーンする数</param>
        /// <param name="action">ProgressingTheTutorial</param>
        public TutorialShot2(int targetAmount, TargetSpawn targetSpawn, MonoBehaviour mono, Action action)
        {
            _targetAmount = targetAmount;
            _targetSpawn = targetSpawn;
            _mono = mono;
            _action = action;
        }

        public void IsComplete()
        {
            for (int i = 0; i < _targetAmount; i++)
            {
                _mono.StartCoroutine(_targetSpawn(i));
            }

            _action();
        }
    }
    #endregion

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

    [Tooltip("チュートリアルの進行度")]
    private TutorialIventType _currentTutorialType = 0;    // opening

    [Tooltip("現在の的の出現回数")]
    private int _targetSpawnCount = 0;

    [Tooltip("テキスト表示が終了")]
    private bool _isFinishTextDisplayed = false;
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
        //StartCoroutine(SpawnTarget(0));
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
        StartCoroutine(CallText(2f, _currentTutorialType));
    }

    /// <summary>
    /// チュートリアル進行処理
    /// </summary>
    private void ProgressingTheTutorial()
    {
        _currentTutorialType++;
        StartCoroutine(CallText(2f, _currentTutorialType));
    }

    /// <summary>
    /// テキストを表示する
    /// </summary>
    /// <param name="waitTime">待機時間</param>
    /// <param name="currentTime">呼び出した時間</param>
    private IEnumerator CallText(float waitTime, TutorialIventType textIndex)
    {
        _isFinishTextDisplayed = false;

        yield return new WaitForSeconds(waitTime);
        _textSystem.TextLikeSpeaking(_tutorialTextsData[(int)textIndex], GenerateClass(_currentTutorialType));
    }

    /// <summary>
    /// チュートリアルの進行度によって、クラスのインスタンスを作る処理
    /// </summary>
    /// <param name="tutorialType">現在の</param>
    /// <returns></returns>
    private ITextLikeSpeaking GenerateClass(TutorialIventType tutorialType)
    {
        ITextLikeSpeaking generatedClass = default;

        switch (tutorialType)
        {
            case TutorialIventType.opening:

                generatedClass = new TutorialOpening(_kakashi, ProgressingTheTutorial);
                break;

            case TutorialIventType.shot1:

                generatedClass = this;
                break;

            case TutorialIventType.shot2:

                generatedClass = new TutorialShot2(_stageDataTable._waveInformation[_targetSpawnCount]._targetData.Count, SpawnTarget, this, ProgressingTheTutorial);
                break;

            default:
                break;
        }

        return generatedClass;
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

        TargetMove target = _poolSystem.CallObject(PoolEnum.PoolObjectType.targetObject, dataPath._spawnPlace.position).GetComponent<TargetMove>();
        target.TargetData = dataPath;

        target.InitializeWhenEnable();
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
        _isFinishTextDisplayed = true;
        //---------------------------------------------------
        ProgressingTheTutorial();// 仮
        //---------------------------------------------------
    }
    #endregion
}
