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
    private GameProgress gameProgress;
    private TextMeshProUGUI textMesh;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        gameProgress = GameObject.FindObjectOfType<GameProgress>();
        textMesh = this.GetComponent<TextMeshProUGUI>();
        textMesh.text = default;
        gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if (progressType == GameProgressType.gamePreparation)
                {
                   StartCoroutine(GamePreparationProcess());
                }

                if (progressType == GameProgressType.inGameLastStageEnd)
                {
                    StartCoroutine(InGameLastStageEndProcess());
                }

                if (progressType == GameProgressType.extraPreparation)
                {
                    StartCoroutine(ExtraPreparationProcess());
                }
            }
            );
    }

    private IEnumerator GamePreparationProcess()
    {
        textMesh.text = "敵を倒せ！";
        yield return new WaitForSeconds(5f);
        textMesh.text = default;
        gameProgress.GamePreparationEnding();
    }

    private IEnumerator InGameLastStageEndProcess()
    {
        textMesh.text = "ステージクリア！";
        yield return new WaitForSeconds(2.5f);
        textMesh.text = default;
        gameProgress.InGameLastStageEnding();
    }

    private IEnumerator ExtraPreparationProcess()
    {
        textMesh.text = "EXTRA STAGE！";
        yield return new WaitForSeconds(4f);
        textMesh.text = default;
        gameProgress.ExtraPreparationEnding();
    }
    #endregion
}