// --------------------------------------------------------- 
// StageManager.cs 
// 
// CreateDay: 2023/06/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using System;
using UnityEngine;

/// <summary>
/// 敵スポーン機能の定義
/// </summary>
interface IStageSpawn
{
    /// <summary>
    /// 敵のスポーン処理
    /// </summary>
    void Spawn();
}

/// <summary>
/// ウェーブの種類
/// </summary>
public enum WaveType
{
    zakoWave1,
    zakoWave2,
    zakoWave3,
    boss
}

public class StageManager : MonoBehaviour, IStageSpawn
{
    #region 変数
    [Tooltip("タグの名前")]
    public TagObject _PoolSystemTagData = default;

    [Tooltip("敵のスポーン座標テーブル")]
    public EnemySpawnerTable _enemySpawnerTable = default;


    [Tooltip("取得したObjectPoolSystemクラス")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("現在の雑魚/的の数")]
    private int _currentNumberOfObject = default;

    [Tooltip("現在のウェーブ数")]
    private WaveType _waveType = WaveType.zakoWave1;     // 初期値0


    public GameObject _bossTest;
    #endregion


    private void Start()
    {
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();
    }

    private void Update()
    {
        // デバッグ用
        if (Input.GetKeyDown(KeyCode.P))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        try
        {
            if (_waveType == WaveType.boss)
            {
                // ボスをスポーン

                Instantiate(_bossTest);
                return;
            }

            // EnemySpawnerTableで設定したスポナーの数だけ雑魚をスポーンさせる
            for (int i = 0; i < _enemySpawnerTable._scriptableESpawnerInformation[(int)_waveType]._birdSpawnPlaces.Count; i++)
            {
                // スポーンさせた雑魚の数を設定
                _currentNumberOfObject = _enemySpawnerTable._scriptableESpawnerInformation[(int)_waveType]._birdSpawnPlaces.Count;

                // 雑魚をプールから呼び出し、呼び出した各雑魚のデリゲート変数にデクリメント関数を登録
                GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird,
                    _enemySpawnerTable._scriptableESpawnerInformation[(int)_waveType]._birdSpawnPlaces[i].position).gameObject;
                temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;

                switch (_waveType)
                {
                    case WaveType.zakoWave1:
                        //呼び出した雑魚にコンポーネントを付与
                        temporaryObject.AddComponent<BirdMoveFirst>();

                        break;

                    case WaveType.zakoWave2:
                        temporaryObject.AddComponent<BirdMoveFirst>();

                        break;

                    default:
                        temporaryObject.AddComponent<BirdMoveSecond>();

                        break;
                }
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

        if (_currentNumberOfObject <= 0)
        {
            // ウェーブを進める
            IncrementWave();
            Spawn();
        }
    }

    /// <summary>
    /// ウェーブを進行させる処理
    /// </summary>
    private void IncrementWave()
    {
        _waveType++;
    }
}