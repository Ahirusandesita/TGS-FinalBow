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
    /// �`���[�g���A��
    /// </summary>
    tutorial,
    /// <summary>
    /// �Q�[������
    /// </summary>
    gamePreparation,
    /// <summary>
    /// InGame
    /// </summary>
    inGame,
    /// <summary>
    /// 1-4�N���A
    /// </summary>
    inGameLastStageEnd,
    /// <summary>
    /// �G�N�X�g������
    /// </summary>
    extraPreparation,
    /// <summary>
    /// �G�N�X�g��
    /// </summary>
    extra,
    /// <summary>
    /// �G�N�X�g���N���A
    /// </summary>
    extraEnd,
    /// <summary>
    /// ���U���g
    /// </summary>
    result,
    /// <summary>
    /// �G���h
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
    /// �`���[�g���A���I��
    /// </summary>
    public void TutorialEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.gamePreparation) return;
        gameProgressProperty.Value = GameProgressType.gamePreparation;
    }
    /// <summary>
    /// �C���Q�[�������I��
    /// </summary>
    public void GamePreparationEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.inGame) return;
        gameProgressProperty.Value = GameProgressType.inGame;
    }
    /// <summary>
    /// �C���Q�[���I��
    /// </summary>
    public void InGameEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.extraPreparation) return;
        gameProgressProperty.Value = GameProgressType.inGameLastStageEnd;
    }
    /// <summary>
    /// �P�|�S�I��
    /// </summary>
    public void InGameLastStageEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.extraPreparation) return;
        gameProgressProperty.Value = GameProgressType.extraPreparation;
    }
    /// <summary>
    /// �G�N�X�g���X�e�[�W�����I��
    /// </summary>
    public void ExtraPreparationEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.extra) return;
        gameProgressProperty.Value = GameProgressType.extra;
    }
    /// <summary>
    /// �G�N�X�g���X�e�[�W�I��
    /// </summary>
    public void ExtraEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.extraEnd) return;
        gameProgressProperty.Value = GameProgressType.extraEnd;
    }
    /// <summary>
    /// �G�N�X�g���G���h�I��
    /// </summary>
    public void ExtraClearEnding()
    {
        if (gameProgressProperty.Value == GameProgressType.result) return;
        gameProgressProperty.Value = GameProgressType.result;
    }
    /// <summary>
    /// ���U���g�I��
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