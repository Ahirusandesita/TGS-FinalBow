// --------------------------------------------------------- 
// EnemyCabeMoveDemo.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class EnemyCurveMoveDemo : MonoBehaviour
{
    #region variable 
    // ����ɃG�l�~�[�̃g�����X�t�H�[�������Ă�
    private Transform _enemyTransform = default;

    private Transform _myTransform = default;

    private Vector3 _rotateValue = default;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
 {

 }
 
 private void Start ()
 {

 }

 private void Update ()
 {

 }

    private void Movement(float angle ,float value ,float shift)
    {

    }

    private void StartSetting(float angle, float value, float shift)
    {
        _myTransform = _enemyTransform;
        _rotateValue.x = shift;
        
    }
 #endregion
}