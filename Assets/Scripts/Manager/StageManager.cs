// --------------------------------------------------------- 
// StageManager.cs 
// 
// CreateDay: 2023/06/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using System.Collections.Generic;
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
    tutorial1,
    tutorial2,
    tutorial3,
    wave1,
    wave2,
    wave3,
    wave4,
    wave5,
    boss
}

public class StageManager : MonoBehaviour, IStageSpawn
{
    #region 変数
    [Tooltip("タグの名前")]
    public TagObject _PoolSystemTagData = default;

    [Header("雑魚の出現位置リスト")]
    [Tooltip("チュートリアル（吸い込み）の的の出現位置")]
    public List<Transform> _targetSpawnPlaces_Tutorial1 = new List<Transform>();

    [Tooltip("チュートリアル（ショット）の的の出現位置")]
    public List<Transform> _targetSpawnPlaces_Tutorial2 = new List<Transform>();

    [Tooltip("チュートリアル（エンチャント）の的の出現位置")]
    public List<Transform> _targetSpawnPlaces_Tutorial3 = new List<Transform>();

    [Tooltip("Wave1の雑魚の出現位置")]
    public List<Transform> _birdSpawnPlaces_Wave1 = new List<Transform>();

    [Tooltip("Wave2の雑魚の出現位置")]
    public List<Transform> _birdSpawnPlaces_Wave2 = new List<Transform>();

    [Tooltip("Wave3の雑魚の出現位置")]
    public List<Transform> _birdSpawnPlaces_Wave3 = new List<Transform>();

    [Tooltip("Wave4の雑魚の出現位置")]
    public List<Transform> _birdSpawnPlaces_Wave4 = new List<Transform>();

    [Tooltip("Wave5の雑魚の出現位置")]
    public List<Transform> _birdSpawnPlaces_Wave5 = new List<Transform>();


    [Tooltip("取得したObjectPoolSystemクラス")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("現在の雑魚/的の数")]
    private int _currentNumberOfObject = default;

    [Tooltip("現在のウェーブ数")]
    private WaveType _waveType = WaveType.wave1;     // 初期値0（tutorial1)
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
        //GameManagerが呼ぶ　TimeManagerには呼ばせない

        // スポーン処理（仮）
        switch (_waveType)
        {
            case WaveType.tutorial2:
                for (int i = 0; i < _targetSpawnPlaces_Tutorial2.Count; i++)
                {
                    // Inspector上でアタッチしたスポーン位置の数だけ的をスポーンさせる
                    _currentNumberOfObject = _targetSpawnPlaces_Tutorial2.Count;

                    // 的をプールから呼び出し、呼び出した各的のデリゲート変数にデクリメント関数を登録
                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.targetObject, _targetSpawnPlaces_Tutorial2[i].position).gameObject;
                    temporaryObject.GetComponent<TargetStats>()._onDeathTarget = DecrementNumberOfObject;
                }

                break;

            case WaveType.tutorial3:
                for (int i = 0; i < _targetSpawnPlaces_Tutorial3.Count; i++)
                {
                    _currentNumberOfObject = _targetSpawnPlaces_Tutorial3.Count;

                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.targetObject, _targetSpawnPlaces_Tutorial3[i].position).gameObject;
                    temporaryObject.GetComponent<TargetStats>()._onDeathTarget = DecrementNumberOfObject;
                }

                break;

            case WaveType.wave1:
                // Inspector上でアタッチしたスポーン位置の数だけ雑魚をスポーンさせる
                for (int i = 0; i < _birdSpawnPlaces_Wave1.Count; i++)
                {
                    // スポーンさせた雑魚の数を設定
                    _currentNumberOfObject = _birdSpawnPlaces_Wave1.Count;

                    // 雑魚をプールから呼び出し、呼び出した各雑魚のデリゲート変数にデクリメント関数を登録
                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _birdSpawnPlaces_Wave1[i].position).gameObject;
                    temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;
                }

                break;

            case WaveType.wave2:
                for (int i = 0; i < _birdSpawnPlaces_Wave2.Count; i++)
                {
                    _currentNumberOfObject = _birdSpawnPlaces_Wave2.Count;

                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _birdSpawnPlaces_Wave2[i].position).gameObject;
                    temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;
                }

                break;

            case WaveType.wave3:
                for (int i = 0; i < _birdSpawnPlaces_Wave3.Count; i++)
                {
                    _currentNumberOfObject = _birdSpawnPlaces_Wave3.Count;

                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _birdSpawnPlaces_Wave3[i].position).gameObject;
                    temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;
                }

                break;

            case WaveType.wave4:
                for (int i = 0; i < _birdSpawnPlaces_Wave4.Count; i++)
                {
                    _currentNumberOfObject = _birdSpawnPlaces_Wave4.Count;

                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _birdSpawnPlaces_Wave4[i].position).gameObject;
                    temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;
                }

                break;

            case WaveType.wave5:
                for (int i = 0; i < _birdSpawnPlaces_Wave5.Count; i++)
                {
                    _currentNumberOfObject = _birdSpawnPlaces_Wave5.Count;

                    GameObject temporaryObject = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _birdSpawnPlaces_Wave5[i].position).gameObject;
                    temporaryObject.GetComponent<BirdStats>()._onDeathBird = DecrementNumberOfObject;
                }

                break;

            case WaveType.boss:
                // ボスを出現させる

                break;

            default:
                X_Debug.LogError("実装しているウェーブを超えています");
                break;
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