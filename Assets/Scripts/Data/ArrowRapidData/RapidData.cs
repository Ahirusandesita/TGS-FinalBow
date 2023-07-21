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
    [Header("連射間隔")]
    public float rapidLate;
}

[System.Serializable]
public class Rapid
{
    public List<RapidParam> rapidParams = new List<RapidParam>();
}

[System.Serializable]
public struct RapidParam
{
    [Header("連射本数が増えるポイント")]
    public int rapidCheckPoint;
    [Header("連射本数")]
    public int rapidIndex;
}