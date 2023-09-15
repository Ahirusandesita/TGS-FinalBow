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
            }
            );
    }

    private IEnumerator GamePreparationProcess()
    {
        textMesh.text = "AAAA";
        yield return new WaitForSeconds(3f);
        textMesh.text = default;
        gameProgress.GamePreparationEnding();
    }
    #endregion
}