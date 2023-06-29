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

    public void EnchantImage1Chenge(SpriteRenderer renderer)
    {
        _renderer1 = renderer;
    }
    public void EnchantImage2Chenge(SpriteRenderer renderer)
    {
        _renderer2 = renderer;
    }

    public void EnchantImageReset()
    {
        _renderer1 = null;
        _renderer2 = null;
    }


    #endregion
}