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
    private int _player_HP = 10;
    private ICanvasManager _canvasManager;
    private IFScoreManager_Hp _scoreManager;
    private void Start()
    {
        _canvasManager = GameObject.FindGameObjectWithTag("CanvasController").GetComponent<CanvasManager>();
        if (GameObject.FindGameObjectsWithTag("ScoreController").Length == 0)
        {
            enabled = false;
        }
        _scoreManager = GameObject.FindWithTag("ScoreController").GetComponent<ScoreManager>();
        _scoreManager.BonusScore_HpValueSetting(_player_HP);
    }

    /// <summary>
    /// プレイヤーがダメージを受ける処理
    /// </summary>
    /// <param name="damage">ダメージの量</param>
    public void PlayerDamage(int damage)
    {
        if (!isInvincible)
        {
            _player_HP -= damage;
            _canvasManager.StagingDamage();
            StartCoroutine(Invincible());
            _scoreManager.BonusScore_HpScore();
        }

    }

    /// <summary>
    /// プレイヤーが回復する処理
    /// </summary>
    /// <param name="heal">回復量</param>
    public void PlayerHealing(int heal)
    {
        _player_HP += heal;
        _canvasManager.StagingRecovery();
    }

    /// <summary>
    /// 死んだ時の処理
    /// </summary>
    public void Player_Dead()
    {
        //まだなにもないけど
        //死ぬ処理入れる予定だよ
    }

    public int Player_HP
    {
        get
        {
            return _player_HP;
        }

    }

    private IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(5f);
        isInvincible = false;
    }
}
