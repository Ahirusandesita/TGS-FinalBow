// --------------------------------------------------------- 
// EnchantUIManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnchantUIManager : MonoBehaviour
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

    public void EnchantImage1Chenge(Sprite sprite)
    {
        _renderer1.sprite = sprite;
    }
    public void EnchantImage2Chenge(Sprite sprite)
    {
        _renderer2.sprite = sprite;
    }

    public void EnchantImageReset()
    {
        _renderer1.sprite = null;
        _renderer2.sprite = null;
    }


    #endregion
}