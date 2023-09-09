// --------------------------------------------------------- 
// TurorialManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public enum TutorialType
{
    enchant,
    attract,
    shot,
    shotComesOff,
    crystalBreak
}

public class TurorialManager : MonoBehaviour
{
    public IReActiveProperty<TutorialData> readOnlyTutorialProperty => TutorialProperty;
    public ReActiveProperty<TutorialData> TutorialProperty = new ReActiveProperty<TutorialData>();
}