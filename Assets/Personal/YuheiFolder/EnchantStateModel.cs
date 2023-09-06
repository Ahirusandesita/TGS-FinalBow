// 81-C# NomalScript-NewScript.cs
//
//CreateDay:
//Creator  :
//

public static class EnchantStateModel
{
    private static EnchantmentEnum.EnchantmentState[] _arrowStateModels =
    {
        EnchantmentEnum.EnchantmentState.normal,
        EnchantmentEnum.EnchantmentState.bomb,
        EnchantmentEnum.EnchantmentState.thunder,
        EnchantmentEnum.EnchantmentState.rapidShots,
        EnchantmentEnum.EnchantmentState.homing,
        EnchantmentEnum.EnchantmentState.penetrate,
        EnchantmentEnum.EnchantmentState.bombThunder,
        EnchantmentEnum.EnchantmentState.bombKnockBack,
        EnchantmentEnum.EnchantmentState.bombHoming,
        EnchantmentEnum.EnchantmentState.bombPenetrate,
        EnchantmentEnum.EnchantmentState.thunderKnockBack,
        EnchantmentEnum.EnchantmentState.thunderHoming,
        EnchantmentEnum.EnchantmentState.thunderPenetrate,
        EnchantmentEnum.EnchantmentState.knockBackHoming,
        EnchantmentEnum.EnchantmentState.knockBackpenetrate,
        EnchantmentEnum.EnchantmentState.homingPenetrate
    };

   public static EnchantmentEnum.EnchantmentState[] ArrowStateModels
    {
        get
        {
            return _arrowStateModels;
        }
    }
}