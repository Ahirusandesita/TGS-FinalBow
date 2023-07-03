// --------------------------------------------------------- 
// ScriptableData.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ScriptableData", menuName = "Scriptables/CreateScriptableData")]
public class ScriptableData : ScriptableObject
{
    public List<Scriptables> scriptables = new List<Scriptables>();
}
[System.Serializable]
public class Scriptables
{
    public ScriptableEnum.scriptableName name;
    public List<Scriptable> scriptables = new List<Scriptable>();
}

[System.Serializable]
public class Scriptable
{
    public ScriptableEnum.name name;
    public ScriptableObject scriptableObject;
}