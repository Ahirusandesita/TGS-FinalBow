// --------------------------------------------------------- 
// Game.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public enum GameProgressType
{
    tutorial,
    gamePreparation,
    inGame,
    result,
    ending
}
public class GameProgress : MonoBehaviour
{
    private void Awake()
    {
        gameProgressProperty.Value = GameProgressType.tutorial;
    }
    public IReActiveProperty<GameProgressType> readOnlyGameProgressProperty => gameProgressProperty;
    private ReActiveProperty<GameProgressType> gameProgressProperty = new ReActiveProperty<GameProgressType>();

    public void TutorialEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.gamePreparation) return;
        gameProgressProperty.Value = GameProgressType.gamePreparation;
    }
    public void GamePreparationEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.inGame) return;
        gameProgressProperty.Value = GameProgressType.inGame;
    }
    public void InGameEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.result) return;
        gameProgressProperty.Value = GameProgressType.result;
    }
    public void ResultEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.ending) return;
        gameProgressProperty.Value = GameProgressType.ending;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GamePreparationEnding();
        }
    }
}