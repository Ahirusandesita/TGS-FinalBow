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
    private IFEnchantUIManager _enchantUIManager;

    #endregion
    #region property
    #endregion
    #region method
    class NullObject : IFEnchantUIManager
    {
        public void EnchantImage1Chenge(Sprite sprite, Color color)
        {
            return;
        }

        public void EnchantImage2Chenge(Sprite sprite, Color color)
        {
            return;
        }

        public void EnchantImageReset()
        {
            return;
        }
    }

    private void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("EnchantUIController");
        if (obj is not null && obj.TryGetComponent<EnchantUIManager>(out EnchantUIManager mng))
            _enchantUIManager = mng;
        else
            _enchantUIManager = new NullObject();
    }
    
    public void ArrowEnchantUI_Normal()
    {
        _enchantUIManager.EnchantImageReset();
        //_enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[0].UIImage);
    }
    public void ArrowEnchantUI_Bomb()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[1].UISprite,_EnchantUI._enchantUIs[1].UIColor);
    }
    public void ArrowEnchantUI_Thunder()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[2].UISprite, _EnchantUI._enchantUIs[2].UIColor);
    }
    public void ArrowEnchantUI_KnockBack()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[3].UISprite, _EnchantUI._enchantUIs[3].UIColor);
    }
    public void ArrowEnchantUI_Homing()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[4].UISprite, _EnchantUI._enchantUIs[4].UIColor);
    }
    public void ArrowEnchantUI_Penetrate()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[5].UISprite, _EnchantUI._enchantUIs[5].UIColor);
    }
    public void ArrowEnchantUI_BombThunder()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[1].UISprite, _EnchantUI._enchantUIs[1].UIColor);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[2].UISprite, _EnchantUI._enchantUIs[2].UIColor);
    }
    public void ArrowEnchantUI_BombKnockBack()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[1].UISprite, _EnchantUI._enchantUIs[1].UIColor);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[3].UISprite, _EnchantUI._enchantUIs[3].UIColor);
    }
    public void ArrowEnchantUI_BombHoming()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[1].UISprite, _EnchantUI._enchantUIs[1].UIColor);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[4].UISprite, _EnchantUI._enchantUIs[4].UIColor);
    }
    public void ArrowEnchantUI_BombPenetrate()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[1].UISprite, _EnchantUI._enchantUIs[1].UIColor);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[5].UISprite, _EnchantUI._enchantUIs[5].UIColor);
    }
    public void ArrowEnchantUI_ThunderKnockBack()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[2].UISprite, _EnchantUI._enchantUIs[2].UIColor);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[3].UISprite, _EnchantUI._enchantUIs[3].UIColor);
    }
    public void ArrowEnchantUI_ThunderHoming()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[2].UISprite, _EnchantUI._enchantUIs[2].UIColor);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[4].UISprite, _EnchantUI._enchantUIs[4].UIColor);
    }
    public void ArrowEnchantUI_ThunderPenetrate()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[2].UISprite, _EnchantUI._enchantUIs[2].UIColor);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[5].UISprite, _EnchantUI._enchantUIs[5].UIColor);
    }
    public void ArrowEnchantUI_KnockBackHoming()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[3].UISprite, _EnchantUI._enchantUIs[3].UIColor);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[4].UISprite, _EnchantUI._enchantUIs[4].UIColor);
    }
    public void ArrowEnchantUI_KnockBackPenetrate()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[3].UISprite, _EnchantUI._enchantUIs[3].UIColor);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[5].UISprite, _EnchantUI._enchantUIs[5].UIColor);
    }
    public void ArrowEnchantUI_HomingPenetrate()
    {
        _enchantUIManager.EnchantImageReset();
        _enchantUIManager.EnchantImage1Chenge(_EnchantUI._enchantUIs[4].UISprite, _EnchantUI._enchantUIs[4].UIColor);
        _enchantUIManager.EnchantImage2Chenge(_EnchantUI._enchantUIs[5].UISprite, _EnchantUI._enchantUIs[5].UIColor);
    }


    #endregion
}