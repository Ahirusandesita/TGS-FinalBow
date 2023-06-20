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
    private int _player_HP = default;
    private ICanvasManager _canvasManager;

    private void Start()
    {
        _canvasManager = GameObject.FindGameObjectWithTag("CanvasController").GetComponent<CanvasManager>();
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
