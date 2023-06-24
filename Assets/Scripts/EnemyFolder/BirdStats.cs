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

    /// <summary>
    /// �������ꂽ�������񂾂Ƃ��ɌĂяo��
    /// </summary>
    public delegate void OnDeathBird();
    [Tooltip("���̒������񂾂Ƃ��Ɏ��s / �u�G�̎c�����v�̃f�N�������g������o�^")]
    public OnDeathBird _onDeathBird;

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

        // �X�R�A�����Z
        _score.NomalScore_NomalEnemyScore();
        _combo.NomalScore_ComboScore();

        // ���񂾂Ƃ��̃G�t�F�N�g�Ăяo��
        _objectPoolSystem.CallObject(EffectPoolEnum.EffectPoolState.enemyDeath, _myTransform.position, Quaternion.identity);

        // �v�[���ɕԂ�
        _objectPoolSystem.ReturnObject(_cashObjectInformation);
    }

    public override void Despawn()
    {
        // �ϐ��̃f�N�������g
        _onDeathBird();

        // �������Ȃ鉉�o

        // �v�[���ɕԂ�
        _objectPoolSystem.ReturnObject(_cashObjectInformation);
    }
}
