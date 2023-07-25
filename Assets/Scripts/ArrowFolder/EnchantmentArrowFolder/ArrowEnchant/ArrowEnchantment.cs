// --------------------------------------------------------- 
// ArrowEnchantment.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System;
using UnityEngine;

/// <summary>
/// ��ɃG���`�����g�o�^���邱�Ƃ��ł���
/// </summary>
interface IArrowEventSetting : IArrowPlusDamage
{
    /// <summary>
    /// ��ɃG���`�����g�o�^����
    /// </summary>
    /// <param name="arrow">Arrow�N���X</param>
    /// <param name="needMoveChenge">Move�������X�V���邩</param>
    /// <param name="enchantmentState">�G���`�����g��Enum</param>
    void EventSetting(IArrowEnchant arrow, bool needMoveChenge, EnchantmentEnum.EnchantmentState enchantmentState);

    /// <summary>
    /// ��̃G���`�����g�����Z�b�g����
    /// </summary>
    void EnchantmentStateReset();
    void EnchantUIReset();

    EnchantmentEnum.EnchantmentState GetSubEnchantment();

    bool TestRapid { get; set; }
}

/// <summary>
/// ��̃_���[�W���v���X����
/// </summary>
interface IArrowPlusDamage
{
    /// <summary>
    /// ��̃_���[�W���v���X����
    /// </summary>
    void ArrowEnchantPlusDamage();
}


/// <summary>
/// ��̃G���`�����g�̑g�ݍ��킹�����N���X
/// </summary>
public sealed class ArrowEnchantment : MonoBehaviour, IArrowEventSetting
{
    #region �ϐ��錾��
    /// <summary>
    /// ��̌��ʃN���X
    /// </summary>
    private ArrowEnchant _arrowEnchant;

    /// <summary>
    /// ��̃G�t�F�N�g�N���X
    /// </summary>
    private ArrowEnchantEffect _arrowEnchantEffect;

    /// <summary>
    /// ��̏펞�G�t�F�N�g�N���X
    /// </summary>
    private ArrowPassiveEffect _arrowEnchantPassiveEffect;

    /// <summary>
    /// ��̌��ʉ��N���X
    /// </summary>
    private ArrowEnchantSound _arrowEnchantSound;

    /// <summary>
    /// ��̈ړ��N���X
    /// </summary>
    private ArrowMove arrowMove;

    /// <summary>
    /// ��̃G���`�����gUI�N���X
    /// </summary>
    private ArrowEnchantUI _arrowEnchantUI;
    
