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

public enum TutorialTextType
{
    /// <summary>
    /// …VRが正常に見えるかどうか確認してください
    /// </summary>
    opening,
    /// <summary>
    /// …すべての的に矢を当ててみましょう
    /// </summary>
    shot1,
    /// <summary>
    /// …コントローラーの左スティックを倒してみましょう
    /// </summary>
    enchant1,
    /// <summary>
    /// …試しに、爆発/Explosionを選んでみましょう
    /// </summary>
    enchant2,
    /// <summary>
    /// …吸い込んだ分エンチャントが強化されます
    /// </summary>
    attract1,
    /// <summary>
    /// …お見事！
    /// </summary>
    end_A,
    /// <summary>
    /// …こんな感じに
    /// </summary>
    end_B,
    /// <summary>
    /// …うまく使って敵を倒しましょう
    /// </summary>
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

    [Tooltip("矢が外れたとき、矢を生成する")]
    public Action _onArrowMissed_Create = default;

    [Tooltip("矢が外れたとき、強制的に矢を飛ばす")]
    public Action<Vector3> _onArrowMissed_Shot = default;


    private ScoreFrameMaganer _frameManager = default;

    private InputManagement _input = default;

    private VR_BowManager _VRbowManager = default;


    [Tooltip("チュートリアルの進行度")]
    private TutorialTextType _currentTutorialType = 0;    // opening

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

    [Tooltip("最初にラジアルメニューが表示された")]
    private bool _isRadialMenuDisplayedFirst = true;

    [Tooltip("ボムが選ばれた")]
    private bool _isSelectedBomb = false;

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

    [Tooltip("最初のトリガー入力")]
    private bool _inputFirst = true;

    private bool _isHit = default;

    private GameProgress gameProgress;

    //#if UNITY_EDITOR
    [HideInInspector]
    public bool _skipTutorial = default;
    //#endif
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
                if (progressType == GameProgressType.tutorial)
                {
                    //#if UNITY_EDITOR
                    if (_skipTutorial)
                    {
                        gameProgress.TutorialEnding();
                        this.enabled = false;
                        return;
                    }
                    //#endif

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

        try
        {
            _VRbowManager = FindObjectOfType<VR_BowManager>();
        }
        catch (Exception)
        {
            X_Debug.LogError("VR_BowManagerが取得できていません。");
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

        if (Input.GetKeyDown(KeyCode.O))
        {
            _textSystem.NextText();
        }

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
    private void ProgressingTheTutorial(float textTime = 1f)
    {
        _currentTutorialType++;
        _isHitFirst = true;

        StartCoroutine(CallText(textTime));
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
        _isHit = true;

        // 最初の爆発
        if (_isHitFirst && _currentTutorialType == TutorialTextType.enchant2)
        {
            _isHitFirst = false;
            StartCoroutine(RemoveTarget());
        }

        if (_spawndTargetAmount <= 0)
        {
            if (_currentTutorialType == TutorialTextType.end_B)
            {
                ProgressingTheTutorial(2f);
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
        yield return new WaitForSeconds(0.35f);

        TargetMove[] targets = FindObjectsOfType<TargetMove>();

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<TargetStats>()._canDeath = false;
            StartCoroutine(targets[i].RotateAtDespawn());
        }
    }

    /// <summary>
    /// 弦を掴んだ
    /// </summary>
    public void OnGrabTheString()
    {
        //if (_isGrabTheStringFirst && _canGrabTheString)
        //{
        //    _isGrabTheStringFirst = false;
        //    ProgressingTheTutorial();
        //}
    }

    /// <summary>
    /// 吸い込み完了した
    /// </summary>
    public void OnAttractCompleted()
    {
        if (_isAttractCompletedFirst && _canAttractCompleted)
        {
            _isAttractCompletedFirst = false;
            _isHit = false;
            _canShotFirst = true;
            CallSpawn();
        }
    }

    /// <summary>
    /// 矢を発射した
    /// </summary>
    public void OnShot()
    {
        if (_isShotFirst && _canShotFirst)
        {
            _isShotFirst = false;

            IEnumerator WaitPossibleHit()
            {
                yield return new WaitForSeconds(0.5f);

                if (_isHit) yield break;

                _currentTutorialType++;

                // 当たってなかったら的を下げる
                StartCoroutine(RemoveTarget());

                // 外したとき用のテキスト分岐
                CallText(0f);
            }

            StartCoroutine(WaitPossibleHit());
        }
    }

    /// <summary>
    /// ボムが選ばれた
    /// </summary>
    public void OnSelectedBomb()
    {
        if (_canSelectedBomb)
        {
            _canSelectedBomb = false;
            _isSelectedBomb = true;
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
            case TutorialTextType.opening:

                // かかしを消す
                _kakashi.SetActive(false);
                ProgressingTheTutorial();
                break;

            case TutorialTextType.shot1:

                // 的を出現させる
                CallSpawn();
                break;

            case TutorialTextType.enchant1:

                // ここでラジアルメニューを検知
                _canRadialMenuDisplayed = true;
                // 同時に爆発の選択も検知
                _canSelectedBomb = true;
                break;

            case TutorialTextType.attract1:

                // クリスタルを出現させて、即割る
                StartCoroutine(Instantiate(_crystal, _crystalTransform.position, Quaternion.identity).GetComponent<TutorialCrystalBreak>().Break());

                // ここで吸い込みを感知
                _canAttractCompleted = true;
                break;

            case TutorialTextType.end_A:

                _currentTutorialType++;
                ProgressingTheTutorial();
                break;

            case TutorialTextType.ending:

                _VRbowManager.CantShotBecauseYouMissed = true;
                FindObjectOfType<FPSBow>().CantDrawBowBecauseYouMissed = true;
                gameProgress.TutorialEnding();
                this.enabled = false;
                break;
        }
    }

    /// <summary>
    /// 最後のテキストが表示されたときに呼ばれる
    /// </summary>
    public void ResponseComplete()
    {
        switch (_currentTutorialType)
        {
            case TutorialTextType.enchant1:

                break;

            case TutorialTextType.enchant2:

                // テキストが表示されたタイミングで既に爆発が選択されていたら、即時的を出す
                if (_isSelectedBomb)
                {
                    IEnumerator WaitBomb()
                    {
                        yield return new WaitForSeconds(1f);

                        CallSpawn();
                        _textSystem.NextText();
                    }
                    StartCoroutine(WaitBomb());
                }
                // そうじゃなければ、ボムが選択されるまで非同期で待機し、的をスポーン
                else
                {
                    IEnumerator WaitBomb() { yield return new WaitUntil(() => _isSelectedBomb); CallSpawn(); }
                    StartCoroutine(WaitBomb());
                }

                break;

            case TutorialTextType.end_B:

                IEnumerator WaitSpawn()
                {
                    yield return new WaitForSeconds(1f);

                    // 的を再スポーン
                    _targetSpawnCount--;
                    CallSpawn();
                    _textSystem.NextText();
                    // 矢を撃てなくする
                    _VRbowManager.CantShotBecauseYouMissed = false;
                    FindObjectOfType<FPSBow>().CantDrawBowBecauseYouMissed = false;

                    yield return new WaitForSeconds(0.8f);

                    // 強制的に矢を飛ばして的に当てる
                    _onArrowMissed_Create();
                    _onArrowMissed_Shot(_stageDataTable._waveInformation[2]._targetData[2]._spawnPlace.position + Vector3.up * 15f);
                }
                StartCoroutine(WaitSpawn());

                break;
        }
    }
    #endregion
}
