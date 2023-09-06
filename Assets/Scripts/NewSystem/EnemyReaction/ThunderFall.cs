// --------------------------------------------------------- 
// ThunderFall.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ThunderFall : MonoBehaviour, IThunderReaction
{
    #region variable 
    public bool ReactionEnd { get; set; }

    private BirdStats _birdStats = default;

    private Transform _transform = default;

    private Animator _animator = default;

    [Tooltip("落下速度")]
    private float _fallSpeed = 45f;

    [Tooltip("回転速度")]
    private float _rotateSpeed = 150f;

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

    #endregion
    #region property
    #endregion
    #region method

    private void OnEnable()
    {
        _needStartReaction = false;
        _isFinished = false;
    }

    private void Awake()
    {
        _transform = this.transform;
        _birdStats = this.GetComponent<BirdStats>();
        _animator = this.GetComponent<Animator>();
    }


    private void Update()
    {
        if (_needStartReaction)
        {
            _elapsedTime += Time.deltaTime;

            // 麻痺時間が終わったら、リアクション（敵の死亡処理）を開始する
            if (_elapsedTime >= _birdStats.ParalysisTime)
            {
                _elapsedTime2 += Time.deltaTime;

                if (_elapsedTime2 >= ERASE_TIME)
                {
                    _isFinished = true;
                    return;
                }

                if (!aaa)
                {
                    //_animator.SetTrigger("Fall");
                    aaa = true;
                }

                _transform.Translate(DOWN * Time.deltaTime * _fallSpeed, Space.World);

                // 180度以上回転しない
                if (_transform.rotation.eulerAngles.z >= 180f) return;

                _transform.Rotate(FORWARD * Time.deltaTime * _rotateSpeed);
            }
        }
    }

    public void Reaction(Transform t1, Vector3 t2)
    {
        _needStartReaction = true;
    }

    public void AfterReaction(Transform t1, Vector3 t2)
    {

    }

    public void OverReaction(Transform t1, Vector3 t2)
    {

    }

    public bool IsComplete() => _isFinished;
    #endregion
}