// --------------------------------------------------------- 
// ReactionHoming.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ReactionHoming : MonoBehaviour,InterfaceReaction.IHomingReaction
{
    [SerializeField] CreateAnimationCurve moveCurveZ = default;
    [SerializeField] CreateAnimationCurve moveCurveY = default;
    [SerializeField] CreateAnimationCurve rotateX = default;
    [SerializeField] Transform effectPosition = default;
    [SerializeField] float ColiderSize = default;
    ReactionMoveRay moveRay = default;
    Animator animator = default;
    ObjectPoolSystem pool = default;
    Vector3 _backDirection = Vector3.back;
    float _cacheTime = 0f;
    bool _isStart = false;
    bool _end = false;
    public bool ReactionEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        pool = FindObjectOfType<ObjectPoolSystem>();
        moveRay = new(effectPosition, ColiderSize * transform.localScale.magnitude);
    }
    public void AfterReaction(Transform t1, Vector3 t2)
    {

    }

    public bool IsComplete()
    {
        return _end;
    }

    public void OverReaction(Transform t1, Vector3 t2)
    {

    }

    public void Reaction(Transform arrow, Vector3 arrowPos)
    {
        _backDirection = (this.transform.position - arrowPos).normalized;

        _backDirection.y = 0;

        _backDirection = _backDirection.normalized;

        _isStart = true;

        _end = false;

        _cacheTime = 0f;

        animator = GetComponent<Animator>();

        animator.SetTrigger("Death");
    }

    private void Update()
    {
        if (_isStart)
        {
            if (EndMove(_cacheTime))
            {
                EndAction();
                return;
            }

            Vector3 moveBack = _backDirection * moveCurveZ.Curve.Evaluate(_cacheTime);

            Vector3 moveDown = Vector3.up * moveCurveY.Curve.Evaluate(_cacheTime);

            Vector3 moveVec = (moveDown + moveBack) * Time.deltaTime;
            transform.Translate(moveVec, Space.World);

            Vector3 rote = transform.rotation.eulerAngles;

            transform.rotation = Quaternion.Euler(rotateX.Curve.Evaluate(_cacheTime), rote.y, rote.z);

            _cacheTime += Time.deltaTime;

            if (moveRay.HitCollision(moveVec))
            {
                EndAction();
            }
        }
    }

    private void EndAction()
    {
        if (_end == false)
        {
            GameObject ef = pool.CallObject(EffectPoolEnum.EffectPoolState.enemyDeath, effectPosition.position);
            pool.ResetActive(ef);
        }
        _end = true;
        _isStart = false;
    }

    private bool EndMove(float Nowtime)
    {
        int lenY = moveCurveY.Curve.keys.Length - 1;
        int lenZ = moveCurveZ.Curve.keys.Length - 1;

        return moveCurveY.Curve.keys[lenY].time < Nowtime &&
            moveCurveZ.Curve.keys[lenZ].time < Nowtime;
    }
}