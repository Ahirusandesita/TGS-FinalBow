// --------------------------------------------------------- 
// StageManager.cs 
// 
// CreateDay: 2023/06/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵スポーン機能の定義
/// </summary>
interface IStageSpawn
{
    /// <summary>
    /// ウェーブの実行処理
    /// </summary>
    void WaveExecution();
}

public class StageManager : MonoBehaviour, IStageSpawn, ISceneFadeCallBack
{
    /// <summary>
    /// ステージ情報クラス
    /// </summary>
    [Serializable]
    public class StageInformation
    {
        [Tooltip("ステージ名")]
        public string _stageName;

        [Tooltip("各ステージのスタート地点")]
        public Transform _stageTransform;
    }

    public enum GameIventType
    {
        missionStart,
        extraStage,
        wait
    }

    #region 変数
    [SerializeField, Tooltip("タグの名前")]
    private TagObject _PoolSystemTagData = default;

    [SerializeField, Tooltip("各ステージのスタート地点リスト")]
    private List<StageInformation> _stageTransforms = new();

    [SerializeField, Tooltip("Wave間の待ち時間")]
    private float _waveWait_s = 1.5f;

    [SerializeField, Tooltip("敵のスポーン座標テーブル")]
    private List<StageDataTable> _stageDataTables = new();

    [SerializeField, Tooltip("ボスのプレハブ")]
    private GameObject _bossPrefab;

    [SerializeField, Tooltip("プレイヤー")]
    private GameObject _player = default;

    [SerializeField, Tooltip("デバッグカメラ")]
    private GameObject _debugPlayer = default;

    [SerializeField, Tooltip("リザルト用Canvas")]
    private GameObject _resultCanvas = default;

    [SerializeField, Tooltip("ゲームスタート用Canvas")]
    private GameObject _startCanvas = default;

    [SerializeField, Tooltip("プレイありがとうキャンバス")]
    private GameObject _thankYouCanvas = default;

    [SerializeField, Tooltip("ResultCanvasの位置をプレイヤーからどれだけ離すか")]
    private float _resultCanvasPositionCorrectionValue = 20f;

    [SerializeField, Tooltip("GameCanvasの位置をプレイヤーの位置からどれだけ離すか")]
    private float _gameCanvasPositionCorrectionValue = 50f;

    [SerializeField]
    private SceneObject _sceneObject = default;


    [Tooltip("取得したObjectPoolSystemクラス")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("取得したリザルト用クラス")]
    private ResultStage _resultStage = default;

    private GameProgress _gameProgress;

    private GamePreparation _gamePreparation = default;

    private GamePreparation _clearPreparation = default;

    private GamePreparation _updatePreparation = default;

    [Tooltip("現在の雑魚/的の数")]
    private int _currentNumberOfObject = 0;

    [Tooltip("現在のステージ番号")]
    private int _currentStageIndex = 0; // チュートリアルステージ

    [Tooltip("現在のウェーブ番号")]
    private int _currentWaveIndex = 0;  // ウェーブ1

    [Tooltip("最初にウェーブ進行処理が走った")]
    private bool _isFirst = false;

    [Tooltip("フェードアウト完了")]
    private bool _isEndFadeOut = false;

    [Tooltip("フェードイン完了")]
    private bool _isEndFadeIn = false;

    private GameIventType _gameIventType = default;

    private Animator _UIAnimator = default;
    #endregion

