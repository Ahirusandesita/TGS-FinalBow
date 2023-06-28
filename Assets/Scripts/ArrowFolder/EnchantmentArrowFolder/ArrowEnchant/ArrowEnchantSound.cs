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
    public SoundParamTable _nomalSound;
    public SoundParamTable _bombSound;
    public SoundParamTable _thunderSound;
    public SoundParamTable _knockBackSound;
    public SoundParamTable _homingSound;
    public SoundParamTable _penetrateSound;
    public SoundParamTable _bombThunderSound;
    public SoundParamTable _bombKnockBackSound;
    public SoundParamTable _bombHomingSound;
    public SoundParamTable _bombPenetrateSound;
    public SoundParamTable _thunderKnockBackSound;
    public SoundParamTable _thunderHomingSound;
    public SoundParamTable _thunderPenetrateSound;
    public SoundParamTable _knockBackHomingSound;
    public SoundParamTable _knockBackPenetrateSound;
    public SoundParamTable _homingPenetrateSound;

    public SoundParamTable _newEnchantSound;

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
        spawnAudioSource.PlayOneShot(_nomalSound._audioClip);
    }
    public void ArrowSound_Bomb(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_bombSound._audioClip);
    }
    public void ArrowSound_Thunder(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_thunderSound._audioClip);
    }
    public void ArrowSound_KnockBack(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_knockBackSound._audioClip);
    }
    public void ArrowSound_Homing(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_homingSound._audioClip);
    }
    public void ArrowSound_Penetrate(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_penetrateSound._audioClip);
    }
    public void ArrowSound_BombThunder(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_bombThunderSound._audioClip);
    }
    public void ArrowSound_BombKnockBack(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_bombKnockBackSound._audioClip);
    }
    public void ArrowSound_BombHoming(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_bombHomingSound._audioClip);
    }
    public void ArrowSound_BombPenetrate(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_bombPenetrateSound._audioClip);
    }
    public void ArrowSound_ThunderKnockBack(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_thunderKnockBackSound._audioClip);
    }
    public void ArrowSound_ThunderHoming(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_thunderHomingSound._audioClip);
    }
    public void ArrowSound_ThunderPenetrate(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_thunderPenetrateSound._audioClip);
    }
    public void ArrowSound_KnockBackHoming(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_knockBackHomingSound._audioClip);
    }
    public void ArrowSound_KnockBackPenetrate(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_knockBackPenetrateSound._audioClip);
    }
    public void ArrowSound_HomingPenetrate(AudioSource spawnAudioSource)
    {
        spawnAudioSource = _audioSource;
        spawnAudioSource.PlayOneShot(_homingPenetrateSound._audioClip);
    }

    public void ArrowSound_EnchantSound()
    {
        _audioSource.PlayOneShot(_newEnchantSound._audioClip);
    }
    #endregion
}