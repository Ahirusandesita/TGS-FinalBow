// --------------------------------------------------------- 
// ArrayDebugLog.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System;

public class ArrayDebugLog
{
    public static void LogArrayObject<T>(T[] ts, string mark) where T : UnityEngine.Object
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

    public static void LogArrayObjectToString<T>(T[] ts, string mark) where T : IFormattable
    {
        Debug.Log("--------LogStart:" + ts + "--------");
        for (int i = 0; i < ts.Length; i++)
        {
            T t = ts[i];
            if (t is null)
            {
                Debug.Log(mark + ":" + "Null" + "/index:" + i);
                continue;
            }
            Debug.Log(mark + ":" + t.ToString() + "/index:" + i);
        }
        Debug.Log("--------LogEnd:" + ts + "--------");
    }

    public static void LogArrayObject<T>(T[,] ts, string mark) where T : UnityEngine.Object
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