// --------------------------------------------------------- 
// TakeArrowBotton.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

/// <summary>
/// レイヤー10,きえる
/// </summary>
interface IFCanTakeArrowButton
{
    protected void Awake()
    {
        ChangeLayer();
    }
    protected void ChangeLayer()
    {
        GetThisObject().layer = 1 << 10;
    }
    /// <summary>
    /// return gameObject;
    /// </summary>
    /// <returns></returns>
    protected GameObject GetThisObject();
    void ButtonPush();
}
/// <summary>
/// レイヤー11,消えない
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

    GameObject IFCanTakeArrowButton.GetThisObject()
    {
        return gameObject;
    }
}