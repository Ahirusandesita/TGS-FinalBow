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
    [Header("�A�ˊԊu")]
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
    [Header("�A�˖{����������|�C���g")]
    public int rapidCheckPoint;
    [Header("�A�˖{��")]
    public int rapidIndex;
}