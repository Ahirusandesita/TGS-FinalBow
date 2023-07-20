// --------------------------------------------------------- 
// GimmickData.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GimmickData", menuName = "Scriptables/CreateGmmickTable")]
public class GimmickData : ScriptableObject
{
    public enum RotateDirection
    {
        Right = 1,
        Left = -1
    }
    public float _maxRotateSpeed = default;
    public float _dicreaceRotateSpeed = default;
    public float _addRotateSpeed = default;
    public RotateDirection direction = default;
    public List<GimmickLinkObjectDataBase> gimmickLinkObjectDataBases = new List<GimmickLinkObjectDataBase>();
}
[System.Serializable]
public class GimmickLinkObjectDataBase
{
    public string name;
    public GameObject gimmickLinkObject;
    public Vector3 spawnPosition;
    public Vector3 gimmickObjectRotation;
}