    private void Awake()
    {
        _gamePreparation = GameObject.FindWithTag("StartUIPanel").GetComponent<GamePreparation>();
        _clearPreparation = GameObject.FindWithTag("ClearUIPanel").GetComponent<GamePreparation>();
        _updatePreparation = GameObject.FindWithTag("UpdateUIPanel").GetComponent<GamePreparation>();
        _clearPreparation.gameObject.SetActive(false);
        _updatePreparation.gameObject.SetActive(false);
        _UIAnimator = _startCanvas.GetComponentInChildren<Animator>();
        _thankYouCanvas.SetActive(false);

        _gameProgress = GameObject.FindObjectOfType<GameProgress>();
        _gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if (progressType == GameProgressType.gamePreparation)
                {
                    IEnumerator WaitFadeIn()
                    {
                        _isEndFadeOut = false;
                        MovingPlayer();
                        MovingGameCanvas();

                        this.SceneFadeInStart();
                        yield return new WaitUntil(() => _isEndFadeIn);

                        _isEndFadeIn = false;

                        yield return StartCoroutine(_gamePreparation.GamePreparationProcess());

                        _gameProgress.GamePreparationEnding();
                    }
                    StartCoroutine(WaitFadeIn());
                }

                if (progressType == GameProgressType.inGame)
                {
                    StartCoroutine(WaveStart());
                }

                if (progressType == GameProgressType.inGameLastStageEnd)
                {
                    IEnumerator WaitFadeOut()
                    {
                        MovingGameCanvas(indexCorrectionValue: -1);

                        StartCoroutine(_updatePreparation.MissionUpdateProcess(waitTime: 8f));
                        yield return new WaitForSeconds(3.5f);

                        StartCoroutine(_gamePreparation.WaitPerocess(waitTime: 6.5f));
                        this.SceneFadeOutStart();
                        yield return new WaitUntil(() => _isEndFadeOut);
                        _isEndFadeOut = false;

                        _gameProgress.InGameLastStageEnding();
                    }
                    StartCoroutine(WaitFadeOut());
                }

                if (progressType == GameProgressType.extraPreparation)
                {
                    // 暗転・移動・明転して「エクストラ開始」と出す
                    IEnumerator WaitFadeIn()
                    {
                        MovingPlayer();
                        MovingGameCanvas();

                        this.SceneFadeInStart();

                        yield return new WaitUntil(() => _isEndFadeIn);

                        _isEndFadeIn = false;

                        yield return StartCoroutine(_gamePreparation.ExtraPreparationProcess());

                        _gameProgress.ExtraPreparationEnding();
                    }
                    StartCoroutine(WaitFadeIn());
                }

                if (progressType == GameProgressType.extra)
                {
                    // 「1-5」敵のスポーン開始
                    StartCoroutine(WaveStart());
                }

                if (progressType == GameProgressType.ending)
                {
                    _thankYouCanvas.SetActive(true);
                }
            }
            );
    }

    private void Start()
    {
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();
        _resultStage = this.GetComponent<ResultStage>();

        // ステージ間リザルトの表示が終了したとき、ResultStageクラスでステージ進行処理が呼ばれる
        //_resultStage.readOnlyStateProperty.Subject.Subscribe(isResult => { if (!isResult) { ProgressingTheStage(); } });

        // ゲーム開始時にプレイヤーのTransformを更新する
        //MovingPlayer();

        // ゲームスタート
        //StartCoroutine(WaveStart());
    }

    private void Update()
    {
        if (_currentNumberOfObject <= 0 && _isFirst)
        {
            // 次のウェーブへ
            ProgressingTheWave();
            _isFirst = false;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            EnemyStats[] obj = FindObjectsOfType<EnemyStats>();

            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].TakeDamage(1);
            }
        }
