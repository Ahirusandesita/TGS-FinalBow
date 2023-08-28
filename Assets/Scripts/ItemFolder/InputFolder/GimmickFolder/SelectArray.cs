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
    /// list�����w�肵���N���X�������Ă�Q�[���I�u�W�F�N�g�݂̂ɂ���
    /// </summary>
    /// <typeparam name="T">�c���N���X</typeparam>
    /// <param name="gameObjects">�I�u�W�F�N�g</param>
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
    /// list�����w�肵���N���X�������Ă�Q�[���I�u�W�F�N�g�݂̂ɂ���
    /// </summary>
    /// <typeparam name="SelectType">�c���N���X</typeparam>
    /// <param name="gameObjects">�I�u�W�F�N�g</param>
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