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

public class StageManager : MonoBehaviour, IStageSpawn
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

    [SerializeField, Tooltip("Canvasの位置をプレイヤーの位置からどれだけ離すか")]
    private float _canvasPositionCorrectionValue = 50f;

    [SerializeField]
    private SceneObject _sceneObject = default;


    [Tooltip("取得したObjectPoolSystemクラス")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("取得したリザルト用クラス")]
    private ResultStage _resultStage = default;
    private GameProgress gameProgress;

    [Tooltip("現在の雑魚/的の数")]
    private int _currentNumberOfObject = 0;

    [Tooltip("現在のステージ番号")]
    private int _currentStageIndex = 0; // チュートリアルステージ

    [Tooltip("現在のウェーブ番号")]
    private int _currentWaveIndex = 0;  // ウェーブ1
    #endregion

    private void Awake()
    {
        gameProgress = GameObject.FindObjectOfType<GameProgress>();
        gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if (progressType == GameProgressType.ending)
                {
                    ProgressingTheStage();
                }
                if (progressType == GameProgressType.inGame)
                {
                    StartCoroutine(WaveStart());
                }
                if (progressType == GameProgressType.gamePreparation)
                {
                    MovingPlayer();
                    MovingStartCanvas();
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

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EnemyStats[] obj = FindObjectsOfType<EnemyStats>();

            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].TakeDamage(1);
            }
        }
    }
#endif


    public void WaveExecution()
    {
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

        if (_currentNumberOfObject == 0)
        {
            // 次のウェーブへ
            ProgressingTheWave();
        }
    }

    /// <summary>
    /// ウェーブを進める
    /// </summary>
    private void ProgressingTheWave()
    {
        _currentWaveIndex++;
        _currentNumberOfObject = 0;
        X_Debug.Log("次のウェーブへ");

        // すべてのウェーブをクリア = 次のステージに進む
        if (_currentWaveIndex >= _stageDataTables[_currentStageIndex]._waveInformation.Count)
        {
            // 次のステージへ
            X_Debug.Log("ステージクリア");
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
        X_Debug.Log("次のステージへ");

        //-------------------------------------------------------------------
        // ボスがいないときはマイナス1
        //-------------------------------------------------------------------
        if (_currentStageIndex > _stageDataTables.Count - 1)
        {
            // すべてのステージをクリア = ゲームオーバー
            // ボス倒したら終了だからここ動かないかも
            //FindObjectOfType<SceneManagement>().SceneLoadSpecifyMove(_sceneObject);
            MovingResultCanvas();
            gameProgress.InGameEnding();
            X_Debug.Log("ゲーム終了");
            return;
        }

        MovingPlayer();
        StartCoroutine(WaveStart());
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
        _resultCanvas.transform.position = _stageTransforms[_currentStageIndex - 1]._stageTransform.position + _player.transform.forward * _canvasPositionCorrectionValue;
        _resultCanvas.transform.rotation = _player.transform.rotation;
    }

    private void MovingStartCanvas()
    {
        _startCanvas.transform.position = _stageTransforms[_currentStageIndex]._stageTransform.position + _player.transform.forward * _canvasPositionCorrectionValue;
        _startCanvas.transform.rotation = _player.transform.rotation;
    }
}