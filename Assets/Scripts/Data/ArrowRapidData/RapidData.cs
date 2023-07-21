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
    [Header("連射間隔")]
    public float rapidLate;

    [System.Serializable]
    public struct RapidArrowIndex
    {
        [Header("連射本数が増えるポイント")]
        public int rapidCheckPoint;
        [Header("連射本数")]
        public int rapidIndex;
    }
}