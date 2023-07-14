// --------------------------------------------------------- 
// ScriptableEffects.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "ChangeEffectsCData", menuName = "Scriptables/CreateChangeEffects")]

public class ScriptableEffectsControll : ScriptableObject
{
    public List<AttractInformation> list = new List<AttractInformation>();

}

[System.Serializable]
public class AttractInformation
{
    [Tooltip("‚Ä‚·‚Æ")]
    public string _name = default;
    public Color attractColor = default;

}