// --------------------------------------------------------- 
// EnemyAttackBase.cs 
// 
// CreateDay: 2023/06/23
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;


public abstract class EnemyAttackBase : MonoBehaviour,IFItemMove
{
    #region variable 
    public TagObject _PoolSystemTagData = default;

    public TagObject _PlayerControllerTagData = default;


    [Tooltip("取得したObjectPoolSystemクラス")]
    private ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("取得したCashObjectInformationクラス")]
    private CashObjectInformation _cashObjectInformation = default;

    [Tooltip("取得したPlayerHitZoneクラス")]
    private PlayerHitZone _playerHitZone = default;


    [Tooltip("自身のTransformのキャッシュ")]
    protected Transform _transform = default;

    [Tooltip("移動可能フラグ")]
    protected bool _canMove = default;

    [Tooltip("生存時間")]
    protected float _lifeTime = default;

    [SerializeField, Tooltip("攻撃の速さ")]
    protected float _attackMoveSpeed = 20f;

    [Tooltip("攻撃の発射ディレイ")]
    protected float _attackStartDelay = 0f;

    [Tooltip("攻撃が動くまでの待機時間")]
    protected WaitForSeconds _attackDelayWait = default;
    #endregion

    public bool CanMove { get; set; }

    #region method

    protected virtual void Awake()
    {
        SetAttackStartDelay();
        _attackDelayWait = new WaitForSeconds(_attackStartDelay);

        _transform = this.transform;
    }

    protected virtual void OnEnable()
    {
        // 攻撃の生存時間を設定
        _lifeTime = 12f;

        // フラグを初期化
        _canMove = false;

        StartCoroutine(AttackStartWait());
    }

    protected virtual void Start()
    {
        // ボスの攻撃をプールするオブジェクトからプールを取得
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();

        // Return用に自分自身の情報を取得
        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        // プレイヤーから当たり判定のクラスを取得
        _playerHitZone = GameObject.FindWithTag(_PlayerControllerTagData.TagName).GetComponent<PlayerHitZone>();
    }

    protected void Update()
    {
        // 動けなければ返す
        if (!CanMove)
        {
            return;
        }

        AttackMove();


        // プレイヤーにヒットしているかどうかを判定
        _playerHitZone.HitZone(_transform.position);

        // 自身の残生存時間をデクリメント
        _lifeTime -= Time.deltaTime;

        // 生存時間が0になったら消す
        if (_lifeTime <= 0f)
        {
            _objectPoolSystem.ReturnObject(_cashObjectInformation);
        }
    }

    /// <summary>
    /// 攻撃の挙動処理中止
    /// </summary>
    public void CanNotMove()
    {
        _canMove = false;
    }


    /// <summary>
    /// 攻撃の挙動
    /// </summary>
    protected abstract void AttackMove();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackStartWait()
    {
        yield return _attackDelayWait;
        _canMove = true;
        CanMove = true;
    }

    /// <summary>
    /// 何秒後にスタートするか
    /// </summary>
    protected virtual void SetAttackStartDelay()
    {
        _attackStartDelay = 0f;
    }

    public void SetParentNull()
    {
        
    }
    #endregion
}
