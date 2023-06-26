// --------------------------------------------------------- 
// BowSE.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class BowSE : MonoBehaviour
{

    #region variable 

    [SerializeField] AudioClip _drawClip = default;

    [SerializeField] AudioClip _attractClip = default;

    [SerializeField] AudioClip _shotClip = default;

    AudioSource _audio = default;

    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }


    public void CallDrawSE(float drawPower)
    {
        _audio.PlayOneShot(_drawClip,drawPower);
    }

    public void CallAttractSE()
    {
        _audio.PlayOneShot(_attractClip);
    }

    public void CallShotSE()
    {
        _audio.PlayOneShot(_shotClip);
    }
    #endregion
}