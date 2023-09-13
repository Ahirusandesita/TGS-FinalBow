// --------------------------------------------------------- 
// ThunderFall.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
interface IFRootFalseEvent
{
    void RootFalseAction();
}
public class ThunderFall : MonoBehaviour, InterfaceReaction.IThunderReaction
{
    #region variable 
    public bool ReactionEnd { get; set; }

    [SerializeField] GameObject hitParticle = default;
    [SerializeField] EffectPoolEnum.EffectPoolState fallingEffect = EffectPoolEnum.EffectPoolState.fallingSmoke;
    ObjectPoolSystem _pool = default;

    GameObject effect = default;

    private BirdStats _birdStats = default;

    private Transform _transform = default;

    private Animator _animator = default;

    private IFRootFalseEvent falseEvent = default;

    [Tooltip("落下速度")]
    private float _fallSpeed = 45f;

    [Tooltip("回転速度")]
    private float _rotateSpeed = 100f;

    [Tooltip("リアクションを開始する")]
    private bool _needStartReaction = default;

    private bool _isFinished = default;

    [Tooltip("経過時間")]
    private float _elapsedTime = 0f;
    private float _elapsedTime2 = 0f;

    private bool aaa = false;

    [Tooltip("消去時間")]
    private const float ERASE_TIME = 0.8f;

    private readonly Vector3 DOWN = Vector3.down;
    private readonly Vector3 FORWARD = Vector3.forward;


    readonly float _paralysisMoveDistance = 0.8f;

    float _paralysisMoveCache = 0f;

    readonly float _paralysisMoveReverceTime = 0.05f;

    float _paralysisSpeed = 0f;

    Vector3 _paralysisMoveVector = Vector3.one.normalized;

    float test = 0;
    #endregion
    #region property
    #endregion
    #region method

    private void OnEnable()
    {
        _needStartReaction = false;
        _isFinished = false;
        _paralysisMoveCache = 0f;
        _paralysisSpeed = _paralysisMoveDistance / _paralysisMoveReverceTime;
        _paralysisMoveVector = Random.onUnitSphere.normalized;
        falseEvent = default;
        hitParticle.SetActive(false);

    }

    private void Awake()
    {
        _transform = this.transform;
        _birdStats = this.GetComponent<BirdStats>();
        _animator = this.GetComponent<Animator>();
        _pool = FindObjectOfType<ObjectPoolSystem>();
    }


    private void Update()
    {
        if (_needStartReaction)
        {
            _elapsedTime += Time.deltaTime;

            

            // 麻痺時間が終わったら、リアクション（敵の死亡処理）を開始する
            if (_elapsedTime >= _birdStats.ParalysisTime)
            {
                _animator.speed = 0;
                _elapsedTime2 += Time.deltaTime;


                if (_elapsedTime2 >= ERASE_TIME)
                {
                    if(falseEvent is not null)
                    falseEvent.RootFalseAction();

                    _isFinished = true;
                    return;
                }

                if (!aaa)
                {
                    //_animator.SetTrigger("Fall");
                    aaa = true;
                }
                effect.SetActive(true);
                _transform.Translate(DOWN * Time.deltaTime * _fallSpeed, Space.World);
                
                // 180度以上回転しない
                if (_transform.rotation.eulerAngles.z >= 180f) return;
                
                
                _transform.Rotate(FORWARD * Time.deltaTime * _rotateSpeed);
                print(FORWARD * Time.deltaTime * _rotateSpeed);

            }
            else
            {
                ParalysisAnimation();
            }
        }
    }

    public void Reaction(Transform t1, Vector3 t2)
    {
        _needStartReaction = true;
        _animator.SetTrigger("Thunder");
        hitParticle.SetActive(true);
        effect = _pool.CallObject(fallingEffect, _transform.position);
        
        effect.transform.SetParent(_transform);
        effect.SetActive(false);

        effect.transform.position = _transform.position;

        effect.TryGetComponent<IFRootFalseEvent>(out falseEvent);

    }


    public bool IsComplete() => _isFinished;

    private void ParalysisAnimation()
    {
        if (_paralysisMoveCache < _paralysisMoveDistance)
        {
            Vector3 move = _paralysisSpeed * Time.deltaTime * _paralysisMoveVector;
            _transform.Translate(move);
            _paralysisMoveCache += move.magnitude;
        }
        else
        {
            _paralysisMoveVector = -_paralysisMoveVector;

            _paralysisMoveCache = 0f;
        }
    }


    #endregion
}