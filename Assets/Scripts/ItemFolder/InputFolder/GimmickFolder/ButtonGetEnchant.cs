// --------------------------------------------------------- 
// ButtonGetEnchant.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface IFCanTakeArrowButtonGetEnchant:IFCanTakeArrowButton
{
    void ButtonPush(EnchantmentEnum.EnchantmentState enchantment);
}

interface IFUseEnchantGimmick
{
    /// <summary>
    /// エンチャントに応じて効果が変わるアクションを呼び出す
    /// </summary>
    /// <param name="enchantment"></param>
    void CallAction(EnchantmentEnum.EnchantmentState enchantment);
}
public class ButtonGetEnchant : MonoBehaviour,IFCanTakeArrowButtonGetEnchant
{
    [SerializeField] GameObject[] gameObjects = default;
    IFUseEnchantGimmick[] gimmicks = default;
    private void Start()
    {
        Init();
    }
    private void OnValidate()
    {
        Init();

    }

    private void Init()
    {
        SelectArray selectArray = new();
        gameObjects = selectArray.GetSelectedArrayReturnGameObjects<IFUseEnchantGimmick>(gameObjects);
        gimmicks = selectArray.GetSelectedArray<IFUseEnchantGimmick>(gameObjects);
    }

    public void ButtonPush(EnchantmentEnum.EnchantmentState enchantment)
    {
        foreach(IFUseEnchantGimmick gim in gimmicks)
        {
            gim.CallAction(enchantment);
        }
    }

    public void ButtonPush()
    {
        foreach(IFUseEnchantGimmick gim in gimmicks)
        {
            gim.CallAction(EnchantmentEnum.EnchantmentState.normal);
        }
    }

    GameObject IFCanTakeArrowButton.GetThisObject()
    {
        return gameObject;
    }
}