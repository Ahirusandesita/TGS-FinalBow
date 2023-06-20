// --------------------------------------------------------- 
// CheckGameController.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class CheckGameController : MonoBehaviour
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EntryPoint()
    {
        if(GameObject.FindGameObjectsWithTag("GameController").Length == 0)
        {
           GameObject gameController = new GameObject("GameController");
            gameController.AddComponent<GameManager>();
            gameController.tag = "GameController";
        }
    }

    #endregion
}