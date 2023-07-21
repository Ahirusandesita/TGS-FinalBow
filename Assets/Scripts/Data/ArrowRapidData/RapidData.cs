// --------------------------------------------------------- 
// RapidData.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RapidData", menuName = "Scriptables/CreateRapidTable")]
public class RapidData : ScriptableObject
{
    public Rapid rapids;
    public RapidParam rapidParam;
}

[System.Serializable]
public class Rapid
{
    public List<RapidParam.RapidArrowIndex> rapidParams = new List<RapidParam.RapidArrowIndex>();
}


[System.Serializable]
public struct RapidParam
{
    [Header("�A�ˊԊu")]
    public float rapidLate;

    [System.Serializable]
    public struct RapidArrowIndex
    {
        [Header("�A�˖{����������|�C���g")]
        public int rapidCheckPoint;
        [Header("�A�˖{��")]
        public int rapidIndex;
    }
}