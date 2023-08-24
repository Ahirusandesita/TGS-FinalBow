// --------------------------------------------------------- 
// MaxInhallObjectSetting.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName ="MaxInhallObjectSettingData",menuName ="Scriptables/CreateMaxInhall")]
public class MaxInhallObjectSetting : ScriptableObject
{
    [SerializeField] int maxInhall = 10;

    public int GetMaxInhall => maxInhall;
}