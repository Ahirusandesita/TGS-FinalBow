// --------------------------------------------------------- 
// TargetAnimation.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TargetAnimation : MonoBehaviour
{
    #region variable 
    Animator _animator;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {

    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TargetPushed()
    {
        _animator.SetBool("IsDead", true);
    }
    private void TargetReset()
    {
        _animator.SetBool("IsDead", false);
    }
    #endregion
}