    /// <summary>
    /// �z�����݃G�t�F�N�g
    /// </summary>
    private AttractEffect _atractEffect;

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
    private EnchantmentEnum.EnchantmentState[,] _enchantPreparationNumbers =
    {
        {EnchantmentEnum.EnchantmentState.normal  ,EnchantmentEnum.EnchantmentState.bomb   ,EnchantmentEnum.EnchantmentState.thunder    ,EnchantmentEnum.EnchantmentState.knockBack       ,EnchantmentEnum.EnchantmentState.homing         ,EnchantmentEnum.EnchantmentState.penetrate          },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.bombThunder,EnchantmentEnum.EnchantmentState.bombKnockBack   ,EnchantmentEnum.EnchantmentState.bombHoming     ,EnchantmentEnum.EnchantmentState.bombPenetrate      },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.thunderKnockBack,EnchantmentEnum.EnchantmentState.thunderHoming  ,EnchantmentEnum.EnchantmentState.thunderPenetrate   },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.nothing         ,EnchantmentEnum.EnchantmentState.knockBackHoming,EnchantmentEnum.EnchantmentState.knockBackpenetrate },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.nothing         ,EnchantmentEnum.EnchantmentState.nothing        ,EnchantmentEnum.EnchantmentState.homingPenetrate    },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.nothing         ,EnchantmentEnum.EnchantmentState.nothing        ,EnchantmentEnum.EnchantmentState.nothing            },
    };

    /// <summary>
    /// �O��̃G���`�����g��������ϐ�
    /// </summary>
    private EnchantmentEnum.EnchantmentState _enchantmentStateLast = EnchantmentEnum.EnchantmentState.normal;

    /// <summary>
    /// Normal�̓Y����
    /// </summary>
    private const int ENCHANT_NORMAL_INDEX = 0;

    private IFPlayerManagerShotArrow playerManager;
    #endregion


    private void Awake()
    {
        //�G���`�����g��Enum�̗��z�z������
        for (int i = ENCHANT_NORMAL_INDEX; i < _enchantmentStateModels.Length; i++)
        {
            //���z�z��쐬
            _enchantmentStateModels[i] = (EnchantmentEnum.EnchantmentState)i;
            //������false ���z�ʂ��Enum��������ꂽ��true�ɂ���
            _isEnchantments[i] = false;
        }
        //�G���`�����g�����Z�b�g����
        EnchantmentStateReset();

    }

    private void Start()
    {
        playerManager = GameObject.FindWithTag(InhallLibTags.PlayerController).GetComponent<PlayerManager>();

        //��̌��ʁ@�G�t�F�N�g�@�T�E���h���擾����
        _arrowEnchant = this.GetComponent<ArrowEnchant>();
        _arrowEnchantEffect = this.GetComponent<ArrowEnchantEffect>();
        _arrowEnchantSound = this.GetComponent<ArrowEnchantSound>();
        _arrowEnchantUI = this.GetComponent<ArrowEnchantUI>();
        _atractEffect = this.GetComponent<AttractEffect>();
    }


    public bool TestRapid { get; set; }

    /// <summary>
    /// ��̃G���`�����g������������
    /// </summary>
    /// <param name="arrow">Arrow�N���X</param>
    /// <param name="needMoveChenge">Move�������X�V���邩</param>
    /// <param name="enchantmentState">�G���`�����g��Enum</param>
    public void EventSetting(IArrowEnchant arrow, bool needMoveChenge, EnchantmentEnum.EnchantmentState enchantmentState)
    {


        //�G���`�����g���ł��Ȃ���ԂȂ�G���`�����g���Ȃ�
        if (!arrow.NeedArrowEnchant)
        {
            return;
        }

        //��̈ړ��@�펞�G�t�F�N�g���擾����
        arrowMove = arrow.EnchantArrowMove;
        _arrowEnchantPassiveEffect = arrow.EnchantArrowPassiveEffect;

        //�擾����Enchant��Enum���|�����킹�đ��
        EnchantmentEnum.EnchantmentState enchantState = EnchantmentStateSetting(enchantmentState);
        Debug.Log("����ɂ���"+enchantState);
        //�O��̃G���`�����g�ƌ��݂̃G���`�����g���Ⴄ�Ƃ�
        if (enchantState != _enchantmentStateLast)
        {
            //���m�[�}���ȊO�̎�
            if (enchantState != EnchantmentEnum.EnchantmentState.normal && !TestRapid)
            {
                //�V�����G���`�����g�̌��ʉ��A�G�t�F�N�g������
                _arrowEnchantSound.ArrowSound_EnchantSound();
                _arrowEnchantEffect.ArrowEffect_NewEnchantEffect(arrow.MyTransform);
            }
            //�O���Enum
            _enchantmentStateLast = enchantState;

            //���Enum���Z�b�g
            arrow.SetEnchantState(enchantState);

            //�G���`�����g�̒���
            EnchantmentPreparation(enchantState, arrow, needMoveChenge);
            
        }
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

        _enchantmentStateLast = EnchantmentEnum.EnchantmentState.nothing;
    }

    public void EnchantUIReset()
    {
        _arrowEnchantUI.ArrowEnchantUI_Normal();
        _atractEffect.AttractEffectEffect_Normal();
    }

    /// <summary>
    /// ��̃_���[�W���v���X����
    /// </summary>
    public void ArrowEnchantPlusDamage()
    {
        //��̃_���[�W���v���X����
        _arrowEnchant.SetAttackDamage();
    }



    /// <summary>
    /// �G���`�����g�̗��z�z��Ƃ̈�v
    /// </summary>
    /// <param name="enchantmentState"></param>
    /// <returns></returns>
    private EnchantmentEnum.EnchantmentState EnchantmentStateSetting(EnchantmentEnum.EnchantmentState enchantmentState)
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
                    _enchantmentStateNow = _enchantPreparationNumbers[i, j];
                }
            }
        }

        return _enchantmentStateNow;
    }

    /// <summary>
    /// �G���`�����g���ɑ������
    /// </summary>
    /// <param name="enchantState">enchant</param>
    /// <param name="arrow">Arrow</param>
    /// <param name="needMoveChenge"></param>
    private void EnchantmentPreparation(EnchantmentEnum.EnchantmentState enchantState, IArrowEnchant arrow, bool needMoveChenge)
    {

        //�G���`�����g�̊֐���Arrow�ɑ�����邽�߂̃f���Q�[�g
        Action<
           Arrow.ArrowEnchantmentDelegateMethod,
           Arrow.ArrowEffectDelegateMethod,
           Arrow.ArrowEffectDelegateMethod,
           Arrow.ArrowEffectDestroyDelegateMethod,
           Arrow.ArrowEnchantSoundDeletgateMethod,
           Arrow.MoveDelegateMethod> ArrowEnchant = (
               arrowEnchantMethod,
               arrowEffectMethod,
               arrowPassiveEffectMethod,
               arrowPassiveEffectDestroyMethod,
               arrowEnchantSoundMethod,
               arrowMoveMethod) =>
           {
               arrow.EventArrow = arrowEnchantMethod;
               arrow.EventArrowEffect = arrowEffectMethod;
               arrow.EventArrowPassiveEffect = arrowPassiveEffectMethod;
               arrow.EventArrowEffectPassiveDestroy = arrowPassiveEffectDestroyMethod;
               arrow.ArrowEnchantSound = arrowEnchantSoundMethod;

               //�ړ����X�V���邩
               if (needMoveChenge)
               {
                   arrow.MoveArrow = arrowMoveMethod;
               }
           };

        if(GetSubEnchantment() != EnchantmentEnum.EnchantmentState.nothing)
        {
            enchantState = GetSubEnchantment();
        }


        //Enum�ɍ��킹�ď����������Ă���
        switch (enchantState)
        {
            case EnchantmentEnum.EnchantmentState.normal:
                //�f���Q�[�g
                ArrowEnchant(
                    //�G���`�����g�����֐����
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_Normal),
                    //�G���`�����g�G�t�F�N�g�֐����
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_Normal),
                    //�G���`�����g�펞�����G�t�F�N�g�֐����
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_Normal),
                    //�G���`�����g�펞�����G�t�F�N�g�폜�֐����
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_Normal),
                    //�G���`�����g�T�E���h�֐����
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_Normal),
                    //�ړ��֐����
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_Normal)
                    );
                //�G���`�����g��UI�\��
                _arrowEnchantUI.ArrowEnchantUI_Normal();

                //�z�����݂̃G�t�F�N�g
                _atractEffect.AttractEffectEffect_Normal();

                break;

            case EnchantmentEnum.EnchantmentState.bomb:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_Bomb),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_Bomb),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_Bomb),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_Bomb),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_Bomb),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_Bomb));
                _arrowEnchantUI.ArrowEnchantUI_Bomb();
                _atractEffect.AttractEffectEffect_Bomb();
                break;

            case EnchantmentEnum.EnchantmentState.thunder:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_Thunder),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_Thunder),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_Thunder),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_Thunder),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_Thunder),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_Thunder));
                _arrowEnchantUI.ArrowEnchantUI_Thunder();
                _atractEffect.AttractEffectEffect_Thunder();
                break;

            case EnchantmentEnum.EnchantmentState.knockBack:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_KnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_KnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_KnockBack),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_KnockBack),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_KnockBack),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_KnockBack));
                _arrowEnchantUI.ArrowEnchantUI_KnockBack();
                _atractEffect.AttractEffectEffect_KnockBack();
                break;

            case EnchantmentEnum.EnchantmentState.homing:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_Homing),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_Homing),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_Homing),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_Homing),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_Homing),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_Homing));
                _arrowEnchantUI.ArrowEnchantUI_Homing();
                _atractEffect.AttractEffectEffect_Homing();
                break;

            case EnchantmentEnum.EnchantmentState.penetrate:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_Penetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_Penetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_Penetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_Penetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_Penetrate),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_Penetrate));
                _arrowEnchantUI.ArrowEnchantUI_Penetrate();
                _atractEffect.AttractEffectEffect_Penetrate();
                break;

            case EnchantmentEnum.EnchantmentState.bombThunder:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_BombThunder),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_BombThunder),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_BombThunder),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_BombThunder),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_BombThunder),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_BombThunder));
                _arrowEnchantUI.ArrowEnchantUI_BombThunder();
                _atractEffect.AttractEffectEffect_BombThunder();
                break;

            case EnchantmentEnum.EnchantmentState.bombKnockBack:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_BombKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_BombKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_BombKnockBack),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_BombKnockBack),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_BombKnockBack),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_BombKnockBack));
                _arrowEnchantUI.ArrowEnchantUI_BombKnockBack();
                _atractEffect.AttractEffectEffect_BombKnockBack();
                break;

            case EnchantmentEnum.EnchantmentState.bombHoming:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_BombHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_BombHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_BombHoming),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_BombHoming),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_BombHoming),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_BombHoming));
                _arrowEnchantUI.ArrowEnchantUI_BombHoming();
                _atractEffect.AttractEffectEffect_BombHoming();
                break;

            case EnchantmentEnum.EnchantmentState.bombPenetrate:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_BombPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_BombPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_BombPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_BombPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_BombPenetrate),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_BombPenetrate));
                _arrowEnchantUI.ArrowEnchantUI_BombPenetrate();
                _atractEffect.AttractEffectEffect_BombPenetrate();
                break;

            case EnchantmentEnum.EnchantmentState.thunderKnockBack:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_ThunderKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_ThunderKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_ThunderKnockBack),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_ThunderKnockBack),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_ThunderKnockBack),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_ThunderKnockBack));
                _arrowEnchantUI.ArrowEnchantUI_ThunderKnockBack();
                _atractEffect.AttractEffectEffect_ThunderKnockBack();
                break;

            case EnchantmentEnum.EnchantmentState.thunderHoming:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_ThunderHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_ThunderHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_ThunderHoming),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_ThunderHoming),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_ThunderHoming),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_ThunderHoming));
                _arrowEnchantUI.ArrowEnchantUI_ThunderHoming();
                _atractEffect.AttractEffectEffect_ThunderHoming();
                break;

            case EnchantmentEnum.EnchantmentState.thunderPenetrate:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_ThunderPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_ThunderPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_ThunderPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_ThunderPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_ThunderPenetrate),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_ThunderPenetrate));
                _arrowEnchantUI.ArrowEnchantUI_ThunderPenetrate();
                _atractEffect.AttractEffectEffect_ThunderPenetrate();
                break;

            case EnchantmentEnum.EnchantmentState.knockBackHoming:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_KnockBackHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_KnockBackHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_KnockBackHoming),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_KnockBackHoming),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_KnockBackHoming),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_KnockBackHoming));
                _arrowEnchantUI.ArrowEnchantUI_KnockBackHoming();
                _atractEffect.AttractEffectEffect_KnockBackHoming();
                break;

            case EnchantmentEnum.EnchantmentState.knockBackpenetrate:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_KnockBackPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_KnockBackPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_KnockBackPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_KnockBackPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_KnockBackPenetrate),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_KnockBackPenetrate));
                _arrowEnchantUI.ArrowEnchantUI_KnockBackPenetrate();
                _atractEffect.AttractEffectEffect_KnockBackPenetrate();
                break;

            case EnchantmentEnum.EnchantmentState.homingPenetrate:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_HomingPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_HomingPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffect_HomingPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_HomingPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_HomingPenetrate),
                    new Arrow.MoveDelegateMethod(arrowMove.ArrowMove_HomingPenetrate));
                _arrowEnchantUI.ArrowEnchantUI_HomingPenetrate();
                _atractEffect.AttractEffectEffect_HomingPenetrate();
                break;
        }
    }

    public EnchantmentEnum.EnchantmentState GetSubEnchantment()
    {
        if(_enchantmentStateLast == EnchantmentEnum.EnchantmentState.knockBack)
        {
            return EnchantmentEnum.EnchantmentState.normal;
        }

        if(_enchantmentStateLast == EnchantmentEnum.EnchantmentState.bombKnockBack)
        {
            return EnchantmentEnum.EnchantmentState.bomb;
        }

        if(_enchantmentStateLast == EnchantmentEnum.EnchantmentState.thunderKnockBack)
        {
            return EnchantmentEnum.EnchantmentState.thunder;
        }

        if(_enchantmentStateLast == EnchantmentEnum.EnchantmentState.knockBackHoming)
        {
            return EnchantmentEnum.EnchantmentState.homing;
        }

        if(_enchantmentStateLast == EnchantmentEnum.EnchantmentState.knockBackpenetrate)
        {
            return EnchantmentEnum.EnchantmentState.penetrate;
        }

        //�ԈႢ
        return EnchantmentEnum.EnchantmentState.nothing;

    }
}
