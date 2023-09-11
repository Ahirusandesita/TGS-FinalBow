// --------------------------------------------------------- 
// LockOnUISystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class LockOnUISystem : MonoBehaviour
{
    #region variable 
    [SerializeField]
    private Image _lockOn;

    [SerializeField]
    private Image _lockOnGage;

    [SerializeField]
    private Image _lockOnComplete;

    private bool _isActive = false;
    #endregion
    #region property
    #endregion
    #region method

    public void LockOnNow(float percent)
    {
        if (!_isActive)
        {
            _isActive = true ;
            _lockOn.enabled = _isActive;
            _lockOnGage.enabled = _isActive;
        }
        _lockOnGage.fillAmount = percent;
        if(percent >= 1)
        {
            _lockOn.enabled = !_isActive;
            _lockOnGage.enabled = !_isActive;
            _lockOnComplete.enabled = _isActive;
        }
    }

    public void LockOnEnd()
    {
        if (_isActive)
        {
            _isActive = false;
            _lockOn.enabled = _isActive;
            _lockOnGage.enabled = _isActive;
            _lockOnComplete.enabled = _isActive;
        }
    } 

 #endregion
}