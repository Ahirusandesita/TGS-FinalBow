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
    protected CashObjectInformation _cashObjectInformation = default;

    protected IFScoreManager_NomalEnemy _score;
    protected IFScoreManager_Combo _combo = default;

    protected WaitForSeconds _paralysisWait = default;

    /// <summary>
    /// 麻痺時間
    /// </summary>
    protected readonly float _paralysisTime = 3f;

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

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        _paralysisWait = new WaitForSeconds(_paralysisTime);
    }

    protected virtual void OnEnable()
    {
        // HPの再設定
        _hp = _maxHp;
    }


    public override void TakeBomb(int damage)
    {
        TakeDamage(damage);
        print("爆発うけた");
    }

    public override void TakeThunder()
    {
        StartCoroutine(ParalysisCoroutine());
    }

    public override void TakeKnockBack()
    {
        StartCoroutine(KnockbackCoroutine());
    }

    public override void Death()
    {
        print("死にました");
        gameObject.SetActive(false);
        //_objPoolSys.ReturnObject(this.gameObject);
    }

    /// <summary>
    /// 逃走（消滅）する
    /// </summary>
    public abstract void Despawn();

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
            _myTransform.Translate(Vector3.back * moveDistance);

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
    private IEnumerator ParalysisCoroutine()
    {
        print("麻痺中");
        _isParalysis = true;

        yield return _paralysisWait;

        print("麻痺会場");
        _isParalysis = false;
    }
}