// --------------------------------------------------------- 
// TakeArrowBotton.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
interface IFCanTakeArrowButton
{
    void ButtonPush();
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