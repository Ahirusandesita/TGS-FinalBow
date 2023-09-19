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
                    tutorialAudioSource.enabled = false;
                }
                if(progressType == GameProgressType.inGame)
                { 
                    inGameAudioSource.enabled = true;
                }
                if (progressType == GameProgressType.inGameLastStageEnd)
                {
                    inGameAudioSource.enabled = false;
                }
                if (progressType == GameProgressType.extra)
                {
                    extraAudioSource.enabled = true;
                }
                if (progressType == GameProgressType.result)
                {
                    extraAudioSource.enabled = false;
                    resultAudioSource.enabled = true;
                }
                if (progressType == GameProgressType.ending)
                {
                    resultAudioSource.enabled = false;
                }
            }
            );
    }
    #endregion
}