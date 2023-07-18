// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム、敵の攻撃、敵、オブジェクトをリストにAddしていくためのクラス
/// </summary>
public static class AttractObjectList
{
    #region 変数宣言部
    private static readonly object lockObject = new object();
    //引き寄せられるGameObject用のリスト
    private static List<GameObject> _attractLists = new List<GameObject>();
    //リストの個数
    private static int _attractCount = 0;
    //現在のリストの個数
    private static int _nowAttractCount = 0;
    #endregion
    /// <summary>
    /// リストにオブジェクトを追加する
    /// </summary>
    /// <param name="attractObject"></param>
    public static void AddAttractObject(GameObject attractObject)
    {
        lock (lockObject)
        {
            _attractLists.Add(attractObject);
            _nowAttractCount++;
        }
    }
    /// <summary>
    /// 更新されているかどうか
    /// </summary>
    /// <returns></returns>
    public static bool AddNewAttractObject()
    {

        if (_attractCount != _nowAttractCount)
        {
            _attractCount = _nowAttractCount;
            return true;
        }

        return false;
    }
    /// <summary>
    /// リストを返す
    /// </summary>
    /// <returns></returns>
    public static List<GameObject> GetAttractObject()
    {
        return _attractLists;
    }
    public static void RemoveAttractObject(GameObject removeObject)
    {
        lock (lockObject)
        {
            _attractLists.Remove(removeObject);
        }
    }
    public static GameObject GetAttractObject(int index)
    {
        lock (lockObject)
        {
            return _attractLists[index];
        }
    }
    public static int AttractObjectsLength()
    {
        return _attractLists.Count;
    }
}
