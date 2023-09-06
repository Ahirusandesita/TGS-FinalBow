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

    [Tooltip("落下速度")]
    private float _fallSpeed = 5f;

    private float _rotateSpeed = 1f;

    [Tooltip("リアクションを開始する")]
    private bool _needStartReaction = default;

    [Tooltip("経過時間")]
    private float _elapsedTime = 0f;

    private readonly Vector3 UP = Vector3.up;
    private readonly Vector3 FORWARD = Vector3.forward;

    #endregion
    #region property
    #endregion
    #region method

    private void OnEnable()
    {
        _needStartReaction = false;
    }
    private void Start()
    {
        _transform = this.transform;
        _birdStats = this.GetComponent<BirdStats>();
    }

    private void Update()
    {
        if (_needStartReaction)
        {
            _elapsedTime += Time.deltaTime;

            X_Debug.Log(_birdStats.ParalysisTime);
            // 麻痺時間が終わったら、リアクション（敵の死亡処理）を開始する
            if (_elapsedTime >= _birdStats.ParalysisTime)
            {
                _transform.Translate(UP * Time.deltaTime * _fallSpeed);
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

    public bool IsComplete() => true;
    #endregion
}