// --------------------------------------------------------- 
// Arrow.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections;
using UnityEngine;

public struct A
{
    bool arrowHit;
    bool arrowHitObject;
    bool arrowHitTitleObject;
    bool arrowHitBarrierObject;
    bool arrowHitBottonObject;
    bool arrowHitCantDestroyObject;
}


/// <summary>
/// 矢の移動を開始する
/// </summary>
interface IArrowMove
{
    /// <summary>
    　　/// 矢の移動を開始するインターフェース
    /// </summary>
    void ArrowMoveStart();
    void SetArrowMoveSpeed(float moveSpeed);
}

public interface IArrowEnchant
{
    /// <summary>
    /// 矢のヒットエフェクト
    /// </summary>
    Arrow.ArrowEffectDelegateMethod EventArrowEffect { set; get; }

    /// <summary>
    /// 矢の常時エンチャント消える
    /// </summary>
    Arrow.ArrowEffectDestroyDelegateMethod EventArrowEffectDestroy { set; get; }

    /// <summary>
    /// 矢の常時発動するエフェクト用デリゲート変数
    /// </summary>
    Arrow.ArrowEffectDelegateMethod EventArrowPassiveEffect { set; get; }

    /// <summary>
    /// 矢の常時発動するエフェクトデストロイ用デリゲート変数
    /// </summary>
    Arrow.ArrowEffectDestroyDelegateMethod EventArrowEffectPassiveDestroy { set; get; }

    /// <summary>
    /// 矢のヒット時の効果音用デリゲート変数
    /// </summary>
    Arrow.ArrowEnchantSoundDeletgateMethod ArrowEnchantSound { set; get; }

    /// <summary>
    /// 矢の効果用デリゲート変数
    /// </summary>
    Arrow.ArrowEnchantmentDelegateMethod EventArrow { set; get; }

    /// <summary>
    /// 矢の移動用デリゲート変数
    /// </summary>
    Arrow.MoveDelegateMethod MoveArrow { set; get; }

    /// <summary>
    /// エンチャントできるかどうか
    /// </summary>
    bool NeedArrowEnchant { set; get; }

    ArrowMove EnchantArrowMove { set; get; }

    ArrowPassiveEffect EnchantArrowPassiveEffect { set; get; }

    void SetEnchantState(EnchantmentEnum.EnchantmentState enchantment);

    Transform MyTransform { get; }

}

/// <summary>
/// 矢
/// </summary>
public class Arrow : MonoBehaviour, IArrowMove, IArrowEnchant, IArrowEnchantDamageable
{

    [SerializeField]
    private AudioClip missHitSound;

    private AudioSource audioSource;

    private ObjectPoolSystem objectPoolSystem;

    public ArrowMove EnchantArrowMove { set; get; }
    public ArrowPassiveEffect EnchantArrowPassiveEffect { set; get; }

    /// <summary>
    /// 矢のエフェクト用デリゲート
    /// </summary>
    public delegate void ArrowEffectDelegateMethod(Transform spawnPosition);
    public delegate void ArrowEffectDestroyDelegateMethod(GameObject arrowObject);
    /// <summary>
    /// 矢のヒット効果用デリゲート
    /// </summary>
    /// <param name="hitObject">ヒットしたオブジェクト</param>
    /// <param name="enchantmentState">矢のEnum</param>
    public delegate void ArrowEnchantmentDelegateMethod(GameObject hitObject, EnchantmentEnum.EnchantmentState enchantmentState);

    /// <summary>
    /// 矢のサウンド
    /// </summary>
    /// <param name="arrowAudioSource"></param>
    public delegate void ArrowEnchantSoundDeletgateMethod(AudioSource arrowAudioSource);


    /// <summary>
    /// 矢の移動用デリゲート
    /// </summary>
    /// <param name="arrowTransform">矢のTransform</param>
    public delegate void MoveDelegateMethod(Transform arrowTransform);

    /// <summary>
    /// 矢のエフェクト用デリゲート変数
    /// </summary>
    public ArrowEffectDelegateMethod EventArrowEffect { set; get; }

    public ArrowEffectDestroyDelegateMethod EventArrowEffectDestroy { set; get; }

    /// <summary>
    /// 矢の常時発動するエフェクト用デリゲート変数
    /// </summary>
    public ArrowEffectDelegateMethod EventArrowPassiveEffect { set; get; }

    /// <summary>
    /// 矢の常時発動するエフェクトデストロイ用デリゲート変数
    /// </summary>
    public ArrowEffectDestroyDelegateMethod EventArrowEffectPassiveDestroy { set; get; }

