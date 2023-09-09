// --------------------------------------------------------- 
// TestTutorialManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public enum TutorialType
{
    enchant,
    attarct,
    shot,
    shotComesOff,
    crystalBreak
}

public class TestTutorialManager : MonoBehaviour
{
    public IReActiveProperty<TutorialType> raedOnlyTutorial => TutorialProperty;
    private ReActiveProperty<TutorialType> TutorialProperty = new ReActiveProperty<TutorialType>();
}