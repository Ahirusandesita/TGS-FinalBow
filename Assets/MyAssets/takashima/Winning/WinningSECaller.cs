// --------------------------------------------------------- 
// WinningSECaller.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class WinningSECaller : MonoBehaviour
{

    [SerializeField] AudioSource source = default;

    [SerializeField] AudioClip firstClip = default;

    [SerializeField] AudioClip secondClip = default;

    [SerializeField] AudioClip thirdClip = default;

    [SerializeField] AudioClip forthClip = default;

    private void Awake()
    {
        if(source is null)
        {
            source = GetComponent<AudioSource>();
        }
    }

    private void PlaySE(AudioClip audio)
    {
        if(audio is not null)
        source.PlayOneShot(audio);
    }

    public void FirstSE()
    {
        PlaySE(firstClip);
    }

    public void SecondSE()
    {
        PlaySE(secondClip);
    }

    public void ThirdSE()
    {
        PlaySE(thirdClip);
    }

    public void ForthSE()
    {
        PlaySE(forthClip);
    }
}