    /// <summary>
    /// 矢のヒット時の効果音用デリゲート変数
    /// </summary>
    public ArrowEnchantSoundDeletgateMethod ArrowEnchantSound { set; get; }

    /// <summary>
    /// 矢の効果用デリゲート変数
    /// </summary>
    public ArrowEnchantmentDelegateMethod EventArrow { set; get; }

    /// <summary>
    /// 矢の移動用デリゲート変数
    /// </summary>
    public MoveDelegateMethod MoveArrow { set; get; }


    /// <summary>
    /// ヒットしたオブジェクト
    /// </summary>
    [System.NonSerialized]
    public GameObject _hitObject;

    [System.NonSerialized]
    public GameObject[] _hitObjects = new GameObject[7];


    /// <summary>
    /// エンチャントできるか
    /// </summary>
    public bool NeedArrowEnchant { set; get; }


    /// <summary>
    /// 矢がActivの時間設定
    /// </summary>
    public float _arrowActivTime = 5f;

    /// <summary>
    /// 自分のTransform
    /// </summary>
    public Transform MyTransform { private set; get; }



    /// <summary>
    /// 弓のオブジェクト
    /// </summary>
    //private GameObject _bowObject;
    private IFBowManagerQue _bowManagerQue;

    /// <summary>
    /// PlayerManagerにObjectをセットする用　インターフェースにする
    /// </summary>
    private IFPlayerManagerSetArrow _playerManager;

    /// <summary>
    /// 矢の初期速度設定とリセットインターフェース
    /// </summary>
    private IArrowMoveSettingReset _arrowMove;

    /// <summary>
    /// 矢のステータス
    /// </summary>
    private EnchantmentEnum.EnchantmentState _enchantState;

    /// <summary>
    /// オブジェクト情報のキャッシュクラス
    /// </summary>
    private CashObjectInformation _cashObjectInformation;

    /// <summary>
    /// 矢の移動処理
    /// </summary>
    private float _moveSpeed = default;

    /// <summary>
    /// 矢の移動開始
    /// </summary>
    private bool _isArrowMove = false;

    private AudioSource _audioSource;

    private GameObject _hitObjectLast;

    private GameObject[] _hitObjectLasts = new GameObject[6];

    private WaitForSeconds _waitArrowActivTime;

    private bool _isStarEnable = true;

    private TrailRenderer _myTrailRenderer;

    private Renderer _myArrowRenderer;

    private Color _arrowStartColor;

    private Color _colorEnd = Color.red;

    private float _colorValue = default;

    private HitZone _hitZone = default;

    private bool _isStart = false;

    private int damage = 0;

