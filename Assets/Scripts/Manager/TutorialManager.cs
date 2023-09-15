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
    ending
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

    [SerializeField]
    private Transform _crystalTransform = default;

    [SerializeField]
    private SceneObject _sceneObject = default;

    [SerializeField]
    private Transform _playerTransform = default;

    [SerializeField]
    private GameObject _player = default;

    [SerializeField]
    private GameObject _debugPlayer = default;


    private ScoreFrameMaganer _frameManager = default;

    private InputManagement _input = default;

    [Tooltip("チュートリアルの進行度")]
    private TutorialIventType _currentTutorialType = 0;    // opening

    [Tooltip("現在の的の出現回数")]
    private int _targetSpawnCount = 0;

    [Tooltip("存在する的の数")]
    private int _spawndTargetAmount = default;

    [Tooltip("最初に的に当たった")]
    private bool _isHitFirst = true;

    [Tooltip("最初に弦を掴んだ")]
    private bool _isGrabTheStringFirst = true;

    [Tooltip("最初に吸い込みした")]
    private bool _isAttractCompletedFirst = true;

    [Tooltip("最初に矢を撃った")]
    private bool _isShotFirst = true;

    [Tooltip("最初にボムが選ばれた")]
    private bool _isSelectedBombFirst = true;

    [Tooltip("最初にラジアルメニューが表示された")]
    private bool _isRadialMenuDisplayedFirst = true;

    [Tooltip("弦を掴んだ通知の許可")]
    private bool _canGrabTheString = false;

    [Tooltip("吸い込みした通知の許可")]
    private bool _canAttractCompleted = false;

    [Tooltip("矢を撃った通知の許可")]
    private bool _canShotFirst = false;

    [Tooltip("ボムが選ばれた通知の許可")]
    private bool _canSelectedBomb = false;

    [Tooltip("ラジアルメニューが表示された通知の許可")]
    private bool _canRadialMenuDisplayed = false;

    [Tooltip("リスタート")]
    private bool _isReStart = false;

    [Tooltip("最初のトリガー入力")]
    private bool _inputFirst = true;

    private bool _isHit { get; set; }

    private GameProgress gameProgress;
    #endregion

    #region property
    #endregion

    #region method
    private void Awake()
    {
        gameProgress = GameObject.FindObjectOfType<GameProgress>();
        gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if(progressType == GameProgressType.tutorial)
                {
                    Tutorial();
                }
            }
            );

        try
        {
            _poolSystem = GameObject.FindWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
        }
        catch (Exception)
        {
            X_Debug.LogError("ObjectPoolSystemがアタッチされていません。");
        }

        try
        {
            _frameManager = _textFrame.GetComponent<ScoreFrameMaganer>();
        }
        catch (Exception)
        {
            X_Debug.LogError("ScoreFrameManagerが取得できていません。");
        }

        try
        {
            _input = GameObject.FindWithTag("InputController").GetComponent<InputManagement>();
        }
        catch (Exception)
        {
            X_Debug.LogError("InputManagemantが取得できていません。");
        }
    }

    private void Start()
    {
        _player.transform.position = _playerTransform.position;
        _player.transform.rotation = _playerTransform.rotation;
        _debugPlayer.transform.position = _playerTransform.position;
        _debugPlayer.transform.rotation = _playerTransform.rotation;
    }

    private void Update()
    {
        if (_input.ButtonLeftDownTrigger() || _input.ButtonRightDownTrigger() || _input.ButtonLeftUpTrigger() || _input.ButtonRightUpTrigger())
        {
            if (_inputFirst)
            {
                _inputFirst = false;
                _textSystem.NextText();
            }
        }
        else
        {
            _inputFirst = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _textSystem.NextText();
        }

        if (Input.GetKeyDown(KeyCode.F1))
            OnGrabTheString();

        if (Input.GetKeyDown(KeyCode.F2))
            OnRadialMenuDisplayed();

        if (Input.GetKeyDown(KeyCode.F3))
            OnSelectedBomb();
    }


    private void Tutorial()
    {
        StartCoroutine(CallText(1f));
    }

    /// <summary>
    /// チュートリアル進行処理
    /// </summary>
    private void ProgressingTheTutorial()
    {
        _currentTutorialType++;
        _isHitFirst = true;
        _isHit = false;

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
        _frameManager.OpenFrame();

        while (!_frameManager._endOpen)
            yield return null;

        _frameManager._endOpen = false;

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

        // 最初の爆発
        if (_isHitFirst && (_currentTutorialType == TutorialIventType.enchant2 || _currentTutorialType == TutorialIventType.attract1))
        {
            _isHitFirst = false;
            //_isHit = true;
            //_canShotFirst = false;
            StartCoroutine(RemoveTarget());
        }

        if (_isReStart && _currentTutorialType == TutorialIventType.attract1)
        {

        }

        if (_spawndTargetAmount <= 0)
        {
            if (_isReStart)
            {
                _isReStart = false;
                _isHit = false;
                return;
            }

            ProgressingTheTutorial();
        }
    }

    /// <summary>
    /// 的の消去処理
    /// </summary>
    private IEnumerator RemoveTarget()
    {
        yield return new WaitForSeconds(0.8f);

        TargetMove[] targets = FindObjectsOfType<TargetMove>();

        //if (targets.Length == 0)
        //{
        //    ProgressingTheTutorial();
        //}

        for (int i = 0; i < targets.Length; i++)
        {
            StartCoroutine(targets[i].RotateAtDespawn());
        }
    }

    /// <summary>
    /// 弦を掴んだ
    /// </summary>
    public void OnGrabTheString()
    {
        if (_isGrabTheStringFirst && _canGrabTheString)
        {
            _isGrabTheStringFirst = false;
            ProgressingTheTutorial();
        }
    }

    /// <summary>
    /// 吸い込み完了した
    /// </summary>
    public void OnAttractCompleted()
    {
        if (_isAttractCompletedFirst && _canAttractCompleted)
        {
            _isAttractCompletedFirst = false;
            _canAttractCompleted = false;
            _canShotFirst = true;
            CallSpawn();
        }
    }

    /// <summary>
    /// 矢を発射した
    /// </summary>
    public void OnShot()
    {
        //if (_isShotFirst && _canShotFirst)
        //{
        //    _isShotFirst = false;
        //    StartCoroutine(WaitPossibleHit());

        //    if (_isHit)
        //    {
        //        return;
        //    }

        //    _isReStart = true;
        //    _targetSpawnCount--;

        //    StartCoroutine(RemoveTarget());

        //    StartCoroutine(WaitTargetDespawn());

        //    // リセット
        //    _isAttractCompletedFirst = true;
        //    _canAttractCompleted = true;
        //}
    }

    /// <summary>
    /// ボムが選ばれた
    /// </summary>
    public void OnSelectedBomb()
    {
        if (_isSelectedBombFirst && _canSelectedBomb)
        {
            _isSelectedBombFirst = false;
            CallSpawn();
        }
    }

    /// <summary>
    /// ラジアルメニューが表示された
    /// </summary>
    public void OnRadialMenuDisplayed()
    {
        if (_isRadialMenuDisplayedFirst && _canRadialMenuDisplayed)
        {
            _isRadialMenuDisplayedFirst = false;
            ProgressingTheTutorial();
        }
    }

    /// <summary>
    /// テキスト表示の終了感知（共通）
    /// </summary>
    public void AllComplete()
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

                // ここで弦を掴んだかどうか検知
                _canGrabTheString = true;
                break;

            // 的当てテキストの後
            case TutorialIventType.shot2:

                // 的を出現させる
                CallSpawn();
                break;

            // ラジアルメニュー展開を指示した後
            case TutorialIventType.enchant1:

                // ここでラジアルメニューを検知
                _canRadialMenuDisplayed = true;
                break;

            // 爆発エンチャントの選択を指示した後
            case TutorialIventType.enchant2:

                // ここで爆発の選択を検知
                _canSelectedBomb = true;
                // ここでは、矢が一発でもいずれかの的に当たったら次のチュートリアルへ進行する

                break;

            // 吸い込みの紹介をした後
            case TutorialIventType.attract1:

                // クリスタルを出現させて、即割る
                StartCoroutine(Instantiate(_crystal, _crystalTransform.position, Quaternion.identity).GetComponent<TutorialCrystalBreak>().Break());

                // ここで吸い込みを感知
                _canAttractCompleted = true;

                break;

            case TutorialIventType.ending:
                //FindObjectOfType<SceneManagement>().SceneLoadSpecifyMove(_sceneObject);
                gameProgress.TutorialEnding();
                break;
        }
    }

    public void ResponseComplete() { }

    private IEnumerator WaitTargetDespawn()
    {
        float wait = 1f;
        float time = 0f;

        while (time <= wait)
        {
            time += Time.deltaTime;
            yield return null;
        }

        // クリスタルを出現させて、即割る
        StartCoroutine(Instantiate(_crystal, _crystalTransform.position, Quaternion.identity).GetComponent<TutorialCrystalBreak>().Break());
        _isShotFirst = true;
    }

    private IEnumerator WaitPossibleHit()
    {
        yield return new WaitForSeconds(1.0f);
    }
    #endregion
}
