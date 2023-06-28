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
    private Image _image1;
    private Image _image2;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _image1 = this.transform.GetChild(0).GetComponent<Image>();
        _image2 = this.transform.GetChild(1).GetComponent<Image>();
    }

    public void EnchantImage1Chenge(Image image)
    {
        _image1 = image;
    }
    public void EnchantImage2Chenge(Image image)
    {
        _image2 = image;
    }

    public void EnchantImageReset()
    {
        _image1 = null;
        _image2 = null;
    }


    #endregion
}