#endif
    }


    public void WaveExecution()
    {
        _isFirst = true;

        try
        {
            // 最終ステージだったら、ボスをスポーン
            //if (_currentStageIndex == _stageDataTables.Count)
            //{
            //    // ボスをスポーン
            //    Instantiate(_bossPrefab);
            //    return;
            //}

            // EnemySpawnerTableで設定したスポナーの数を設定
            _currentNumberOfObject += _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._birdsData.Count;
            _currentNumberOfObject += _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._groundEnemysData.Count;

            // 各ウェーブで設定された数、鳥雑魚をスポーンさせる
            for (int i = 0; i < _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._birdsData.Count; i++)
            {
                // プールから呼び出す
                StartCoroutine(SpawnBird(listIndex: i));
            }

            // 各ウェーブで設定された数、地上雑魚をスポーンさせる
            for (int i = 0; i < _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._groundEnemysData.Count; i++)
            {
                StartCoroutine(SpawnGroundEnemy(listIndex: i));
            }
        }
        catch (Exception)
        {
            X_Debug.LogError("実装しているウェーブを超えているか、参照先のアタッチが外れています");
        }
    }

    /// <summary>
    /// スポーンさせたオブジェクトのデクリメント処理
    /// </summary>
    private void DecrementNumberOfObject()
    {
        _currentNumberOfObject--;
    }

    /// <summary>
    /// ウェーブを進める
    /// </summary>
    private void ProgressingTheWave()
    {
        _currentWaveIndex++;
        _currentNumberOfObject = 0;
        // すべてのウェーブをクリア = 次のステージに進む
        if (_currentWaveIndex >= _stageDataTables[_currentStageIndex]._waveInformation.Count)
        {
            // 次のステージへ
            ProgressingTheStage();
            return;
        }

        StartCoroutine(WaveStart());
    }

    /// <summary>
    /// ステージを進める
    /// </summary>
    private void ProgressingTheStage()
    {
        _currentStageIndex++;
        _currentWaveIndex = 0;

        //-------------------------------------------------------------------
        // ボスがいないときはマイナス1
        //-------------------------------------------------------------------
        // すべてのステージをクリア = ゲームオーバー
        if (_currentStageIndex >= _stageDataTables.Count)
        {
            IEnumerator WaitResult()
            {
                _gameProgress.ExtraEnding();

                yield return StartCoroutine(_clearPreparation.InGameLastStageEndProcess());

                // 暗転
                this.SceneFadeOutStart();
                yield return new WaitUntil(() => _isEndFadeOut);

                _isEndFadeOut = false;

                // プレイヤーをチュートリアルの場所に移動
                MovingPlayer();

                // 明転
                this.SceneFadeInStart();
                yield return new WaitUntil(() => _isEndFadeIn);

                _isEndFadeIn = false;

                yield return new WaitForSeconds(1f);

                // リザルト表示
                MovingResultCanvas();
                _gameProgress.ExtraClearEnding();
            }
            StartCoroutine(WaitResult());

            return;
        }
        else
        {
            _gameProgress.InGameEnding();
        }
    }

    /// <summary>
    /// ウェーブ開始（固定秒数待ってからスポーン開始）
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaveStart()
    {
        // 設定された秒数が経過したら、ステージスタート（読み込み待ち）
        yield return new WaitForSeconds(_stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._startDelay_s);

        WaveExecution();
    }

    /// <summary>
    /// 鳥雑魚を出現させるコルーチン
    /// </summary>
    /// <param name="listIndex">リストの添え字（for文のiをそのまま渡す）</param>
    /// <returns></returns>
    private IEnumerator SpawnBird(int listIndex)
    {
        PoolEnum.PoolObjectType selectedPrefab;
        BirdDataTable birdDataPath = _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._birdsData[listIndex];

        // 設定された秒数だけ待機する
        yield return new WaitForSeconds(birdDataPath._spawnDelay_s);


        // どの敵をスポーンさせるか判定（Scriptableから取得）
        switch (birdDataPath._birdType)
        {
            // 以下Enumの変換処理
            case BirdType.normalBird:
                selectedPrefab = PoolEnum.PoolObjectType.normalBird;
                break;

            case BirdType.bomberBug:
                selectedPrefab = PoolEnum.PoolObjectType.bomberBug;
                break;

            case BirdType.watcher:
                selectedPrefab = PoolEnum.PoolObjectType.watcher;
                break;

            case BirdType.giantBat:
                selectedPrefab = PoolEnum.PoolObjectType.giantBat;
                break;

            case BirdType.oceanDragon:
                selectedPrefab = PoolEnum.PoolObjectType.oceanDragon;
                break;

            case BirdType.desertDragon:
                selectedPrefab = PoolEnum.PoolObjectType.DesertDragon;
                break;

            // 例外処理
            default:
                selectedPrefab = PoolEnum.PoolObjectType.normalBird;
                break;
        }


        // 雑魚をプールから呼び出し、呼び出した各雑魚のデリゲート変数にデクリメント関数を登録
        GameObject temporaryObject = _objectPoolSystem.CallObject(selectedPrefab, birdDataPath._birdSpawnPlace.position).gameObject;
        temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;

        BirdMoveComponents birdMove = temporaryObject.GetComponent<BirdMoveComponents>();

        // 呼び出した雑魚の変数に設定
        birdMove.NumberOfBullet = birdDataPath._bullet;
        birdMove.NeedRoop = birdDataPath._needRoop;
        birdMove.GoalIndexOfRooop = birdDataPath._goalIndexOfRoop;
        birdMove.DespawnTime = birdDataPath._despawnTime_s;

        try
        {
            birdMove.StageTransform = _stageTransforms[_currentStageIndex]._stageTransform;
        }
        catch (Exception)
        {
            X_Debug.LogError("StageControllerにステージのTransformが設定されていません。");
            birdMove.StageTransform = null;
        }

        for (int i = 0; i < birdDataPath._birdGoalPlaces.Count; i++)
        {
            birdMove.MoveTypes = birdDataPath._birdGoalPlaces[i]._moveType;
            birdMove.GoalPositions = birdDataPath._birdGoalPlaces[i]._birdGoalPlace.position;
            birdMove.MovementSpeeds = birdDataPath._birdGoalPlaces[i]._speed;
            birdMove.ReAttackTimes = birdDataPath._birdGoalPlaces[i]._stayTime_s;

            birdMove.AttackIntervalTimes_Moving = birdDataPath._birdGoalPlaces[i]._attackInterval_s_a;
            birdMove.AttackIntervalTimes_Stopping = birdDataPath._birdGoalPlaces[i]._attackInterval_s_b;

            birdMove.BirdAttackTypes_Moving = birdDataPath._birdGoalPlaces[i]._birdAttackType_a;
            birdMove.BirdAttackTypes_Stopping = birdDataPath._birdGoalPlaces[i]._birdAttackType_b;

            birdMove.AttackTimings_Moving = birdDataPath._birdGoalPlaces[i]._attackTiming_s1_a;
            birdMove.AttackTimings_Stopping = birdDataPath._birdGoalPlaces[i]._attackTiming_s1_b;
            birdMove.AttackTimings_Moving = birdDataPath._birdGoalPlaces[i]._attackTiming_s2_a;
            birdMove.AttackTimings_Stopping = birdDataPath._birdGoalPlaces[i]._attackTiming_s2_b;
            birdMove.AttackTimings_Moving = birdDataPath._birdGoalPlaces[i]._attackTiming_s3_a;
            birdMove.AttackTimings_Stopping = birdDataPath._birdGoalPlaces[i]._attackTiming_s3_b;
            birdMove.AttackTimings_Moving = birdDataPath._birdGoalPlaces[i]._attackTiming_s4_a;
            birdMove.AttackTimings_Stopping = birdDataPath._birdGoalPlaces[i]._attackTiming_s4_b;
            birdMove.AttackTimings_Moving = birdDataPath._birdGoalPlaces[i]._attackTiming_s5_a;
            birdMove.AttackTimings_Stopping = birdDataPath._birdGoalPlaces[i]._attackTiming_s5_b;

            birdMove.AttackTimes_Moving = birdDataPath._birdGoalPlaces[i]._attackTimes_a;
            birdMove.AttackTimes_Stopping = birdDataPath._birdGoalPlaces[i]._attackTimes_b;

            birdMove.CooldownTime_Moving = birdDataPath._birdGoalPlaces[i]._cooldownTime_s_a;
            birdMove.CooldownTime_Stopping = birdDataPath._birdGoalPlaces[i]._cooldownTime_s_b;

            birdMove.ConsecutiveIntervalTimes_Moving = birdDataPath._birdGoalPlaces[i]._consecutiveAttackInterval_s_a;
            birdMove.ConsecutiveIntervalTimes_Stopping = birdDataPath._birdGoalPlaces[i]._consecutiveAttackInterval_s_b;

            birdMove.MoveSpeedArcs = birdDataPath._birdGoalPlaces[i]._arcHeight;
            birdMove.ArcMoveDirections = birdDataPath._birdGoalPlaces[i]._arcMoveDirection;

            birdMove.DirectionTypes_Moving = birdDataPath._birdGoalPlaces[i]._directionType_moving;
            birdMove.DirectionTypes_Stopping = birdDataPath._birdGoalPlaces[i]._directionType_stopping;
            birdMove.DirectionTypes_Attack = birdDataPath._birdGoalPlaces[i]._directionType_attack;
        }

        birdMove.BirdEnable();
    }

    private IEnumerator SpawnGroundEnemy(int listIndex)
    {
        GroundEnemyDataTable dataPath = _stageDataTables[_currentStageIndex]._waveInformation[_currentWaveIndex]._groundEnemysData[listIndex];

        yield return new WaitForSeconds(dataPath._spawnDelay_s);

        GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.groundEnemy, dataPath._groundEnemySpawnPlace.position).gameObject;

        GroundEnemyMoveBase temporaryMove = temporaryObject.GetComponent<GroundEnemyMoveBase>();
        temporaryMove.GroundEnemyData = dataPath;
        temporaryMove.InitializeOnEnable();

        temporaryObject.GetComponent<GroundEnemyStats>()._onDeathEnemy = DecrementNumberOfObject;
    }

    /// <summary>
    /// プレイヤーを移動させる
    /// </summary>
    private void MovingPlayer()
    {
        _player.transform.position = _stageTransforms[_currentStageIndex]._stageTransform.position;
        _player.transform.rotation = _stageTransforms[_currentStageIndex]._stageTransform.rotation;
        _debugPlayer.transform.position = _stageTransforms[_currentStageIndex]._stageTransform.position;
        _debugPlayer.transform.rotation = _stageTransforms[_currentStageIndex]._stageTransform.rotation;
    }

    /// <summary>
    /// リザルトキャンバスを移動させる
    /// </summary>
    private void MovingResultCanvas()
    {
        _resultCanvas.transform.position = _stageTransforms[_currentStageIndex]._stageTransform.position + _player.transform.forward * _resultCanvasPositionCorrectionValue;
        _resultCanvas.transform.rotation = _player.transform.rotation;
    }

    /// <summary>
    /// ゲーム進行キャンバスを移動させる
    /// </summary>
    private void MovingGameCanvas(int indexCorrectionValue = 0)
    {
        _startCanvas.transform.position = _stageTransforms[_currentStageIndex + indexCorrectionValue]._stageTransform.position + _player.transform.forward * _gameCanvasPositionCorrectionValue;
        _startCanvas.transform.rotation = _player.transform.rotation;
    }

    public void SceneFadeInComplete()
    {
        _isEndFadeIn = true;
    }

    public void SceneFadeOutComplete()
    {
        _isEndFadeOut = true;
    }
}