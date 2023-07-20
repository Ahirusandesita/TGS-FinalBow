// --------------------------------------------------------- 
// EnchantUIManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

interface IFEnchantUIManager
{
    void EnchantImage1Chenge(Sprite sprite, Color color);

    void EnchantImage2Chenge(Sprite sprite, Color color);

    void EnchantImageReset();
}
public class EnchantUIManager : MonoBehaviour,IFEnchantUIManager
{
    #region variable 
    private SpriteRenderer _renderer1;
    private SpriteRenderer _renderer2;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _renderer1 = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _renderer2 = this.transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    public void EnchantImage1Chenge(Sprite sprite,Color color)
    {
        _renderer1.sprite = sprite;
        _renderer1.color = color;
    }
    public void EnchantImage2Chenge(Sprite sprite,Color color)
    {
        _renderer2.sprite = sprite;
        _renderer2.color = color;
    }

    public void EnchantImageReset()
    {
        _renderer1.sprite = null;
        _renderer2.sprite = null;
    }


    #endregion
}