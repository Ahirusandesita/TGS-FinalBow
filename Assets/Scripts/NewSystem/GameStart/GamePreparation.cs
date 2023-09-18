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
    private TextMeshProUGUI textMesh;
    #endregion
    #region method

    private void Awake()
    {
        textMesh = this.GetComponent<TextMeshProUGUI>();
        textMesh.text = default;
    }

    public IEnumerator GamePreparationProcess()
    {
        textMesh.text = "敵を倒せ！";
        yield return new WaitForSeconds(2.5f);
        textMesh.text = default;
    }

    public IEnumerator InGameLastStageEndProcess()
    {
        textMesh.text = "ステージクリア！";
        yield return new WaitForSeconds(2.5f);
        textMesh.text = default;
    }

    public IEnumerator ExtraPreparationProcess()
    {
        textMesh.text = "EXTRA STAGE！";
        yield return new WaitForSeconds(2.5f);
        textMesh.text = default;
    }
    #endregion
}