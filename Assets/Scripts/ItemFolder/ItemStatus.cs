// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nishigaki
// --------------------------------------------------------- 
using UnityEngine;

public class ItemStatus : MonoBehaviour
{
    //アイテムにEnumをセットする
    public EnchantmentEnum.ItemAttributeState itemState;
    public bool DropItem;
    
    IStats<EnchantmentEnum.ItemAttributeState> stats;
    private void Awake()
    {
        stats = new Stats<EnchantmentEnum.ItemAttributeState>(itemState);
    }
    private void OnEnable()
    {       
        //リストに引き寄せられるGameObjectとしてAddする
        AttractObjectList.AddAttractObject(this.gameObject);
    }
    private void OnDisable()
    {
        AttractObjectList.RemoveAttractObject(this.gameObject);
    }
    /// <summary>
    /// アイテムのEnumを返すメソッド
    /// </summary>
    /// <returns></returns>
    public EnchantmentEnum.ItemAttributeState GetState()
    {
        return stats.GetStatus();
    }
}
