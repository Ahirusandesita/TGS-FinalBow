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
    /// �X�|�[�����̏�����
    /// </summary>
    private void InitializeWhenEnable()
    {
        
    }

    /// <summary>
    /// ��]���ăX�|�[�����鏈��
    /// </summary>
    private void SpawnRotate()
    {

    }
    #endregion
}