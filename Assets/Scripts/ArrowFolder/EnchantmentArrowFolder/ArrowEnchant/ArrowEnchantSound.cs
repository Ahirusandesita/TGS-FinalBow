// --------------------------------------------------------- 
// ArrowEnchantSound.cs 
// 
// CreateDay: 2023/06/09
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ArrowEnchantSound : MonoBehaviour
{
    #region variable 
    public AudioClip _nomalSound;
    public AudioClip _bombSound;
    public AudioClip _thunderSound;
    public AudioClip _knockBackSound;
    public AudioClip _homingSound;
    public AudioClip _penetrateSound;
    public AudioClip _bombThunderSound;
    public AudioClip _bombKnockBackSound;
    public AudioClip _bombHomingSound;
    public AudioClip _bombPenetrateSound;
    public AudioClip _thunderKnockBackSound;
    public AudioClip _thunderHomingSound;
    public AudioClip _thunderPenetrateSound;
    public AudioClip _knockBackHomingSound;
    public AudioClip _knockBackPenetrateSound;
    public AudioClip _homingPenetrateSound;

    private AudioSource _audioSource;
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void ArrowSound_Normal(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_nomalSound);
    }
    public void ArrowSound_Bomb(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_bombSound);
    }
    public void ArrowSound_Thunder(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_thunderSound);
    }
    public void ArrowSound_KnockBack(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_knockBackSound);
    }
    public void ArrowSound_Homing(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_homingSound);
    }
    public void ArrowSound_Penetrate(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_penetrateSound);
    }
    public void ArrowSound_BombThunder(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_bombThunderSound);
    }
    public void ArrowSound_BombKnockBack(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_bombKnockBackSound);
    }
    public void ArrowSound_BombHoming(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_bombHomingSound);
    }
    public void ArrowSound_BombPenetrate(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_bombPenetrateSound);
    }
    public void ArrowSound_ThunderKnockBack(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_thunderKnockBackSound);
    }
    public void ArrowSound_ThunderHoming(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_thunderHomingSound);
    }
    public void ArrowSound_ThunderPenetrate(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_thunderPenetrateSound);
    }
    public void ArrowSound_KnockBackHoming(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_knockBackHomingSound);
    }
    public void ArrowSound_KnockBackPenetrate(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_knockBackPenetrateSound);
    }
    public void ArrowSound_HomingPenetrate(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_homingPenetrateSound);
    }

    public void ArrowSound_EnchantSound(AudioClip enchantSound)
    {
        _audioSource.PlayOneShot(enchantSound);
    }
    #endregion
}