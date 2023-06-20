// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

interface IFBowDraw
{
    /// <summary>
    /// 弓を引く処理
    /// </summary>
    /// <param name="handPosition">手の位置</param>
    /// <param name="drawObject">引くオブジェクト</param>
    /// <returns>引いた距離</returns>
    public float BowDrawing(Vector3 handPosition, GameObject drawObject, Vector3 firstPosition);

    /// <summary>
    /// 弓矢ゆっくり引いた効果の処理
    /// </summary>
    //void BowDrawingSlow();

    /// <summary>
    /// 弓矢早く引いた効果の処理
    /// </summary>
   //void BowDrawingFast();

    /// <summary>
    /// 遅い早いミックス(仮)
    /// </summary>
    //void FastSlowMix();
}
public class BowDraw : MonoBehaviour,IFBowDraw
{   
    /// <summary>
    /// 弓を引く処理
    /// </summary>
    /// <param name="handPosition">手の位置</param>
    /// <param name="drawObject">引くオブジェクト</param>
    /// <returns>引いた距離</returns>
    public float BowDrawing(Vector3 handPosition,GameObject drawObject,Vector3 firstPosition)
    {
        drawObject.transform.position = handPosition;
        
        return Vector3.Distance(firstPosition, drawObject.transform.localPosition);
    }

   
}
