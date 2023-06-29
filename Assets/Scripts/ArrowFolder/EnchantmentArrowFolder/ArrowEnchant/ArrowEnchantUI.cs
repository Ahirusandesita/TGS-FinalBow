// --------------------------------------------------------- 
// ArrowEnchantUI.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

public class ArrowEnchantUI : MonoBehaviour
{
    #region variable 
    public EnchantUIData _EnchantUI;
    private EnchantUIManager _enchantUIManager;

    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _enchantUIManager = GameObject.FindGameObjectWithTag("EnchantUIController").GetComponent<EnchantUIManager>();
    }

    public void ArrowEnchantUI_Normal()
    {
        _enchantUIManager.EnchantImageReset();
        //_enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[0].UIImage);
    }
    public void ArrowEnchantUI_Bomb()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[1].UIRenderer);
    }
    public void ArrowEnchantUI_Thunder()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[2].UIRenderer);
    }
    public void ArrowEnchantUI_KnockBack()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[3].UIRenderer);
    }
    public void ArrowEnchantUI_Homing()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[4].UIRenderer);
    }
    public void ArrowEnchantUI_Penetrate()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[5].UIRenderer);
    }
    public void ArrowEnchantUI_BombThunder()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[1].UIRenderer);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[2].UIRenderer);
    }
    public void ArrowEnchantUI_BombKnockBack()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[1].UIRenderer);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[3].UIRenderer);
    }
    public void ArrowEnchantUI_BombHoming()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[1].UIRenderer);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[4].UIRenderer);
    }
    public void ArrowEnchantUI_BombPenetrate()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[1].UIRenderer);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[5].UIRenderer);
    }
    public void ArrowEnchantUI_ThunderKnockBack()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[2].UIRenderer);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[3].UIRenderer);
    }
    public void ArrowEnchantUI_ThunderHoming()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[2].UIRenderer);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[4].UIRenderer);
    }
    public void ArrowEnchantUI_ThunderPenetrate()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[2].UIRenderer);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[5].UIRenderer);
    }
    public void ArrowEnchantUI_KnockBackHoming()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[3].UIRenderer);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[4].UIRenderer);
    }
    public void ArrowEnchantUI_KnockBackPenetrate()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[3].UIRenderer);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[5].UIRenderer);
    }
    public void ArrowEnchantUI_HomingPenetrate()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[4].UIRenderer);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[5].UIRenderer);
    }


    #endregion
}