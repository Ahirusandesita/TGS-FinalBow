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
    start,
    enchant,
    attarct,
    shot,
    shotComesOff,
    crystalBreak,
    end
}

public class TestTutorialManager : MonoBehaviour
{
    public IReActiveProperty<TutorialType> readOnlyTutorial => TutorialProperty;
    private ReActiveProperty<TutorialType> TutorialProperty = new ReActiveProperty<TutorialType>();

    public void StartEnchant() => TutorialProperty.Value = TutorialType.enchant;//ボムしか選べなくする
    public void EnchantEnd() => TutorialProperty.Value = TutorialType.attarct;//吸い込もう
    public void AttractEnd() => TutorialProperty.Value = TutorialType.shot; //撃とう見たいなテキストだすイベント
    public void ShotComesOff() => TutorialProperty.Value = TutorialType.shot;//もう一回撃とう
    public void ShotEnd() => TutorialProperty.Value = TutorialType.crystalBreak;//crystalブレイク開始
    public void CryStalBreakEnd() => TutorialProperty.Value = TutorialType.end;

}