// --------------------------------------------------------- 
// BossStats.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStats : EnemyStats
{
    public SceneObject _sceneObject;
    public SceneObject _sceneObject2;


    private void OnEnable()
    {
        // HPÇÃê›íË
        _hp = _maxHp;
    }

    public override void TakeBomb(int damage)
    {
        TakeDamage(damage);
    }

    public override void TakeThunder(int a)
    {

    }

    public override void TakeKnockBack()
    {

    }

    public override void Death()
    {
        print("É{ÉXÇì|ÇµÇ‹ÇµÇΩ");
        this.gameObject.SetActive(false);
        SceneManagement scene = GameObject.FindWithTag("SceneController").GetComponent<SceneManagement>();

        if (SceneManager.GetActiveScene().name == "DebugScene")
        {
            scene.SceneLoadSpecifyMove(_sceneObject);
        }
        else
        {
            scene.SceneLoadSpecifyMove(_sceneObject2);
        }
    }

    public override int HP
    {
        get
        {
            return _hp;
        }
    }
}
