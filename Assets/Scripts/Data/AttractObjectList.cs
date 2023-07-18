// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���A�G�̍U���A�G�A�I�u�W�F�N�g�����X�g��Add���Ă������߂̃N���X
/// </summary>
public static class AttractObjectList
{
    #region �ϐ��錾��
    private static readonly object lockObject = new object();
    //�����񂹂���GameObject�p�̃��X�g
    private static List<GameObject> _attractLists = new List<GameObject>();
    //���X�g�̌�
    private static int _attractCount = 0;
    //���݂̃��X�g�̌�
    private static int _nowAttractCount = 0;
    #endregion
    /// <summary>
    /// ���X�g�ɃI�u�W�F�N�g��ǉ�����
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
    /// �X�V����Ă��邩�ǂ���
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
    /// ���X�g��Ԃ�
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
