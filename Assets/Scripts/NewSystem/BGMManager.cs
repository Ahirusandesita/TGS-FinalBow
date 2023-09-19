// --------------------------------------------------------- 
// BGMManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BGMManager : MonoBehaviour
{
    #region variable 
    public AudioSource tutorialAudioSource;
    public AudioSource inGameAudioSource;
    public AudioSource extraAudioSource;
    public AudioSource resultAudioSource;
    private GameProgress gameProgress;
    #endregion
    #region property
    #endregion
    #region method
    private void Awake()
    {
        inGameAudioSource.enabled = false;
        tutorialAudioSource.enabled = false;
        extraAudioSource.enabled = false;
        resultAudioSource.enabled = false;
        gameProgress = GameObject.FindObjectOfType<GameProgress>();

        gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if(progressType == GameProgressType.tutorial)
                {
                    tutorialAudioSource.enabled = true;
                }
                if(progressType == GameProgressType.gamePreparation)
                {
                    StartCoroutine(AudioFadeOut(tutorialAudioSource));
                }
                if(progressType == GameProgressType.inGame)
                { 
                    inGameAudioSource.enabled = true;
                }
                if (progressType == GameProgressType.inGameLastStageEnd)
                {
                    StartCoroutine(AudioFadeOut(inGameAudioSource));
                }
                if (progressType == GameProgressType.extra)
                {
                    extraAudioSource.enabled = true;
                }
                if (progressType == GameProgressType.extraEnd)
                {
                    StartCoroutine(AudioFadeOut(extraAudioSource));
                }
                if (progressType == GameProgressType.result)
                {
                    resultAudioSource.enabled = true;
                }
                if (progressType == GameProgressType.ending)
                {
                    StartCoroutine(AudioFadeOut(resultAudioSource));
                }
            }
            );
    }

    private IEnumerator AudioFadeOut(AudioSource audioSource)
    {
        for (; ; )
        {
            audioSource.volume -= 0.01f;
            if(audioSource.volume <= 0)
            {
                audioSource.volume = 0f;
                break;
            }
            yield return new WaitForSeconds(0.025f);
        }
        audioSource.enabled = false;
    }
    #endregion
}