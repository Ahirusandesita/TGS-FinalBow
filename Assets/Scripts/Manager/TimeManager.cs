// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
interface ITime
{
    /// <summary>
    /// 残り時間
    /// </summary>
    /// <returns></returns>
    float TimeCounter();
}
public class TimeManager : ITime
{
    public float TimeCounter()
    {
        //キャッチのときとかに呼ぶ
        return 1;
    }




}