    public int Damage => damage;
    private ArrowEnchant arrowEnchant;
    private ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        audioSource = this.GetComponent<AudioSource>();
        objectPoolSystem = GameObject.FindWithTag(InhallLibTags.PoolSystem).GetComponent<ObjectPoolSystem>();
    }

    private void OnEnable()
    {
        //矢のクラスをゲットコンポーネントする
        NeedArrowEnchant = true;
        if (_isStarEnable)
        {
            EnchantArrowMove = this.gameObject.GetComponent<ArrowMove>();
            EnchantArrowPassiveEffect = this.gameObject.GetComponent<ArrowPassiveEffect>();
            _isStarEnable = false;
            _arrowMove = EnchantArrowMove;
        }

        _playerManager = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerManager>();

        //PlayerManagerにGameObjectをセットする
        if (_playerManager != null)
        {
            _playerManager.SetArrow(this);
        }
        else
        {
            Debug.LogWarning("セットされていない");
        }
        //エラー無ければ削除
        //_bowManagerQue = StaticBowObject.BowManagerQue;

        _bowManagerQue = GameObject.FindGameObjectWithTag("BowController").GetComponent<BowManager>();
        MyTransform = this.transform;

    }
    private void Start()
    {
        NeedArrowEnchant = true;
        MyTransform = this.transform;
        _hitZone = new HitZone(2f, MyTransform.position);
        //_playerManager = StaticPlayerManager.PlayerManager;
        //Transformキャッシュ
        _myTrailRenderer = MyTransform.GetChild(1).GetComponent<TrailRenderer>();
        _myTrailRenderer.enabled = false;

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();


        _audioSource = this.GetComponent<AudioSource>();

        _waitArrowActivTime = new WaitForSeconds(_arrowActivTime);

        _myArrowRenderer = MyTransform.GetChild(2).gameObject.transform.GetChild(4).GetComponent<Renderer>();
        _colorValue = _myArrowRenderer.material.color.g / 10;

        _isStart = true;

        //プール
        this.gameObject.SetActive(false);

        arrowEnchant = GameObject.FindWithTag(InhallLibTags.ArrowEnchantmentController).GetComponent<ArrowEnchant>();
    }


    //いつかupdate 動的生成する

    private void Update()
    {
        if (MyTransform is null) MyTransform = this.transform;

        if (!_isStart)
        {
            return;
        }



        _hitZone.SetPosition(MyTransform.position);

        if (EventArrowPassiveEffect != null)
        {
            EventArrowPassiveEffect(MyTransform);
        }

        if (_playerManager != null)
        {

        }
        //スタートされたら矢を移動させる
        if (!_isArrowMove)
        {
            return;
        }

        //矢を移動する関数を呼ぶ
        if (MoveArrow != null)
            MoveArrow(MyTransform);
        else
            Debug.LogError(_enchantState);


        bool[] isArrowHits = ArrowGetObject.ArrowHitFinalultraピーポー(MyTransform, this);

        //地面
        if (isArrowHits[6])
        {
            if (EventArrowEffect != null)
            {
                objectPoolSystem.CallObject(EffectPoolEnum.EffectPoolState.arrowMissedEffect, MyTransform.position);
                if (missHitSound is not null)
                    audioSource.PlayOneShot(missHitSound);
            }
            ReturnQue();
            return;
        }

        //矢がどこかにヒットしたら
        if (isArrowHits[0])
        {
            //ヒットしたオブジェクトが同じオブジェクトならヒットしていないことにする
            if (_hitObjects[0] == _hitObjectLasts[0])
            {
                return;
            }
            _hitObjectLasts[0] = _hitObjects[0];

            arrowEnchant.SetArrowTransform(this.transform);
            //ヒットしたオブジェクトとエンチャントEnumを渡す　ヒット処理開始
            EventArrow(_hitObjects[0], _enchantState);
            //_hitObjects[0].GetComponent<EnemyStats>()

            //ヒットエフェクトを発動
            if (EventArrowEffect != null)
            {
                EventArrowEffect(MyTransform);
                ArrowEnchantSound(_audioSource);
            }
            //矢をリセットする

            scoreManager.HitCount();

            //貫通系ならプールに戻さない
            if (
                _enchantState == EnchantmentEnum.EnchantmentState.penetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.thunderPenetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.homingPenetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.bombPenetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.knockBackpenetrate)
            {
                // Debug.LogError(_enchantState+"貫通") ;
                return;
            }
            //貫通系じゃなければプールに戻す処理をよぶ
            ReturnQue();
        }
        //仮
        if (_hitObjects[1])
        {
            if (_hitObjects[1] == _hitObjectLasts[1])
            {
                return;
            }
            _hitObjectLasts[1] = _hitObjects[1];

            _hitObjects[1].GetComponent<SceneMoveHitArrow>().SceneMove(MyTransform);
            if (EventArrowEffect != null)
            {
                EventArrowEffect(MyTransform);
                ArrowEnchantSound(_audioSource);
            }
            scoreManager.HitCount();
            ReturnQue();
        }

        //仮
        if (_hitObjects[2])
        {
            if (_hitObjects[2] == _hitObjectLasts[2])
            {
                return;
            }
            _hitObjectLasts[2] = _hitObjects[2];

            _hitObjects[2].GetComponent<TargetAnimation>().TargetPushed();
            if (EventArrowEffect != null)
            {
                EventArrowEffect(MyTransform);
                ArrowEnchantSound(_audioSource);
            }
            scoreManager.HitCount();
            ReturnQue();
        }

        //バリアオブジェクトに触れたらリターン
        if (_hitObjects[3])
        {
            if (
                _enchantState == EnchantmentEnum.EnchantmentState.penetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.thunderPenetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.homingPenetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.bombPenetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.knockBackpenetrate)
            {
                return;
            }
            ReturnQue();
        }

        //ボタン押した時
        if (_hitObjects[4])
        {
            if (_hitObjects[4] == _hitObjectLasts[4])
            {
                return;
            }
            _hitObjectLasts[4] = _hitObjects[4];

            if (_hitObjects[4].TryGetComponent<IFCanTakeArrowButtonGetEnchant>(out IFCanTakeArrowButtonGetEnchant enchant))
            {
                enchant.ButtonPush(_enchantState);
            }
            else
            {
                _hitObjects[4].GetComponent<IFCanTakeArrowButton>().ButtonPush();
            }

            if (EventArrowEffect != null)
            {
                EventArrowEffect(MyTransform);
                ArrowEnchantSound(_audioSource);
            }
            scoreManager.HitCount();
            ReturnQue();
        }

        //矢消えないボタン押した時
        if (_hitObjects[5])
        {
            if (_hitObjects[5] == _hitObjectLasts[5])
            {
                return;
            }
            _hitObjectLasts[5] = _hitObjects[5];

            if (EventArrowEffect != null)
            {
                EventArrowEffect(MyTransform);
                ArrowEnchantSound(_audioSource);
            }
            _hitObjects[5].GetComponent<IFCanTakeArrowButtonCantDestroy>().ButtonPush(MyTransform);
            scoreManager.HitCount();
        }
    }


    /// <summary>
    /// 矢を発射するフラグ
    /// </summary>
    public void ArrowMoveStart()
    {
        if (MyTransform == null)
        {
            MyTransform = gameObject.transform;
        }
        //親オブジェクトをNullにする
        MyTransform.parent = null;

        //float untitti = 1.5f;

        //float x = Random.Range(-untitti, untitti);
        //float y = Random.Range(-untitti, untitti);
        //float z = Random.Range(-untitti, untitti);
        //x = MyTransform.rotation.eulerAngles.x + x;
        //y = MyTransform.rotation.eulerAngles.y + y;
        //z = MyTransform.rotation.eulerAngles.z + z;

        // MyTransform.rotation = Quaternion.Euler(new Vector3(x, y, z));


        //移動スピードをセットする
        _arrowMove.SetArrowSpeed = _moveSpeed;

        //移動開始
        _isArrowMove = true;

        //時間で消滅にする
        StartCoroutine(IEArrowQue());

        NeedArrowEnchant = false;

        if (_myTrailRenderer == null)
        {
            _myTrailRenderer = MyTransform.GetChild(1).GetComponent<TrailRenderer>();
            _myTrailRenderer.enabled = false;
        }

        _myTrailRenderer.enabled = true;

    }

    /// <summary>
    /// 矢の移動速度セット
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void SetArrowMoveSpeed(float moveSpeed)
    {
        //移動速度代入
        this._moveSpeed = moveSpeed;
    }

    /// <summary>
    /// 矢にEnumセット
    /// </summary>
    /// <param name="enchantment"></param>
    public void SetEnchantState(EnchantmentEnum.EnchantmentState enchantment)
    {
        //Enum代入
        this._enchantState = enchantment;
    }

    public EnchantmentEnum.EnchantmentState GetEncantState()
    {
        return _enchantState;
    }

    public void ArrowPowerColor()
    {

        Color color = _myArrowRenderer.material.color;
        if (color.r >= 255)
        {
            return;
        }
        color.r += _colorValue;
        color.g -= _colorValue;
        _myArrowRenderer.material.color = color;
    }



    /// <summary>
    /// 矢のparameterをリセットする
    /// </summary>
    private void ArrowReset()
    {
        if (_arrowMove != null)
        {
            //矢のリセット処理
            _arrowMove.ResetsStart();
        }

        //矢が移動していない
        _isArrowMove = false;
        _hitObject = default;
        _enchantState = EnchantmentEnum.EnchantmentState.normal;
        EventArrow = null;
        EventArrowEffect = null;
        EventArrowEffectDestroy = null;
        EventArrowEffectPassiveDestroy = null;
        EventArrowPassiveEffect = null;
        MoveArrow = null;
        _myArrowRenderer.material.color = Color.green;
        NeedArrowEnchant = false;
        damage = 0;
    }

    /// <summary>
    /// プールに戻すメソッド
    /// </summary>
    public void ReturnQue()
    {
        //常時発動エフェクト削除を実行
        if (EventArrowEffectPassiveDestroy != null)
        {
            EventArrowEffectPassiveDestroy(this.gameObject);
        }
        ArrowReset();
        _myTrailRenderer.Clear();
        _myTrailRenderer.enabled = false;
        _bowManagerQue.ArrowQue(_cashObjectInformation);
    }




    /// <summary>
    /// コールチンが終わるとプールに戻す
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEArrowQue()
    {
        yield return _waitArrowActivTime;
        ReturnQue();
    }

    public ArrowPassiveEffect GetPassiveEffect()
    {
        return EnchantArrowPassiveEffect;
    }

    public void SetAttackDamage()
    {
        damage++;
    }
}
