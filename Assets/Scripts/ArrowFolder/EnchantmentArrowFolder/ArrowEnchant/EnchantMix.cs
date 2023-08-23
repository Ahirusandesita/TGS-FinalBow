// 81-C# NomalScript-NewScript.cs
//
//CreateDay:
//Creator  :
//
interface IEnchantMix
{

}

/// <summary>
/// エンチャントのミックス
/// </summary>
public class EnchantMix:IEnchantMix
{

    /// <summary>
    /// EnchantmentState型の理想配列を代入する配列
    /// </summary>
    private EnchantmentEnum.EnchantmentState[] _enchantmentStateModels =
        new EnchantmentEnum.EnchantmentState[EnchantmentEnum.EnchantmentStateLength()];


    /// <summary>
    /// エンチャントがセットされるところがtrueになる配列
    /// </summary>
    private bool[] _isEnchantments = new bool[EnchantmentEnum.EnchantmentStateLength()];


    /// <summary>
    /// エンチャントの組み合わせ設計図
    /// </summary>
    private EnchantmentEnum.EnchantmentState[,] _enchantMixPreparationBlueprint =
    {
        {EnchantmentEnum.EnchantmentState.normal  ,EnchantmentEnum.EnchantmentState.bomb   ,EnchantmentEnum.EnchantmentState.thunder    ,EnchantmentEnum.EnchantmentState.knockBack       ,EnchantmentEnum.EnchantmentState.homing         ,EnchantmentEnum.EnchantmentState.penetrate          },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.bombThunder,EnchantmentEnum.EnchantmentState.bombKnockBack   ,EnchantmentEnum.EnchantmentState.bombHoming     ,EnchantmentEnum.EnchantmentState.bombPenetrate      },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.thunderKnockBack,EnchantmentEnum.EnchantmentState.thunderHoming  ,EnchantmentEnum.EnchantmentState.thunderPenetrate   },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.nothing         ,EnchantmentEnum.EnchantmentState.knockBackHoming,EnchantmentEnum.EnchantmentState.knockBackpenetrate },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.nothing         ,EnchantmentEnum.EnchantmentState.nothing        ,EnchantmentEnum.EnchantmentState.homingPenetrate    },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.nothing         ,EnchantmentEnum.EnchantmentState.nothing        ,EnchantmentEnum.EnchantmentState.nothing            },
    };


    /// <summary>
    /// Normalの添え字
    /// </summary>
    private const int ENCHANT_NORMAL_INDEX = 0;


    public EnchantMix()
	{
        EnchantModelSetting();
	}




    /// <summary>
    /// エンチャントの理想配列との一致
    /// </summary>
    /// <param name="enchantmentState"></param>
    /// <returns></returns>
    public EnchantmentEnum.EnchantmentState EnchantmentStateSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        //エンチャントの理想配列を一致したらtrueを対応する添え字の配列に代入
        for (int i = ENCHANT_NORMAL_INDEX; i < _enchantmentStateModels.Length; i++)
        {
            //理想配列と一致しているか判定
            if (_enchantmentStateModels[i] == enchantmentState)
            {
                //一致していればtrue
                _isEnchantments[i] = true;
            }
        }
        return EnchantmentPreparation();
    }

    /// <summary>
    /// エンチャントのEnumの組み合わせからEnum代入
    /// </summary>
    /// <returns>掛け合わせ完成EnchantState</returns>
    private EnchantmentEnum.EnchantmentState EnchantmentPreparation()
    {
        //現在のエンチャント
        EnchantmentEnum.EnchantmentState _enchantmentStateNow = EnchantmentEnum.EnchantmentState.normal;
        _isEnchantments[0] = true;

        for (int i = ENCHANT_NORMAL_INDEX; i < _isEnchantments.Length - 1; i++)
        {
            for (int j = i + 1; j < _isEnchantments.Length; j++)
            {
                //２つエンチャントがあれば
                if (_isEnchantments[i] && _isEnchantments[j])
                {
                    //i,jに登録してあるEnchantをNowに代入
                    _enchantmentStateNow = _enchantMixPreparationBlueprint[i, j];
                }
            }
        }

        return _enchantmentStateNow;
    }



    private void EnchantModelSetting()
    {
        //エンチャントのEnumの理想配列を作る
        for (int i = ENCHANT_NORMAL_INDEX; i < _enchantmentStateModels.Length; i++)
        {
            //理想配列作成
            _enchantmentStateModels[i] = (EnchantmentEnum.EnchantmentState)i;
            //初期はfalse 理想通りにEnumが代入されたらtrueにする
            _isEnchantments[i] = false;
        }
        EnchantmentStateReset();
    }


    /// <summary>
    /// エンチャントのStateをリセットする　エンチャント対象の矢が変更される時に呼ぶ
    /// </summary>
    public void EnchantmentStateReset()
    {
        //リセットする
        for (int i = ENCHANT_NORMAL_INDEX; i < _isEnchantments.Length; i++)
        {
            _isEnchantments[i] = false;
        }
        // _isEnchantments[ENCHANT_NORMAL_INDEX] = true;
    }





}