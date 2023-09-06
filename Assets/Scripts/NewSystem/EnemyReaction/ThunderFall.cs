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

    [Tooltip("�������x")]
    private float _fallSpeed = 5f;

    private float _rotateSpeed = 1f;

    [Tooltip("���A�N�V�������J�n����")]
    private bool _needStartReaction = default;

    [Tooltip("�o�ߎ���")]
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
            // ��჎��Ԃ��I�������A���A�N�V�����i�G�̎��S�����j���J�n����
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