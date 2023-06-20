// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

interface IFBowHold
{
    /// <summary>
    /// 弦を掴む
    /// </summary>
    /// <param name="handPos">手のトランスフォーム</param>
    /// <param name="drawObject">引くオブジェクト</param>
    void BowHoldStart(Vector3 handPos, GameObject drawObject);
}
public class BowHold : MonoBehaviour,IFBowHold
{
    /// <summary>
    /// 弦を掴む
    /// </summary>
    /// <param name="handPos">手のトランスフォーム</param>
    /// <param name="drawObject">引くオブジェクト</param>
    public void BowHoldStart(Vector3 handPos,GameObject drawObject)
    {
        drawObject.transform.position = handPos;
    }
}
