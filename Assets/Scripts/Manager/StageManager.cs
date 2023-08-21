// --------------------------------------------------------- 
// StageManager.cs 
// 
// CreateDay: 2023/06/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using System;
using System.Collections;
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

/// <summary>
/// ウェーブの種類
/// </summary>
public enum WaveType
{
    zakoWave1,
    zakoWave2,
    zakoWave3,
    zakoWave4,
    zakoWave5,
    boss
}

public class StageManager : MonoBehaviour, IStageSpawn
{
    #region 変数
    [Tooltip("タグの名前")]
    public TagObject _PoolSystemTagData = default;

    [Tooltip("敵のスポーン座標テーブル")]
    public WaveManagementTable _waveManagementTable = default;

    [Tooltip("ボスのプレハブ")]
    public GameObject _bossPrefab;


    [Tooltip("取得したObjectPoolSystemクラス")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("現在の雑魚/的の数")]
    private int _currentNumberOfObject = 0;

    [Tooltip("現在のウェーブ数")]
    private WaveType _currentWave = WaveType.zakoWave1;     // 初期値0

    [Tooltip("現在実行中のコルーチン")]
    private Coroutine _currentActiveCoroutine = default;
    #endregion


    private void Start()
    {
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();

        if (_waveManagementTable._waveInformation.Count > 0)
        {
            // ゲームスタート
            _currentActiveCoroutine = StartCoroutine(WaveStart());
        }

        for (int i = 0; i < _waveManagementTable._groundEnemyInformation.Count; i++)
        {
            StartCoroutine(GroundEnemySpawn(listIndex: i));
        }
    }


    public void WaveExecution()
    {
        // コルーチンが動いていたら止める
        //StopCoroutine(_currentActiveCoroutine);

        try
        {
            if (_currentWave == WaveType.boss)
            {
                // ボスをスポーン
                Instantiate(_bossPrefab);
                return;
            }

            // EnemySpawnerTableで設定したスポナーの数を設定
            _currentNumberOfObject += _waveManagementTable._waveInformation[(int)_currentWave]._birdsData.Count;

            // EnemySpawnerTableで設定したスポナーの数だけ雑魚をスポーンさせる
            for (int i = 0; i < _waveManagementTable._waveInformation[(int)_currentWave]._birdsData.Count; i++)
            {
                // プールから呼び出す
                StartCoroutine(Spawn(listIndex: i));
            }

            // 次のWaveの強制実行までのカウントダウンを開始する
            //_currentActiveCoroutine = StartCoroutine(WaveStart());
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

        if (_currentNumberOfObject <= 0)
        {
            _currentWave++;
            // ウェーブを進める
            WaveExecution();
        }
    }

    /// <summary>
    /// Wave開始（n秒待ってからスポーンさせる）
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaveStart()
    {
        // 設定された秒数が経過したら、強制的にウェーブを進行させる
        float waitTime = _waveManagementTable._waveInformation[(int)_currentWave]._startWaveTime_s;
        yield return new WaitForSeconds(waitTime);

        WaveExecution();
    }

    /// <summary>
    /// 敵を出現させるコルーチン
    /// </summary>
    /// <param name="listIndex">リストの添え字（for文のiをそのまま渡す）</param>
    /// <returns></returns>
    private IEnumerator Spawn(int listIndex)
    {
        PoolEnum.PoolObjectType selectedPrefab;
        BirdDataTable birdDataPath = _waveManagementTable._waveInformation[(int)_currentWave]._birdsData[listIndex];

        // 設定された秒数だけ待機する
        yield return new WaitForSeconds(birdDataPath._spawnDelay_s);


        // どの敵をスポーンさせるか判定（Scriptableから取得）
        switch (birdDataPath._birdType)
        {
            // 以下Enumの変換処理
            case BirdType.normalBird:
                selectedPrefab = PoolEnum.PoolObjectType.normalBird;
                break;

            case BirdType.bombBird:
                selectedPrefab = PoolEnum.PoolObjectType.bombBird;
                break;

            case BirdType.penetrateBird:
                selectedPrefab = PoolEnum.PoolObjectType.penetrateBird;
                break;

            case BirdType.thunderBird:
                selectedPrefab = PoolEnum.PoolObjectType.thunderBird;
                break;

            case BirdType.bombBirdBig:
                selectedPrefab = PoolEnum.PoolObjectType.bombBirdBig;
                break;

            case BirdType.thunderBirdBig:
                selectedPrefab = PoolEnum.PoolObjectType.thunderBirdBig;
                break;

            case BirdType.penetrateBirdBig:
                selectedPrefab = PoolEnum.PoolObjectType.penetrateBirdBig;
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
        birdMove.AttackIntervalTime = birdDataPath._attackInterval_s;

        for (int i = 0; i < birdDataPath._birdGoalPlaces.Count; i++)
        {
            birdMove.MoveTypes = birdDataPath._birdGoalPlaces[i]._moveType;
            birdMove.GoalPositions = birdDataPath._birdGoalPlaces[i]._birdGoalPlace.position;
            birdMove.MovementSpeeds = birdDataPath._birdGoalPlaces[i]._speed;
            birdMove.ReAttackTimes = birdDataPath._birdGoalPlaces[i]._stayTime_s;

            birdMove.MoveSpeedArcs = birdDataPath._birdGoalPlaces[i]._arcHeight;
            birdMove.ArcMoveDirections = birdDataPath._birdGoalPlaces[i]._arcMoveDirection;
        }
    }

    private IEnumerator GroundEnemySpawn(int listIndex)
    {
        GroundEnemyDataTable dataPath = _waveManagementTable._groundEnemyInformation[listIndex];

        yield return new WaitForSeconds(dataPath._spawnDelay_s);

        GroundEnemyMoveBase temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.groundEnemy, dataPath._groundEnemySpawnPlace.position).GetComponent<GroundEnemyMoveBase>();
        temporaryObject._attackType = dataPath._attackType;
        temporaryObject._reAttackTime_s = dataPath._reAttackTime_s;
        temporaryObject._despawnTime_s = dataPath._despawnTime_s;
        //temporaryObject._jumpDirectionState = dataPath._groundEnemyActionInformation[listIndex]._jumpDirectionState;
    }
}