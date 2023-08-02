// --------------------------------------------------------- 
// BirdAnimationController.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BirdAnimationController : MonoBehaviour
{
    #region variable 
    Animator animator = default;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamageAnimation()
    {
        
    }
    #endregion
}