// --------------------------------------------------------- 
// TargetMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TargetMove : MonoBehaviour
{
    #region variable 
    private TargetDataTable _targetData = default;
    #endregion
    #region property
    public TargetDataTable TargetData { set => _targetData = value; }
    #endregion
    #region method

    private void Awake()
    {

    }

    private void Start()
    {
        X_Debug.Log(_targetData._speed);
    }

    private void Update()
    {

    }

    /// <summary>
    /// スポーン時の初期化
    /// </summary>
    private void InitializeWhenEnable()
    {
        
    }

    /// <summary>
    /// 回転してスポーンする処理
    /// </summary>
    private void SpawnRotate()
    {

    }
    #endregion
}