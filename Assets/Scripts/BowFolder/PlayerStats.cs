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
    /// �v���C���[���_���[�W���󂯂鏈��
    /// </summary>
    /// <param name="damage">�_���[�W�̗�</param>
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
    /// �v���C���[���񕜂��鏈��
    /// </summary>
    /// <param name="heal">�񕜗�</param>
    public void PlayerHealing(int heal)
    {
        _player_HP += heal;
        _canvasManager.StagingRecovery();
    }

    /// <summary>
    /// ���񂾎��̏���
    /// </summary>
    public void Player_Dead()
    {
        //�܂��Ȃɂ��Ȃ�����
        //���ʏ��������\�肾��
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
