// --------------------------------------------------------- 
// GamePreparation.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using TMPro;
public class GamePreparation : MonoBehaviour
{
    #region variable 
    private Animator _animator;
    #endregion
    #region method

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    public IEnumerator GamePreparationProcess()
    {
        _animator.Play("anim_Start");
        yield return new WaitForSeconds(5f);
    }

    public IEnumerator InGameLastStageEndProcess()
    {
        yield return new WaitForSeconds(2.5f);
    }

    public IEnumerator ExtraPreparationProcess()
    {
        _animator.Play("anim_ExtraStage");
        yield return new WaitForSeconds(5f);
    }

    public IEnumerator WaitPerocess(float waitTime)
    {
        _animator.Play("anim_Wait");
        yield return new WaitForSeconds(waitTime);
        _animator.SetTrigger("Exit");
    }
    #endregion
}