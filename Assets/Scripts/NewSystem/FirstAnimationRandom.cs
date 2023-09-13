// --------------------------------------------------------- 
// FirstAnimationRandom.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class FirstAnimationRandom : MonoBehaviour
{
    #region variable 
    private Animator anim;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {

    }

    private void OnEnable()
    {
        anim = this.GetComponent<Animator>();
        anim.Play(anim.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, Random.Range(0f, 1f));
    }

    private void Update()
    {

    }
    #endregion
}