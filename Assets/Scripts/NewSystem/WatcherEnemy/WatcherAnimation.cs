// --------------------------------------------------------- 
// WatcherAnimation.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WatcherAnimation : MonoBehaviour
{
    private Animation myAnimation;
    private List<AnimationClip> animationClips = new List<AnimationClip>();
    #region variable 
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        myAnimation = this.GetComponent<Animation>();
    }

    private void Start()
    {
        foreach(AnimationState animationState in myAnimation)
        {
            animationClips.Add(animationState.clip);
        }
        Fly();
    }

    public void Fly()
    {
        myAnimation.Play();
    }
    #endregion
}