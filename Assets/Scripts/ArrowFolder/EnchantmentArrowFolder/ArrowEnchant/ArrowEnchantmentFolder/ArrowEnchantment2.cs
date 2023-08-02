// --------------------------------------------------------- 
// ArrowEnchantment.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System;
using UnityEngine;

//�v���X�_���[�W���܂��Ȃ�

interface IArrowEnchantSet : IArrowEnchantReset
{

    /// <summary>
    /// �G���`�����g��������
    /// </summary>
    /// <param name="enchantmentState"></param>
    void EnchantSetting(EnchantmentEnum.EnchantmentState enchantmentState);
}
interface IArrowEnchantPlusSet : IArrowEnchantReset
{
    /// <summary>
    /// �G���`�����g���~�b�N�X����
    /// </summary>
    /// <param name="enchantmentState"></param>
    void EnchantMixSetting(EnchantmentEnum.EnchantmentState enchantmentState);
}

interface IArrowEventSet:IArrowEnchantPlusSet,IArrowEnchantSet,IArrowPlusDamage
{

    /// <summary>
    /// �G���`�����g���m�肳����
    /// </summary>
    /// <param name="arrow"></param>
    void EventSetting(IArrowEnchant arrow);

    /// <summary>
    /// �A�˃G���`�����g�̃T�u�G���`�����g���擾
    /// </summary>
    /// <returns></returns>
    EnchantmentEnum.EnchantmentState GetSubEnchantment();

    /// <summary>
    /// �G���`�����g���s�b�g
    /// </summary>
    /// <param name="enchantmentState"></param>
    void EnchantRapidSetting(EnchantmentEnum.EnchantmentState enchantmentState);
}

interface IArrowEnchantReset
{
    /// <summary>
    /// �G���`�����g�����Z�b�g����
    /// </summary>
    void EnchantmentReset();
}

/// <summary>
/// ��̃G���`�����g�̑g�ݍ��킹�����N���X
/// </summary>
public sealed class ArrowEnchantment2 : MonoBehaviour, IArrowEnchantSet, IArrowEnchantPlusSet, IArrowEventSet,IArrowPlusDamage
{


    private EnchantMix _enchantMix = new EnchantMix();

    private EnchantEventParameter _enchantEventParameter;

    private EnchantEventParameter.EnchantEvents _enchantEvents;

    //�O��̃G���`�����g
    private EnchantmentEnum.EnchantmentState _enchantmentStateNow = EnchantmentEnum.EnchantmentState.normal;

    private EnchantmentEnum.EnchantmentState _enchantmentStateLast = default;

    private IFPlayerManagerHave _playerManager;

    private delegate void EnchantStatePreparation();

    private void Start()
    {

        //��̌��ʁ@�G�t�F�N�g�@�T�E���h���擾����
        _enchantEvents._arrowEnchant = this.GetComponent<ArrowEnchant>();
        _enchantEvents._arrowEnchantEffect = this.GetComponent<ArrowEnchantEffect>();
        _enchantEvents._arrowEnchantSound = this.GetComponent<ArrowEnchantSound>();
        _enchantEvents._arrowEnchantUI = this.GetComponent<ArrowEnchantUI>();
        _enchantEvents._atractEffect = this.GetComponent<AttractEffect>();

        _enchantEventParameter = new EnchantEventParameter(_enchantEvents);

        _playerManager = GameObject.FindWithTag(InhallLibTags.PlayerController).GetComponent<PlayerManager>();

        ActiveClass activeClass = new ActiveClass();
        activeClass.SetClass(this);
        activeClass.SetClass(_enchantEvents._arrowEnchant);
         PoolActiveSelect poolActiveSelect = GameObject.Find("TestActiveObjects").GetComponent<PoolActiveSelect>();
        poolActiveSelect.Enabled(activeClass);
    }

    /// <summary>
    /// �G���`�����g���~�b�N�X����
    /// </summary>
    /// <param name="enchantmentState"></param>
    public void EnchantMixSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        EnchantDecision(
            new EnchantStatePreparation(
                () => 
                { _enchantmentStateNow = _enchantMix.EnchantmentStateSetting(enchantmentState); }));
    }

    public void EnchantSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        EnchantDecision(
            new EnchantStatePreparation(
                () =>
                { _enchantmentStateNow = enchantmentState; }));
    }


    private void EnchantDecision(EnchantStatePreparation enchantStatePreparation)
    {
        if (_playerManager.CanRapid)
        {
            return;
        }
        enchantStatePreparation();
        NewEnchantState();
        _enchantmentStateLast = _enchantmentStateNow;

        if(_playerManager.GetOnlyArrow != default)
        {
            EventSetting(_playerManager.GetOnlyArrow);
        }

    }


    public void EnchantRapidSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        _enchantmentStateNow = enchantmentState;
        _enchantmentStateLast = _enchantmentStateNow;
    }


    private void NewEnchantState()
    {
        if(_enchantmentStateNow != _enchantmentStateLast && _enchantmentStateNow != EnchantmentEnum.EnchantmentState.normal)
        {
            _enchantEvents._arrowEnchantSound.ArrowSound_EnchantSound();
            _enchantEventParameter.NewEnchantEvent(_enchantmentStateNow);
           //_arrowEnchantEffect.ArrowEffect_NewEnchantEffect(arrow.MyTransform);
        }
    }

    /// <summary>
    /// �G���`�����g�����肷��
    /// </summary>
    /// <param name="arrow"></param>
    public void EventSetting(IArrowEnchant arrow)
    {
        if(_enchantEventParameter == null)
        {
            return;
        }
        _enchantEventParameter.EnchantEventAttribute(_enchantmentStateNow, arrow);
    }


    public void EnchantmentReset()
    {
        _enchantMix.EnchantmentStateReset();
        _enchantmentStateNow = EnchantmentEnum.EnchantmentState.nothing;
        _enchantEventParameter.EnchantEventReset();
        _enchantmentStateLast = EnchantmentEnum.EnchantmentState.nothing;
    }

    public void ArrowEnchantPlusDamage()
    {
        _enchantEvents._arrowEnchant.SetAttackDamage();
    }

    public EnchantmentEnum.EnchantmentState GetSubEnchantment()
    {
        if (_enchantmentStateNow == EnchantmentEnum.EnchantmentState.knockBack)
        {
            return EnchantmentEnum.EnchantmentState.normal;
        }

        if (_enchantmentStateNow == EnchantmentEnum.EnchantmentState.bombKnockBack)
        {
            return EnchantmentEnum.EnchantmentState.bomb;
        }

        if (_enchantmentStateNow == EnchantmentEnum.EnchantmentState.thunderKnockBack)
        {
            return EnchantmentEnum.EnchantmentState.thunder;
        }

        if (_enchantmentStateNow == EnchantmentEnum.EnchantmentState.knockBackHoming)
        {
            return EnchantmentEnum.EnchantmentState.homing;
        }

        if (_enchantmentStateNow == EnchantmentEnum.EnchantmentState.knockBackpenetrate)
        {
            return EnchantmentEnum.EnchantmentState.penetrate;
        }

        //�ԈႢ
        return EnchantmentEnum.EnchantmentState.nothing;

    }

}
