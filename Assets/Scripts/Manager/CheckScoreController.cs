// --------------------------------------------------------- 
// CheckScoreController.cs 
// 
// CreateDay: 2023/06/19
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
public class CheckScoreController : MonoBehaviour
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EntryPoint()
    {
        if (GameObject.FindGameObjectsWithTag("ScoreController").Length == 0)
        {
            GameObject gameController = new GameObject("ScoreController");
            gameController.AddComponent<ScoreManager>();
            gameController.tag = "ScoreController";
        }
    }
    #endregion
}