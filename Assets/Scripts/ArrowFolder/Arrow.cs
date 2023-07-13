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
public class Arrow : MonoBehaviour, IArrowMove, IArrowEnchant
{


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

    private WaitForSeconds _waitArrowActivTime;

    private bool _isStarEnable = true;

    private TrailRenderer _myTrailRenderer;

    private Renderer _myArrowRenderer;

    private Color _arrowStartColor;

    private Color _colorEnd = Color.red;

    private float _colorValue = default;

    private HitZone _hitZone = default;

    private void OnEnable()
    {
        if (_isStarEnable)
        {
            EnchantArrowMove = this.gameObject.GetComponent<ArrowMove>();
            EnchantArrowPassiveEffect = this.gameObject.GetComponent<ArrowPassiveEffect>();
            _isStarEnable = false;
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

    }
    private void Start()
    {

        NeedArrowEnchant = true;
        _playerManager = StaticPlayerManager.PlayerManager;
        //Transformキャッシュ
        MyTransform = gameObject.transform;

        _hitZone = new HitZone(2f, MyTransform.position);
        //矢のクラスをゲットコンポーネントする
        _arrowMove = EnchantArrowMove;

        _cashObjectInformation = this.GetComponent<CashObjectInformation>();


        _audioSource = this.GetComponent<AudioSource>();

        _waitArrowActivTime = new WaitForSeconds(_arrowActivTime);

        _myTrailRenderer = MyTransform.GetChild(1).GetComponent<TrailRenderer>();
        _myTrailRenderer.enabled = false;

        _myArrowRenderer = MyTransform.GetChild(2).gameObject.transform.GetChild(4).GetComponent<Renderer>();
        _colorValue = _myArrowRenderer.material.color.g / 10;
    }
    private void Update()
    {

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
        MoveArrow(MyTransform);

        //矢がどこかにヒットしたら
        if (ArrowGetObject.ArrowHit(MyTransform, this))
        {
            //ヒットしたオブジェクトが同じオブジェクトならヒットしていないことにする
            if (_hitObject == _hitObjectLast)
            {
                return;
            }
            _hitObjectLast = _hitObject;

            //ヒットしたオブジェクトとエンチャントEnumを渡す　ヒット処理開始
            EventArrow(_hitObject, _enchantState);

            //ヒットエフェクトを発動
            if (EventArrowEffect != null)
            {
                EventArrowEffect(MyTransform);
                ArrowEnchantSound(_audioSource);
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
        if (ArrowGetObject.ArrowHit_Object(MyTransform, this))
        {
            if (_hitObject == _hitObjectLast)
            {
                return;
            }
            _hitObjectLast = _hitObject;

            _hitObject.GetComponent<SceneMoveHitArrow>().SceneMove(MyTransform);
            if (EventArrowEffect != null)
            {
                EventArrowEffect(MyTransform);
                ArrowEnchantSound(_audioSource);
            }
            ReturnQue();
        }

        //仮
        if (ArrowGetObject.ArrowHit_TitleObject(MyTransform, this))
        {
            if (_hitObject == _hitObjectLast)
            {
                return;
            }
            _hitObjectLast = _hitObject;

            _hitObject.GetComponent<TargetAnimation>().TargetPushed();
            if (EventArrowEffect != null)
            {
                EventArrowEffect(MyTransform);
                ArrowEnchantSound(_audioSource);
            }
            ReturnQue();
        }

        //バリアオブジェクトに触れたらリターン
        if (ArrowGetObject.ArrowHit_BarrierObject(_hitZone, this))
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


    }


    /// <summary>
    /// 矢を発射するフラグ
    /// </summary>
    public void ArrowMoveStart()
    {

        //親オブジェクトをNullにする
        MyTransform.parent = null;

        //移動スピードをセットする
        _arrowMove.SetArrowSpeed = _moveSpeed;

        //移動開始
        _isArrowMove = true;

        //時間で消滅にする
        StartCoroutine(IEArrowQue());

        NeedArrowEnchant = false;

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
        NeedArrowEnchant = true;

        _myArrowRenderer.material.color = Color.green;
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
}
