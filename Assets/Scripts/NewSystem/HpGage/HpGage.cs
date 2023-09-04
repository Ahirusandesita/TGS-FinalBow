// --------------------------------------------------------- 
// HpGage.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HpGage : MonoBehaviour
{
    #region variable 
    private Camera myCamera;
    private Slider slider;
    private RectTransform sliderpos;

    [SerializeField]
    private float Distance;

    private float decijion; // éOäpíÍï”Å@ë„ì¸óp
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {

    }

    private void Start()
    {
        slider = this.GetComponent<Slider>();
        sliderpos = this.GetComponent<RectTransform>();
        Hp(1f);
    }

    public void SetPosition(Transform targetTransform)
    {

    }
    public void Hp(float hp) => slider.value = hp;

    public void SetCamera(Camera camera) => myCamera = camera;
    #endregion
}