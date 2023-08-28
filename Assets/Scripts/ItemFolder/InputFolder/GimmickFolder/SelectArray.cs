// --------------------------------------------------------- 
// ListInInterface.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectArray
{
    /// <summary>
    /// list内を指定したクラスが入ってるゲームオブジェクトのみにする
    /// </summary>
    /// <typeparam name="T">残すクラス</typeparam>
    /// <param name="gameObjects">オブジェクト</param>
    /// <returns></returns>
    public T[] GetSelectedArray<T>(GameObject[] gameObjects)
    {
        List<T> setList = new();
        foreach (GameObject obj in gameObjects)
        {
            if (obj.TryGetComponent<T>(out T compornent))
            {
                setList.Add(compornent);
            }
        }
        return setList.ToArray();
    }

    /// <summary>
    /// list内を指定したクラスが入ってるゲームオブジェクトのみにする
    /// </summary>
    /// <typeparam name="SelectType">残すクラス</typeparam>
    /// <param name="gameObjects">オブジェクト</param>
    /// <returns></returns>
    public GameObject[] GetSelectedArrayReturnGameObjects<SelectType>(GameObject[] gameObjects)
    {
        List<GameObject> setList = new();
        foreach (GameObject obj in gameObjects)
        {
            if (obj.TryGetComponent<SelectType>(out _))
            {
                setList.Add(obj);
            }
        }
        return setList.ToArray();
    }

}