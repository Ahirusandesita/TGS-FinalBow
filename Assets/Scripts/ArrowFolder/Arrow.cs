// --------------------------------------------------------- 
// Arrow.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections;
using UnityEngine;

/// <summary>
/// 矢の移動を開始する
/// </summary>
interface IArrowMove
{
    /// <summary>
　　/// 矢の移動を開始するインターフェース
    /// </summary>
    void ArrowMoveStart();
}

/// <summary>
/// 矢
/// </summary>
public class Arrow : MonoBehaviour,IArrowMove
{


    public ArrowMove EnchantArrowMove { private set; get; }
    public ArrowPassiveEffect EnchantArrowPassiveEffect { private set; get; }

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
    public delegate void ArrowEnchantmentDelegateMethod(GameObject hitObject,EnchantmentEnum.EnchantmentState enchantmentState);

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
    public ArrowEffectDelegateMethod _EventArrowEffect;

    public ArrowEffectDestroyDelegateMethod _EventArrowEffectDestroy;

    /// <summary>
    /// 矢の常時発動するエフェクト用デリゲート変数
    /// </summary>
    public ArrowEffectDelegateMethod _EventArrowPassiveEffect;

    /// <summary>
    /// 矢の常時発動するエフェクトデストロイ用デリゲート変数
    /// </summary>
    public ArrowEffectDestroyDelegateMethod _EventArrowEffectPassiveDestroy;

    /// <summary>
    /// 矢のヒット時の効果音用デリゲート変数
    /// </summary>
    public ArrowEnchantSoundDeletgateMethod _ArrowEnchantSound;

    /// <summary>
    /// 矢の効果用デリゲート変数
    /// </summary>
    public ArrowEnchantmentDelegateMethod _EventArrow;

    /// <summary>
    /// 矢の移動用デリゲート変数
    /// </summary>
    public MoveDelegateMethod _MoveArrow;


    /// <summary>
    /// ヒットしたオブジェクト
    /// </summary>
    [System.NonSerialized]
    public GameObject _hitObject;


    /// <summary>
    /// エンチャントできるか
    /// </summary>
    public bool _needArrowEnchant = true;


    /// <summary>
    /// 矢がActivの時間設定
    /// </summary>
    public float _arrowActivTime = 5f;


    /// <summary>
    /// 弓のオブジェクト
    /// </summary>
    //private GameObject _bowObject;
    private IFBowManagerQue _bowManagerQue;

    /// <summary>
    /// 自分のTransform
    /// </summary>
    private Transform _myTransform;

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

    private WaitForSeconds _waitArrowActivTime;

    private void OnEnable()
    {
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

    }
    private void OnDisable()
    {
        ArrowReset();
    }
    private void Start()
    {

        EnchantArrowMove = this.GetComponent<ArrowMove>();
        EnchantArrowPassiveEffect = this.GetComponent<ArrowPassiveEffect>();


        _playerManager = StaticPlayerManager.PlayerManager;
        //Transformキャッシュ
        _myTransform = gameObject.transform;

        //矢のクラスをゲットコンポーネントする
        _arrowMove = EnchantArrowMove;

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();


        _audioSource = this.GetComponent<AudioSource>();

        _waitArrowActivTime = new WaitForSeconds(_arrowActivTime);

    }
    private void Update()
    {
        if (_EventArrowPassiveEffect != null)
        {
            _EventArrowPassiveEffect(_myTransform);
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
        _MoveArrow(_myTransform);

        //矢がどこかにヒットしたら
        if (ArrowGetObject.ArrowHit(_myTransform,this))
        {
            //ヒットしたオブジェクトが同じオブジェクトならヒットしていないことにする
            if(_hitObject == _hitObjectLast)
            {
                return;
            }
            _hitObjectLast = _hitObject;

            //ヒットしたオブジェクトとエンチャントEnumを渡す　ヒット処理開始
            _EventArrow(_hitObject, _enchantState);

            //ヒットエフェクトを発動
            if (_EventArrowEffect != null)
            {
                _EventArrowEffect(_myTransform);
                _ArrowEnchantSound(_audioSource);
            }
            //矢をリセットする
            

            //貫通系ならプールに戻さない
            if (
                _enchantState == EnchantmentEnum.EnchantmentState.penetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.thunderPenetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.homingPenetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.bombPenetrate ||
                _enchantState == EnchantmentEnum.EnchantmentState.knockBackpenetrate)
            {
                return;
            }
            //貫通系じゃなければプールに戻す処理をよぶ
            ReturnQue();
        }
        //仮
        if (ArrowGetObject.ArrowHit_Object(_myTransform,this))
        {
            if (_hitObject == _hitObjectLast)
            {
                return;
            }
            _hitObjectLast = _hitObject;

            _hitObject.GetComponent<SceneMoveHitArrow>().SceneMove(_myTransform);
            if (_EventArrowEffect != null)
            {
                _EventArrowEffect(_myTransform);
                _ArrowEnchantSound(_audioSource);
            }
            ReturnQue();
        }

        //仮
        if (ArrowGetObject.ArrowHit_TitleObject(_myTransform,this))
        {
            if (_hitObject == _hitObjectLast)
            {
                return;
            }
            _hitObjectLast = _hitObject;

            _hitObject.GetComponent<TargetAnimation>().TargetPushed();
            if (_EventArrowEffect != null)
            {
                _EventArrowEffect(_myTransform);
                _ArrowEnchantSound(_audioSource);
            }
            ReturnQue();
        }


    }


    /// <summary>
    /// 矢を発射するフラグ
    /// </summary>
    public void ArrowMoveStart()
    {

        //親オブジェクトをNullにする
        _myTransform.parent = null;
       
        //移動スピードをセットする
        _arrowMove.SetArrowSpeed = _moveSpeed;

        //移動開始
        _isArrowMove = true;

        //時間で消滅にする
        StartCoroutine(IEArrowQue());

        _needArrowEnchant = false;
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
        _enchantState = EnchantmentEnum.EnchantmentState.nomal;
        _EventArrow = null;
        _EventArrowEffect = null;
        _EventArrowEffectDestroy = null;
        _EventArrowEffectPassiveDestroy = null;
        _EventArrowPassiveEffect = null;
        _MoveArrow = null;
        _needArrowEnchant = true;
    }

    /// <summary>
    /// プールに戻すメソッド
    /// </summary>
    private void ReturnQue()
    {
        //常時発動エフェクト削除を実行
        if (_EventArrowEffectPassiveDestroy != null)
        {
            _EventArrowEffectPassiveDestroy(this.gameObject);
        }
        ArrowReset();
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
}
