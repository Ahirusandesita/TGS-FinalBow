// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
public class EnchantmentEnum
{
    public enum ItemAttributeState {
        normal,
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
        normal,
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
