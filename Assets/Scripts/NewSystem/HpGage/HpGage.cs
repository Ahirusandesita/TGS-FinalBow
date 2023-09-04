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
    }

    public void SetPosition(Transform targetTransform)
    {
        transform.position = RectTransformUtility.WorldToScreenPoint(
          myCamera,
          targetTransform.position + Vector3.up);
        transform.localScale = targetTransform.localScale / (Vector3.Distance(myCamera.transform.position, targetTransform.position) / 5f);
    }
    public void Hp(float hp) => slider.value = hp;

    public void SetCamera(Camera camera) => myCamera = camera;
    #endregion
}