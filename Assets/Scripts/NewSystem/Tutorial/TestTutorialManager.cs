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

    public void StartEnchant() => TutorialProperty.Value = TutorialType.enchant;//�{�������I�ׂȂ�����
    public void EnchantEnd() => TutorialProperty.Value = TutorialType.attarct;//�z��������
    public void AttractEnd() => TutorialProperty.Value = TutorialType.shot; //���Ƃ��������ȃe�L�X�g�����C�x���g
    public void ShotComesOff() => TutorialProperty.Value = TutorialType.shot;//������񌂂Ƃ�
    public void ShotEnd() => TutorialProperty.Value = TutorialType.crystalBreak;//crystal�u���C�N�J�n
    public void CryStalBreakEnd() => TutorialProperty.Value = TutorialType.end;

}