// --------------------------------------------------------- 
// ArrayDebugLog.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ArrayDebugLog
{
    public static void LogArrayObject<T>(T[] ts, string mark) where T : Object
    {
        Debug.Log("--------LogStart:" + ts + "--------");
        for (int i = 0; i < ts.Length; i++)
        {
            T t = ts[i];
            if(t is null)
            {
                Debug.Log(mark + ":" + "Null" + "/index:" + i);
                continue;
            }
            Debug.Log(mark + ":" + t.name + "/index:" + i);
        }
        Debug.Log("--------LogEnd:" + ts + "--------");
    }

    public static void LogArrayObject<T>(T[,] ts, string mark) where T : Object
    {
        Debug.Log("--------LogStart:" + ts + "--------");
        for (int i = 0; i < ts.GetLength(0); i++)
        {
            for(int k = 0; k < ts.GetLength(1); k++)
            {
                T t = ts[i, k];
                if (t is null)
                {
                    Debug.Log(mark + ":" + "Null" + "/index:[" + i + "," + k + "]");
                    continue;
                }
                Debug.Log(mark + ":" + t.name + "/index:[" + i + "," + k + "]");
            }
           
        }
        Debug.Log("--------LogEnd:" + ts + "--------");
    }

}