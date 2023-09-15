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
    /// 麻痺時間
    /// </summary>
    protected readonly float _baseParalysisTime = 0.5f;

    protected readonly float _maxParalysisTime = 3f;

    /// <summary>
    /// ノックバック距離
    /// </summary>
    protected readonly float _knockBackDistance = 10f;

    /// <summary>
    /// ノックバックの急な減衰が始まるのが早まる(1以上で)
    /// </summary>
    protected readonly float _knockBackDown = 1f;

    /// <summary>
    /// タイムアウトする時間
    /// </summary>
    protected readonly float _timeOutTime = 0.5f;

    protected bool _isParalysis = false;

    protected const int PARALYSIS_EFFECTS_INDEX = 4;

    protected float _addParalysisTime = 0f;

    protected float _paralysisTime = default;

    private Coroutine _activeCoroutine = default;

    public float ParalysisTime { get => _paralysisTime; }

    /// <summary>
    /// 召喚された敵が死んだときに呼び出す
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
            X_Debug.LogError("ScoreManagerがワルサー4p98");
        }

        _paralysisEffects = transform.GetChild(PARALYSIS_EFFECTS_INDEX).gameObject;

        _paralysisEffects.SetActive(false);

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        _addParalysisTime = (_maxParalysisTime - _baseParalysisTime) / maxInhallData.GetMaxInhall;

        _drop = this.GetComponent<Drop>();
    }

    protected virtual void OnEnable()
    {
        // HPの再設定
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
        // スコアを加算
        _score.NormalScore_NormalEnemyScore();
        // 死んだときのエフェクト呼び出し
        _objectPoolSystem.CallObject(EffectPoolEnum.EffectPoolState.enemyDeath, _transform.position, Quaternion.identity);

        _objectPoolSystem.ReturnObject(_cashObjectInformation);
    }

    /// <summary>
    /// 逃走（消滅）する
    /// </summary>
    public virtual void Despawn()
    {
        // プールに返す
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
    /// ノックバックする
    /// </summary>
    private IEnumerator KnockbackCoroutine()
    {
        // 現在のノックバックしている距離
        float nowKnockBackedDistance = 0;

        // タイムアウト用
        float timeOut = Time.time + _timeOutTime;

        // 距離がすぎるまで
        while (_knockBackDistance > nowKnockBackedDistance)
        {
            // 移動する距離
            float moveDistance = Mathf.Pow(_knockBackDistance * Time.deltaTime,
                (nowKnockBackedDistance / _knockBackDistance) * _knockBackDown);

            // 移動した距離に加算する
            nowKnockBackedDistance += moveDistance;

            // 位置変更
            _transform.Translate(Vector3.back * moveDistance);

            yield return null;

            if (Time.time > timeOut)
            {
                Debug.LogWarning("[test]タイムアウト");
                yield break;
            }
        }
    }

    /// <summary>
    /// サンダー受けたときの麻痺？
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