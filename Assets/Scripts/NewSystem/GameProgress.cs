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
    /// <summary>
    /// チュートリアル
    /// </summary>
    tutorial,
    /// <summary>
    /// ゲーム準備
    /// </summary>
    gamePreparation,
    /// <summary>
    /// InGame
    /// </summary>
    inGame,
    /// <summary>
    /// 1-4クリア
    /// </summary>
    inGameLastStageEnd,
    /// <summary>
    /// エクストラ準備
    /// </summary>
    extraPreparation,
    /// <summary>
    /// エクストラ
    /// </summary>
    extra,
    /// <summary>
    /// エクストラクリア
    /// </summary>
    extraEnd,
    /// <summary>
    /// リザルト
    /// </summary>
    result,
    /// <summary>
    /// エンド
    /// </summary>
    ending
}
public class GameProgress : MonoBehaviour
{
    private void Start()
    {
        gameProgressProperty.Value = GameProgressType.tutorial;
    }
    public IReActiveProperty<GameProgressType> readOnlyGameProgressProperty => gameProgressProperty;
    private ReActiveProperty<GameProgressType> gameProgressProperty = new ReActiveProperty<GameProgressType>();

    /// <summary>
    /// チュートリアル終了
    /// </summary>
    public void TutorialEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.gamePreparation) return;
        gameProgressProperty.Value = GameProgressType.gamePreparation;
    }
    /// <summary>
    /// インゲーム準備終了
    /// </summary>
    public void GamePreparationEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.inGame) return;
        gameProgressProperty.Value = GameProgressType.inGame;
    }
    /// <summary>
    /// インゲーム終了
    /// </summary>
    public void InGameEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.extraPreparation) return;
        gameProgressProperty.Value = GameProgressType.inGameLastStageEnd;
    }
    /// <summary>
    /// １－４終了
    /// </summary>
    public void InGameLastStageEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.extraPreparation) return;
        gameProgressProperty.Value = GameProgressType.extraPreparation;
    }
    /// <summary>
    /// エクストラステージ準備終了
    /// </summary>
    public void ExtraPreparationEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.extra) return;
        gameProgressProperty.Value = GameProgressType.extra;
    }
    /// <summary>
    /// エクストラステージ終了
    /// </summary>
    public void ExtraEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.extraEnd) return;
        gameProgressProperty.Value = GameProgressType.extraEnd;
    }
    /// <summary>
    /// エクストラエンド終了
    /// </summary>
    public void ExtraClearEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.result) return;
        gameProgressProperty.Value = GameProgressType.result;
    }
    /// <summary>
    /// リザルト終了
    /// </summary>
    public void ResultEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.ending) return;
        gameProgressProperty.Value = GameProgressType.ending;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ExtraEnding();
        }
    }
}