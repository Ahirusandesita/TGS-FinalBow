// --------------------------------------------------------- 
// TakeArrowBotton.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
/// <summary>
/// ���C���[10,������
/// </summary>
interface IFCanTakeArrowButton
{
    
    void ButtonPush();
}
/// <summary>
/// ���C���[11,�����Ȃ�
/// </summary>
interface IFCanTakeArrowButtonCantDestroy
{
    void ButtonPush(Transform arrow);
}
public class TakeArrowBotton : MonoBehaviour, IFCanTakeArrowButton
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method


    #endregion
    public void ButtonPush()
    {
        transform.Translate(Vector3.one);
        X_Debug.Log("");
    }
}