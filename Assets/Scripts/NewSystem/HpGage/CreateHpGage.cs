// --------------------------------------------------------- 
// CreateHpGage.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateHpGage : MonoBehaviour
{
    #region variable 
    public Slider slider;
    #endregion
    #region property
    #endregion
    #region method
    public HpGage Create()
    {
        GameObject sliderObject = Instantiate(slider.gameObject);
        return sliderObject.GetComponent<HpGage>();
    }

    #endregion
}