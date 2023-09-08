// --------------------------------------------------------- 
// ChainAudioPlaySE.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class ChainAudioPlaySE : MonoBehaviour
{
    #region variable 
    [SerializeField] AudioClip clip = default;
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }

    #endregion
}