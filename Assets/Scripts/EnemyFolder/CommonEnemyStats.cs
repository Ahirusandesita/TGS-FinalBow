// --------------------------------------------------------- 
// CommonEnemy.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

interface IFCommonEnemyGetParalysis
{
    bool Get_isParalysis { get; }

}
public abstract class CommonEnemyStats : EnemyStats, IFCommonEnemyGetParalysis
{
    [SerializeField] protected MaxInhallObjectSetting maxInhallData = default;

    protected CashObjectInformation _cashObjectInformation = default;

    protected IFScoreManager_NomalEnemy _score;
    protected IFScoreManager_Combo _combo = default;

    protected float _paralysisWait = default;

    protected GameObject _paralysisEffects = default;

    [SerializeField]
    protected DropData _dropData;

    protected Drop _drop;

    /// <summary>
    /// ��჎���
    /// </summary>
    protected readonly float _baseParalysisTime = 0.5f;

    protected readonly float _maxParalysisTime = 3f;

    /// <summary>
    /// �m�b�N�o�b�N����
    /// </summary>
    protected readonly float _knockBackDistance = 10f;

    /// <summary>
    /// �m�b�N�o�b�N�̋}�Ȍ������n�܂�̂����܂�(1�ȏ��)
    /// </summary>
    protected readonly float _knockBackDown = 1f;

    /// <summary>
    /// �^�C���A�E�g���鎞��
    /// </summary>
    protected readonly float _timeOutTime = 0.5f;

    protected bool _isParalysis = false;

    protected const int PARALYSIS_EFFECTS_INDEX = 4;

    protected float _addParalysisTime = 0f;

    protected float _paralysisTime = default;

    private Coroutine _activeCoroutine = default;

    public float ParalysisTime { get => _paralysisTime; }

    /// <summary>
    /// �������ꂽ�G�����񂾂Ƃ��ɌĂяo��
    /// </summary>
    public delegate void OnDeathEnemy();

    protected override void Start()
    {
        base.Start();

        try
        {
            _score = GameObject.FindWithTag("ScoreController").GetComponent<ScoreManager>();
            _combo = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreManager>();
        }
        catch (System.NullReferenceException)
        {
            X_Debug.LogError("ScoreManager�������T�[4p98");
        }

        _paralysisEffects = transform.GetChild(PARALYSIS_EFFECTS_INDEX).gameObject;

        _paralysisEffects.SetActive(false);

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        _addParalysisTime = (_maxParalysisTime - _baseParalysisTime) / maxInhallData.GetMaxInhall;

        _drop = this.GetComponent<Drop>();
    }

    protected virtual void OnEnable()
    {
        // HP�̍Đݒ�
        _hp = _maxHp;
    }

    protected virtual void OnDisable()
    {
        _isParalysis = false;
    }


    public override void TakeBomb(int damage, Transform arrowTransform, Vector3 arrowVector)
    {
        base.TakeBomb(damage, arrowTransform, arrowVector);

        TakeDamage(damage, arrowTransform, arrowVector);
    }

    public override void TakeBomb(int damage)
    {
        base.TakeBomb(damage);

        TakeDamage(damage);
    }

    public override void TakeThunder(int power)
    {
        base.TakeThunder(power);

        _paralysisTime = 1.2f;
        _activeCoroutine = StartCoroutine(ParalysisCoroutine(_paralysisTime));
    }

    public override void Death()
    {
        // �X�R�A�����Z
        _score.NormalScore_NormalEnemyScore();
        // ���񂾂Ƃ��̃G�t�F�N�g�Ăяo��
        _objectPoolSystem.CallObject(EffectPoolEnum.EffectPoolState.enemyDeath, _transform.position, Quaternion.identity);

        _objectPoolSystem.ReturnObject(_cashObjectInformation);
    }

    /// <summary>
    /// �����i���Łj����
    /// </summary>
    public virtual void Despawn()
    {
        // �v�[���ɕԂ�
        _objectPoolSystem.ReturnObject(_cashObjectInformation);
    }

    public override int HP
    {
        get
        {
            return _hp;
        }
    }

    public bool Get_isParalysis
    {
        get
        {
            return _isParalysis;
        }
    }


    /// <summary>
    /// �m�b�N�o�b�N����
    /// </summary>
    private IEnumerator KnockbackCoroutine()
    {
        // ���݂̃m�b�N�o�b�N���Ă��鋗��
        float nowKnockBackedDistance = 0;

        // �^�C���A�E�g�p
        float timeOut = Time.time + _timeOutTime;

        // ������������܂�
        while (_knockBackDistance > nowKnockBackedDistance)
        {
            // �ړ����鋗��
            float moveDistance = Mathf.Pow(_knockBackDistance * Time.deltaTime,
                (nowKnockBackedDistance / _knockBackDistance) * _knockBackDown);

            // �ړ����������ɉ��Z����
            nowKnockBackedDistance += moveDistance;

            // �ʒu�ύX
            _transform.Translate(Vector3.back * moveDistance);

            yield return null;

            if (Time.time > timeOut)
            {
                Debug.LogWarning("[test]�^�C���A�E�g");
                yield break;
            }
        }
    }

    /// <summary>
    /// �T���_�[�󂯂��Ƃ��̖�ჁH
    /// </summary>
    /// <returns></returns>
    private IEnumerator ParalysisCoroutine(float time)
    {

        _paralysisEffects.SetActive(true);
        _isParalysis = true;

        yield return new WaitForSeconds(time);

        _paralysisEffects.SetActive(false);
        _isParalysis = false;
    }
}