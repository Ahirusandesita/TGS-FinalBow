// --------------------------------------------------------- 
// TutorialManagementData.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TutorialManagementData", menuName = "TutorialScriptables/CreateTutorialManagementTable")]
public class TutorialManagementData : ScriptableObject
{
    public List<TutorialManagementItem> tutorialManagementItem = new List<TutorialManagementItem>();
}
[System.Serializable]
public class TutorialManagementItem
{
    public string myName;
    public TutorialData tutorialDatas;
}
