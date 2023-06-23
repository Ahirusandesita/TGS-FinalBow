// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using UnityEngine;

public class BirdStats : CommonEnemyStats
{
    [Tooltip("�������ꂽ�����ǂ���")]
    private bool _isSummmon = false;

    IFScoreManager_NomalEnemy _score = default;

    IFScoreManager_Combo _combo = default;

    string caritag = "ScoreController";
    /// <summary>
    /// �������ꂽ�������񂾂Ƃ��ɌĂяo��
    /// </summary>
    public delegate void OnDeathBird();
    [Tooltip("���̒������񂾂Ƃ��Ɏ��s / �u�G�̎c�����v�̃f�N�������g������o�^")]
    public OnDeathBird _onDeathBird;

    protected override void Start()
    {
        base.Start();

        try
        {
            _score = GameObject.FindGameObjectWithTag(caritag).GetComponent<ScoreManager>();

            _combo = GameObject.FindGameObjectWithTag(caritag).GetComponent<ScoreManager>();
        }
        catch(System.NullReferenceException)
        {
            X_Debug.LogError("ScoreManager�������T�[4p98");
        }

    }

    protected override void OnEnable()
    {
        // HP�̍Đݒ���Ăяo��
        base.OnEnable();

        // �u��������Ă��Ȃ��v�ɏ�����
        _isSummmon = false;
    }

    /// <summary>
    /// �������ꂽ�����ǂ���
    /// </summary>
    public bool IsSummmon
    {
        get
        {
            return _isSummmon;
        }
        set
        {
            _isSummmon = value;
        }
    }

    public override void TakeBomb(int damage)
    {
        TakeDamage(damage);
        X_Debug.Log("��������������");
    }

    public override void Death()
    {
        // �ϐ��̃f�N�������g
        _onDeathBird();
        _score.NomalScore_NomalEnemyScore();
        _combo.NomalScore_ComboScore();
        X_Debug.Log("�������ɂ܂���");
        gameObject.SetActive(false);

        this.GetComponent<EnemyDeath>().Death();
    }
}
