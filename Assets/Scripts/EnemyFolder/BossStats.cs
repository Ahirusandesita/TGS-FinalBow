// --------------------------------------------------------- 
// BossStats.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class BossStats : EnemyStats
{
    [Tooltip("�{�X�̍ő�HP")]
    public const int MAX_BOSS_HP = 100;

    public SceneObject _sceneObject;


    private void OnEnable()
    {
        // HP�̐ݒ�
        _hp = MAX_BOSS_HP;
    }

    public override void TakeBomb(int damage)
    {
        TakeDamage(damage);
    }

    public override void TakeThunder()
    {
        
    }

    public override void TakeKnockBack()
    {

    }

    public override void Death()
    {
        print("�{�X��|���܂���");
        this.gameObject.SetActive(false);

        GameObject.FindWithTag("SceneController").GetComponent<SceneManagement>().SceneLoadSpecifyMove(_sceneObject);
    }

    public override int HP
    {
        get
        {
            return _hp;
        }
    }
}
