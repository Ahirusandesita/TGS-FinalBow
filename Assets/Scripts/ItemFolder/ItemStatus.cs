// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nishigaki
// --------------------------------------------------------- 
using UnityEngine;

public class ItemStatus : MonoBehaviour
{
    //�A�C�e����Enum���Z�b�g����
    public EnchantmentEnum.ItemAttributeState itemState;
    public bool DropItem;
    
    IStats<EnchantmentEnum.ItemAttributeState> stats;
    private void Awake()
    {
        stats = new Stats<EnchantmentEnum.ItemAttributeState>(itemState);
    }
    private void OnEnable()
    {       
        //���X�g�Ɉ����񂹂���GameObject�Ƃ���Add����
        AttractObjectList.AddAttractObject(this.gameObject);
    }
    private void OnDisable()
    {
        AttractObjectList.RemoveAttractObject(this.gameObject);
    }
    /// <summary>
    /// �A�C�e����Enum��Ԃ����\�b�h
    /// </summary>
    /// <returns></returns>
    public EnchantmentEnum.ItemAttributeState GetState()
    {
        return stats.GetStatus();
    }
}
