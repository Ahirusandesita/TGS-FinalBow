// --------------------------------------------------------- 
// ReactionNormals.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ReactionNormals : MonoBehaviour,INormalReaction,IPenetrateReaction,IHomingReaction,IKnockBackReaction
{
    [SerializeField] CreateAnimationCurve moveCurveZ = default;
    [SerializeField] CreateAnimationCurve moveCurveY = default;
    Vector3 _backDirection = Vector3.back;
    float _cacheTime = 0f;
    bool _isStart = false;
    bool _end = false;
    public bool ReactionEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


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

    public void Reaction(Transform enemy, Vector3 arrowPos)
    {
        _backDirection = (enemy.position - arrowPos).normalized;

        _isStart = true;

        _end = false;

        _cacheTime = 0f;
    }

    private void Update()
    {
        if (_isStart)
        {
            if (EndMove(_cacheTime))
            {
                _end = true;
                _isStart = false;
                return;
            }

            Vector3 moveBack = _backDirection * moveCurveZ.Curve.Evaluate(_cacheTime);

            Vector3 moveDown = Vector3.up * moveCurveY.Curve.Evaluate(_cacheTime);

            transform.Translate((moveDown + moveBack) * Time.deltaTime, Space.World) ;

            _cacheTime += Time.deltaTime;

            
        }
    }

    private bool EndMove(float Nowtime)
    {
        int lenY = moveCurveY.Curve.keys.Length - 1;
        int lenZ = moveCurveZ.Curve.keys.Length - 1;

        return moveCurveY.Curve.keys[lenY].time < Nowtime &&
            moveCurveZ.Curve.keys[lenZ].time < Nowtime;
    }
}