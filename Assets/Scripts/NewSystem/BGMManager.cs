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
    private GameProgress gameProgress;
    #endregion
    #region property
    #endregion
    #region method
    private void Awake()
    {
        gameProgress = GameObject.FindObjectOfType<GameProgress>();

        gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if(progressType == GameProgressType.tutorial)
                {
                    tutorialAudioSource.enabled = true;
                }
                if(progressType == GameProgressType.inGame)
                {
                    tutorialAudioSource.enabled = false;
                    inGameAudioSource.enabled = true;
                }
            }
            );
    }
    #endregion
}