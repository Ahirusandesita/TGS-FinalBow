// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
public class EnchantmentEnum
{
    public enum ItemAttributeState {
        nomal,
        bomb,
        thunder,
        knockBack,
        homing,
        penetrate,//ŠÑ’Ê

        enemy,
        obj,
    };
    public enum EnchantmentState
    {
        nomal,
        bomb,
        thunder,
        knockBack,
        homing,
        penetrate,

        bombThunder,
        bombKnockBack,
        bombHoming,
        bombPenetrate,

        thunderKnockBack,
        thunderHoming,
        thunderPenetrate,

        knockBackHoming,
        knockBackpenetrate,

        homingPenetrate,

        nothing


    };
    public static int EnchantmentStateLength()
    {
        return System.Enum.GetValues(typeof(EnchantmentEnum.EnchantmentState)).Length;      
    }
}
