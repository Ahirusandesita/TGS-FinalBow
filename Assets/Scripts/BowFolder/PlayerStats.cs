// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class PlayerStats : MonoBehaviour
{
    private bool isInvincible = false;


    public IReActiveProperty<int> readOnlyPlayerHp => playerHp;
    private ReActiveProperty<int> playerHp = new ReActiveProperty<int>();


    private ICanvasManager _canvasManager;
    private IFScoreManager_Hp _scoreManager;

    private ScifiBowConputerCtrl scifiBowConputerCtrl;
    private DamageUIManager damageUIManager;
    private void Start()
    {
        scifiBowConputerCtrl = GameObject.FindObjectOfType<ScifiBowConputerCtrl>();
        damageUIManager = GameObject.FindObjectOfType<DamageUIManager>();
        playerHp.Value = 100;
       // _canvasManager = GameObject.FindGameObjectWithTag("CanvasController").GetComponent<CanvasManager>();
        if (GameObject.FindGameObjectsWithTag("ScoreController").Length == 0)
        {
            enabled = false;
        }
        _scoreManager = GameObject.FindWithTag("ScoreController").GetComponent<ScoreManager>();
        //_scoreManager.BonusScore_HpValueSetting(playerHp.Value);
    }

    /// <summary>
    /// プレイヤーがダメージを受ける処理
    /// </summary>
    /// <param name="damage">ダメージの量</param>
    public void PlayerDamage(int damage)
    {
        if (!isInvincible)
        {
            playerHp.Value -= damage;
            scifiBowConputerCtrl.HpUpdate(playerHp.Value);
            damageUIManager.TakeDamageUIEvent();
            //_canvasManager.StagingDamage();
            StartCoroutine(Invincible());
            //_scoreManager.BonusScore_HpScore();
        }

    }

    /// <summary>
    /// プレイヤーが回復する処理
    /// </summary>
    /// <param name="heal">回復量</param>
    public void PlayerHealing(int heal)
    {
        playerHp.Value += heal;
        //_canvasManager.StagingRecovery();
    }

    /// <summary>
    /// 死んだ時の処理
    /// </summary>
    public void Player_Dead()
    {
        //まだなにもないけど
        //死ぬ処理入れる予定だよ
    }

    public int Player_HP => playerHp.Value;

    private IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.5f);
        isInvincible = false;
    }
}
