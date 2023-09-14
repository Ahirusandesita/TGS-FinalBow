// --------------------------------------------------------- 
// ObjectPoolSystem.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// プールオブジェクト生成方法
/// </summary>
public enum CreateType
{
    /// <summary>
    /// 自動的に非表示にする
    /// </summary>
    automatic,
    /// <summary>
    /// 手動での非表示が必要
    /// </summary>
    manual
}


/// <summary>
/// オブジェクトプールの管理クラス
/// </summary>
public class ObjectPoolSystem : MonoBehaviour
{
    #region 変数
    [Tooltip("取得したPoolObjectParamTableクラス")]
    public PoolObjectParamTable _poolObjectParamTable = default;

    [Tooltip("生成したプレハブを格納するためのキュー配列")]
    private Queue<CashObjectInformation>[] _objectQueues = default;

    [Tooltip("生成したエフェクトを格納するためのキュー配列")]
    private Queue<GameObject>[] _effectQueues = default;

    [Tooltip("生成/取得したオブジェクトを格納する中間変数")]
    private CashObjectInformation _temporaryObject = default;

    [Tooltip("生成/取得したエフェクトを格納する中間変数")]
    private GameObject _temporaryEffect = default;

    // 文字列定数
    private const string ST_SHORTAGE_QUEUE = "キューの容量不足";
    #endregion


    private void Awake()
    {
        // プールオブジェクトの種類分、配列領域を確保する
        _objectQueues = new Queue<CashObjectInformation>[_poolObjectParamTable._scriptablePoolInformation.Count];

        // プールエフェクトの種類分、配列領域を確保する
        _effectQueues = new Queue<GameObject>[_poolObjectParamTable._scriptableEffectInformation.Count];

        // プールの生成
        CreatePool();
    }


    #region Call
    /// <summary>
    /// オブジェクトを呼び出す（オブジェクトプレハブ）
    /// </summary>
    /// <param name="poolObjectType">取り出すオブジェクトの種類</param>
    /// <param name="spawnPosition">スポーン座標</param>
    /// <param name="spawnAngle">初期角度</param>
    public CashObjectInformation CallObject(PoolEnum.PoolObjectType poolObjectType, Vector3 spawnPosition, Quaternion? spawnAngle = null)
    {
        try
        {
            // enumで指定されたキューからオブジェクトを取り出す
            _temporaryObject = _objectQueues[(int)poolObjectType].Dequeue();
        }
        // 容量不足でキューからの取り出しに失敗したとき
        catch (InvalidOperationException)
        {
            Debug.LogWarning(poolObjectType + ST_SHORTAGE_QUEUE);

            // オブジェクトを生成する
            _temporaryObject = Instantiate(_poolObjectParamTable._scriptablePoolInformation[(int)poolObjectType]._prefab);

            // 生成したオブジェクトに、どのプールオブジェクトかの情報を持たせる
            _temporaryObject.GetComponent<CashObjectInformation>()._myObjectType = poolObjectType;

            // 生成したオブジェクトをキューに追加する
            _objectQueues[(int)poolObjectType].Enqueue(_temporaryObject);
        }

        // 取り出したオブジェクトの位置を、指定された位置に変更
        _temporaryObject.transform.position = spawnPosition;

        // Quaternion引数が指定されていたとき
        if (spawnAngle != null)
        {
            // 取り出したオブジェクトの角度を、指定された角度に変更
            _temporaryObject.transform.rotation = (Quaternion)spawnAngle;
        }

        // 取り出したオブジェクトを表示する
        _temporaryObject.gameObject.SetActive(true);

        return _temporaryObject;
    }

    /// <summary>
    /// オブジェクトを呼び出す（エフェクト）
    /// </summary>
    /// <param name="poolObjectType">取り出すエフェクトの種類</param>
    /// <param name="spawnPosition">スポーン座標</param>
    /// <param name="spawnAngle">初期角度</param>
    public GameObject CallObject(EffectPoolEnum.EffectPoolState poolEffectType, Vector3 spawnPosition, Quaternion? spawnAngle = null)
    {
        try
        {
            // enumで指定されたキューからエフェクトを取り出す
            _temporaryEffect = _effectQueues[(int)poolEffectType].Dequeue();
        }
        // 容量不足でキューからの取り出しに失敗したとき
        catch (InvalidOperationException)
        {
            Debug.LogWarning(poolEffectType + ST_SHORTAGE_QUEUE);

            // エフェクトを生成する
            _temporaryEffect = Instantiate(_poolObjectParamTable._scriptableEffectInformation[(int)poolEffectType]._prefab);

            // 生成したエフェクトをキューに追加する
            _effectQueues[(int)poolEffectType].Enqueue(_temporaryEffect);
        }

        // 取り出したエフェクトの位置を、指定された位置に変更
        _temporaryEffect.transform.position = spawnPosition;

        // Quaternion引数が指定されていたとき
        if (spawnAngle != null)
        {
            // 取り出したエフェクトの角度を、指定された角度に変更
            _temporaryEffect.transform.rotation = (Quaternion)spawnAngle;
        }

        // 取り出したエフェクトを表示する
        _temporaryEffect.SetActive(true);

        return _temporaryEffect;
    }
    #endregion


