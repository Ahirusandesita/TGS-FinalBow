// 81-C# NomalScript-NewScript.cs
//
//CreateDay:
//Creator  :
//
interface IEnchantMix
{

}

/// <summary>
/// �G���`�����g�̃~�b�N�X
/// </summary>
public class EnchantMix:IEnchantMix
{

    /// <summary>
    /// EnchantmentState�^�̗��z�z���������z��
    /// </summary>
    private EnchantmentEnum.EnchantmentState[] _enchantmentStateModels =
        new EnchantmentEnum.EnchantmentState[EnchantmentEnum.EnchantmentStateLength()];


    /// <summary>
    /// �G���`�����g���Z�b�g�����Ƃ��낪true�ɂȂ�z��
    /// </summary>
    private bool[] _isEnchantments = new bool[EnchantmentEnum.EnchantmentStateLength()];


    /// <summary>
    /// �G���`�����g�̑g�ݍ��킹�݌v�}
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
    /// Normal�̓Y����
    /// </summary>
    private const int ENCHANT_NORMAL_INDEX = 0;


    public EnchantMix()
	{
        EnchantModelSetting();
	}




    /// <summary>
    /// �G���`�����g�̗��z�z��Ƃ̈�v
    /// </summary>
    /// <param name="enchantmentState"></param>
    /// <returns></returns>
    public EnchantmentEnum.EnchantmentState EnchantmentStateSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        //�G���`�����g�̗��z�z�����v������true��Ή�����Y�����̔z��ɑ��
        for (int i = ENCHANT_NORMAL_INDEX; i < _enchantmentStateModels.Length; i++)
        {
            //���z�z��ƈ�v���Ă��邩����
            if (_enchantmentStateModels[i] == enchantmentState)
            {
                //��v���Ă����true
                _isEnchantments[i] = true;
            }
        }
        return EnchantmentPreparation();
    }

    /// <summary>
    /// �G���`�����g��Enum�̑g�ݍ��킹����Enum���
    /// </summary>
    /// <returns>�|�����킹����EnchantState</returns>
    private EnchantmentEnum.EnchantmentState EnchantmentPreparation()
    {
        //���݂̃G���`�����g
        EnchantmentEnum.EnchantmentState _enchantmentStateNow = EnchantmentEnum.EnchantmentState.normal;
        _isEnchantments[0] = true;

        for (int i = ENCHANT_NORMAL_INDEX; i < _isEnchantments.Length - 1; i++)
        {
            for (int j = i + 1; j < _isEnchantments.Length; j++)
            {
                //�Q�G���`�����g�������
                if (_isEnchantments[i] && _isEnchantments[j])
                {
                    //i,j�ɓo�^���Ă���Enchant��Now�ɑ��
                    _enchantmentStateNow = _enchantMixPreparationBlueprint[i, j];
                }
            }
        }

        return _enchantmentStateNow;
    }



    private void EnchantModelSetting()
    {
        //�G���`�����g��Enum�̗��z�z������
        for (int i = ENCHANT_NORMAL_INDEX; i < _enchantmentStateModels.Length; i++)
        {
            //���z�z��쐬
            _enchantmentStateModels[i] = (EnchantmentEnum.EnchantmentState)i;
            //������false ���z�ʂ��Enum��������ꂽ��true�ɂ���
            _isEnchantments[i] = false;
        }
        EnchantmentStateReset();
    }


    /// <summary>
    /// �G���`�����g��State�����Z�b�g����@�G���`�����g�Ώۂ̖�ύX����鎞�ɌĂ�
    /// </summary>
    public void EnchantmentStateReset()
    {
        //���Z�b�g����
        for (int i = ENCHANT_NORMAL_INDEX; i < _isEnchantments.Length; i++)
        {
            _isEnchantments[i] = false;
        }
        // _isEnchantments[ENCHANT_NORMAL_INDEX] = true;
    }





}