    #region Return
    /// <summary>
    /// オブジェクトを返却する（オブジェクトプレハブ）
    /// </summary>
    /// <param name="returnObject">返却するオブジェクト</param>
    public void ReturnObject(CashObjectInformation returnObject)
    {
        // 渡されたオブジェクトを非表示にする
        returnObject.gameObject.SetActive(false);

        // 渡されたオブジェクトを指定されたプールに返す
        _objectQueues[(int)returnObject._myObjectType].Enqueue(returnObject);
    }

    /// <summary>
    /// オブジェクトを返却する（エフェクト）
    /// </summary>
    /// <param name="poolEffectType">返却するエフェクトの種類</param>
    /// <param name="returnObject">返却するエフェクト</param>
    public void ReturnObject(EffectPoolEnum.EffectPoolState poolEffectType, GameObject returnObject)
    {
        // 渡されたエフェクトを非表示にする
        returnObject.SetActive(false);

        // 渡されたエフェクトを指定されたプールに返す
        _effectQueues[(int)poolEffectType].Enqueue(returnObject);
    }
    #endregion


    #region Create
    /// <summary>
    /// プールを生成する
    /// </summary>
    private void CreatePool()
    {
        // オブジェクトプレハブの生成処理--------------------------------------------------

        // 配列の要素数 = プールオブジェクトの種類分、繰り返す
        for (int i = 0; i < _objectQueues.Length; i++)
        {
            // 配列の中の各キューを生成
            _objectQueues[i] = new Queue<CashObjectInformation>();

            // メインシーン以外で生成しないオブジェクトは処理をスキップ
            if (_poolObjectParamTable._scriptablePoolInformation[i]._onlyMainScene && SceneManager.GetActiveScene().name != "HDRPDebugscene")
            {
                continue;
            }

            // 各プールオブジェクトに設定されたキューの最大容量まで、オブジェクト生成を繰り返す
            for (int k = 0; k < _poolObjectParamTable._scriptablePoolInformation[i]._queueMax; k++)
            {
                // オブジェクトを生成する
                _temporaryObject = Instantiate(_poolObjectParamTable._scriptablePoolInformation[i]._prefab);

                // 設定されたenumによって処理をスキップ
                if (_poolObjectParamTable._scriptablePoolInformation[i]._createType == CreateType.automatic)
                {
                    // 生成したオブジェクトを非表示にする
                    _temporaryObject.gameObject.SetActive(false);
                }

                // 生成したオブジェクトに、どのプールオブジェクトかの情報を持たせる
                _temporaryObject.GetComponent<CashObjectInformation>()._myObjectType = (PoolEnum.PoolObjectType)i;

                // 生成したオブジェクトを各キューに追加する
                _objectQueues[i].Enqueue(_temporaryObject);
            }
        }

        // エフェクトの生成処理------------------------------------------------------------

        // 配列の要素数 = プールエフェクトの種類分、繰り返す
        for (int i = 0; i < _effectQueues.Length; i++)
        {
            // 配列の中の各キューを生成
            _effectQueues[i] = new Queue<GameObject>();

            // 各プールエフェクトに設定されたキューの最大容量まで、オブジェクト生成を繰り返す
            for (int k = 0; k < _poolObjectParamTable._scriptableEffectInformation[i]._queueMax; k++)
            {
                // オブジェクトを生成する
                _temporaryEffect = Instantiate(_poolObjectParamTable._scriptableEffectInformation[i]._prefab);

                // 生成したオブジェクトを非表示にする
                _temporaryEffect.SetActive(false);

                // 生成したオブジェクトを各キューに追加する
                _effectQueues[i].Enqueue(_temporaryEffect);
            }
        }
    }
    #endregion
}
